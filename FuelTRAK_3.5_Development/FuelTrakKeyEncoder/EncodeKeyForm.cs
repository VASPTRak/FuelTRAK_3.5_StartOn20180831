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
using System.Deployment.Application;
using FuelTrakKeyEncoder.Controls;
using System.Runtime.Remoting.Messaging;

namespace FuelTrakKeyEncoder
{
    public delegate void Action();
    public delegate void Action<T1, T2>(T1 value1, T2 value2);
    public delegate T1 Func<T1, T2>(T2 value);

    public partial class EncodeKeyForm : Form
    {
        private IUserSettings settings;
        private FuelTrakKeyData currentDisplayedData;

        public EncodeKeyForm(IUserSettings settings)
        {

            try
            {
                InitializeComponent();

                SettingsService.UserSettingsChanged += OnSettingsChanged;

                if (settings == null)
                {
                    Configuration config = new Configuration(true);
                    config.ShowDialog(this);

                    if (!config.SettingsSaved)
                    {
                        // the user closed the settings dialog without completing the configuration so we cannot continue
                        // Exit code 0 = normal termination
                        Environment.Exit(0);
                    }
                }
                else
                {
                    this.settings = settings;
                }
            }
            catch (Exception ex)
            {
            }
        }

        void OnSettingsChanged(object sender, EventArgs e)
        {
            SettingsService settingsService = new SettingsService();
            settings = settingsService.GetUserSettings();
        }

        private void Log(string message)
        {
            System.Diagnostics.Debug.Write(message);
        }

        private void OnReadKeyRequest(object sender, EventArgs e)
        {
            try
            {
                DisplayStatusMessage("Reading data", true);
                pnlInformationDisplay.Controls.Clear();
                ReadKeyCommand readCommand = new ReadKeyCommand(settings.ComPort);
                Action readAction = new Action(readCommand.Execute);
                readAction.BeginInvoke(ReadRequestCallback, readCommand);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Probelm Reading Key:" + ex.InnerException.ToString(), "ERROR", MessageBoxButtons.OK);
            }
        }

