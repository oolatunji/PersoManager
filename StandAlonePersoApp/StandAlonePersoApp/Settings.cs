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
    public partial class Settings : Telerik.WinControls.UI.RadForm
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            txtPersoFilePath.Text = Properties.Settings.Default.PersoFilePath;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.PersoFilePath = txtPersoFilePath.Text;

            MessageBox.Show("Settings saved successfully.", "VISA Instant Issuance", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }     
    }
}
