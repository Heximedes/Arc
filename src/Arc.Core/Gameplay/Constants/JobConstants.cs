using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arc.Core.Types;
using Arc.Network.Packets;

namespace Arc.Core.Gameplay.Constants
{
    public class JobConstants
    {
        private static bool enableJobs = true;

        //private static byte jobOrder = 8;
        public static IPacket EncodeLoginJobs(Packet outPacket, byte jobOrder)
        {
            outPacket.Encode<bool>(enableJobs);
            outPacket.Encode<byte>(jobOrder);
            foreach (LoginJob job in Enum.GetValues(typeof(LoginJob)))
            {
                outPacket.Encode<bool>(Jobs.isEnabled(job));
                outPacket.Encode<short>(Jobs.isEnabled(job) ? 1 : 0);
            }
            return outPacket;
        }
    }
}
