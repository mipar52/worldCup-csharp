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
    public partial class StartupForm : Form
    {
        public StartupForm()
        {
            InitializeComponent();
            SetLanguageStrings();
            cbLanguage.Items.AddRange(new string[] { "English", "Croatian" });
          //  cbLanguage.SelectedIndexChanged += cbLanguage_SelectedIndexChanged;
            cbLanguage.SelectedIndex = 0;
            rbMen.Checked = true;
            rbApi.Checked = true;

            AcceptButton = btnConfirm;
            CancelButton = btnCancel;

        }

        private void SetLanguageStrings()
        {
            this.Text = LanguageService.StartupTitle();
            welcomeLabel.Text = LanguageService.SetWelcomeMessage();
            lblSelectLang.Text = LanguageService.SetApplicationLangugeString();
            grpChampionship.Text = LanguageService.SetWorldChampionShipPicker();
            rbMen.Text = LanguageService.SetMenWorldChampion();
            rbWomen.Text = LanguageService.SetWorldChampionShipPicker();
            rbApi.Text = LanguageService.ViaApi();
            rbLocal.Text = LanguageService.Locally();
            btnConfirm.Text = LanguageService.Confirm();
            btnCancel.Text = LanguageService.Cancel();
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
                SetLanguageStrings();
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (cbLanguage.SelectedItem != null) // Ensure SelectedItem is not null
            {
                string selectedLanguage = cbLanguage.SelectedItem.ToString()!;
                ChampionshipType selectedChamp = rbMen.Checked ? ChampionshipType.Men : ChampionshipType.Women;
                DataSourceMode selectedMode = rbApi.Checked ? DataSourceMode.Api : DataSourceMode.File;

                AppSettings.Language = selectedLanguage == "Croatian" ? "hr" : "en";
                AppSettings.Championship = selectedChamp;
                AppSettings.DataSourceMode = selectedMode;

                var settingsService = new SettingsService();
                settingsService.Save();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Please select a language before confirming.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void StartupForm_Load(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
