using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foundatio.Messaging;

namespace Arc.Core.Distributed.Nodes.Messaging
{
    public abstract class AbstractMessageBusFactory : IMessageBusFactory
    {
        private readonly IDictionary<string, IMessageBus> _busses;

        protected AbstractMessageBusFactory()
        {
            _busses = new Dictionary<string, IMessageBus>();
        }

        public IMessageBus Build(string topic)
        {
            if (!_busses.ContainsKey(topic))
                _busses[topic] = Create(topic);
            return _busses[topic];
        }

        protected abstract IMessageBus Create(string topic);
    }
}
