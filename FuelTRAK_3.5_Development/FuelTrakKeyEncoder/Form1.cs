using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;
using FuelTrakKeyEncoder.Commands;
using FuelTrakKeyEncoder.Services;
using FuelTrakKeyEncoder.FuelTrak;

namespace FuelTrakKeyEncoder
{
    public partial class Form1 : Form
    {
        private IUserSettings settings;

        public Form1(IUserSettings settings)
        {
            InitializeComponent();

            SettingsService.UserSettingsChanged += OnSettingsChanged;

            if (settings == null)
            {
                Configuration config = new Configuration(true);
                config.ShowDialog(this);
            }
        }

        void OnSettingsChanged(object sender, EventArgs e)
        {
            SettingsService settingsService = new SettingsService();
            settings = settingsService.GetUserSettings();
        }

        private void LogDebugMessage(string message)
        {
            if (!txtDebugInfo.InvokeRequired)
            {
                txtDebugInfo.AppendText(message);
                txtDebugInfo.AppendText(Environment.NewLine);
                return;
            }

            Action<string> logDebugMessageAction = LogDebugMessage;
            txtDebugInfo.Invoke(logDebugMessageAction, message);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ReadKeyCommand readCommand = new ReadKeyCommand(settings.ComPort);
                readCommand.Execute();

                LogDebugMessage("Recieved data: " + readCommand.DataRead);

                FuelTrakKeyData data = FuelTrakKeyData.Parse(readCommand.DataRead);
                BindDataToView(data);
            }
            catch (Exception ex)
            {
                LogDebugMessage("ERROR: " + ex.Message);
                LogDebugMessage("ERROR: " + ex.StackTrace);
            }
        }

        private void BindDataToView(FuelTrakKeyData data)
        {
            txtKeyType.Text = data.KeyType.ToString();
            txtKeyNumber.Text = data.KeyId;
            txtVehicleId.Text = data.ItemId;
            txtExpiration.Text = data.Expiration;
            txtSystemNumber.Text = data.SystemNumber;

            txtFuelLimit.Text = data.FuelLimit;
            txtMaster.Text = data.IsMasterKey ? "Yes" : "No";
            txtMileage.Text = data.Mileage;
            txtOption.Text = data.Option ? "Yes" : "No";
            txtRequiredMileage.Text = data.IsMileageEntryRequired ? "Yes" : "No";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("Insert key to read");

                ReadKeyCommand readCommand = new ReadKeyCommand(settings.ComPort);
                readCommand.Execute();

                FuelTrakKeyData inputData = FuelTrakKeyData.Parse(readCommand.DataRead);

                MessageBox.Show("Data read successful. Insert key to write.");

                string dataToWrite = inputData.ToKeyDataString();

                LogDebugMessage("Writing data: " + dataToWrite);

                WriteKeyCommand writeKeyCommand = new WriteKeyCommand(settings.ComPort, dataToWrite);
                writeKeyCommand.Execute();

                if (writeKeyCommand.ExecutionStatus)
                    LogDebugMessage("Data written to key successfully");
                else
                    LogDebugMessage("Data NOT written to key successfully");
            }
            catch (Exception ex)
            {
                LogDebugMessage("ERROR: " + ex.Message);
                LogDebugMessage("ERROR: " + ex.StackTrace);
            }
        }
    }



}