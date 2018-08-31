using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FuelTrakKeyEncoder.Controls
{
    public partial class PersonnelKeyInfo : UserControl
    {
        public PersonnelKeyInfo()
        {
            InitializeComponent();
        }

        private FuelTrakKeyData data;

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
        }

        private void ClearData()
        {
            txtKeyType.Clear();
            txtKeyNumber.Clear();
            txtVehicleId.Clear();
            txtExpiration.Clear();
            txtSystemNumber.Clear();
        }
    }
}
