using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arc.Core.Distributed.Nodes.Messaging
{
    public class NodeServiceInfoEntry
    {
        public NodeServiceInfo Info { get; set; }
        public DateTime Expiry { get; set; }
    }
}
