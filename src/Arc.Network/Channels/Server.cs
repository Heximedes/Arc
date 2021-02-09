using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using Serilog;
using Arc.Network.Packets.Codecs;
using System.Linq;

namespace Arc.Network.Channels
{
    public class Server
    {
        private readonly ILogger _logger;
        private readonly ISocketFactory _socketFactory;

        private IChannel Channel { get; set; }
        private IEventLoopGroup BossGroup { get; set; }
        private IEventLoopGroup WorkerGroup { get; set; }

        public ICollection<ISocket> Sockets { get; }

        public Server(ISocketFactory socketFactory, ILogger logger)
        {
            _logger = logger;
            _socketFactory = socketFactory;
            Sockets = new List<ISocket>();
        }

        public async Task Start(string host, int port, string aKey, short version)
        {
            
            BossGroup = new MultithreadEventLoopGroup();
            WorkerGroup = new MultithreadEventLoopGroup();

            Channel = await new ServerBootstrap()
                .Group(BossGroup, WorkerGroup)
                .Channel<TcpServerSocketChannel>()
                .Option(ChannelOption.SoBacklog, 1024)
                .ChildHandler(new ActionChannelInitializer<IChannel>(ch =>
                {
                    ch.Pipeline.AddLast(
                        new PacketDecoder(version),
                        new ServerAdapter(_logger, this, _socketFactory, version, aKey),
                        new PacketEncoder(version)
                    );
                    ch.GetAttribute(AbstractSocket.CryptoKey).Set(new Crypto.Cryptograph(aKey));
                }))
                .BindAsync(port);
            _logger.Information($"Bound server to {host}:{port}");
            
        }

        public async Task Stop()
        {
            await Task.WhenAll(Sockets.Select(s => s.Close()));
            await Channel.CloseAsync();
            await BossGroup.ShutdownGracefullyAsync();
            await WorkerGroup.ShutdownGracefullyAsync();

        }
    }
}