using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WorldCupData.Model;
using WorldCupData.Service;

namespace WorldCupWPF.Controls
{
    /// <summary>
    /// Interaction logic for PlayerCardSmallControl.xaml
    /// </summary>
    public partial class PlayerCardSmallControl : UserControl
    {
        public StartingEleven Player { get; private set; }
        public event EventHandler<StartingEleven> PlayerClicked;

        public PlayerCardSmallControl(StartingEleven player)
        {
            InitializeComponent();
            Player = player;
            UpdateUI();

            this.MouseLeftButtonUp += (s, e) =>
            {
                PlayerClicked?.Invoke(this, Player);
            };
        }

        private void UpdateUI()
        {
            // Split the name on space and join with a line break
            if (!string.IsNullOrWhiteSpace(Player.Name))
            {
                txtPlayerName.Text = Player.Name.Replace(" ", Environment.NewLine);
            }

            // Optional: reduce font for multi-line names
            if (Player.Name.Contains(" "))
            {
                txtPlayerName.FontSize = 7; // slightly smaller to accommodate multiple lines
            }
            else
            {
                txtPlayerName.FontSize = 9;
            }
            try
            {
                string? imagePath = ImageService.GetPlayerImagePath(AppSettings.Championship, Player.Name);
                if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
                {
                    imgPlayer.Source = new BitmapImage(new Uri(imagePath));
                }
                else
                {
                    imgPlayer.Source = new BitmapImage(new Uri(ImageService.GetPlaceholderImagePath(AppSettings.Championship)));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading image for {Player.Name}: {ex.Message}");
                imgPlayer.Source = new BitmapImage(new Uri(ImageService.GetPlaceholderImagePath(AppSettings.Championship)));
            }
        }
    }

}
