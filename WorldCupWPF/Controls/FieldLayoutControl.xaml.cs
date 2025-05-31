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

            double marginX = fieldWidth * 0.05;
            double marginY = fieldHeight * 0.05;
            double usableWidth = fieldWidth - 2 * marginX;
            double usableHeight = fieldHeight - 2 * marginY;

            // Corrected spacing based on your new rules
            double goalieX = isHomeTeam ? marginX + usableWidth * 0.10 : marginX + usableWidth * 0.90;
            double defenderX = isHomeTeam ? marginX + usableWidth * 0.20 : marginX + usableWidth * 0.80;
            double forwardX = isHomeTeam ? marginX + usableWidth * 0.32 : marginX + usableWidth * 0.68;
            double midfieldX = isHomeTeam ? marginX + usableWidth * 0.45 : marginX + usableWidth * 0.55;




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
                int count = linePlayers.Count;

                // Define dynamic card size
                double cardWidth = Math.Clamp(fieldWidth * 0.08, 60, 140);
                double cardHeight = Math.Clamp(fieldHeight * 0.12, 90, 180);

                // Set margins
                 marginY = fieldHeight * 0.05;
                usableHeight = fieldHeight - 2 * marginY;

                // Calculate spacing
                double spacing = count > 1
                    ? Math.Max(cardHeight * 1.1, usableHeight / (count - 0.5))
                    : 0;

                double totalHeight = spacing * (count - 1);
                double startY = marginY + (usableHeight - totalHeight - cardHeight) / 2;


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
                       
                    }
                    if (!isHomeTeam && AwayTeamEvents != null)
                    {
                        goals = AwayTeamEvents.Count(ev => ev.Player == player.Name && (ev.TypeOfEvent == TypeOfEvent.Goal || ev.TypeOfEvent == TypeOfEvent.GoalPenalty));
                        yellowCards = AwayTeamEvents.Count(ev => ev.Player == player.Name && ev.TypeOfEvent == TypeOfEvent.YellowCard);
                        goalsOwn = AwayTeamEvents.Count(ev => ev.Player == player.Name && ev.TypeOfEvent == TypeOfEvent.GoalOwn);
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
                    card.PlayerClicked += (s, p) =>
                    {


                        var window = new PlayerInfoWindow(p, goals, yellowCards);
                        window.ShowDialog();
                    };


                    double baseX = xByPosition[position];
                    double centerY = fieldHeight / 2;
                     spacing = 150; // base vertical spacing
                    double y = centerY;

                    if (count <= 3)
                    {
                        y = centerY - spacing * (count - 1) / 2 + spacing * i;
                    }
                    else
                    {
                        // Arc layout
                        double angleStep = Math.PI / (count + 1);
                        double angle = angleStep * (i + 1);
                        double radius = 240; // Increased radius for better spread

                        // Larger spacing factor
                        double verticalStretch = 1.1; // exaggerate the curve
                        double horizontalStretch = 0.9; // optional: exaggerate width too

                        y = centerY + Math.Cos(angle) * radius * verticalStretch - 50;

                        // Shift X slightly to avoid collisions
                        baseX += (isHomeTeam ? 1 : -1) * Math.Sin(angle) * 100 * horizontalStretch;
                    }

                    // Calculate card size based on field size
                 //   double cardWidth = Math.Clamp(fieldWidth * 0.08, 60, 140);   // 8% width, clamped
                 //   cardHeight = Math.Clamp(fieldHeight * 0.15, 90, 200); // 15% height, clamped

                    double Clamp(double val, double min, double max) => Math.Max(min, Math.Min(max, val));

                    // Ensure card is fully inside field
                    baseX = Clamp(baseX, 0, fieldWidth - cardWidth);
                    y = Clamp(y, 0, fieldHeight - cardHeight);

              //      card.Width = cardWidth;
              //      card.Height = cardHeight;

                    Canvas.SetLeft(card, baseX);
                    Canvas.SetTop(card, y);
                    
                    card.ToolTip = $"{LanguageService.Goals()}: {goals}, {LanguageService.YellowCards()}: {yellowCards}";
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

