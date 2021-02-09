using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foundatio.Messaging;
using StackExchange.Redis;

namespace Arc.Core.Distributed.Nodes.Messaging
{
    public class RedisMessageBusFactory : AbstractMessageBusFactory
    {
        private readonly ConnectionMultiplexer _connection;

        public RedisMessageBusFactory(ConnectionMultiplexer connection)
        {
            _connection = connection;
        }

        protected override IMessageBus Create(string topic)
        {
            return new RedisMessageBus(new RedisMessageBusOptions
            {
                Subscriber = _connection.GetSubscriber(),
                Topic = topic
            });
        }
    }
}
