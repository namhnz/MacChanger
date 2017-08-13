using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MacChangerProject
{
    public partial class AllMacList : Form
    {
        public AllMacList()
        {
            InitializeComponent();
        }

        public List<MACEntity> AllMac { get; set; }

        private void AllMacList_Load(object sender, EventArgs e)
        {
            dgvAllMac.DataSource = AllMac;
        }
    }
}
