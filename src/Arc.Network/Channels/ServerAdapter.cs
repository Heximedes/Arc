using System;
using DotNetty.Transport.Channels;
using Serilog;
using Arc.Network.Channels;
using Arc.Network.Crypto;
using Arc.Network.Packets;

namespace Arc.Network
{
    public class ServerAdapter : ChannelHandlerAdapter
    {
        //private static readonly ILog Logger = LogCore.Templates.GetCurrentClassLogger();
        private readonly ILogger _logger;
        private readonly short _version;
        private readonly string _aKey;
        private readonly Server _server;
        private readonly ISocketFactory _socketFactory;

        public ServerAdapter(
            ILogger logger,
            Server server,
            ISocketFactory socketFactory,
            short version,
            string aKey
        )
        {
            _logger = logger;
            _server = server;
            _socketFactory = socketFactory;
            _version = version;
            _aKey = aKey;
        }

        public override void ChannelActive(IChannelHandlerContext context)
        {
            var random = new Random();
            var socket = _socketFactory.Build(
                context.Channel,
                (uint) random.Next(),
                (uint) random.Next()
            );

            using (var p = new Packet())
            {
                p.Encode<short>(_version);
                p.Encode<string>("2:0"); //TODO: Load from config file
                p.Encode<int>((int) socket.SeqRecv);
                p.Encode<int>((int) socket.SeqSend);
                p.Encode<short>(8);
                p.Encode<short>(_version);
                p.Encode<int>(_version);

                socket.SendPacket(p);
            }

            context.Channel.GetAttribute(AbstractSocket.SocketKey).Set(socket);
            context.Channel.GetAttribute(AbstractSocket.CryptoKey).Set(new Cryptograph(_aKey));

            _server.Sockets.Add(socket);

            _logger.Information($"Accepted connection from {socket.GetIP()}");
        }

        public override void ChannelInactive(IChannelHandlerContext context)
        {
            var socket = context.Channel.GetAttribute(AbstractSocket.SocketKey).Get();

            socket?.OnDisconnect();
            _server.Sockets.Remove(socket);
            base.ChannelInactive(context);

            _logger.Information($"Released connection from {socket.GetIP()}");
        }

        public override void ChannelRead(IChannelHandlerContext context, object message)
        {
            var socket = context.Channel.GetAttribute(AbstractSocket.SocketKey).Get();
            socket?.OnPacket((Packet) message);
        }

        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception)
        {
            var socket = context.Channel.GetAttribute(AbstractSocket.SocketKey).Get();
            socket?.OnException(exception);
        }
    }
}