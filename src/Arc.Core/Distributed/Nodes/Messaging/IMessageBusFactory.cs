using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foundatio.Messaging;

namespace Arc.Core.Distributed.Nodes.Messaging
{
    public interface IMessageBusFactory
    {
        IMessageBus Build(string topic);
    }
}
