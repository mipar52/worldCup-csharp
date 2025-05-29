using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WorldCupData.Enums;
using WorldCupData.Service;
using WorldCupData.Model;

namespace WorldCupForms
{
    public partial class MainForm : Form
    {
        private readonly DataProvider _dataProvider;
        private readonly DataSourceMode currentMode = DataSourceMode.File;

        public MainForm()
        {
            InitializeComponent();
            
            _dataProvider = new DataProvider();
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            var settingsService = new SettingsService();
            settingsService.Load();
            ChangeLanguageStrings();
            cbFavoriteTeam.SelectedIndexChanged += cbFavoriteTeam_SelectedIndexChanged;
            await LoadTeamsAsync();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            var result = MessageBox.Show(LanguageService.ExitConfirmation(), LanguageService.Exit(), MessageBoxButtons.YesNo);
            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
            base.OnFormClosing(e);
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var settingsForm = new SettingsForm();
            if (settingsForm.ShowDialog() == DialogResult.OK)
            {
                ChangeLanguageStrings();
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(LanguageService.AppInfo(), LanguageService.About(), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO
        }
        private void ChangeLanguageStrings()
        {
            this.Text = LanguageService.MainFormTitle();
            fileToolStripMenuItem.Text = LanguageService.File();
            settingsToolStripMenuItem.Text = LanguageService.Settings();
            changeApplicationSettingsToolStripMenuItem.Text = LanguageService.ChangeAppSettings();
            resetSettingsToolStripMenuItem.Text = LanguageService.ResetSettings();
            exitToolStripMenuItem1.Text = LanguageService.Exit();
            helpToolStripMenuItem.Text = LanguageService.Help();
            aboutToolStripMenuItem.Text = LanguageService.About();
            lbCurrentTeam.Text = LanguageService.FavoriteTeam();
            lbFavoritePlayers.Text = LanguageService.FavoritePlayers();
            btnChooseFavoritePlayers.Text = LanguageService.ChooseFavoritePlayers();
            btnRanking.Text = LanguageService.GetRankings();

            foreach (Control ctrl in flpFavoritePlayers.Controls)
            {
                if (ctrl is PlayerCardControl card)
                {
                    card.UpdateLanguage();
                }
            }
        }
        private async System.Threading.Tasks.Task LoadTeamsAsync()
        {
            var teams = await _dataProvider.GetTeamsAsync(AppSettings.Championship, currentMode);
            cbFavoriteTeam.Items.Clear();
            foreach (var team in teams)
            {
                cbFavoriteTeam.Items.Add($"{team.Country} ({team.FifaCode})");
            }

            var favorites = FavoriteService.Load(AppSettings.Championship);
            if (favorites != null)
            {
                string savedCode = favorites.Value.TeamCode;

                var matchedItem = cbFavoriteTeam.Items
                    .Cast<string>()
                    .FirstOrDefault(item => item.EndsWith($"({savedCode})"));

                if (matchedItem != null)
                {
                    cbFavoriteTeam.SelectedItem = matchedItem;
                    await LoadFavoritePlayersAsync(savedCode); // This uses FavoriteService.Load() internally
                }
            }
        }

        private async void cbFavoriteTeam_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFavoriteTeam.SelectedItem is not string selectedText) return;

            var fifaCode = selectedText.Substring(selectedText.LastIndexOf('(') + 1).TrimEnd(')');
            File.WriteAllText("favorite_team.txt", fifaCode);
            await LoadFavoritePlayersAsync(fifaCode);
        }

        private async void btnChooseFavoritePlayers_Click(object sender, EventArgs e)
        {
            if (cbFavoriteTeam.SelectedItem is not string selectedText) return;

            var fifaCode = selectedText.Substring(selectedText.LastIndexOf('(') + 1).TrimEnd(')');

            var matches = await _dataProvider.GetMatchesByCountryAsync(AppSettings.Championship, currentMode, fifaCode);
            var firstMatch = matches.FirstOrDefault();

            if (firstMatch == null)
            {
                MessageBox.Show("No match data found for the selected team.");
                return;
            }

            var players = firstMatch.HomeTeam?.Code == fifaCode
                ? firstMatch.HomeTeamStatistics?.StartingEleven.Concat(firstMatch.HomeTeamStatistics?.Substitutes).ToList()
                : firstMatch.AwayTeamStatistics?.StartingEleven.Concat(firstMatch.AwayTeamStatistics?.Substitutes).ToList();

            if (players == null || players.Count == 0)
            {
                MessageBox.Show("No players found for this match.");
                return;
            }

            var selectorForm = new FavoritePlayerSelectorForm(players, fifaCode);
            if (selectorForm.ShowDialog() == DialogResult.OK)
            {
                var selectedFavorites = selectorForm.SelectedFavorites;

                flpFavoritePlayers.Controls.Clear();
                foreach (var player in selectedFavorites)
                {
                    flpFavoritePlayers.Controls.Add(new PlayerCardControl(player));
                }
            }
        }

        private async System.Threading.Tasks.Task LoadFavoritePlayersAsync(string fifaCode)
        {

            var favoritePlayerNames = FavoriteService.Load(AppSettings.Championship);

            if (favoritePlayerNames == null || favoritePlayerNames.Value.PlayerNames.Count == 0)
                return;

            var matches = await _dataProvider.GetMatchesByCountryAsync(AppSettings.Championship, currentMode, fifaCode);
            var firstMatch = matches.FirstOrDefault();

            if (firstMatch == null) return;

            var players = firstMatch.HomeTeam?.Code == fifaCode
                ? firstMatch.HomeTeamStatistics?.StartingEleven.Concat(firstMatch.HomeTeamStatistics?.Substitutes).ToList()
                : firstMatch.AwayTeamStatistics?.StartingEleven.Concat(firstMatch.AwayTeamStatistics?.Substitutes).ToList();

            if (players == null) return;

            var favoritePlayers = players
                .Where(p => favoritePlayerNames.Value.PlayerNames.Contains(p.Name))
                .ToList();

            flpFavoritePlayers.Controls.Clear();

            foreach (var player in favoritePlayers)
            {
                flpFavoritePlayers.Controls.Add(new PlayerCardControl(player));
            }
        }

        private void btnRanking_Click(object sender, EventArgs e)
        {
            var rankingForm = new RankingForm(FavoriteService.Load(AppSettings.Championship).Value.TeamCode);
            rankingForm.ShowDialog();

        }

        private void resetSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show(
    "Are you sure you want to reset application settings?\nThe app will restart and show the startup screen.",
    "Reset Confirmation",
    MessageBoxButtons.YesNo,
    MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                var service = new SettingsService();
                service.Reset();

                // Restart the app to show the startup screen
                Application.Restart();
                Environment.Exit(0);
            }
        }

        private void changeApplicationSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var settingsForm = new SettingsForm();
            if (settingsForm.ShowDialog() == DialogResult.OK)
            {
                ChangeLanguageStrings();
                // Reload settings if needed
            }
        }
    }
}