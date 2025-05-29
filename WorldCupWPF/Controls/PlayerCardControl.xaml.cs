using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    /// Interaction logic for PlayerCardControl.xaml
    /// </summary>
    public partial class PlayerCardControl : UserControl
    {
        public StartingEleven Player { get; private set; }

        public PlayerCardControl(StartingEleven player)
        {
            InitializeComponent();
            Player = player;
            UpdateUI();
        }

        private void UpdateUI()
        {
            txtPlayerName.Text = Player.Name;
           // txtPLayerShirtNumber.Text = $"#{Player.ShirtNumber}";
            txtPlayerPosition.Text = Player.Position.ToString();
           // captainText.Visibility = Player.Captain ? Visibility.Visible : Visibility.Collapsed;

            string imagePath = ImageService.GetPlayerImagePath(AppSettings.Championship, Player.Name);
            if (File.Exists(imagePath))
                imgPlayer.Source = new BitmapImage(new Uri(imagePath));
            else
                imgPlayer.Source = new BitmapImage(new Uri(ImageService.GetPlaceholderImagePath(AppSettings.Championship)));
        }
    }
}
