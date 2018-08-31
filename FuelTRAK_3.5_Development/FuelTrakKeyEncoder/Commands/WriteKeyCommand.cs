using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;

namespace FuelTrakKeyEncoder.Commands
{
    public class WriteKeyCommand
    {
        private const string keyWriteCommandTextFormat = "!1<E{0}>\r";
        private const string keyWriteMileageTextFormat = "!1<W082{0},{1}>\r";
        private const string keyWriteMileageWindowTextFormat = "!1<W0e{0}>\r";
        private const int baudRate = 1200;
        private const Parity parity = Parity.None;
        private const int databits = 8;
        private const StopBits stopbits = StopBits.One;

        private readonly string portName;
        private readonly string data;

        private readonly string dataMileage;

        private readonly string dataMileageWindow;

        private readonly string dataToCheckKeyType;

        private bool executeStatus;

        public bool ExecutionStatus { get { return executeStatus; } }

        public WriteKeyCommand(string portName, string keyData, string mileageData, string mileageWindowData,string keyType)
        {
            List<string> validPortNames = new List<string>(SerialPort.GetPortNames());
            if (!validPortNames.Contains(portName))
            {
                throw new ArgumentOutOfRangeException("The provided port name is not valid for this computer");
            }

            this.portName = portName;
            if (keyType == "Personnel")
            {
                this.data = string.Format(keyWriteCommandTextFormat, keyData);
                this.dataToCheckKeyType = keyType;
                //Commented By Varun Moota, not required for Personnel.
                //this.dataMileage = string.Format(keyWriteMileageTextFormat, mileageData.Substring(0,4), mileageData.Substring(4,4));
                //this.dataMileageWindow = string.Format(keyWriteMileageWindowTextFormat, mileageWindowData);
            }
            else
            {
                this.data = string.Format(keyWriteCommandTextFormat, keyData);
                this.dataToCheckKeyType = keyType;
                this.dataMileage = string.Format(keyWriteMileageTextFormat, mileageData.Substring(0,4), mileageData.Substring(4,4));
                this.dataMileageWindow = string.Format(keyWriteMileageWindowTextFormat, mileageWindowData);
            }
        }

        public void Execute()
        {
            using (SerialPort comPort = new SerialPort(portName, baudRate, parity, databits, stopbits))
            {

                try
                {
                    comPort.Open();
                }
                catch
                {
                    MessageBox.Show("Unable to open port: " + portName,
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1);
                    return;
                }

                comPort.DtrEnable = true;
                comPort.RtsEnable = true;

                comPort.Write(data);
                //comPort.Write("!1<E200101017      1000675000000000000000000000000>");

                // I dont like using Thread.Sleep, it seems hackish but had little success with any other methods.
                Thread.Sleep(2000);
                string returnedData = comPort.ReadExisting();
                executeStatus = returnedData.StartsWith("+");
                if (!executeStatus)
                    return;
               
                if (dataToCheckKeyType == "Vehicle")
                {
                    comPort.Write(dataMileage);
                    Thread.Sleep(1000);
                    returnedData = comPort.ReadExisting();
                    executeStatus = returnedData.EndsWith("+");
                    if (!executeStatus)
                        return;

                    comPort.Write(dataMileageWindow);
                    Thread.Sleep(1000);
                    returnedData = comPort.ReadExisting();
                    executeStatus = returnedData.EndsWith("+");
                }
            }
        }
    }
}
