using System;
using System.Collections.Generic;
using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using Arc.Network.Crypto;
using Serilog;

namespace Arc.Network.Packets.Codecs
{
    public class PacketDecoder : ReplayingDecoder<PacketDecoder.PacketState>
    {
        private readonly short _version;

        public enum PacketState
        {
            Header,
            Payload
        }

        private short _sequence;
        private short _length;

        public PacketDecoder(short version) : base(PacketState.Header)
        {
            _version = version;
        }

        protected override void Decode(IChannelHandlerContext context, IByteBuffer input, List<object> output)
        {
            var socket = context.Channel.GetAttribute(AbstractSocket.SocketKey).Get();

            var cryptograph = context.Channel.GetAttribute(AbstractSocket.CryptoKey).Get();

            try
            {
                switch (State)
                {
                    case PacketState.Header:
                        {
                            if (socket != null)
                            {
                                var sequence = input.ReadShortLE();
                                var length = input.ReadShortLE();
                                if (socket.EncryptData) 
                                { 
                                    length ^= sequence; 
                                }
                                _sequence = sequence;
                                _length = length;
                            }
                            else
                            {
                                _length = input.ReadShortLE();
                            }
                            Checkpoint(PacketState.Payload);
                            return;
                        }
                        case PacketState.Payload:
                        {
                            var buffer = new byte[_length];

                            input.ReadBytes(buffer);
                            

                            if (_length < 0x2)
                            {
                                return;
                            }

                            if (socket != null)
                            {
                                var seqRecv = socket.SeqRecv;
                                var version = (short)(seqRecv >> 16) ^ _sequence;

                                if (!(version == -(_version + 1) ||
                                        version == _version)) return;

                                if (socket.EncryptData)
                                {
                                    buffer = cryptograph.Transform(buffer, seqRecv);
                                }

                                socket.SeqRecv = IGCipher.InnoHash(seqRecv, 4, 0);
                                
                            }
                            output.Add(new Packet(Unpooled.CopiedBuffer(buffer)));

                            Checkpoint(PacketState.Header);
                            return;
                        }
                }

            }
            catch (IndexOutOfRangeException)
            {
                RequestReplay();
            }
        }
    }
}