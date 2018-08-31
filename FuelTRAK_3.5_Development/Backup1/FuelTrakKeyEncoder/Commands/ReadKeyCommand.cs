using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;

namespace FuelTrakKeyEncoder.Commands
{
    public class ReadKeyCommand
    {
        private const string keyReadCommandText = "!1<R>";
        private const string keyReadMileageText = "!1<Q082>";
        private const string keyReadMileageWindowText = "!1<Q0e>";

        private const int baudRate = 1200;
        private const Parity parity = Parity.None;
        private const int databits = 8;
        private const StopBits stopbits = StopBits.One;

        private readonly string portName;

        public ReadKeyCommand(string portName)
        {
            List<string> validPortNames = new List<string>(SerialPort.GetPortNames());
            if (!validPortNames.Contains(portName))
            {
                throw new ArgumentOutOfRangeException("The provided port name is not valid for this computer");
            }

            this.portName = portName;

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


                comPort.Write(keyReadCommandText);

                // I dont like using Thread.Sleep, it seems hackish but had little success with any other methods.
                Thread.Sleep(2000);

                data = comPort.ReadExisting();
                
                // RSmith, separate calls to get mileage and mileage window.
                comPort.Write(keyReadMileageText);
                Thread.Sleep(1000);
                dataMileage = comPort.ReadExisting();

                comPort.Write(keyReadMileageWindowText);
                Thread.Sleep(1000);
                dataMileageWindow = comPort.ReadExisting();
            }
        }

        private string data;
        public string DataRead
        {
            get { return data; }
        }

        private string dataMileage;
        public string DataReadMileage
        {
            get { return dataMileage; }
        }

        private string dataMileageWindow;
        public string DataReadMileageWindow
        {
            get { return dataMileageWindow; }
        }
    }
}
