using MacChangerProject.GetIPRange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MacChangerProject.GetIPByMAC
{
    public class IpFinder
    {
        public void UpdateARPTable()
        {
            Parallel.ForEach(GetListOfAllIPs(), delegate (string s)
            {
                DeviceScanner.IsHostAccessible(s);
            }); //chạy hàm này để cập nhật bảng ARP;
            //hàm này đã được chỉnh sửa so với bản gốc
        }

        public string FindIpAddressByMacAddress(string macAddress) //chuỗi MAC này chứa dấu gạch ngang
        {
            var arpEntities = new ArpHelper().GetArpResult();
            var ip = arpEntities.FirstOrDefault(
                a => string.Equals(
                    a.MacAddress,
                    macAddress,
                    StringComparison.CurrentCultureIgnoreCase))?.Ip;

            return ip;
            //nếu trả vể null thì không tìm thấy MAC đó trong bảng ARP
        }

        private List<string> GetListOfSubnetIps(string currentIp)
        {
            var a = currentIp.Split('.');
            var lst = new List<string>();

            for (int i = 1; i <= 255; i++)
            {
                lst.Add($"{a[0]}.{a[1]}.{a[2]}.{i}");
            }

            lst.Remove(currentIp);

            return lst;
        }

        private List<string> GetListOfAllIPs()
        {
            GetLocalPCIPAndMask localInformation = new GetLocalPCIPAndMask();
            IPAddress currentIP = localInformation.GetLocalIP();
            IPAddress currentMask = localInformation.GetLocalMask();

            IPAddress broadcastAddress = currentIP.GetBroadcastAddress(currentMask);
            var broadcast = broadcastAddress.ToString().Split('.');
            IPAddress netAddress = currentIP.GetNetworkAddress(currentMask);
            var net = netAddress.ToString().Split('.');

            var a = currentIP.ToString().Split('.');
            var lst = new List<string>();

            for(int i = int.Parse(net[2])+1; i<=int.Parse(broadcast[2]); i++)
            {
                for(int j = 1; j<=255; j++)
                {
                    lst.Add($"{a[0]}.{a[1]}.{i}.{j}");
                }
            }
            lst.Remove(currentIP.ToString());

            return lst;
        }
    }
}

//Source: 
//http://www.maniuk.net/2016/08/get-ip-address-by-mac-address-in-csharp-updating-arp-table.html
//http://www.maniuk.net/2016/08/get-ip-address-by-mac-address-in-csharp.html