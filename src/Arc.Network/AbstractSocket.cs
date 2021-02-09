using System;
using System.Threading.Tasks;
using DotNetty.Common.Utilities;
using DotNetty.Transport.Channels;
using Arc.Network.Crypto;
using Arc.Network.Packets;

namespace Arc.Network
{
    public abstract class AbstractSocket : ISocket
    {

        public static readonly AttributeKey<ISocket> SocketKey = AttributeKey<ISocket>.ValueOf("Socket");
        public static readonly AttributeKey<Cryptograph> CryptoKey = AttributeKey<Cryptograph>.ValueOf("Crypto");

        private readonly IChannel _channel;

        public uint SeqSend { get; set; }
        public uint SeqRecv { get; set; }
        public bool EncryptData => true;
        

        public AbstractSocket(IChannel channel, uint seqSend, uint seqRecv)
        {
            _channel = channel;
            SeqSend = seqSend;
            SeqRecv = seqRecv;
        }

        public string GetIP()
        {
            return _channel.RemoteAddress.ToString().Split(":")[3][..^1];
        }

        public int GetPort()
        {
            return int.Parse(_channel.LocalAddress.ToString().Split(":")[4]);
        }

        public abstract Task OnPacket(IPacket packet);
        public abstract Task OnDisconnect();
        public abstract Task OnException(Exception exception);
        public Task Close() => _channel.DisconnectAsync();
        public async Task SendPacket(IPacket packet)
        {
            if (_channel.Open)
            {
                await _channel.WriteAndFlushAsync(packet);
            }
        }


    }
}