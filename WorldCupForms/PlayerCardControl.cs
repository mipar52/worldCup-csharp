using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorldCupData.Model;
using WorldCupData.Service;

namespace WorldCupForms
{
    public partial class PlayerCardControl : UserControl
    {
        public StartingEleven Player { get; private set; }

        public event Action<PlayerCardControl> OnPromoteToFavorite;
        public event Action<PlayerCardControl> OnDemoteToOther;

        public PlayerCardControl(StartingEleven player)
        {
            InitializeComponent();
            this.ContextMenuStrip = contextMenu;
            Player = player;
            UpdateUI(player);
            SetupDragHandlers(); // 👈 attach drag to all parts
            this.MouseDown += PlayerCardControl_MouseDown;
        }

        private void UpdateUI(StartingEleven Player)
        {
            lbPlayerName.Text = Player.Name;
            lbPlayerNumber.Text = $"#{Player.ShirtNumber}";
            lbPlayerPosition.Text = Player.Position.ToString();
            lbPlayerCaptain.Visible = Player.Captain;
            string placeholderPath = ImageService.GetPlaceholderImagePath();
            pbPlayer.Image = Image.FromFile(placeholderPath);
            //pbPlayer.Image = Image.FromFile(ImageService.LoadPlaceholderImage());
            if (lbPlayerCaptain.Visible)
            {
                lbPlayerCaptain.Text = "⭐ Captain";
            }

            // Optional: Load default image or use country code lookup
            //  pbPlayer.Image = Properties.Resources.default_player;
        }
        private void SetupDragHandlers()
        {
            // Attach to root control
            this.MouseDown += PlayerCardControl_MouseDown;

            // Attach to all children (recursively)
            foreach (Control c in this.Controls)
            {
                c.MouseDown += PlayerCardControl_MouseDown;
            }
        }

        private void PlayerCardControl_Load(object sender, EventArgs e)
        {

        }

        private void contextMenu_Click(object sender, EventArgs e)
        {

        }

        private void promoteToFavoriteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnPromoteToFavorite?.Invoke(this);

        }

        private void demoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnDemoteToOther?.Invoke(this);

        }

        private void PlayerCardControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DoDragDrop(this, DragDropEffects.Move);
            }
        }
    }
}
