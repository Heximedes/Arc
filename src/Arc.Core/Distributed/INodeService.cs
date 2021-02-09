using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Arc.Core.Distributed.Nodes;

namespace Arc.Core.Distributed
{
    public interface INodeService : IHostedService, IServiceTick
    {
        Task OnStart();
        Task OnStop();

        Task BroadcastMessage<T>(T message) where T : class;
        Task SendMessage<T>(NodeServiceInfo info, T message) where T : class;
    }
}
