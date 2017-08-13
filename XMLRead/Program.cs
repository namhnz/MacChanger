using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Reflection;
using System.Net.NetworkInformation;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Management;

namespace XMLRead
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(ConvertToOnlyNumberMac("Et:he:rn:et")); 

            Console.ReadLine();
        }

        public static string ConvertToOnlyNumberMac(string macWithSeparator)
        {
            string macOnlyNum = "";
            macOnlyNum = macWithSeparator.Replace(":", "").Replace("-", "");
            return macOnlyNum;
        }

        static void DisableAndEnableNetworkAdapter(string nicName)
        {
            ManagementClass managementClass = new ManagementClass("Win32_NetworkAdapter");
            ManagementObjectCollection mgmtObjectColl = managementClass.GetInstances();

            ManagementObject myObject = null;

            foreach (ManagementObject mgmtObject in mgmtObjectColl)
            {
                if (mgmtObject["NetConnectionID"] != null && mgmtObject["NetConnectionID"].Equals(nicName))
                {
                    Console.WriteLine("found");
                    myObject = mgmtObject;

                    //object result = mgmtObject.InvokeMethod("Disable", new object[] {});
                    //
                    // When there is no parameter, you don't need to pass an object array with
                    // no element in it.
                    object result = mgmtObject.InvokeMethod("Disable", null);
                    Console.WriteLine("{0}", Convert.ToInt16(result));
                }
                //Console.WriteLine("{0}, {1}", mgmtObject["Name"], mgmtObject["NetConnectionID"]);

            }

            object result3 = myObject.InvokeMethod("Enable", null);

        }



        static void DisableAndEnableNet()
        { 
            string networkInterfaceName = "";
            try
            {
                networkInterfaceName = GetEthernetInterface().Name; // Set Network Interface from Arguments

                Task TaskOne = Task.Factory.StartNew(() => DisableAdapter(networkInterfaceName));
                TaskOne.Wait();
                Task TaskTwo = Task.Factory.StartNew(() => EnableAdapter(networkInterfaceName));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void EnableAdapter(string interfaceName)
        {
            ProcessStartInfo psi = new ProcessStartInfo("netsh", "interface set interface \"" + interfaceName + "\" enable");
            Process p = new Process();
            p.StartInfo = psi;
            p.Start();
        }

        static void DisableAdapter(string interfaceName)
        {
            ProcessStartInfo psi = new ProcessStartInfo("netsh", "interface set interface \"" + interfaceName + "\" disable");
            Process p = new Process();
            p.StartInfo = psi;
            p.Start();
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



        static void ShowAllIP()
        {
            List<string> all = GetListOfAllIPs();
            foreach (var item in all)
            {
                Console.WriteLine(item);
            }
        }

        public static List<string> GetListOfAllIPs()
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

            for (int i = int.Parse(net[2]) + 1; i <= int.Parse(broadcast[2]); i++)
            {
                for (int j = 1; j <= 255; j++)
                {
                    lst.Add($"{a[0]}.{a[1]}.{i}.{j}");
                }
            }
            lst.Remove(currentIP.ToString());

            return lst;
        }

        static void ShowNetAddressAndMask()
        {
            IPAddress ip = IPAddress.Parse("192.172.6.20");
            IPAddress sn = IPAddress.Parse("255.255.240.0");
            Console.WriteLine(ip.GetNetworkAddress(sn));
            Console.WriteLine(ip.GetBroadcastAddress(sn));
        }

        static void ShowLocalIP()
        {
            Console.WriteLine(localIPAddress());
        }

        public static string localIPAddress()
        {
            IPHostEntry host;
            string localIP = "";
            host = Dns.GetHostEntry(Dns.GetHostName());

            foreach (IPAddress ip in host.AddressList)
            {
                localIP = ip.ToString();

                string[] temp = localIP.Split('.');

                if (ip.AddressFamily == AddressFamily.InterNetwork && temp[0] == "192" && temp[1] =="172")
                {
                    break;
                }
                else
                {
                    localIP = null;
                }
            }

            return localIP;
        }


        static void Mask()
        {
            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
            {

                if (ni.Name == "Ethernet") 
                {
                    Console.WriteLine(ni.Name);

                    Console.WriteLine("Operational? {0}", ni.OperationalStatus == OperationalStatus.Up);

                    Console.WriteLine("MAC: {0}", ni.GetPhysicalAddress());

                    Console.WriteLine("Gateways:");

                    foreach (GatewayIPAddressInformation gipi in ni.GetIPProperties().GatewayAddresses)
                    {

                        Console.WriteLine("\t{0}", gipi.Address);

                    }

                    Console.WriteLine("IP Addresses:");

                    foreach (UnicastIPAddressInformation uipi in ni.GetIPProperties().UnicastAddresses)
                    {

                        Console.WriteLine("\t{0} / {1}", uipi.Address, uipi.IPv4Mask);

                    }

                    Console.WriteLine();
                }
                



                

            }
        }




        static void Tester()
        {
            string location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string path = location + "\\employees.xml";
            XmlDocument doc = new XmlDocument();
            doc.Load(path);

            Console.WriteLine(doc.DocumentElement.Name);

            XmlNodeList nodes = doc.DocumentElement.ChildNodes;
            foreach (XmlNode childNode in nodes)
            {
                Console.WriteLine("  >" + childNode.Name + ": " + childNode.Attributes["employeeid"].Name + childNode.Attributes["employeeid"].Value);

                foreach (XmlNode node in childNode.ChildNodes)
                {
                    Console.WriteLine("    -" + node.Name + ": " + node.InnerText);
                }
            }


            Console.WriteLine("----------");

            XmlNode node2 = doc.SelectSingleNode("/employees/employee[@employeeid = '2']");
            if (node2 != null)
            {
                foreach (XmlNode node in node2.ChildNodes)
                {
                    Console.WriteLine("    -" + node.Name + ": " + node.InnerText);
                }
            }

            doc.Save(path);
        }

        static void TestNull()
        {
            string customer = "Mai";
            string name = "Nam";
            if (customer != null)
            {
                name = customer;
            }

            string name2 = customer?.Length.ToString();
            Console.WriteLine(name2);
        }

        static void TestARP()
        {
            var p = Process.Start(new ProcessStartInfo("arp", "-a")
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardOutput = true
            });

            var output = p?.StandardOutput.ReadToEnd();
            p?.Close();

            Console.WriteLine(output);
        }
    }





    public static class IPAddressExtensions
    {
        public static IPAddress GetBroadcastAddress(this IPAddress address, IPAddress subnetMask)
        {
            byte[] ipAdressBytes = address.GetAddressBytes();
            byte[] subnetMaskBytes = subnetMask.GetAddressBytes();

            if (ipAdressBytes.Length != subnetMaskBytes.Length)
                throw new ArgumentException("Lengths of IP address and subnet mask do not match.");

            byte[] broadcastAddress = new byte[ipAdressBytes.Length];
            for (int i = 0; i < broadcastAddress.Length; i++)
            {
                broadcastAddress[i] = (byte)(ipAdressBytes[i] | (subnetMaskBytes[i] ^ 255));
            }
            return new IPAddress(broadcastAddress);
        }

        public static IPAddress GetNetworkAddress(this IPAddress address, IPAddress subnetMask)
        {
            byte[] ipAdressBytes = address.GetAddressBytes();
            byte[] subnetMaskBytes = subnetMask.GetAddressBytes();

            if (ipAdressBytes.Length != subnetMaskBytes.Length)
                throw new ArgumentException("Lengths of IP address and subnet mask do not match.");

            byte[] broadcastAddress = new byte[ipAdressBytes.Length];
            for (int i = 0; i < broadcastAddress.Length; i++)
            {
                broadcastAddress[i] = (byte)(ipAdressBytes[i] & (subnetMaskBytes[i]));
            }
            return new IPAddress(broadcastAddress);
        }

        public static bool IsInSameSubnet(this IPAddress address2, IPAddress address, IPAddress subnetMask)
        {
            IPAddress network1 = address.GetNetworkAddress(subnetMask);
            IPAddress network2 = address2.GetNetworkAddress(subnetMask);

            return network1.Equals(network2);
        }
    }


    class GetLocalPCIPAndMask
    {
        NetworkInterface localEthernetInterface;

        public GetLocalPCIPAndMask()
        {
            GetMyPCEthernetInterface();
        }

        public void GetMyPCEthernetInterface()
        {
            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
            {

                if (ni.Name == "Ethernet")
                {
                    localEthernetInterface = ni;
                }
            }
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


//http://diendan.congdongcviet.com/threads/t5688::xu-ly-xml-trong-lap-trinh-csharp-net.cpp