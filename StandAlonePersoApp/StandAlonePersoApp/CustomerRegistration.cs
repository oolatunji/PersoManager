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
    public partial class CustomerRegistration : Telerik.WinControls.UI.RadForm
    {
        public CustomerRegistration()
        {
            InitializeComponent();
        }

        private void CustomerRegistration_Load(object sender, EventArgs e)
        {
            try
            {
                var clientApi = new ClientAPI.ClientAPI();
                var cardProfiles = clientApi.GetCardProfiles();

                comboCardProfile.DataSource = cardProfiles;
                comboCardProfile.DisplayMember = "Name";
                comboCardProfile.ValueMember = "ID";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "EMV Instant Card Issuance", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorHandler.WriteError(ex);
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtAccountNumber.Text))
                {
                    MessageBox.Show("Kindly enter account number.", "EMV Instant Card Issuance", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    var accountnumber = txtAccountNumber.Text;
                    var userid = Properties.Settings.Default.UserID;
                    var userbranch = Properties.Settings.Default.UserBranch;
                    var cardprofileid = Convert.ToInt64(comboCardProfile.SelectedValue.ToString());

                    var client = new ClientAPI.ClientAPI();
                    
                    var customerModel = new ClientAPI.ClientCustomerModel();
                    customerModel.AccountNumber = accountnumber;
                    customerModel.CustomerBranch = userbranch;
                    customerModel.CardProfileID = cardprofileid;
                    customerModel.Downloaded = true;

                    var persoData = client.RegisterCustomer(customerModel);
                    if (!string.IsNullOrEmpty(persoData))
                    {
                        FileWriter.Write(persoData);
                        MessageBox.Show("Customer registration was successful.", "EMV Instant Card Issuance", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtAccountNumber.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Customer registration failed.", "EMV Instant Card Issuance", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "EMV Instant Card Issuance", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorHandler.WriteError(ex);
            }
        }
    }
}
