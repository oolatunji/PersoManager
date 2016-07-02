using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;

namespace StandAlonePersoApp
{
    public partial class Landing : Telerik.WinControls.UI.RadForm
    {
        public Landing()
        {
            InitializeComponent();
        }

        public void ShowForm(Form sender)
        {
            sender.ControlBox = false;
            sender.FormBorderStyle = FormBorderStyle.None;
            sender.ShowInTaskbar = false;
            sender.TopLevel = false;
            sender.Visible = true;

            this.splitContainer1.Panel1.Controls.Clear(); //clear panel first            
            this.splitContainer1.Panel1.Controls.Add(sender);
        }

        public void ResetHome()
        {
            this.splitContainer1.Panel1.Controls.Clear();
            this.splitContainer1.Panel1.Controls.Add(panelDashboard);
        }

        private void menuRegisterCustomer_Click(object sender, EventArgs e)
        {
            var registerCustomer = new CustomerRegistration();
            ShowForm(registerCustomer);
        }

        private void Landing_Load(object sender, EventArgs e)
        {
            try
            {
                long userID = Properties.Settings.Default.UserID;

                var clientApi = new ClientAPI.ClientAPI();
                var userModel = clientApi.GetUser();

                if (userModel != null && userModel.ID != 0)
                {
                    Properties.Settings.Default.UserID = userModel.ID;
                    Properties.Settings.Default.UserBranch = userModel.Branch.ID;
                    lbluser.Text = userModel.Username;
                }

                var registerCustomer = new CustomerRegistration();
                ShowForm(registerCustomer);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "VISA Instant Issuance", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorHandler.WriteError(ex);
            }
        }

        private void menuSettings_Click(object sender, EventArgs e)
        {
            var settings = new Settings();
            ShowForm(settings);
        }

        private void menuExit_Click(object sender, EventArgs e)
        {
            Application.Exit();  
        }
    }
}
