namespace StandAlonePersoApp
{
    partial class CustomerRegistration
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
            this.aquaTheme1 = new Telerik.WinControls.Themes.AquaTheme();
            this.breezeTheme1 = new Telerik.WinControls.Themes.BreezeTheme();
            this.desertTheme1 = new Telerik.WinControls.Themes.DesertTheme();
            this.vistaTheme1 = new Telerik.WinControls.Themes.VistaTheme();
            this.telerikTheme1 = new Telerik.WinControls.Themes.TelerikTheme();
            this.radGroupBox1 = new Telerik.WinControls.UI.RadGroupBox();
            this.btnRegister = new Telerik.WinControls.UI.RadButton();
            this.label3 = new System.Windows.Forms.Label();
            this.txtAccountNumber = new Telerik.WinControls.UI.RadTextBox();
            this.comboCardProfile = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox1)).BeginInit();
            this.radGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnRegister)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAccountNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radGroupBox1
            // 
            this.radGroupBox1.Controls.Add(this.btnRegister);
            this.radGroupBox1.Controls.Add(this.label3);
            this.radGroupBox1.Controls.Add(this.txtAccountNumber);
            this.radGroupBox1.Controls.Add(this.comboCardProfile);
            this.radGroupBox1.Controls.Add(this.label7);
            this.radGroupBox1.FooterImageIndex = -1;
            this.radGroupBox1.FooterImageKey = "";
            this.radGroupBox1.HeaderImageIndex = -1;
            this.radGroupBox1.HeaderImageKey = "";
            this.radGroupBox1.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.radGroupBox1.HeaderText = "Request Card";
            this.radGroupBox1.Location = new System.Drawing.Point(12, 45);
            this.radGroupBox1.Name = "radGroupBox1";
            this.radGroupBox1.Padding = new System.Windows.Forms.Padding(10, 20, 10, 10);
            // 
            // 
            // 
            this.radGroupBox1.RootElement.Padding = new System.Windows.Forms.Padding(10, 20, 10, 10);
            this.radGroupBox1.Size = new System.Drawing.Size(518, 239);
            this.radGroupBox1.TabIndex = 0;
            this.radGroupBox1.Text = "Request Card";
            this.radGroupBox1.ThemeName = "Breeze";
            // 
            // btnRegister
            // 
            this.btnRegister.Location = new System.Drawing.Point(386, 178);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(102, 33);
            this.btnRegister.TabIndex = 27;
            this.btnRegister.Text = "Request Card";
            this.btnRegister.ThemeName = "Desert";
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(31, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 15);
            this.label3.TabIndex = 26;
            this.label3.Text = "Account Number";
            // 
            // txtAccountNumber
            // 
            this.txtAccountNumber.Location = new System.Drawing.Point(138, 48);
            this.txtAccountNumber.Name = "txtAccountNumber";
            this.txtAccountNumber.Size = new System.Drawing.Size(350, 20);
            this.txtAccountNumber.TabIndex = 24;
            this.txtAccountNumber.TabStop = false;
            // 
            // comboCardProfile
            // 
            this.comboCardProfile.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.comboCardProfile.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboCardProfile.FormattingEnabled = true;
            this.comboCardProfile.Items.AddRange(new object[] {
            "Admin User",
            "Regular User"});
            this.comboCardProfile.Location = new System.Drawing.Point(138, 95);
            this.comboCardProfile.Name = "comboCardProfile";
            this.comboCardProfile.Size = new System.Drawing.Size(350, 23);
            this.comboCardProfile.TabIndex = 25;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(31, 103);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 15);
            this.label7.TabIndex = 24;
            this.label7.Text = "Card Profile";
            // 
            // CustomerRegistration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(542, 409);
            this.Controls.Add(this.radGroupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CustomerRegistration";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "CustomerRegistration";
            this.ThemeName = "Aqua";
            this.Load += new System.EventHandler(this.CustomerRegistration_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox1)).EndInit();
            this.radGroupBox1.ResumeLayout(false);
            this.radGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnRegister)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAccountNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.Themes.AquaTheme aquaTheme1;
        private Telerik.WinControls.Themes.BreezeTheme breezeTheme1;
        private Telerik.WinControls.Themes.DesertTheme desertTheme1;
        private Telerik.WinControls.Themes.VistaTheme vistaTheme1;
        private Telerik.WinControls.Themes.TelerikTheme telerikTheme1;
        private Telerik.WinControls.UI.RadGroupBox radGroupBox1;
        private System.Windows.Forms.Label label3;
        private Telerik.WinControls.UI.RadTextBox txtAccountNumber;
        private System.Windows.Forms.ComboBox comboCardProfile;
        private System.Windows.Forms.Label label7;
        private Telerik.WinControls.UI.RadButton btnRegister;
    }
}

