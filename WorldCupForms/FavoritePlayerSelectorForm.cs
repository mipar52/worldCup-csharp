using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using WorldCupData.Model;
using WorldCupData.Service;
using WorldCupForms.UIUtils;

namespace WorldCupForms
{
    public partial class FavoritePlayerSelectorForm : Form
    {
        private const int MaxFavorites = 3;
        private Dictionary<string, PlayerCardControl> _playerControls = new();

        public List<StartingEleven> SelectedFavorites { get; private set; } = new();
        private List<StartingEleven> _allPlayers;
        private string _teamCode;
        public FavoritePlayerSelectorForm(List<StartingEleven> allPlayers, string code)
        {
            InitializeComponent();
            ChangeLanguageStrings();
            var loadingPanel = LoadingPanelUtils.ShowLoadingPanel(this, LanguageService.LoadingAllPlayers());
            _allPlayers = allPlayers;
            _teamCode = code;
            try
            {
                LoadList();

                flpAllPlayers.AllowDrop = true;
                flpAllPlayers.AutoScroll = true;
                flpAllPlayers.WrapContents = false;
                flpAllPlayers.FlowDirection = FlowDirection.TopDown;

                flpFavoritesOne.AllowDrop = true;
                flpFavoritesTwo.AllowDrop = true;
                flpFavoritesThree.AllowDrop = true;

                flpAllPlayers.DragEnter += DragEnterHandler;
                flpFavoritesOne.DragEnter += DragEnterHandler;
                flpFavoritesTwo.DragEnter += DragEnterHandler;
                flpFavoritesThree.DragEnter += DragEnterHandler;


                flpAllPlayers.DragDrop += DragDropToAllPlayers;
                flpFavoritesOne.DragDrop += DragDropToFavorites;
                flpFavoritesTwo.DragDrop += DragDropToFavorites;
                flpFavoritesThree.DragDrop += DragDropToFavorites;
                loadingPanel.Visible = false;
            }
            catch (Exception ex)
            {
                loadingPanel.Visible = false;
                MessageBox.Show(LanguageService.ErrorLoadingMatches(ex.Message) + "\n" + ex.Message, LanguageService.Warning(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Debug.WriteLine(ex);

            }
        }

        private void DragEnterHandler(object? sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(PlayerCardControl)))
                e.Effect = DragDropEffects.Move;
        }

        private void DragDropToAllPlayers(object? sender, DragEventArgs e)
        {
            if (e.Data.GetData(typeof(PlayerCardControl)) is PlayerCardControl card)
            {
                if (flpFavoritesOne.Controls.Contains(card)) flpFavoritesOne.Controls.Remove(card);
                if (flpFavoritesTwo.Controls.Contains(card)) flpFavoritesTwo.Controls.Remove(card);
                if (flpFavoritesThree.Controls.Contains(card)) flpFavoritesThree.Controls.Remove(card);

                bool exists = flpAllPlayers.Controls
                    .Cast<PlayerCardControl>()
                    .Any(i => (i.Tag as StartingEleven)?.Name == card.Player.Name);

                if (!exists)
                {
                    flpAllPlayers.Controls.Add(card);
                }
            }
        }

