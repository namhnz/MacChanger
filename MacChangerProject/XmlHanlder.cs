using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Xml;

namespace MacChangerProject
{
    public class XmlHandlder
    {
        public static string location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public static string pathToAllMAC = location + "\\all-mac.xml";
        public static string pathToMACNew = location + "\\ipscan-mac-list.xml";
        public static string pathToConfig = location + "\\config.xml";
        public static string vmwareManufacturer = "VMware, Inc."; //dùng để loại bỏ địa chỉ MAC của VMWare
        public static string routerManufacturer = "TP-LINK TECHNOLOGIES CO.,LTD."; //dùng để loại bỏ địa chỉ MAC của router
        public static int lastRowVisited;
        public static MACEntity lastMacUsed = new MACEntity();
        public static bool isMacFromAllMac;

        XmlDocument macListIPScanDoc; //chứa danh sách địa chỉ MAC
        XmlDocument allMacDoc;
        XmlDocument configDoc;

        public XmlHandlder()
        {
            macListIPScanDoc = new XmlDocument();
            macListIPScanDoc.Load(pathToMACNew);

            allMacDoc = new XmlDocument();
            allMacDoc.Load(pathToAllMAC);

            configDoc = new XmlDocument();
            configDoc.Load(pathToConfig);
        }

        /// <summary>
        /// Thêm MAC mới từ IPScan sang AllMAC
        /// </summary>
        /// <returns>Số lượng MAC thêm mới</returns>
        public int AddNewMACToAllMAC()
        {
            int rowAdded = 0;

            XmlNodeList nodes = macListIPScanDoc.DocumentElement.ChildNodes;
            foreach (XmlNode childNode in nodes)
            {
                string mac = childNode.Attributes["mac"].Value;
                string hostName = childNode.Attributes["name"].Value;
                string manufacturer = (childNode.Attributes["manufacturer"]?.Value)??"";

                if (!IsExistInAllMAC(mac)) //nếu trong allMAC không có địa chỉ mac này thì thêm vào allMAC
                {
                    CreateAndAddNewRow(manufacturer, mac, hostName);
                    rowAdded++;
                }
            }

            allMacDoc.Save(pathToAllMAC);

            return rowAdded;
        }

