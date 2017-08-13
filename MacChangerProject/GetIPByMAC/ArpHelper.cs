using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace MacChangerProject.GetIPByMAC
{
    class ArpHelper
    {
        public List<ArpEntity> GetArpResult()
        {
            var p = Process.Start(new ProcessStartInfo("arp", "-a")
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardOutput = true
            });

            var output = p?.StandardOutput.ReadToEnd(); //sẽ cho ra một chuỗi giống khi gõ lệnh: arp -a trong CMD
            p?.Close();

            return ParseArpResult(output);
        }

        /// <summary>
        /// Chuyển đổi bảng ARP sang danh sách các ARPEntity
        /// </summary>
        /// <param name="output">Danh sách các ARPEntity</param>
        /// <returns></returns>
        private List<ArpEntity> ParseArpResult(string output)
        {
            var lines = output.Split('\n').Where(l => !string.IsNullOrWhiteSpace(l));

            var result =
                (from line in lines
                    select Regex.Split(line, @"\s+")
                    .Where(i => !string.IsNullOrWhiteSpace(i)).ToList()
                    into items
                    where items.Count == 3
                    select new ArpEntity()
                    {
                        Ip = items[0],
                        MacAddress = items[1],
                        Type = items[2]
                    });

            return result.ToList();
        }
    }
}
