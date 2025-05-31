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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WorldCupData.Model;
using WorldCupData.Service;

namespace WorldCupWPF.Views
{
    /// <summary>
    /// Interaction logic for PlayerInfoWindow.xaml
    /// </summary>
    public partial class PlayerInfoWindow : Window
    {
        public PlayerInfoWindow(StartingEleven player, int goals = 0, int yellowCards = 0)
        {
            InitializeComponent();
            DataContext = player;

            UpdateUI(player, goals, yellowCards);

            this.Loaded += PlayerInfoWindow_Loaded;
        }

        private void PlayerInfoWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var storyboard = new Storyboard();

            var fade = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = new Duration(TimeSpan.FromSeconds(0.3))
            };
            Storyboard.SetTarget(fade, this);
            Storyboard.SetTargetProperty(fade, new PropertyPath(Window.OpacityProperty));

            var scaleXAnim = new DoubleAnimation
            {
                From = 0.8,
                To = 1.0,
                Duration = new Duration(TimeSpan.FromSeconds(0.3))
            };
            Storyboard.SetTarget(scaleXAnim, RootGrid);
            Storyboard.SetTargetProperty(scaleXAnim, new PropertyPath("RenderTransform.ScaleX"));

            var scaleYAnim = new DoubleAnimation
            {
                From = 0.8,
                To = 1.0,
                Duration = new Duration(TimeSpan.FromSeconds(0.3))
            };
            Storyboard.SetTarget(scaleYAnim, RootGrid);
            Storyboard.SetTargetProperty(scaleYAnim, new PropertyPath("RenderTransform.ScaleY"));

            storyboard.Children.Add(fade);
            storyboard.Children.Add(scaleXAnim);
            storyboard.Children.Add(scaleYAnim);

            storyboard.Begin();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void UpdateUI(StartingEleven player, int goals, int yellowCards)
        {
            Goals.Text = $"Goals: {goals}";
            Yellows.Text = $"Yellow Cards: {yellowCards}";
            PlayerName.Text = player.Name;
            PlayerPosition.Text = player.Position.ToString();
            PlayerNumber.Text = $"#{player.ShirtNumber}";
            IsCaptain.Text = player.Captain ? "Captain" : "";
            string imagePath = ImageService.GetPlayerImagePath(AppSettings.Championship, player.Name);
            if (File.Exists(imagePath))
                PlayerImage.Source = new BitmapImage(new Uri(imagePath));
            else
                PlayerImage.Source = new BitmapImage(new Uri(ImageService.GetPlaceholderImagePath(AppSettings.Championship)));
        }
    }
}

