using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FuelTrakKeyEncoder.FuelTrak;

namespace FuelTrakKeyEncoder
{
    public partial class Lookup : Form
    {
        private bool lookupVehicle;

        public Lookup(bool lookupVehicle)
        {
            InitializeComponent();

            this.lookupVehicle = lookupVehicle;
        }

        private string enteredId;
        public string EnteredId { get { return enteredId; } }

        private void btnLookup_Click(object sender, EventArgs e)
        {
            string enteredText = textBox1.Text;
            if (string.IsNullOrEmpty(enteredText))
            {
                MessageBox.Show("Error: No Data Entered", "Invalid Id", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //string id;
            //if (!int.TryParse(enteredText, out id))
            //{
            //    MessageBox.Show("Error: Invalid Id Entered", "Invalid Id", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}

            enteredId = enteredText;
            Close();
        }
    }
}