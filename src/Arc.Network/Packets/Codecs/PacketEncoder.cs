using System;
using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using Arc.Network.Crypto;

namespace Arc.Network.Packets.Codecs
{
    public class PacketEncoder : MessageToByteEncoder<Packet>
    {
        private readonly short _version;
        public PacketEncoder(short version)
        {
            _version = version;
        }

        protected override void Encode(IChannelHandlerContext context, Packet message, IByteBuffer output)
        {
            var socket = context.Channel.GetAttribute(AbstractSocket.SocketKey).Get();

            var cryptograph = context.Channel.GetAttribute(AbstractSocket.CryptoKey).Get();

            if (socket != null)
            { 
                var seqSend = socket.SeqSend;
                var rawSeq = (short) ((seqSend >> 16) ^ -(_version + 1));
                var dataLen =  message.Length;
                var buffer = new byte[dataLen];

                Array.Copy(message.Buffer, buffer, dataLen);

                if (socket.EncryptData)
                {
                    dataLen ^= rawSeq;

                    if (socket.GetPort() == 8484) // TODO: shit hack, fix later
                    {
                        buffer = cryptograph.Transform(buffer, seqSend);
                    }
                    else
                    {
                        for (int i = 0; i < buffer.Length; i++)
                        {
                            buffer[i] += (byte)seqSend;
                        }
                    }
                        
                }

                output.WriteShortLE(rawSeq);
                output.WriteShortLE(dataLen);
                output.WriteBytes(buffer);

                socket.SeqSend = IGCipher.InnoHash(seqSend, 4, 0);
               
            }
            else
            {
                var length = message.Length;
                var buffer = new byte[length];

                Array.Copy(message.Buffer, buffer, length);
                
                output.WriteShortLE(length);
                output.WriteBytes(buffer);
            }
        }
    }
}