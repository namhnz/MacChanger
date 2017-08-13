using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MacChangerProject.GetIPByMAC;

namespace MacChangerProject
{
    public partial class LoadingForm : Form
    {
        Timer timer;

        public LoadingForm()
        {
            InitializeComponent();
        }

        private void LoadingForm_Shown(object sender, EventArgs e)
        {
            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;

            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            DialogResult updateTable = MessageBox.Show("Do you want to update ARP Table?", "Update ARP Table", MessageBoxButtons.YesNo);
            if (updateTable == DialogResult.Yes) 
                UpdateARPTableBeforeRun();

            MacChanger mainForm = new MacChanger();
            mainForm.Show();
            this.Hide();
        }

        private void UpdateARPTableBeforeRun()
        {
            IpFinder finder = new IpFinder();
            finder.UpdateARPTable();
        }
    }
}
