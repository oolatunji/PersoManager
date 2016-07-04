namespace StandAlonePersoApp
{
    partial class Landing
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Landing));
            this.radMenu1 = new Telerik.WinControls.UI.RadMenu();
            this.lbluser = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.menuRegisterCustomer = new Telerik.WinControls.UI.RadMenuItem();
            this.radMenuSeparatorItem1 = new Telerik.WinControls.UI.RadMenuSeparatorItem();
            this.menuSettings = new Telerik.WinControls.UI.RadMenuItem();
            this.radMenuSeparatorItem2 = new Telerik.WinControls.UI.RadMenuSeparatorItem();
            this.radMenuSeparatorItem3 = new Telerik.WinControls.UI.RadMenuSeparatorItem();
            this.radMenuSeparatorItem4 = new Telerik.WinControls.UI.RadMenuSeparatorItem();
            this.menuExit = new Telerik.WinControls.UI.RadMenuItem();
            this.panelDashboard = new Telerik.WinControls.UI.RadPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.radMenu1)).BeginInit();
            this.radMenu1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelDashboard)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radMenu1
            // 
            this.radMenu1.Controls.Add(this.lbluser);
            this.radMenu1.Controls.Add(this.lblUsername);
            this.radMenu1.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.menuRegisterCustomer,
            this.radMenuSeparatorItem1,
            this.menuSettings,
            this.radMenuSeparatorItem2,
            this.radMenuSeparatorItem3,
            this.radMenuSeparatorItem4,
            this.menuExit});
            this.radMenu1.Location = new System.Drawing.Point(0, 0);
            this.radMenu1.Name = "radMenu1";
            this.radMenu1.Size = new System.Drawing.Size(546, 22);
            this.radMenu1.TabIndex = 2;
            this.radMenu1.Text = "radMenu1";
            this.radMenu1.ThemeName = "Telerik";
            // 
            // lbluser
            // 
            this.lbluser.AutoSize = true;
            this.lbluser.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbluser.ForeColor = System.Drawing.Color.Blue;
            this.lbluser.Location = new System.Drawing.Point(416, 0);
            this.lbluser.Name = "lbluser";
            this.lbluser.Size = new System.Drawing.Size(111, 13);
            this.lbluser.TabIndex = 0;
            this.lbluser.Text = "pmbano@pajuno.com";
            this.lbluser.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.BackColor = System.Drawing.Color.Transparent;
            this.lblUsername.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsername.ForeColor = System.Drawing.Color.Purple;
            this.lblUsername.Location = new System.Drawing.Point(888, 5);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(0, 17);
            this.lblUsername.TabIndex = 0;
            this.lblUsername.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // menuRegisterCustomer
            // 
            this.menuRegisterCustomer.Class = "";
            this.menuRegisterCustomer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(1)))), ((int)(((byte)(1)))));
            this.menuRegisterCustomer.Name = "menuRegisterCustomer";
            this.menuRegisterCustomer.ShowArrow = false;
            this.menuRegisterCustomer.Text = "Request Card";
            this.menuRegisterCustomer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.menuRegisterCustomer.TextSeparatorVisibility = Telerik.WinControls.ElementVisibility.Visible;
            this.menuRegisterCustomer.Click += new System.EventHandler(this.menuRegisterCustomer_Click);
            // 
            // radMenuSeparatorItem1
            // 
            this.radMenuSeparatorItem1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(2)))), ((int)(((byte)(2)))));
            this.radMenuSeparatorItem1.Class = "";
            this.radMenuSeparatorItem1.Name = "radMenuSeparatorItem1";
            this.radMenuSeparatorItem1.Text = "radMenuSeparatorItem1";
            // 
            // menuSettings
            // 
            this.menuSettings.Class = "";
            this.menuSettings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(2)))), ((int)(((byte)(2)))));
            this.menuSettings.Name = "menuSettings";
            this.menuSettings.ShowArrow = false;
            this.menuSettings.Text = "Settings";
            this.menuSettings.Click += new System.EventHandler(this.menuSettings_Click);
            // 
            // radMenuSeparatorItem2
            // 
            this.radMenuSeparatorItem2.Class = "";
            this.radMenuSeparatorItem2.Name = "radMenuSeparatorItem2";
            this.radMenuSeparatorItem2.Text = "radMenuSeparatorItem2";
            // 
            // radMenuSeparatorItem3
            // 
            this.radMenuSeparatorItem3.Class = "";
            this.radMenuSeparatorItem3.Name = "radMenuSeparatorItem3";
            this.radMenuSeparatorItem3.Text = "radMenuSeparatorItem3";
            // 
            // radMenuSeparatorItem4
            // 
            this.radMenuSeparatorItem4.Class = "";
            this.radMenuSeparatorItem4.Name = "radMenuSeparatorItem4";
            this.radMenuSeparatorItem4.Text = "radMenuSeparatorItem4";
            // 
            // menuExit
            // 
            this.menuExit.Class = "";
            this.menuExit.Name = "menuExit";
            this.menuExit.ShowArrow = false;
            this.menuExit.Text = "Exit";
            this.menuExit.Click += new System.EventHandler(this.menuExit_Click);
            // 
            // panelDashboard
            // 
            this.panelDashboard.Location = new System.Drawing.Point(3, 33);
            this.panelDashboard.Name = "panelDashboard";
            this.panelDashboard.Size = new System.Drawing.Size(540, 391);
            this.panelDashboard.TabIndex = 3;
            this.panelDashboard.ThemeName = "Breeze";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panelDashboard);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.White;
            this.splitContainer1.Panel2Collapsed = true;
            this.splitContainer1.Size = new System.Drawing.Size(546, 427);
            this.splitContainer1.SplitterDistance = 521;
            this.splitContainer1.TabIndex = 4;
            // 
            // Landing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(546, 427);
            this.Controls.Add(this.radMenu1);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Landing";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EMV Instant Card Issuance";
            this.ThemeName = "Aqua";
            this.Load += new System.EventHandler(this.Landing_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radMenu1)).EndInit();
            this.radMenu1.ResumeLayout(false);
            this.radMenu1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelDashboard)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadMenu radMenu1;
        private System.Windows.Forms.Label lblUsername;
        private Telerik.WinControls.UI.RadMenuItem menuRegisterCustomer;
        private Telerik.WinControls.UI.RadMenuSeparatorItem radMenuSeparatorItem1;
        private Telerik.WinControls.UI.RadMenuItem menuSettings;
        private Telerik.WinControls.UI.RadMenuSeparatorItem radMenuSeparatorItem2;
        private Telerik.WinControls.UI.RadMenuSeparatorItem radMenuSeparatorItem3;
        private Telerik.WinControls.UI.RadMenuSeparatorItem radMenuSeparatorItem4;
        private Telerik.WinControls.UI.RadMenuItem menuExit;
        private Telerik.WinControls.UI.RadPanel panelDashboard;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label lbluser;
    }
}

