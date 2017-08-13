using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;

namespace MacChangerProject.GetIPByMAC
{
    public class DeviceScanner
    {
        public static bool IsHostAccessible(string hostNameOrAddress)
        {
            Ping ping = new Ping();
            try
            {
                PingReply reply = ping.Send(hostNameOrAddress, 1000);

                return reply != null && reply.Status == IPStatus.Success;
            }
            catch
            {
                throw;
            }
        }
    }
}
