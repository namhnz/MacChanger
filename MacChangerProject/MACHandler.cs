using MacChangerProject.GetIPByMAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Management;

namespace MacChangerProject
{
    public class MACHandler //class này dùng để xử lý toàn bộ danh sách từ xml
    {
        IpFinder finder;

        public MACHandler()
        {
            finder = new IpFinder();
            //finder.UpdateARPTable(); //cập nhật lại bảng ARP 1 lần cho 1 lần xử lý toàn bộ MAC
        }

        /// <summary>
        /// Loại bỏ dấu 2 chấm và gạch ngang trong chuỗi MAC
        /// </summary>
        /// <param name="macWithSeparator"></param>
        /// <returns></returns>
        public static string ConvertToOnlyNumberMac(string macWithSeparator)
        {
            string macOnlyNum = "";
            macOnlyNum = macWithSeparator.Replace(":", "").Replace("-", "");
            return macOnlyNum;
        }

        /// <summary>
        /// Chuyển sang MAC dạng có ký tự phân cách
        /// </summary>
        /// <param name="macRaw"></param>
        /// <param name="separator">":" hoặc "-"</param>
        /// <returns>Chuỗi MAC đã được thêm ký tự phân cách</returns>
        public static string ConvertToSeparatedMac(string macRaw, string separator)
        {
            macRaw = ConvertToOnlyNumberMac(macRaw);

            string macWithSeparator = "";
            for (int i = 0; i < macRaw.Length; i++)
            {
                if (i % 2 == 0 || i == macRaw.Length - 1)
                {
                    macWithSeparator += macRaw[i];
                }
                else
                {
                    macWithSeparator += (macRaw[i] + separator);
                }
            }
            return macWithSeparator;
        }

        /// <summary>
        /// Kiểm tra xem địa chỉ MAC có online không
        /// </summary>
        /// <param name="macWithoutSeparator">MAC không có dấu phân cách</param>
        /// <returns></returns>
        public bool IsOnline(string macWithoutSeparator)
        {
            string macForARPTable = MACHandler.ConvertToSeparatedMac(macWithoutSeparator, "-");
            string ipOfMAC = finder.FindIpAddressByMacAddress(macForARPTable);
            try
            {
                return DeviceScanner.IsHostAccessible(ipOfMAC);
            }
            catch(ArgumentNullException e)
            {
                throw new ArgumentNullException("This MAC is not in ARP Table", e); //có thể dùng được MAC này
            }
            catch
            {
                throw;
            }
        }


        public static void DisableAndEnableNetworkAdapter(string nicName)
        {
            try
            {
                ManagementClass managementClass = new ManagementClass("Win32_NetworkAdapter");
                ManagementObjectCollection mgmtObjectColl = managementClass.GetInstances();

                ManagementObject myObject = null;

                foreach (ManagementObject mgmtObject in mgmtObjectColl)
                {
                    if (mgmtObject["NetConnectionID"] != null && mgmtObject["NetConnectionID"].Equals(nicName))
                    {
                        myObject = mgmtObject;

                        //object result = mgmtObject.InvokeMethod("Disable", new object[] {});
                        //
                        // When there is no parameter, you don't need to pass an object array with
                        // no element in it.
                        object result = mgmtObject.InvokeMethod("Disable", null);
                    }
                    //Console.WriteLine("{0}, {1}", mgmtObject["Name"], mgmtObject["NetConnectionID"]);

                }

                object result3 = myObject.InvokeMethod("Enable", null);
            }
            catch
            {
                throw;
            }

            //source: https://jongampark.wordpress.com/2012/05/29/enabling-and-disabling-network-interface-on-windows-using-c/
        }
    }
}
