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
        public FieldLayoutControl()
        {
            InitializeComponent();
            string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files", "worldcup.sfg.io", "men", "Images", "field2.png");
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
            spinner.Message = LanguageService.RenderingField();
            spinner.Visibility = Visibility.Visible;
            try
            {
                FieldCanvas.Children.Clear();

                RenderTeam(HomeTeamPlayers, isHomeTeam: true);
                RenderTeam(AwayTeamPlayers, isHomeTeam: false);
                spinner.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                spinner.Visibility = Visibility.Collapsed;
                MessageBox.Show(LanguageService.ErrorRendering(ex.Message), LanguageService.Warning(), MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RenderTeam(IEnumerable<StartingEleven> players, bool isHomeTeam)
        {
            if (players == null) return;

            var grouped = players.GroupBy(p => p.Position).ToDictionary(g => g.Key, g => g.ToList());

            // Define X-lanes for each position
            double fieldWidth = FieldCanvas.ActualWidth;
            double fieldHeight = FieldCanvas.ActualHeight;
            bool useSmallCard = fieldWidth < 900 || fieldHeight < 600;

            double marginX = fieldWidth * 0.05;
            double marginY = fieldHeight * 0.05;
            double usableWidth = fieldWidth - 2 * marginX;
            double usableHeight = fieldHeight - 2 * marginY;

            // Corrected spacing based on your new rules
            double goalieX = isHomeTeam
                ? marginX + usableWidth * 0.02
                : marginX + usableWidth * 0.9;

            double defenderX = isHomeTeam
                ? marginX + usableWidth * 0.12
                : marginX + usableWidth * 0.80;

            double midfieldX = isHomeTeam
                ? marginX + usableWidth * 0.25
                : marginX + usableWidth * 0.68;

            double forwardX = isHomeTeam
                ? marginX + usableWidth * 0.38
                : marginX + usableWidth * 0.55;




            //  double forwardX = isHomeTeam ? (defenderX + midfieldX) / 2 + 0.2 : (defenderX + midfieldX) / 2 - 0.2;



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
                int count = linePlayers.Count;

                double cardWidth = useSmallCard
                    ? Math.Clamp(fieldWidth * 0.06, 40, 85)
                    : Math.Clamp(fieldWidth * 0.08, 70, 160); // increased max from 140 → 160


                double cardHeight = useSmallCard
                    ? Math.Clamp(fieldHeight * 0.10, 60, 110)
                    : Math.Clamp(fieldHeight * 0.15, 90, 200);

                // 1. Base spacing multiplier depending on card size
                double spacingMultiplier = useSmallCard ? 1.01 : 1.01;

                // 2. Apply multiplier
                double baseSpacing = cardHeight * spacingMultiplier;

                // 3. Calculate available spacing based on field height
                double maxFittableSpacing = (usableHeight - cardHeight) / count;

                // 4. Final spacing = min of baseSpacing and max allowed
                double spacing = Math.Max(Math.Min(baseSpacing, maxFittableSpacing), 10); // never less than 10px



                double startY = marginY + (usableHeight - ((count - 1) * spacing + cardHeight)) / 2;

                for (int i = 0; i < count; i++)
                {
                    var player = linePlayers[i];
                    UserControl card = useSmallCard
                        ? new PlayerCardSmallControl(player)
                        : new PlayerCardControl(player);

                    int goals = 0;
                    int yellowCards = 0;
                    int goalsOwn = 0;
                    var color = new SolidColorBrush(Colors.White);

                    var events = isHomeTeam ? HomeTeamEvents : AwayTeamEvents;

                    if (events != null)
                    {
                        goals = events.Count(ev => ev.Player == player.Name && (ev.TypeOfEvent == TypeOfEvent.Goal || ev.TypeOfEvent == TypeOfEvent.GoalPenalty));
                        yellowCards = events.Count(ev => ev.Player == player.Name && ev.TypeOfEvent == TypeOfEvent.YellowCard);
                        goalsOwn = events.Count(ev => ev.Player == player.Name && ev.TypeOfEvent == TypeOfEvent.GoalOwn);

                        if (goals > 0)
                            color = new SolidColorBrush(Colors.LightGreen);
                        else if (yellowCards > 0)
                            color = new SolidColorBrush(Colors.Yellow);
                        else if (goalsOwn > 0)
                            color = new SolidColorBrush(Colors.IndianRed);
                    }

                    if (card is PlayerCardControl largeCard)
                        largeCard.PlayerClicked += (s, p) => new PlayerInfoWindow(p, goals, yellowCards).ShowDialog();
                    else if (card is PlayerCardSmallControl smallCard)
                        smallCard.PlayerClicked += (s, p) => new PlayerInfoWindow(p, goals, yellowCards).ShowDialog();

                    double xPos = Math.Clamp(x, 0, fieldWidth - cardWidth);
                    double yPos = Math.Clamp(startY + i * spacing, 0, fieldHeight - cardHeight);

                    card.Width = cardWidth;
                  //  card.Height = cardHeight;
                    card.ToolTip = $"{LanguageService.Goals()}: {goals}, {LanguageService.YellowCards()}: {yellowCards}";
                    card.Background = color;

                    Canvas.SetLeft(card, xPos);
                    Canvas.SetTop(card, yPos);

                    FieldCanvas.Children.Add(card);
                }
            }

        }
    }
    }

