using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using FuelTrakKeyEncoder.Services;
using FuelTrakKeyEncoder.FuelTrak;

namespace FuelTrakKeyEncoder
{
    public partial class Configuration : Form
    {
        private SettingsService settingsService = new SettingsService();
        private readonly bool userMustSaveChanges;

        public Configuration(bool mustSaveChanges)
        {
            InitializeComponent();

            userMustSaveChanges = mustSaveChanges;
            comboBox1.DataSource = SerialPort.GetPortNames();

            IUserSettings settings = settingsService.GetUserSettings();
            if (settings != null)
            {
                comboBox1.SelectedItem = settings.ComPort;
                txtFuelTrakUrl.Text = settings.FuelTrakUrl;
            }
        }

        private void OnCancel(object sender, EventArgs e)
        {
            if (userMustSaveChanges)
            {
                MessageBox.Show("You must complete the application configuration before continuing.", "Configuration Incomplete", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.Close();
        }

        private bool ValidateSettings()
        {
            if (string.IsNullOrEmpty(txtFuelTrakUrl.Text) || !Uri.IsWellFormedUriString(txtFuelTrakUrl.Text, UriKind.Absolute))
            {
                MessageBox.Show("Error: Invalid FuelTRAK Url Entered. Please enter the base url to the FuelTRAK website (e.g http://<your-web-server>/FuelTRAK/).", "Invalid Setting", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            using (KeyEncoderService service = new KeyEncoderService())
            {
                try
                {
                    service.Url = txtFuelTrakUrl.Text + (txtFuelTrakUrl.Text.EndsWith("/") ? "" : "/") + "Services/KeyEncoderService.asmx";
                    service.Ping();
                    return true;
                }
                catch (Exception)
                {
                    MessageBox.Show("Error: Invalid FuelTRAK Url Entered. Please enter the base url to the FuelTRAK website (e.g http://<your-web-server>/FuelTRAK/).", "Invalid Setting", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        private bool settingSaved = false;
        public bool SettingsSaved
        {
            get { return settingSaved; }
        }

        private void OnSave(object sender, EventArgs e)
        {
            if (ValidateSettings())
            {
                settingsService.UpdateSettings(comboBox1.SelectedItem.ToString(), txtFuelTrakUrl.Text);
                settingSaved = true;
                this.Close();
            }
        }
    }
}