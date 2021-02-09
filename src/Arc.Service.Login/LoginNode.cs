using System;
using System.Linq;
using System.Threading.Tasks;
using DotNetty.Transport.Channels;
using Microsoft.Extensions.Options;
using Arc.Core.Distributed;
using Arc.Core.Distributed.Nodes.Info;
using Arc.Core.Distributed.Nodes.Messaging;
using Arc.Database;
using Arc.Network;
using Arc.Network.Channels;
using Arc.Service.Login.Sockets;
using Serilog;

namespace Arc.Service.Login
{
    public class LoginNode : AbstractNodeServerService<LoginNodeServerServiceInfo>
    {
        private readonly ILogger _logger;

        public LoginNode(
            IOptions<LoginNodeServerServiceInfo> info,
            ILogger logger,
            IMessageBusFactory messageBus
        ) : base(info.Value, logger, messageBus)
        {
            _logger = logger;
        }

        public override async Task OnStart()
        {
            await base.OnStart();
            //await Server.Start(NodeInfo.Host, NodeInfo.Port, NodeInfo.AKey, NodeInfo.Version);
        }

        public override async Task OnStop()
        {
            //await Server.Stop();
            await base.OnStop();
        }

        public override ISocket Build(IChannel channel, uint seqSend, uint seqRecv)
            => new LoginClientSocket(channel, seqSend, seqRecv, this, _logger);

        public override async Task OnTick(DateTime now)
        {
            await Task.CompletedTask;
        }
    }
}