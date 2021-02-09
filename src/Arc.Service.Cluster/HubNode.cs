using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foundatio.Messaging;
using Serilog;
using Microsoft.Extensions.Options;
using Arc.Core.Distributed;
using Arc.Core.Distributed.Nodes.Info;
using Arc.Core.Distributed.Nodes.Messaging;
using Arc.Core.Distributed.Nodes.Status;


namespace Arc.Service.Cluster
{
    public class HubNode : AbstractNodeService<HubNodeServiceInfo>
    {

        private readonly ILogger _logger;

        public HubNode(ILogger logger, IOptions<HubNodeServiceInfo> info,
                              IMessageBusFactory messageBusFactory) : base(info.Value, logger, messageBusFactory)
        {
            _logger = logger;
            MessageBus.SubscribeAsync<NodeServiceStatusMessage>(HandleServiceStatusMessage);
        }


        private void HandleServiceStatusMessage(NodeServiceStatusMessage message)
        {
            _logger.Information($"{message.Info} has {message.Status}");
        }


        public override Task OnStart()
        {
            _logger.Information($"Cluster Server Listening...");
            return base.OnStart();
        }

        public override Task OnTick(DateTime now)
        {
            return Task.CompletedTask;
        }
    }
}
