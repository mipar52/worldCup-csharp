using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
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
using WorldCupData.Enums;
using WorldCupData.Model;
using WorldCupData.Service;
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
            string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files", "worldcup.sfg.io", "men", "Images", "field.jpg");
            if (File.Exists(imagePath))
            {
                var bitmap = new BitmapImage(new Uri(imagePath, UriKind.Absolute));
                FieldCanvas.Background = new ImageBrush(bitmap) { Stretch = Stretch.Fill };
            }

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

        public List<WorldCupData.Model.TeamEvent> HomeTeamEvents
        {
            get => (List<WorldCupData.Model.TeamEvent>)GetValue(HomeTeamEventsProperty);
            set => SetValue(HomeTeamEventsProperty, value);
        }

        public static readonly DependencyProperty HomeTeamEventsProperty =
            DependencyProperty.Register(nameof(HomeTeamEvents), typeof(List<WorldCupData.Model.TeamEvent>), typeof(FieldLayoutControl));

        public List<WorldCupData.Model.TeamEvent> AwayTeamEvents
        {
            get => (List<WorldCupData.Model.TeamEvent>)GetValue(AwayTeamEventsProperty);
            set => SetValue(AwayTeamEventsProperty, value);
        }

        public static readonly DependencyProperty AwayTeamEventsProperty =
            DependencyProperty.Register(nameof(AwayTeamEvents), typeof(List<WorldCupData.Model.TeamEvent>), typeof(FieldLayoutControl));


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
                    int goals = 0;
                    int yellowCards = 0;
                    int goalsOwn = 0;
                    var color = new SolidColorBrush(Colors.White);
                    if (isHomeTeam && HomeTeamEvents != null)
                    {
                        goals = HomeTeamEvents.Count(ev => ev.Player == player.Name && ev.TypeOfEvent == TypeOfEvent.Goal);
                        yellowCards = HomeTeamEvents.Count(ev => ev.Player == player.Name && ev.TypeOfEvent == TypeOfEvent.YellowCard);
                        goalsOwn = HomeTeamEvents.Count(ev => ev.Player == player.Name && ev.TypeOfEvent == TypeOfEvent.GoalOwn);
                        if (goals > 0)
                        {
                            color = new SolidColorBrush(Colors.LightGreen);
                        }
                        else if (yellowCards > 0)
                        {
                            color = new SolidColorBrush(Colors.Yellow);
                        }
                        else if (goalsOwn > 0)
                        {
                            color = new SolidColorBrush(Colors.IndianRed);
                        }
                        
                        if (!isHomeTeam && AwayTeamEvents != null)
                        {
                            goals = AwayTeamEvents.Count(ev => ev.Player == player.Name && (ev.TypeOfEvent == TypeOfEvent.Goal || ev.TypeOfEvent == TypeOfEvent.GoalPenalty));
                            yellowCards = AwayTeamEvents.Count(ev => ev.Player == player.Name && ev.TypeOfEvent == TypeOfEvent.YellowCard);
                            goalsOwn = HomeTeamEvents.Count(ev => ev.Player == player.Name && ev.TypeOfEvent == TypeOfEvent.GoalOwn);
                            if (goals > 0)
                            {
                                color = new SolidColorBrush(Colors.LightGreen);
                            }
                            else if (yellowCards > 0)
                            {
                                color = new SolidColorBrush(Colors.Yellow);
                            }
                            else if (goalsOwn > 0)
                            {
                                color = new SolidColorBrush(Colors.IndianRed);
                            }
                        }
                    }
                    card.PlayerClicked += (s, p) =>
                    {


                        var window = new PlayerInfoWindow(p, goals, yellowCards);
                        window.ShowDialog();
                    };

                    double y = startY + i * spacing;
                    double adjustedX = x;

                    // If 5 or more players, stagger players beyond the 4th
                    if (count > 4 && i >= 4)
                    {
                        y -= _random.Next(180, 200) * (i - 3); // move upward
                        adjustedX += isHomeTeam ? _random.Next(100, 150) * (i - 3) : _random.Next(-150, -100) * (i - 3); // move sideways
                    }

                    Canvas.SetLeft(card, adjustedX);
                    Canvas.SetTop(card, y);
                    card.ToolTip = $"{LanguageService.Goals}: {goals}, {LanguageService.YellowCards}: {yellowCards}";
                    card.Background = color;
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

