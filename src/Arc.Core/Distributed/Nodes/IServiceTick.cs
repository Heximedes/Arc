using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arc.Core.Distributed.Nodes
{
    public interface IServiceTick
    {
        Task OnTick(DateTime now);
    }
}
