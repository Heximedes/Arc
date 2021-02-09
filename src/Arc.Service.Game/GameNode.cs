using System;
using System.Threading.Tasks;
using DotNetty.Transport.Channels;
using Microsoft.Extensions.Options;
using Arc.Core.Distributed;
using Arc.Core.Distributed.Nodes.Info;
using Arc.Core.Distributed.Nodes.Messaging;
using Arc.Database;
using Arc.Network;
using Arc.Network.Channels;
using Arc.Core.Templates;
using Arc.Service.Game.Fields;
using Arc.Service.Game.Sockets;
using Serilog;

namespace Arc.Service.Game
{
    public class GameNode : AbstractNodeServerService<GameServerNodeServiceInfo>
    {
        //private IServer Server { get; set; }

        private readonly ILogger _logger;

        public IDataStore DataStore { get; }
        public ITemplateManager TemplateManager { get; }


        public FieldHandler FieldHandler { get; }

        public GameNode(
            IOptions<GameServerNodeServiceInfo> info,
            ILogger logger,
            IMessageBusFactory messageBus
        ) : base(info.Value, logger, messageBus)
        {
            _logger = logger;
        }

        public override async Task OnStart()
        {
            await base.OnStart();
            _logger.Information($"Staring GameNode - {NodeInfo.Name}");
        }

        public override async Task OnStop()
        {

            //await Server.Stop();
            await base.OnStop();
        }

        public override ISocket Build(IChannel channel, uint seqSend, uint seqRecv)
            => new WvsGameSocket(channel, seqSend, seqRecv, this, _logger);

        public override async Task OnTick(DateTime now)
        {
            await Task.CompletedTask;
        }
    }
}