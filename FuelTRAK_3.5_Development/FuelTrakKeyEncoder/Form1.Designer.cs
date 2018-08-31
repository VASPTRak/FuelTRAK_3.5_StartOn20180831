namespace FuelTrakKeyEncoder
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtMileage = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtFuelLimit = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtOption = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtRequiredMileage = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtMaster = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSystemNumber = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtExpiration = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtVehicleId = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtKeyNumber = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtKeyType = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDebugInfo = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 333);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(166, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Read Key Information";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtMileage);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtFuelLimit);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtOption);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtRequiredMileage);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtMaster);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtSystemNumber);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtExpiration);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtVehicleId);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtKeyNumber);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtKeyType);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(386, 301);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Key Information";
            // 
            // txtMileage
            // 
            this.txtMileage.Location = new System.Drawing.Point(86, 259);
            this.txtMileage.Name = "txtMileage";
            this.txtMileage.ReadOnly = true;
            this.txtMileage.Size = new System.Drawing.Size(124, 20);
            this.txtMileage.TabIndex = 19;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 262);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(44, 13);
            this.label10.TabIndex = 18;
            this.label10.Text = "Mileage";
            // 
            // txtFuelLimit
            // 
            this.txtFuelLimit.Location = new System.Drawing.Point(86, 233);
            this.txtFuelLimit.Name = "txtFuelLimit";
            this.txtFuelLimit.ReadOnly = true;
            this.txtFuelLimit.Size = new System.Drawing.Size(124, 20);
            this.txtFuelLimit.TabIndex = 17;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 236);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(51, 13);
            this.label9.TabIndex = 16;
            this.label9.Text = "Fuel Limit";
            // 
            // txtOption
            // 
            this.txtOption.Location = new System.Drawing.Point(86, 207);
            this.txtOption.Name = "txtOption";
            this.txtOption.ReadOnly = true;
            this.txtOption.Size = new System.Drawing.Size(124, 20);
            this.txtOption.TabIndex = 15;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 210);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(38, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Option";
            // 
            // txtRequiredMileage
            // 
            this.txtRequiredMileage.Location = new System.Drawing.Point(86, 181);
            this.txtRequiredMileage.Name = "txtRequiredMileage";
            this.txtRequiredMileage.ReadOnly = true;
            this.txtRequiredMileage.Size = new System.Drawing.Size(124, 20);
            this.txtRequiredMileage.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 184);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Mileage Req.";
            // 
            // txtMaster
            // 
            this.txtMaster.Location = new System.Drawing.Point(86, 155);
            this.txtMaster.Name = "txtMaster";
            this.txtMaster.ReadOnly = true;
            this.txtMaster.Size = new System.Drawing.Size(124, 20);
            this.txtMaster.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 158);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Master";
            // 
            // txtSystemNumber
            // 
            this.txtSystemNumber.Location = new System.Drawing.Point(86, 129);
            this.txtSystemNumber.Name = "txtSystemNumber";
            this.txtSystemNumber.ReadOnly = true;
            this.txtSystemNumber.Size = new System.Drawing.Size(124, 20);
            this.txtSystemNumber.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 132);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "System No.";
            // 
            // txtExpiration
            // 
            this.txtExpiration.Location = new System.Drawing.Point(86, 103);
            this.txtExpiration.Name = "txtExpiration";
            this.txtExpiration.ReadOnly = true;
            this.txtExpiration.Size = new System.Drawing.Size(124, 20);
            this.txtExpiration.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Expiration";
            // 
            // txtVehicleId
            // 
            this.txtVehicleId.Location = new System.Drawing.Point(86, 77);
            this.txtVehicleId.Name = "txtVehicleId";
            this.txtVehicleId.ReadOnly = true;
            this.txtVehicleId.Size = new System.Drawing.Size(124, 20);
            this.txtVehicleId.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Vehicle Id";
            // 
            // txtKeyNumber
            // 
            this.txtKeyNumber.Location = new System.Drawing.Point(86, 51);
            this.txtKeyNumber.Name = "txtKeyNumber";
            this.txtKeyNumber.ReadOnly = true;
            this.txtKeyNumber.Size = new System.Drawing.Size(124, 20);
            this.txtKeyNumber.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Key No.";
            // 
            // txtKeyType
            // 
            this.txtKeyType.Location = new System.Drawing.Point(86, 25);
            this.txtKeyType.Name = "txtKeyType";
            this.txtKeyType.ReadOnly = true;
            this.txtKeyType.Size = new System.Drawing.Size(124, 20);
            this.txtKeyType.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Key Type";
            // 
            // txtDebugInfo
            // 
            this.txtDebugInfo.Location = new System.Drawing.Point(12, 374);
            this.txtDebugInfo.Multiline = true;
            this.txtDebugInfo.Name = "txtDebugInfo";
            this.txtDebugInfo.Size = new System.Drawing.Size(386, 193);
            this.txtDebugInfo.TabIndex = 3;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(230, 333);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(168, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "Copy Key Information";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(230, 310);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(80, 17);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.Text = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 579);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.txtDebugInfo);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtKeyType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMileage;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtFuelLimit;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtOption;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtRequiredMileage;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtMaster;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtSystemNumber;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtExpiration;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtVehicleId;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtKeyNumber;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDebugInfo;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}