        private void OnEncodeKeyRequest(object sender, EventArgs e)
        {
            try
            {
                //if (currentDisplayedData == null) return;
                //string dataToWrite = currentDisplayedData.ToKeyDataString();
                //string dataToWriteMileage = currentDisplayedData.ToKeyMileageString();
                //string dataToWriteMileageWindow = currentDisplayedData.ToKeyMileageWindowString();

                //DisplayStatusMessage("Writing data", true);

                //WriteKeyCommand writeKeyCommand = new WriteKeyCommand(settings.ComPort, dataToWrite,
                //    dataToWriteMileage, dataToWriteMileageWindow);
                //Action writeAction = new Action(writeKeyCommand.Execute);
                //writeAction.BeginInvoke(WriteRequestCallback, writeKeyCommand);

                //Added By Varun Moota, since we need to differentiate both Vehicles and Personnel Key write's.03/05/2012
                if (currentDisplayedData.KeyType.ToString() == "Personnel")
                {
                    if (currentDisplayedData == null) return;
                    string dataToWrite = currentDisplayedData.ToKeyDataString();
                    string dataToWriteMileage = Convert.ToString(null);
                    string dataToWriteMileageWindow = Convert.ToString(null);
                    string dataToCheckKeyType = currentDisplayedData.KeyType.ToString();
                    DisplayStatusMessage("Writing data", true);

                    WriteKeyCommand writeKeyCommand = new WriteKeyCommand(settings.ComPort, dataToWrite,
                        dataToWriteMileage, dataToWriteMileageWindow, dataToCheckKeyType);
                    Action writeAction = new Action(writeKeyCommand.Execute);
                    writeAction.BeginInvoke(WriteRequestCallback, writeKeyCommand);
                }
                else
                {
                    if (currentDisplayedData == null) return;
                    string dataToWrite = currentDisplayedData.ToKeyDataString();
                    string dataToWriteMileage = currentDisplayedData.ToKeyMileageString();
                    string dataToWriteMileageWindow = currentDisplayedData.ToKeyMileageWindowString();
                    string dataToCheckKeyType = currentDisplayedData.KeyType.ToString();

                    DisplayStatusMessage("Writing data", true);

                    WriteKeyCommand writeKeyCommand = new WriteKeyCommand(settings.ComPort, dataToWrite,
                        dataToWriteMileage, dataToWriteMileageWindow, dataToCheckKeyType);
                    Action writeAction = new Action(writeKeyCommand.Execute);
                    writeAction.BeginInvoke(WriteRequestCallback, writeKeyCommand);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Probelm Encoding Key:"+ex.InnerException.ToString(), "ERROR", MessageBoxButtons.OK);
            }
        }

        private void BindDataToView(FuelTrakKeyData data)
        {
            try
            {
                if (InvokeRequired)
                {
                    Invoke(new Action<FuelTrakKeyData>(BindDataToView), data);
                    return;
                }


                pnlInformationDisplay.Controls.Clear();
                currentDisplayedData = data;

                if (currentDisplayedData == null)
                {
                    btnEncodeKey.Enabled = false;
                    return;
                }

                if (currentDisplayedData.KeyType == FuelTrakKeyType.Personnel)
                {
                    PersonnelKeyInfo infoControl = new PersonnelKeyInfo();
                    infoControl.Data = currentDisplayedData;
                    pnlInformationDisplay.Controls.Add(infoControl);
                    infoControl.Dock = DockStyle.Fill;
                }
                else
                {
                    VehicleKeyInfo infoControl = new VehicleKeyInfo();
                    infoControl.Data = currentDisplayedData;
                    pnlInformationDisplay.Controls.Add(infoControl);
                    infoControl.Dock = DockStyle.Fill;
                }

                btnEncodeKey.Enabled = true;
            }
            catch (Exception ex)
            {
            }
        }

        public void SetVehicleId(string strVehicleId)
        {
            try
            {
                DisplayStatusMessage("Looking up vehicle id:" + strVehicleId, true);
                Func<bool, string> getDataAndDisplay = new Func<bool, string>(RetrieveVehicleInformationAndBind);
                getDataAndDisplay.BeginInvoke(strVehicleId, GetDataAndDisplayCallback, null);
            }
             
            catch (Exception ex)
            {
            }
        }

        private bool RetrieveVehicleInformationAndBind(string vehicleId)
        {
            try
            {
                VehicleInformation info;
                string vehicleKey;
                using (KeyEncoderService keyEncoderService = new KeyEncoderService())
                {
                    keyEncoderService.Url = settings.FuelTrakUrl + (settings.FuelTrakUrl.EndsWith("/") ? "" : "/") + "Services/KeyEncoderService.asmx";

                    info = keyEncoderService.GetVehicleInformation(vehicleId);
                    if (info == null)
                    {
                        DisplayStatusMessage("No data found for vehicle: " + vehicleId, false);
                        return false;
                    }

                    vehicleKey = keyEncoderService.GetVehicleKeyId(vehicleId);
                }

                FuelTrakKeyData data = FuelTrakKeyData.ParseFromVehicleInformation(info, vehicleKey);
                BindDataToView(data);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void SetPersonnelId(string strPersonnelId)
        {
            try
            {
                DisplayStatusMessage("Looking up personnel id:" + strPersonnelId, true);
                Func<bool, string> getDataAndDisplay = new Func<bool, string>(RetrievePersonnelInformationAndBind);
                getDataAndDisplay.BeginInvoke(strPersonnelId, GetDataAndDisplayCallback, null);
            }
            catch (Exception ex)
            {
                Log(ex.Message);
                Log(ex.StackTrace);
                DisplayStatusMessage("Exception:" + ex.InnerException.ToString(), false);
            }
        }

        private bool RetrievePersonnelInformationAndBind(string personnelId)
        {

            try
            {
                PersonnelInformation info;
                string personnelKey;
                using (KeyEncoderService keyEncoderService = new KeyEncoderService())
                {
                    keyEncoderService.Url = settings.FuelTrakUrl + (settings.FuelTrakUrl.EndsWith("/") ? "" : "/") + "Services/KeyEncoderService.asmx";

                    info = keyEncoderService.GetPersonnelInformation(personnelId);
                    if (info == null)
                    {
                        DisplayStatusMessage("No data found for personnel: " + personnelId, false);
                        return false;
                    }

                    personnelKey = keyEncoderService.GetPersonnelKeyId(personnelId);
                }

                FuelTrakKeyData data = FuelTrakKeyData.ParseFromPersonnelInformation(info, personnelKey);
                BindDataToView(data);
                return true;
            }
            catch (Exception ex)
            {
                Log(ex.Message);
                Log(ex.StackTrace);
                DisplayStatusMessage("Exception:" + ex.InnerException.ToString(), false);           
                return false;
            }
        }

        private void editSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            try
            {
                Configuration config = new Configuration(false);
                config.ShowDialog(this);
            }
            catch (Exception ex)
            {
                Log(ex.Message);
                Log(ex.StackTrace);
                DisplayStatusMessage("Exception:" + ex.InnerException.ToString(), false);
            }
        }

        private void lookupVehicleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                LookupAndBindData(true);
            }
            catch (Exception ex)
            {
                Log(ex.Message);
                Log(ex.StackTrace);
                DisplayStatusMessage("Exception:" + ex.InnerException.ToString(), false);
            }
        }

