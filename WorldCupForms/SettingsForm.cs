using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
            ChangeLanguageStrings();
            _settingsService = new SettingsService();

            cbLanguage.Items.AddRange(new[] { "English", "Croatian" });

            this.AcceptButton = btnConfirm;
            this.CancelButton = btnCancel;
        }



        private void ChangeLanguageStrings()
        {
            this.Text = LanguageService.SettingsTitle();
            lbChange.Text = LanguageService.WantAChange();
            lbLanguage.Text = LanguageService.SetApplicationLangugeString();
            grpChampionship.Text = LanguageService.SetWorldChampionShipPicker();
            rbMen.Text = LanguageService.SetMenWorldChampion();
            rbWomen.Text = LanguageService.SetWomenWorldChampion();
            rbApi.Text = LanguageService.ViaApi();
            rbLocal.Text = LanguageService.Locally();
            btnConfirm.Text = LanguageService.Confirm();
            btnCancel.Text = LanguageService.Cancel();
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
            
            MessageBox.Show(LanguageService.SaveSuccess(), LanguageService.Success(), MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void cbLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbLanguage.SelectedItem != null)
            {
                Debug.WriteLine($"Selected language: {cbLanguage.SelectedItem.ToString()}");
                string selectedLanguage = cbLanguage.SelectedItem.ToString()!;
                AppSettings.Language = selectedLanguage == "Croatian" ? "hr" : "en";
                LanguageService.SetLanguage(AppSettings.Language);
                // Reload language strings
                ChangeLanguageStrings();
            }
        }
    }
}
