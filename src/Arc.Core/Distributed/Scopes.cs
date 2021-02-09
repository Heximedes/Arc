using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arc.Core.Distributed
{
    public static class Scopes
    {
        public const string PeerDiscovery = "messages:discovery";
        public const string PeerMessaging = "messages:peers";

        public const string MigrationAccountCache = "migrations:account";
        public const string MigrationCharacterCache = "migrations:account";
        public const string MigrationCache = "migrations";
    }
}