        private void lookupPersonnelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                LookupAndBindData(false);
            }
            catch (Exception ex)
            {
                Log(ex.Message);
                Log(ex.StackTrace);

                DisplayStatusMessage("Exception:" + ex.InnerException.ToString(), false);
            }
        }

        private void LookupAndBindData(bool vehicle)
        {
            try
            {
                Lookup lookupForm = new Lookup(vehicle);
                lookupForm.ShowDialog(this);
                if (string.IsNullOrEmpty(lookupForm.EnteredId)) return;

                if (vehicle)
                {
                    SetVehicleId(lookupForm.EnteredId);
                }
                else
                {
                    SetPersonnelId(lookupForm.EnteredId);
                }
            }
            catch (Exception ex)
            {
                Log(ex.Message);
                Log(ex.StackTrace);

                DisplayStatusMessage("Exception:" + ex.InnerException.ToString(), false);
            }
        }

        private void DisplayStatusMessage(string message, bool showWaitAnimation)
        {
            try
            {
                if (statusStrip1.InvokeRequired)
                {
                    statusStrip1.Invoke(new Action<string, bool>(DisplayStatusMessage), message, showWaitAnimation);
                    return;
                }

                lblStatus.Text = message;
                toolStripProgressBar1.Visible = showWaitAnimation;
            }
            catch (Exception ex)
            {
                Log(ex.Message);
                Log(ex.StackTrace);
                
                DisplayStatusMessage("Exception:"+ex.InnerException.ToString(), false);
            }
        }

        private void ReadRequestCallback(IAsyncResult result)
        {
            try
            {
                AsyncResult r = (AsyncResult)result;
                Action originalAction = (Action)r.AsyncDelegate;
                originalAction.EndInvoke(result);

                ReadKeyCommand command = (ReadKeyCommand)result.AsyncState;

                if (command.DataRead.StartsWith("+"))
                    DisplayStatusMessage("Successful Read", false);
                else
                    DisplayStatusMessage("Read Failed", false);

                FuelTrakKeyData data = FuelTrakKeyData.Parse(command.DataRead, 
                    command.DataReadMileage,command.DataReadMileageWindow);
                BindDataToView(data);
            }
            catch (Exception ex)
            {
                Log(ex.Message);
                Log(ex.StackTrace);

                DisplayStatusMessage("Error attempting read", false);
            }
        }

        private void WriteRequestCallback(IAsyncResult result)
        {
            try
            {
                AsyncResult r = (AsyncResult)result;
                Action originalAction = (Action)r.AsyncDelegate;
                originalAction.EndInvoke(result);

                WriteKeyCommand command = (WriteKeyCommand)result.AsyncState;

                if (command.ExecutionStatus)
                {
                    DisplayStatusMessage("Successful Key Encode", false);
                }
                else
                {
                    DisplayStatusMessage("Key Encode Failed", false);
                }
            }
            catch (Exception ex)
            {
                Log(ex.Message);
                Log(ex.StackTrace);

                DisplayStatusMessage("Error attempting to encode key", false);
            }

        }

        private void GetDataAndDisplayCallback(IAsyncResult result)
        {
            AsyncResult r = (AsyncResult)result;
            Func<bool, string> originalAction = (Func<bool, string>)r.AsyncDelegate;
            bool status = originalAction.EndInvoke(result);
            // if data recieved successfully display a message, else the action sets a failure message internally
            if (status == true) DisplayStatusMessage("Data Retrieved Successfully", false);
        }        
    }
}