        /// <summary>
        /// Kiểm tra 1 địa chỉ MAC có tồn tại trong AllMAC không.
        /// </summary>
        /// <param name="mac">Địa chỉ MAC cần kiểm tra. (có cả dấu 2 chấm)</param>
        /// <returns></returns>
        public bool IsExistInAllMAC(string mac)
        {
            XmlNode nodeToFind = allMacDoc.SelectSingleNode("/Advanced_IP_scanner/row[@mac = '" + mac + "']");
            if (nodeToFind != null)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Tạo 1 row mới chứa MAC và thêm vào allMAC
        /// </summary>
        /// <param name="manufacturer"></param>
        /// <param name="mac"></param>
        /// <param name="hostName"></param>
        public void CreateAndAddNewRow(string manufacturer, string mac, string hostName)
        {
            XmlElement row = allMacDoc.CreateElement("row");

            XmlAttribute manufacturerAttribute = allMacDoc.CreateAttribute("manufacturer");
            XmlAttribute macAttribute = allMacDoc.CreateAttribute("mac");
            XmlAttribute hostNameAttribute = allMacDoc.CreateAttribute("name");

            manufacturerAttribute.Value = manufacturer;
            macAttribute.Value = mac;
            hostNameAttribute.Value = hostName;

            row.Attributes.Append(manufacturerAttribute);
            row.Attributes.Append(macAttribute);
            row.Attributes.Append(hostNameAttribute);

            allMacDoc.DocumentElement.AppendChild(row);
        }


        /// <summary>
        /// Duyệt qua các phần tử của AllMacDoc từ vị trí của lần duyệt trước đó
        /// </summary>
        /// <returns>Một MACEntity chứa thông tin của địa chỉ MAC</returns>
        public MACEntity IterateAllRow()
        {
            MACEntity resultMAC = new MACEntity();
            MACHandler macHandler = new MACHandler();

            XmlNodeList nodes = allMacDoc.DocumentElement.ChildNodes;

            for (int i = lastRowVisited + 1; i <= nodes.Count; i++) 
            {
                string manufacturer = (nodes[i].Attributes["manufacturer"]?.Value)??"";
                string mac = nodes[i].Attributes["mac"].Value;
                string hostName = nodes[i].Attributes["name"].Value;

                if (manufacturer == vmwareManufacturer || manufacturer == routerManufacturer)
                    continue;
                try
                {
                    if (!macHandler.IsOnline(mac)) //MAc này không online nên có thể dùng được
                    {
                        resultMAC.MAC = mac;
                        resultMAC.Manufacturer = manufacturer;
                        resultMAC.HostName = hostName;
                        lastRowVisited = i;
                        break;
                    }
                }
                catch(ArgumentNullException) //MAC này không xuất hiện trong ARP table nên có thể dùng được
                {
                    resultMAC.MAC = mac;
                    resultMAC.Manufacturer = manufacturer;
                    resultMAC.HostName = hostName;
                    lastRowVisited = i;
                    break;
                }
            }
            return resultMAC;
        }


        public bool SaveSetingsToConfigFile()
        {
            try
            {
                XmlNode oldConfig = configDoc.DocumentElement.FirstChild;
                XmlElement newConfig = configDoc.CreateElement("config");
                XmlAttribute lastRowVisitedAttribute = configDoc.CreateAttribute("last_row_visited");
                lastRowVisitedAttribute.Value = lastRowVisited.ToString();
                newConfig.Attributes.Append(lastRowVisitedAttribute);

                //tạo node: last_used_mac
                XmlElement lastUsedMacConfig = configDoc.CreateElement("last_used_mac");

                //tạo và thêm các thuộc tính cho node: last_used_mac
                XmlAttribute isFromAllMacAttribute = configDoc.CreateAttribute("is_from_all_mac");
                isFromAllMacAttribute.Value = isMacFromAllMac.ToString();

                XmlAttribute macAttribute = configDoc.CreateAttribute("mac");
                macAttribute.Value = lastMacUsed.MAC;

                XmlAttribute nameAttribute = configDoc.CreateAttribute("name");
                nameAttribute.Value = lastMacUsed.HostName; //giá trị lastMacUsed.HostName = "" trước khi thực hiện phép gán này

                XmlAttribute manufacturerAttribute = configDoc.CreateAttribute("manufacturer");
                manufacturerAttribute.Value = lastMacUsed.Manufacturer; //giá trị lastMacUsed.Manufacturer = "" trước khi thực hiện phép gán này


                //thêm các thuộc tính vào node: last_used_mac
                lastUsedMacConfig.Attributes.Append(isFromAllMacAttribute);
                lastUsedMacConfig.Attributes.Append(macAttribute);
                lastUsedMacConfig.Attributes.Append(nameAttribute);
                lastUsedMacConfig.Attributes.Append(manufacturerAttribute);

                newConfig.AppendChild(lastUsedMacConfig);

                configDoc.DocumentElement.ReplaceChild(newConfig, oldConfig);
                configDoc.Save(pathToConfig);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public void LoadSettingsFromConfigFile()
        {
            XmlNode config = configDoc.DocumentElement.FirstChild;
            lastRowVisited = int.Parse(config.Attributes["last_row_visited"].Value);
            XmlNode macEntityConfig = config.FirstChild;
            isMacFromAllMac = bool.Parse(macEntityConfig.Attributes["is_from_all_mac"].Value);

            lastMacUsed.MAC = macEntityConfig.Attributes["mac"].Value;
            if (isMacFromAllMac)
            {
                lastMacUsed.HostName = macEntityConfig.Attributes["name"].Value;
                lastMacUsed.Manufacturer = (macEntityConfig.Attributes["manufacturer"]?.Value) ?? "";
            }
        }

        public List<MACEntity> GetAllMac()
        {
            List<MACEntity> lst = new List<MACEntity>();

            XmlNodeList nodes = allMacDoc.DocumentElement.ChildNodes;
            foreach (XmlNode childNode in nodes)
            {
                lst.Add(new MACEntity()
                {
                    MAC = childNode.Attributes["mac"].Value,
                    Manufacturer = childNode.Attributes["manufacturer"].Value,
                    HostName = childNode.Attributes["name"].Value
                });
            }

            return lst;
        }
    }

    
}
