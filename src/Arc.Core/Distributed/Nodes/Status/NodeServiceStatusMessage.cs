using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arc.Core.Distributed.Nodes.Status
{
    public class NodeServiceStatusMessage
    {
        public NodeServiceInfo Info { get; set; }
        public NodeServiceStatus Status { get; set; }
    }
}
