using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Foundatio.Messaging;
using Arc.Core.Distributed.Nodes;
using Arc.Core.Distributed.Nodes.Messaging;
using Arc.Core.Distributed.Nodes.Status;
using Serilog;
using Timer = System.Timers.Timer;

namespace Arc.Core.Distributed
{
    public abstract class AbstractNodeService<TNodeInfo> : INodeService where TNodeInfo : NodeServiceInfo
    {


        public readonly TNodeInfo NodeInfo;

        public IMessageBus MessageBus { get; }

        private readonly IMessageBusFactory _messageBusFactory;

        private readonly IMessageBus _peerMessageBus;

        private readonly IDictionary<string, NodeServiceInfoEntry> _peers;
        private readonly ILogger _logger;

        public IEnumerable<NodeServiceInfo> Peers => _peers
            .Values
            .Where(p => p.Expiry > DateTime.Now)
            .Select(p => p.Info)
            .ToImmutableList();

        private Timer _peerTimer;
        private Timer _tickTimer;

        public AbstractNodeService(TNodeInfo info, ILogger logger, IMessageBusFactory messageBusFactory)
        {
            NodeInfo = info;
            _logger = logger;
            _messageBusFactory = messageBusFactory;

            MessageBus = messageBusFactory.Build($"{Scopes.PeerMessaging}:{info.Name}");

            _peerMessageBus = messageBusFactory.Build(Scopes.PeerDiscovery);
            _peers = new ConcurrentDictionary<string, NodeServiceInfoEntry>();
        }

        public virtual async Task OnStart()
        {
            {
                await _peerMessageBus.SubscribeAsync<NodeServiceStatusMessage>(message =>
                {
                    switch (message.Status)
                    {
                        case NodeServiceStatus.Online:
                            var expiry = DateTime.Now.AddSeconds(30);

                            if (_peers.ContainsKey(message.Info.Name))
                            {
                                var peer = _peers[message.Info.Name];

                                if (peer.Expiry < DateTime.Now)
                                    _logger.Debug($"Reconnected peer service, {message.Info.Name} to {NodeInfo.Name}");
                                _peers[message.Info.Name].Expiry = expiry;
                                break;
                            }

                            _peers[message.Info.Name] = new NodeServiceInfoEntry
                            {
                                Info = message.Info,
                                Expiry = expiry
                            };
                            _logger.Debug($"Connected peer service, {message.Info.Name} to {NodeInfo.Name}");
                            break;
                        case NodeServiceStatus.Offline:
                        default:
                            _peers.Remove(message.Info.Name);
                            _logger.Debug($"Removed peer service, {message.Info.Name} from {NodeInfo.Name}");
                            break;
                    }
                });

                _peerTimer = new Timer
                {
                    Interval = 15000,
                    AutoReset = true
                };
                _peerTimer.Elapsed += async (sender, args) => await _peerMessageBus.PublishAsync(
                    new NodeServiceStatusMessage
                    {
                        Info = NodeInfo,
                        Status = NodeServiceStatus.Online
                    }
                );
                _peerTimer.Start();
            }

            {
                await _peerMessageBus.PublishAsync(new NodeServiceStatusMessage
                {
                    Info = NodeInfo,
                    Status = NodeServiceStatus.Online

                });
            }

            {
                _tickTimer = new Timer
                {
                    Interval = 1000,
                    AutoReset = true
                };
                _tickTimer.Elapsed += async (sender, args) => await OnTick(DateTime.Now);
                _tickTimer.Start();
            }
        }

        public virtual async Task OnStop()
        {
            _peerTimer.Stop();
            await _peerMessageBus.PublishAsync(new NodeServiceStatusMessage
            {
                Info = NodeInfo,
                Status = NodeServiceStatus.Offline
            });
        }

        public abstract Task OnTick(DateTime now);

        public Task StartAsync(CancellationToken cancellationToken) => OnStart();
        public Task StopAsync(CancellationToken cancellationToken) => OnStop();

        public Task BroadcastMessage<T>(T message) where T : class
            => Task.WhenAll(Peers.Select(p => SendMessage(p, message)));

        public Task SendMessage<T>(NodeServiceInfo info, T message) where T : class
            => _messageBusFactory
                .Build($"messages:{info.Name}")
                .PublishAsync<T>(message);
    }
}
