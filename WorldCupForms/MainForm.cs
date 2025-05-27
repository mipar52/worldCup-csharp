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
        private readonly AppSettings currentSettings;
        private readonly DataSourceMode currentMode = DataSourceMode.File;

        public MainForm()
        {
            InitializeComponent();
            _dataProvider = new DataProvider();
            currentSettings = new AppSettings();
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            var settingsService = new SettingsService();
            var settings = settingsService.Load();
            if (settings != null)
            {
                currentSettings.Language = settings.Language;
                currentSettings.Championship = settings.Championship;
            }

            cbFavoriteTeam.SelectedIndexChanged += cbFavoriteTeam_SelectedIndexChanged;
            await LoadTeamsAsync();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo);
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
                // Reload settings if needed
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("World Cup App\nVersion 1.0", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO
        }

        private async System.Threading.Tasks.Task LoadTeamsAsync()
        {
            var teams = await _dataProvider.GetTeamsAsync(currentSettings.Championship, currentMode);
            cbFavoriteTeam.Items.Clear();
            foreach (var team in teams)
            {
                cbFavoriteTeam.Items.Add($"{team.Country} ({team.FifaCode})");
            }

            var favorites = FavoriteService.Load();
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

            var matches = await _dataProvider.GetMatchesByCountryAsync(currentSettings.Championship, currentMode, fifaCode);
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
            
            var favoritePlayerNames = FavoriteService.Load();

            if (favoritePlayerNames == null || favoritePlayerNames.Value.PlayerNames.Count == 0)
                return;

            var matches = await _dataProvider.GetMatchesByCountryAsync(currentSettings.Championship, currentMode, fifaCode);
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
    }
}