        private void DragDropToFavorites(object? sender, DragEventArgs e)
        {
            if (e.Data.GetData(typeof(PlayerCardControl)) is PlayerCardControl card &&
                sender is FlowLayoutPanel targetPanel)
            {
                if (IsCardInAnyFavoritePanel(card))
                    return;

                int totalFavorites =
                    flpFavoritesOne.Controls.Count +
                    flpFavoritesTwo.Controls.Count +
                    flpFavoritesThree.Controls.Count;

                if (totalFavorites >= 3)
                {
                    MessageBox.Show(LanguageService.MaxThree());
                    return;
                }

                if (targetPanel.Controls.Count >= 1)
                {
                    MessageBox.Show(LanguageService.AlreadyTaken(), LanguageService.SlotFull(),
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                RemoveFromAllPanels(card);
                card.Width = targetPanel.ClientSize.Width;
                card.Height = targetPanel.ClientSize.Height; 
                targetPanel.Controls.Add(card);

                var itemToRemove = flpAllPlayers.Controls
                    .Cast<PlayerCardControl>()
                    .FirstOrDefault(i => (i.Tag as StartingEleven)?.Name == card.Player.Name);

                if (itemToRemove != null)
                    flpAllPlayers.Controls.Remove(card);
            }
        }




        private void btnSave_Click(object sender, EventArgs e)
        {
            SelectedFavorites = new List<StartingEleven>();
            foreach (var panel in new[] { flpFavoritesOne, flpFavoritesTwo, flpFavoritesThree })
            {
                foreach (var card in panel.Controls.OfType<PlayerCardControl>())
                {
                    SelectedFavorites.Add(card.Player);
                }
            }

            string currentTeamCode = _teamCode;

            var playerNames = SelectedFavorites.Select(p => p.Name).ToList();
            FavoriteService.Save(AppSettings.Championship, currentTeamCode, playerNames);

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void FavoritePlayerSelectorForm_Load(object sender, EventArgs e)
        {

        }



        private void playerListView_MouseDown(object sender, MouseEventArgs e)
        {
 
        }

        private void LoadList()
        {


            foreach (var player in _allPlayers)
            {
                var card = new PlayerCardControl(player);
                card.BackColor = Color.GreenYellow;
                card.OnPromoteToFavorite += PromoteToFavorite;
                card.OnDemoteToOther += DemoteToOther;

                flpAllPlayers.Controls.Add(card);
                _playerControls[player.Name] = card;
            }


        }

        private void RemoveFromAllPanels(PlayerCardControl card)
        {
            flpFavoritesOne.Controls.Remove(card);
            flpFavoritesTwo.Controls.Remove(card);
            flpFavoritesThree.Controls.Remove(card);
        }

        private void ChangeLanguageStrings()
        {
            this.Text = LanguageService.FavoritePlayersTitle();
            btnSave.Text = LanguageService.SaveSelection();
            btnCancel.Text = LanguageService.Cancel();
        }

        private void PromoteToFavorite(PlayerCardControl card)
        {
            if (IsCardInAnyFavoritePanel(card))
                return;

            RemoveFromAllPanels(card);

            FlowLayoutPanel targetPanel = GetAvailableFavoritePanel();
            if (targetPanel == null)
            {
                MessageBox.Show(LanguageService.MaxThree());
                return;
            }

            var itemToRemove = flpAllPlayers.Controls
                .Cast<PlayerCardControl>()
                .FirstOrDefault(i => (i.Tag as StartingEleven)?.Name == card.Player.Name);

            if (itemToRemove != null)
                flpAllPlayers.Controls.Remove(card);

            targetPanel.Controls.Add(card);
        }


        private void DemoteToOther(PlayerCardControl card)
        {
            RemoveFromAllPanels(card);

            bool exists = flpAllPlayers.Controls
                .Cast<PlayerCardControl>()
                .Any(i => (i.Tag as StartingEleven)?.Name == card.Player.Name);

            if (!exists)
            {
                var item = new ListViewItem(card.Player.Name) { Tag = card.Player };
                flpAllPlayers.Controls.Add(card);
            }
        }


        private bool IsCardInAnyFavoritePanel(PlayerCardControl card)
        {
            return flpFavoritesOne.Controls.Contains(card) ||
                   flpFavoritesTwo.Controls.Contains(card) ||
                   flpFavoritesThree.Controls.Contains(card);
        }

        private FlowLayoutPanel GetAvailableFavoritePanel()
        {
            if (flpFavoritesOne.Controls.Count == 0) return flpFavoritesOne;
            if (flpFavoritesTwo.Controls.Count == 0) return flpFavoritesTwo;
            if (flpFavoritesThree.Controls.Count == 0) return flpFavoritesThree;
            return null;
        }

        private void flpFavoritesOne_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DoDragDrop(this, DragDropEffects.Move);
            }
        }
    }
}