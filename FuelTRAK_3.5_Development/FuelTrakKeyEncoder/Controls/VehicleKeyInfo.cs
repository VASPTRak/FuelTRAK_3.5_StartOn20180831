using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FuelTrakKeyEncoder.Controls
{
    public partial class VehicleKeyInfo : UserControl
    {
        private FuelTrakKeyData data;

        public VehicleKeyInfo()
        {
            InitializeComponent();
        }

        public FuelTrakKeyData Data
        {
            get { return data; }
            set 
            {
               data = value;
               OnDataMemberSet();
            }
        }

        private void OnDataMemberSet()
        {
            if (data == null)
                ClearData();
            else
                DisplayData();
        }

        private void DisplayData()
        {
            txtKeyType.Text = data.KeyType.ToString();
            txtKeyNumber.Text = data.KeyId;
            //txtVehicleId.Text = data.ItemId.PadLeft(6, '0');
            txtVehicleId.Text = data.ItemId;
            txtExpiration.Text = data.Expiration;
            txtSystemNumber.Text = data.SystemNumber;
            txtFuelLimit.Text = data.FuelLimit;
            txtFuelTypes.Text = data.FuelTypes;
            txtMaster.Text = data.IsMasterKey ? "Yes" : "No";
            txtSecondKeyReq.Text = data.IsSecondKeyReq ? "Yes" : "No";
            txtOption.Text = data.Option ? "Yes" : "No";
            txtRequiredMileage.Text = data.IsMileageEntryRequired ? "Yes" : "No";
            txtMileage.Text = data.Mileage;
            txtMileageWindow.Text = data.MileageWindow;
        }

        public void ClearData()
        {
            txtKeyType.Clear();
            txtKeyNumber.Clear();
            txtVehicleId.Clear();
            txtExpiration.Clear();
            txtSystemNumber.Clear();
            txtFuelLimit.Clear();
            txtMaster.Clear();
            txtSecondKeyReq.Clear();
            txtOption.Clear();
            txtMileage.Clear();
            txtMileageWindow.Clear();
            txtRequiredMileage.Clear();
        }

    }
}
