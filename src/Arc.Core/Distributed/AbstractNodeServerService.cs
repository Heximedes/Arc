using System.Threading.Tasks;
using DotNetty.Transport.Channels;
using Arc.Core.Distributed.Nodes;
using Arc.Core.Distributed.Nodes.Messaging;
using Arc.Network;
using Arc.Network.Channels;
using Serilog;

namespace Arc.Core.Distributed
{
    public abstract class AbstractNodeServerService<TNodeInfo> : AbstractNodeService<TNodeInfo>, ISocketFactory where TNodeInfo : ServerNodeServiceInfo
    {
        protected readonly Server Server;
        private readonly ILogger _logger;


        public AbstractNodeServerService(TNodeInfo info, ILogger logger, IMessageBusFactory messageBusFactory) : base(info, logger, messageBusFactory)
        {
            _logger = logger;
            Server = new Server(this, _logger);
        }

        public override async Task OnStart()
        {

            
            await Server.Start(NodeInfo.Host, NodeInfo.Port, NodeInfo.AKey, NodeInfo.Version);
            await base.OnStart();

        }

        public override async Task OnStop()
        {
            await Server.Stop();
            await base.OnStop();

        }

        public abstract ISocket Build(IChannel channel, uint seqSend, uint seqRecv);
    }

}
