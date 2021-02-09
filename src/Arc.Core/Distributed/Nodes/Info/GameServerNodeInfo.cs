using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arc.Core.Distributed.Nodes.Info
{
    public class GameServerNodeServiceInfo : ServerNodeServiceInfo
    {
        public byte WorldID { get; set; }
        public string DKey { get; set; }
    }
}
