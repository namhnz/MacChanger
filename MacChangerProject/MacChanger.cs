using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using MacChangerProject.GetIPByMAC;

namespace MacChangerProject
{
    public partial class MacChanger : Form
    {
        XmlHandlder xmlHandler;

        public MacChanger()
        {
            InitializeComponent();
        }

        private void MacChanger_Load(object sender, EventArgs e)
        {
            xmlHandler = new XmlHandlder();
            xmlHandler.LoadSettingsFromConfigFile();

            LoadSettingToForm();

            LoadCurrentMac();
        }

        private void LoadSettingToForm()
        {
            txtMac.Text = XmlHandlder.lastMacUsed.MAC;
            txtHostName.Text = XmlHandlder.lastMacUsed.HostName;
            txtManufacturer.Text = XmlHandlder.lastMacUsed.Manufacturer;
            cbFromAllMac.Checked = XmlHandlder.isMacFromAllMac;
        }

        private void MacChanger_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveFormToXmlHandler();

            if (xmlHandler.SaveSetingsToConfigFile())
            {
                MessageBox.Show($"Saved config to {XmlHandlder.pathToConfig}", "Saved");
            }
            else
            {
                MessageBox.Show($"Error when saving config to {XmlHandlder.pathToConfig}", "Can't saved");
            }
        }

        private void SaveFormToXmlHandler()
        {
            XmlHandlder.isMacFromAllMac = cbFromAllMac.Checked;
            XmlHandlder.lastMacUsed.MAC = txtMac.Text;
            if (cbFromAllMac.Checked)
            {
                XmlHandlder.lastMacUsed.HostName = txtHostName.Text;
                XmlHandlder.lastMacUsed.Manufacturer = txtManufacturer.Text;
            }
            else
            {
                XmlHandlder.lastMacUsed.HostName = XmlHandlder.lastMacUsed.Manufacturer = "";
            }
        }

        private void btnChangeMac_Click(object sender, EventArgs e)
        {
            if (cbFromAllMac.Checked)
            {
                MACEntity newMac = xmlHandler.IterateAllRow();
                txtMac.Text = newMac.MAC;
                txtManufacturer.Text = newMac.Manufacturer;
                txtHostName.Text = newMac.HostName;
            }
            else
            {
                if (string.IsNullOrEmpty(txtMac.Text))
                {
                    MessageBox.Show("MAC value can't be empty", "Empty MAC");
                    return;
                }
            }

            if (SetNewMac(txtMac.Text))
                MessageBox.Show("Changed successfully", "Changed Registry Key", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Failed to change registry key value", "Changed Registry Key Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            try
            {
                MACHandler.DisableAndEnableNetworkAdapter("Ethernet");
            }
            catch
            {
                MessageBox.Show("Failed to Disable and Enable Ethernet Adapter", "Disable And Enable Net Adapter", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnShowAllMac_Click(object sender, EventArgs e)
        {
            AllMacList allMacList = new AllMacList()
            {
                AllMac = xmlHandler.GetAllMac()
            };

            allMacList.ShowDialog();
        }

        private void btnAddNewMac_Click(object sender, EventArgs e)
        {
            int rowAdded = xmlHandler.AddNewMACToAllMAC();
            MessageBox.Show($"Added {rowAdded} MACs", "New Rows Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnCurrentMac_Click(object sender, EventArgs e)
        {
            LoadCurrentMac();
        }

        private void LoadCurrentMac()
        {
            RegistryKey rKey;
            string macValue;

            rKey = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\Class\{4d36e972-e325-11ce-bfc1-08002be10318}\0002", true);
            macValue = rKey.GetValue("NetworkAddress").ToString();

            rKey.Close();
            lbCurrentMac.Text = MACHandler.ConvertToSeparatedMac(macValue, ":");
        }

        private bool SetNewMac(string newMac)
        {
            string newMacWithoutSeparator = MACHandler.ConvertToOnlyNumberMac(newMac);

            RegistryKey rKey = null;
            try
            {
                rKey = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\Class\{4d36e972-e325-11ce-bfc1-08002be10318}\0002", true);
                rKey.SetValue("NetworkAddress", newMacWithoutSeparator);

                rKey.Close();
                
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                rKey.Close();
            }
        }


        private void btnThisMacOnline_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMacToCheck.Text))
            {
                MessageBox.Show("MAC to check can't be empty", "Empty MAC");
                return;
            }

            MACHandler macHandler = new MACHandler();
            try
            {
                if (macHandler.IsOnline(MACHandler.ConvertToOnlyNumberMac(txtMacToCheck.Text)))
                {
                    MessageBox.Show("The PC of this MAC is online now", "Online", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("The PC of this MAC is offline now", "Offline", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch(ArgumentNullException ex)
            {
                MessageBox.Show($"{ex.Message}\n" + $"Error: {ex.InnerException.Message}", "This MAC Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Error when ping: {ex.Message}", "Ping Error");
            }
        }

        private void MacChanger_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
