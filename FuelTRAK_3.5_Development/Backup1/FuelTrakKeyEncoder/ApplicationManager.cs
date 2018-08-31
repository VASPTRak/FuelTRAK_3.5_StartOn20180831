using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FuelTrakKeyEncoder.Services;

namespace FuelTrakKeyEncoder
{
    public partial class ApplicationManager : Form
    {
        public ApplicationManager()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EncodeKeyForm form = new EncodeKeyForm(new SettingsService().GetUserSettings());
            form.SetVehicleId(textBox1.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EncodeKeyForm form = new EncodeKeyForm(new SettingsService().GetUserSettings());
            form.SetPersonnelId(textBox1.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }


    }
}