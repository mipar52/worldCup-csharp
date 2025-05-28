using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
            pbPlayer.Click += (s, e) => ChooseImage();
            this.ContextMenuStrip = contextMenu;
            Player = player;
            UpdateUI(player);
            SetupDragHandlers();
            this.MouseDown += PlayerCardControl_MouseDown;
        }

        private void UpdateUI(StartingEleven Player)
        {
            lbPlayerName.Text = Player.Name;
            lbPlayerNumber.Text = $"#{Player.ShirtNumber}";
            lbPlayerPosition.Text = Player.Position.ToString();
            lbPlayerCaptain.Visible = Player.Captain;
            pbPlayer.SizeMode = PictureBoxSizeMode.Zoom;

            string? imagePath = ImageService.GetPlayerImagePath(AppSettings.Championship, Player.Name);
            if (!string.IsNullOrWhiteSpace(imagePath) && File.Exists(imagePath))
            {
                pbPlayer.Image = Image.FromFile(imagePath);
            }
            else
            {
                pbPlayer.Image = Image.FromFile(ImageService.GetPlaceholderImagePath(AppSettings.Championship));
            }
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
                if (!(c is PictureBox))
                c.MouseDown += PlayerCardControl_MouseDown;
            }
        }

        private void ChooseImage()
        {
            Debug.WriteLine("[FORMS DEBUG] Image pressed");
            using OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "Image Files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg"
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                ImageService.SavePlayerImage(AppSettings.Championship,Player.Name, dialog.FileName);
                pbPlayer.Image = Image.FromFile(dialog.FileName);
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
                if (pbPlayer.Bounds.Contains(e.Location))
                    return; // Don't start drag if clicking the image
                DoDragDrop(this, DragDropEffects.Move);
            }
        }
    }
}
