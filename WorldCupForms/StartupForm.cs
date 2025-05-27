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
    public partial class StartupForm : Form
    {
        public AppSettings SelectedSettings { get; private set; }

        public StartupForm()
        {
            InitializeComponent();

            cbLanguage.Items.AddRange(new string[] { "English", "Croatian" });
            cbLanguage.SelectedIndex = 0;
            rbMen.Checked = true;
            rbApi.Checked = true;

            AcceptButton = btnConfirm;
            CancelButton = btnCancel;

        }

        private void cbLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            string selectedLanguage = cbLanguage.SelectedItem.ToString();
            ChampionshipType selectedChamp = rbMen.Checked ? ChampionshipType.Men : ChampionshipType.Women;
            DataSourceMode selectedMode = rbApi.Checked ? DataSourceMode.Api : DataSourceMode.File;

            SelectedSettings = new AppSettings
            {
                Language = selectedLanguage == "Croatian" ? "hr" : "en",
                Championship = selectedChamp,
                DataSourceMode = selectedMode
            };

            var settingsService = new SettingsService();
            settingsService.Save(SelectedSettings);
            this.DialogResult = DialogResult.OK;
            this.Close();
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
    }
}
