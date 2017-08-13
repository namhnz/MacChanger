using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Net;
using System.Threading.Tasks;

namespace MacChangerProject.GetIPRange
{
    class GetLocalPCIPAndMask
    {
        NetworkInterface localEthernetInterface;

        public GetLocalPCIPAndMask()
        {
            GetMyPCEthernetInterface();
        }

        public void GetMyPCEthernetInterface()
        {
            localEthernetInterface = GetEthernetInterface();
        }

        public static NetworkInterface GetEthernetInterface()
        {
            NetworkInterface netI = null;
            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
            {

                if (ni.Name == "Ethernet")
                {
                    netI = ni;
                    break;
                }
            }

            return netI;
        }

        public IPAddress GetLocalIP()
        {
            IPAddress localIP = null;
            foreach (UnicastIPAddressInformation uipi in localEthernetInterface.GetIPProperties().UnicastAddresses)
            {

                localIP = uipi.Address;

            }

            return localIP;
        }

        public IPAddress GetLocalMask()
        {
            IPAddress localMask = null;
            foreach (UnicastIPAddressInformation uipi in localEthernetInterface.GetIPProperties().UnicastAddresses)
            {

                localMask = uipi.IPv4Mask;
            }

            return localMask;
        }
    }
}

//Trích dẫn tại:
//https://arstechnica.com/civis/viewtopic.php?t=192698