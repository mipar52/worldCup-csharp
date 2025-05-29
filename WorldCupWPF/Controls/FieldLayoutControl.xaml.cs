using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using WorldCupWPF.Views;

namespace WorldCupWPF.Controls
{
    /// <summary>
    /// Interaction logic for FieldLayoutControl.xaml
    /// </summary>
    public partial class FieldLayoutControl : UserControl
    {
        private static readonly Random _random = new Random();

        public FieldLayoutControl()
        {
            InitializeComponent();
            this.Loaded += FieldLayoutControl_Loaded;
            this.SizeChanged += (_, __) => RenderPlayers();
        }

        public IEnumerable<StartingEleven> HomeTeamPlayers
        {
            get => (IEnumerable<StartingEleven>)GetValue(HomeTeamPlayersProperty);
            set => SetValue(HomeTeamPlayersProperty, value);
        }

        public static readonly DependencyProperty HomeTeamPlayersProperty =
            DependencyProperty.Register(nameof(HomeTeamPlayers), typeof(IEnumerable<StartingEleven>), typeof(FieldLayoutControl), new PropertyMetadata(null, OnPlayersChanged));

        public IEnumerable<StartingEleven> AwayTeamPlayers
        {
            get => (IEnumerable<StartingEleven>)GetValue(AwayTeamPlayersProperty);
            set => SetValue(AwayTeamPlayersProperty, value);
        }

        public static readonly DependencyProperty AwayTeamPlayersProperty =
            DependencyProperty.Register(nameof(AwayTeamPlayers), typeof(IEnumerable<StartingEleven>), typeof(FieldLayoutControl), new PropertyMetadata(null, OnPlayersChanged));

        private static void OnPlayersChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FieldLayoutControl control)
            {
                control.RenderPlayers();
            }
        }

        private void FieldLayoutControl_Loaded(object sender, RoutedEventArgs e)
        {
            RenderPlayers();
        }

        private void RenderPlayers()
        {
            FieldCanvas.Children.Clear();

            RenderTeam(HomeTeamPlayers, isHomeTeam: true);
            RenderTeam(AwayTeamPlayers, isHomeTeam: false);
        }

        private void RenderTeam(IEnumerable<StartingEleven> players, bool isHomeTeam)
        {
            if (players == null) return;

            var grouped = players.GroupBy(p => p.Position).ToDictionary(g => g.Key, g => g.ToList());

            // Define X-lanes for each position
            double fieldWidth = FieldCanvas.ActualWidth;
            double fieldHeight = FieldCanvas.ActualHeight;

            double goalieX = isHomeTeam ? 60 : fieldWidth - 130;
            double defenderX = isHomeTeam ? 190 : fieldWidth - 290;
            double midfieldX = isHomeTeam ? fieldWidth / 2 - 100 : fieldWidth / 2 + 100;
            double forwardX = isHomeTeam ? fieldWidth - 400 : 300;

            Dictionary<WorldCupData.Enums.Position, double> xByPosition = new()
            {
                [WorldCupData.Enums.Position.Goalie] = goalieX,
                [WorldCupData.Enums.Position.Defender] = defenderX,
                [WorldCupData.Enums.Position.Midfield] = midfieldX,
                [WorldCupData.Enums.Position.Forward] = forwardX
            };

            foreach (var kvp in grouped)
            {
                var position = kvp.Key;
                var linePlayers = kvp.Value;

                double x = xByPosition[position];

                // Dynamic vertical layout
                double cardHeight = 110;
                double padding = 20;
                double availableHeight = fieldHeight - 2 * padding;
                int count = linePlayers.Count;
                double spacing = count > 1
                    ? Math.Min(140, availableHeight / (count - 1))
                    : 0;

                double totalHeight = spacing * (count - 1);
                double startY = (fieldHeight - totalHeight - cardHeight) / 2;

                for (int i = 0; i < count; i++)
                {
                    var player = linePlayers[i];
                    var card = new PlayerCardControl(player);
                    card.PlayerClicked += (s, p) =>
                    {
                      //  int goals = match.HomeTeamEvents.Count(ev => ev.Player == p.Name && ev.Type == "goal");
                      //  int yellowCards = match.HomeTeamEvents.Count(ev => ev.Player == p.Name && ev.Type == "yellow-card");
                        var window = new PlayerInfoWindow(p); // Optionally pass stats if available
                        window.ShowDialog();
                    };

                    double y = startY + i * spacing;
                    double adjustedX = x;

                    // 👇 If 5 or more players, stagger players beyond the 4th
                    if (count > 4 && i >= 4)
                    {
                        y -= _random.Next(180, 200) * (i - 3); // move upward
                        adjustedX += isHomeTeam ? _random.Next(100, 150) * (i - 3) : _random.Next(-150, -100) * (i - 3); // move sideways
                    }

                    Canvas.SetLeft(card, adjustedX);
                    Canvas.SetTop(card, y);

                    FieldCanvas.Children.Add(card);
                }
            }
            }





        private (double x, double y) GetPositionForPlayer(StartingEleven player, int index, bool isHomeTeam)
        {
            double fieldWidth = FieldCanvas.ActualWidth;
            double spacingX = 80;
            double spacingY = 70;

            // Horizontal spacing
            double baseX = isHomeTeam ? 150 : fieldWidth - 850;
            double x = baseX + index * spacingX;

            // Vertical based on position (bottom-up)
            double y = player.Position switch
            {
                WorldCupData.Enums.Position.Goalie => 260,
                WorldCupData.Enums.Position.Defender => 180,
                WorldCupData.Enums.Position.Midfield => 100,
                WorldCupData.Enums.Position.Forward => 30,
                _ => 180
            };

            return (x, y);
        }
    }
    }

