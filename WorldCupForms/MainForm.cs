using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WorldCupData.Enums;
using WorldCupData.Service;
using WorldCupData.Model;
using System.Diagnostics;
using CustomControls;
using WorldCupForms.UIUtils;

namespace WorldCupForms
{
    public partial class MainForm : Form
    {
        private readonly DataProvider _dataProvider;

        public MainForm()
        {
            InitializeComponent();
            
            _dataProvider = new DataProvider();
        }
        private async void MainForm_Load(object sender, EventArgs e)
        {

           var loadingPanel = LoadingPanelUtils.ShowLoadingPanel(this, LanguageService.LoadingTeams());
            var settingsService = new SettingsService();
            settingsService.Load();
            ChangeLanguageStrings();
            
            try
            {
                cbFavoriteTeam.SelectedIndexChanged += cbFavoriteTeam_SelectedIndexChanged;
                await LoadTeamsAsync();
            }
            catch (Exception ex)
            {
                loadingPanel.Visible = false;
                loadingPanel.Dispose();
                DialogResult result = MessageBox.Show(
                LanguageService.LoadServiceError(ex.Message),
                LanguageService.Warning(),
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);

                if (result == DialogResult.OK)
                {
                    AppSettings.DataSourceMode = AppSettings.DataSourceMode == DataSourceMode.Api ? DataSourceMode.File : DataSourceMode.Api;
                    settingsService.Save();
                    try
                    {
                        await LoadTeamsAsync();
                    }
                    catch (Exception ex2)
                    {
                        MessageBox.Show(LanguageService.LoadAltServiceError(ex2.Message), LanguageService.Warning(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                return;
            }
            loadingPanel.Visible = false;
            loadingPanel.Dispose();
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
            var teams = await _dataProvider.GetTeamsAsync(AppSettings.Championship, AppSettings.DataSourceMode);
            cbFavoriteTeam.Items.Clear();
            if (teams != null)
            {
                foreach (var team in teams)
                {
                    cbFavoriteTeam.Items.Add($"{team.Country} ({team.FifaCode})");
                }
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
                    if (favorites.Value.PlayerNames != null && favorites.Value.PlayerNames.Count != 0)
                        await LoadFavoritePlayersAsync(savedCode);
                }
            }
        }

        private async void cbFavoriteTeam_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFavoriteTeam.SelectedItem is not string selectedText) return;
            var loadingPanel = LoadingPanelUtils.ShowLoadingPanel(this, LanguageService.LoadingFavPlayers());
            var fifaCode = selectedText.Substring(selectedText.LastIndexOf('(') + 1).TrimEnd(')');
         //   File.WriteAllText(PathHelper.GetFavoritesFilePath(AppSettings.Championship), fifaCode);
            await LoadFavoritePlayersAsync(fifaCode);
            loadingPanel.Visible = false;
            loadingPanel.Dispose();
        }

        private async void btnChooseFavoritePlayers_Click(object sender, EventArgs e)
        {
            try
            {
                var loadingPanel = LoadingPanelUtils.ShowLoadingPanel(this, LanguageService.LoadingFavPlayers());
                if (cbFavoriteTeam.SelectedItem is not string selectedText) return;

                var fifaCode = selectedText.Substring(selectedText.LastIndexOf('(') + 1).TrimEnd(')');

                var matches = await _dataProvider.GetMatchesByCountryAsync(AppSettings.Championship, AppSettings.DataSourceMode, fifaCode);
                var firstMatch = matches?.FirstOrDefault();

                if (firstMatch == null)
                {
                    MessageBox.Show(LanguageService.NoDataSelectedTeam());
                    return;
                }

                var players = firstMatch.HomeTeam?.Code == fifaCode
                    ? firstMatch.HomeTeamStatistics?.StartingEleven.Concat(firstMatch.HomeTeamStatistics?.Substitutes).ToList()
                    : firstMatch.AwayTeamStatistics?.StartingEleven.Concat(firstMatch.AwayTeamStatistics?.Substitutes).ToList();

                if (players == null || players.Count == 0)
                {
                    MessageBox.Show(LanguageService.NoPlayersError());
                    return;
                }

                var selectorForm = new FavoritePlayerSelectorForm(players, fifaCode);
                loadingPanel.Visible = false;
                loadingPanel.Dispose();
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
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.ErrLoadingFavTeams(ex.Message), LanguageService.Warning(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private async System.Threading.Tasks.Task LoadFavoritePlayersAsync(string fifaCode)
        {
            try
            {
                var loadingPanel = LoadingPanelUtils.ShowLoadingPanel(this, LanguageService.LoadingFavPlayers());
                var favoritePlayerNames = FavoriteService.Load(AppSettings.Championship);

                if (favoritePlayerNames == null || favoritePlayerNames.Value.PlayerNames.Count == 0)
                {
                    loadingPanel.Visible = false;
                    loadingPanel.Dispose();
                    flpFavoritePlayers.Controls.Clear();
                    MessageBox.Show(LanguageService.NoFavoritePlayersSelected(), LanguageService.Warning(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var matches = await _dataProvider.GetMatchesByCountryAsync(AppSettings.Championship, AppSettings.DataSourceMode, fifaCode);
                if (matches == null || !matches.Any())
                {
                    loadingPanel.Visible = false;
                    loadingPanel.Dispose();
                    flpFavoritePlayers.Controls.Clear();
                    return;
                }

                var firstMatch = matches?.FirstOrDefault();
                if (firstMatch == null)
                {
                    loadingPanel.Visible = false;
                    flpFavoritePlayers.Controls.Clear();
                    MessageBox.Show(LanguageService.NoMatchData(), LanguageService.Warning(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                };

                var players = firstMatch.HomeTeam?.Code == fifaCode
                    ? (firstMatch.HomeTeamStatistics?.StartingEleven ?? Enumerable.Empty<StartingEleven>())
                        .Concat(firstMatch.HomeTeamStatistics?.Substitutes ?? Enumerable.Empty<StartingEleven>())
                        .ToList()
                    : (firstMatch.AwayTeamStatistics?.StartingEleven ?? Enumerable.Empty<StartingEleven>())
                        .Concat(firstMatch.AwayTeamStatistics?.Substitutes ?? Enumerable.Empty<StartingEleven>())
                        .ToList();

                if (players == null) return;

                var favoritePlayers = players
                    .Where(p => favoritePlayerNames.Value.PlayerNames.Contains(p.Name))
                    .ToList();

                flpFavoritePlayers.Controls.Clear();

                foreach (var player in favoritePlayers)
                {
                    flpFavoritePlayers.Controls.Add(new PlayerCardControl(player));
                }
                loadingPanel.Dispose();
                loadingPanel.Visible = false; 
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.ErrLoadingFavTeams(ex.Message), LanguageService.Warning(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRanking_Click(object sender, EventArgs e)
        {
            var loadingPanel = LoadingPanelUtils.ShowLoadingPanel(this, LanguageService.LoadingRankings());
            try
            {
                var favoriteData = FavoriteService.Load(AppSettings.Championship);
                if (favoriteData == null || string.IsNullOrEmpty(favoriteData.Value.TeamCode))
                {
                    MessageBox.Show(LanguageService.NoFavoriteCountrySelected(), LanguageService.Warning(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    loadingPanel.Dispose();
                    loadingPanel.Visible = false;
                    return;
                }

                var teamcode = favoriteData.Value.TeamCode;

                var rankingForm = new RankingForm(teamcode);
                loadingPanel.Dispose();
                loadingPanel.Visible = false;
                rankingForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.ErrLoadingFavTeams(ex.Message), LanguageService.Warning(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                loadingPanel.Dispose();
                loadingPanel.Visible = false;
                return;
            }
        }

        private void resetSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show(
   LanguageService.AreYouSureReset(),
    LanguageService.ResetSettings(),
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
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (cbFavoriteTeam.SelectedIndex == -1)
                {
                    MessageBox.Show(LanguageService.NoFavoriteCountrySelected(), LanguageService.Warning(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                } else
                {
                    btnChooseFavoritePlayers.PerformClick();
                }
                return true;
            }
            if(keyData == Keys.R)
            {
                if (cbFavoriteTeam.SelectedIndex == -1)
                {
                    MessageBox.Show(LanguageService.NoFavoriteCountrySelected(), LanguageService.Warning(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    btnRanking.PerformClick();
                }
                return true;
            }

            if (keyData == Keys.Escape)
            {
                Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}