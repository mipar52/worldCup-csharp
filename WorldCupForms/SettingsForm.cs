using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorldCupData.Enums;
using WorldCupData.Service;

namespace WorldCupForms
{
    public partial class SettingsForm : Form
    {
        private readonly SettingsService _settingsService;

        public SettingsForm()
        {
            InitializeComponent();
            _settingsService = new SettingsService();

            cbLanguage.Items.AddRange(new[] { "English", "Croatian" });

            this.AcceptButton = btnConfirm;
            this.CancelButton = btnCancel;
        }






        private void btnConfirm_Click_1(object sender, EventArgs e)
        {
            var selectedLanguage = cbLanguage.SelectedItem?.ToString() ?? "English";
            var selectedChamp = rbMen.Checked ? ChampionshipType.Men : ChampionshipType.Women;
            var selectedDataSource = rbApi.Checked ? DataSourceMode.Api : DataSourceMode.File;

            AppSettings.Language = selectedLanguage == "Croatian" ? "hr" : "en";
            AppSettings.Championship = selectedChamp;
            AppSettings.DataSourceMode = selectedDataSource;

            _settingsService.Save();

            MessageBox.Show("Settings saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                btnConfirm.PerformClick();
                return true;
            }
            if (keyData == Keys.Escape)
            {
                btnCancel.PerformClick();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            _settingsService.Load();

            cbLanguage.SelectedItem = AppSettings.Language == "hr" ? "Croatian" : "English";
            if (AppSettings.Championship == ChampionshipType.Men)
                rbMen.Checked = true;
            else
                rbWomen.Checked = true;
            
            if (AppSettings.DataSourceMode == DataSourceMode.Api)
                rbApi.Checked = true;
            else
                rbLocal.Checked = true;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
