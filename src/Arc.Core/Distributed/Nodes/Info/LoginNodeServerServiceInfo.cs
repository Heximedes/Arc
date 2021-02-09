using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arc.Core.Distributed.Nodes.Info
{
    public class LoginNodeServerServiceInfo : ServerNodeServiceInfo
    {
        public string SplashScreen { get; set; }
        public byte JobOrder { get; set; }
        public ICollection<WorldInfo> Worlds { get; set; }
    }

    public class WorldInfo
    {
        public byte ID { get; set; }
        public string Name { get; set; }
        public bool BlockCharCreation { get; set; }
    }
}
