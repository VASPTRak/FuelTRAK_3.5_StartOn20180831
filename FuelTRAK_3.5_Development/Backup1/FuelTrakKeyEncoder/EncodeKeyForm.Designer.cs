namespace FuelTrakKeyEncoder
{
    partial class EncodeKeyForm
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
            this.btnReadKey = new System.Windows.Forms.Button();
            this.btnEncodeKey = new System.Windows.Forms.Button();
            this.pnlInformationDisplay = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lookupVehicleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lookupPersonnelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.editSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnReadKey
            // 
            this.btnReadKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReadKey.Location = new System.Drawing.Point(12, 153);
            this.btnReadKey.Name = "btnReadKey";
            this.btnReadKey.Size = new System.Drawing.Size(250, 23);
            this.btnReadKey.TabIndex = 0;
            this.btnReadKey.Text = "Read Key Information";
            this.btnReadKey.UseVisualStyleBackColor = true;
            this.btnReadKey.Click += new System.EventHandler(this.OnReadKeyRequest);
            // 
            // btnEncodeKey
            // 
            this.btnEncodeKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEncodeKey.Enabled = false;
            this.btnEncodeKey.Location = new System.Drawing.Point(12, 182);
            this.btnEncodeKey.Name = "btnEncodeKey";
            this.btnEncodeKey.Size = new System.Drawing.Size(250, 23);
            this.btnEncodeKey.TabIndex = 6;
            this.btnEncodeKey.Text = "Encode Key";
            this.btnEncodeKey.UseVisualStyleBackColor = true;
            this.btnEncodeKey.Click += new System.EventHandler(this.OnEncodeKeyRequest);
            // 
            // pnlInformationDisplay
            // 
            this.pnlInformationDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlInformationDisplay.AutoSize = true;
            this.pnlInformationDisplay.Location = new System.Drawing.Point(12, 27);
            this.pnlInformationDisplay.Name = "pnlInformationDisplay";
            this.pnlInformationDisplay.Size = new System.Drawing.Size(250, 120);
            this.pnlInformationDisplay.TabIndex = 8;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip1.Size = new System.Drawing.Size(274, 24);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lookupVehicleToolStripMenuItem,
            this.lookupPersonnelToolStripMenuItem,
            this.toolStripSeparator1,
            this.editSettingsToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // lookupVehicleToolStripMenuItem
            // 
            this.lookupVehicleToolStripMenuItem.Name = "lookupVehicleToolStripMenuItem";
            this.lookupVehicleToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.lookupVehicleToolStripMenuItem.Text = "Lookup Vehicle";
            this.lookupVehicleToolStripMenuItem.Click += new System.EventHandler(this.lookupVehicleToolStripMenuItem_Click);
            // 
            // lookupPersonnelToolStripMenuItem
            // 
            this.lookupPersonnelToolStripMenuItem.Name = "lookupPersonnelToolStripMenuItem";
            this.lookupPersonnelToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.lookupPersonnelToolStripMenuItem.Text = "Lookup Personnel";
            this.lookupPersonnelToolStripMenuItem.Click += new System.EventHandler(this.lookupPersonnelToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(166, 6);
            // 
            // editSettingsToolStripMenuItem
            // 
            this.editSettingsToolStripMenuItem.Name = "editSettingsToolStripMenuItem";
            this.editSettingsToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.editSettingsToolStripMenuItem.Text = "Edit Settings";
            this.editSettingsToolStripMenuItem.Click += new System.EventHandler(this.editSettingsToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus,
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 208);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(274, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 10;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(259, 17);
            this.lblStatus.Spring = true;
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            this.toolStripProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.toolStripProgressBar1.Visible = false;
            // 
            // EncodeKeyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(274, 230);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.pnlInformationDisplay);
            this.Controls.Add(this.btnEncodeKey);
            this.Controls.Add(this.btnReadKey);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "EncodeKeyForm";
            this.Text = "FuelTRAK Data Key Encoder";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnReadKey;
        private System.Windows.Forms.Button btnEncodeKey;
        private System.Windows.Forms.Panel pnlInformationDisplay;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lookupVehicleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lookupPersonnelToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem editSettingsToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
    }
}

