using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WorldCupData.Model;
using WorldCupData.Service;
using WorldCupWPF.Utils;
using WorldCupWPF.Views;

namespace WorldCupWPF.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
            private readonly DataProvider _dataProvider = new();

            public ObservableCollection<TeamViewModel> AvailableTeams { get; set; } = new();
            public ObservableCollection<string> OpponentOptions { get; set; } = new();

        public ICommand ShowTeamInfo => new RelayCommand(ExecuteShowTeamInfo);




        public ObservableCollection<StartingEleven> HomeStartingEleven { get; set; } = new();
        public ObservableCollection<StartingEleven> AwayStartingEleven { get; set; } = new();

        public List<StartingEleven> HomeTeamPlayers => HomeStartingEleven.ToList();
        public List<StartingEleven> AwayTeamPlayers => AwayStartingEleven.ToList();



        private TeamViewModel _selectedTeam;
            public TeamViewModel SelectedTeam
            {
            get => _selectedTeam;
            set
            {
                if (SetProperty(ref _selectedTeam, value))
                {
                    _ = LoadOpponentsAsync();
                    TryLoadMatchAsync();
                }
            }
        }

            private string _selectedOpponentCode;
            public string SelectedOpponentCode
            {
            get => _selectedOpponentCode;
            set
            {
                if (SetProperty(ref _selectedOpponentCode, value))
                    TryLoadMatchAsync();
            }
        }


        private string _matchResult;
            public string MatchResult
            {
                get => _matchResult;
                set => SetProperty(ref _matchResult, value);
            }

            public MainViewModel()
            {
                _ = LoadTeamsAsync();
            }

        private async void TryLoadMatchAsync()
        {
            if (SelectedTeam == null || string.IsNullOrEmpty(SelectedOpponentCode))
                return;

            HomeStartingEleven.Clear();
            AwayStartingEleven.Clear();

            var matches = await _dataProvider.GetMatchesByCountryAsync(AppSettings.Championship, AppSettings.DataSourceMode, SelectedTeam.Team.FifaCode);
            var match = matches.FirstOrDefault(m =>
                (m.HomeTeam.Code == SelectedTeam.Team.FifaCode && m.AwayTeam.Code == SelectedOpponentCode) ||
                (m.AwayTeam.Code == SelectedTeam.Team.FifaCode && m.HomeTeam.Code == SelectedOpponentCode));

            if (match == null)
            {
                MatchResult = "Match not found.";
                return;
            }

            // Match Result
            MatchResult = match.HomeTeam.Code == SelectedTeam.Team.FifaCode
                ? $"{match.HomeTeam.Goals} : {match.AwayTeam.Goals}"
                : $"{match.AwayTeam.Goals} : {match.HomeTeam.Goals}";

            // Players
            if (match.HomeTeamStatistics?.StartingEleven != null)
                foreach (var p in match.HomeTeamStatistics.StartingEleven)
                {
                    HomeStartingEleven.Add(p);
                    HomeTeamPlayers.Add(p);
                }


            if (match.AwayTeamStatistics?.StartingEleven != null)
                foreach (var p in match.AwayTeamStatistics.StartingEleven)
                {
                    AwayStartingEleven.Add(p);
                    AwayTeamPlayers.Add(p);
                }

            // Notify field layout to refresh
            OnPropertyChanged(nameof(HomeTeamPlayers));
            OnPropertyChanged(nameof(AwayTeamPlayers));
            OnPropertyChanged(nameof(HomeStartingEleven));
            OnPropertyChanged(nameof(AwayStartingEleven));
        }


        public async Task LoadTeamsAsync()
            {
                var teams = await _dataProvider.GetTeamsAsync(AppSettings.Championship, AppSettings.DataSourceMode);
                AvailableTeams.Clear();
                foreach (var team in teams)
                    AvailableTeams.Add(new TeamViewModel(team));

                var favoriteCode = FavoriteService.Load(AppSettings.Championship).Value.TeamCode;
                SelectedTeam = AvailableTeams.FirstOrDefault(vm => vm.Team.FifaCode == favoriteCode);
            }

            private async Task LoadOpponentsAsync()
            {
                OpponentOptions.Clear();
                if (SelectedTeam == null) return;

                var matches = await _dataProvider.GetMatchesByCountryAsync(AppSettings.Championship, AppSettings.DataSourceMode, SelectedTeam.Team.FifaCode);
                var opponents = matches.Select(m => m.HomeTeam.Code == SelectedTeam.Team.FifaCode ? m.AwayTeam.Code : m.HomeTeam.Code).Distinct();
                foreach (var code in opponents)
                    OpponentOptions.Add(code);
            }

            private async Task LoadMatchResultAsync()
            {
                if (SelectedTeam == null || string.IsNullOrEmpty(SelectedOpponentCode)) return;

                var matches = await _dataProvider.GetMatchesByCountryAsync(AppSettings.Championship, AppSettings.DataSourceMode, SelectedTeam.Team.FifaCode);
                var match = matches.FirstOrDefault(m =>
                    (m.HomeTeam.Code == SelectedTeam.Team.FifaCode && m.AwayTeam.Code == SelectedOpponentCode) ||
                    (m.AwayTeam.Code == SelectedTeam.Team.FifaCode && m.HomeTeam.Code == SelectedOpponentCode));

                if (match == null)
                {
                    MatchResult = "Match not found.";
                    return;
                }

                if (match.HomeTeam.Code == SelectedTeam.Team.FifaCode)
                    MatchResult = $"{match.HomeTeam.Goals} : {match.AwayTeam.Goals}";
                else
                    MatchResult = $"{match.AwayTeam.Goals} : {match.HomeTeam.Goals}";
            HomeStartingEleven.Clear();
            AwayStartingEleven.Clear();

            if (match.HomeTeamStatistics?.StartingEleven != null)
                foreach (var p in match.HomeTeamStatistics.StartingEleven)
                    HomeStartingEleven.Add(p);

            if (match.AwayTeamStatistics?.StartingEleven != null)
                foreach (var p in match.AwayTeamStatistics.StartingEleven)
                    AwayStartingEleven.Add(p);

            OnPropertyChanged(nameof(HomeTeamPlayers));
            OnPropertyChanged(nameof(AwayTeamPlayers));

        }

        private void ExecuteShowTeamInfo()
        {
            if (SelectedTeam == null) return;

            var teamInfoWindow = new TeamInfoWindow
            {
                DataContext = new TeamInfoViewModel(SelectedTeam.Team)
            };

            teamInfoWindow.ShowDialog();
        }

        public event PropertyChangedEventHandler PropertyChanged;
            protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
                => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
            {
                if (EqualityComparer<T>.Default.Equals(field, value)) return false;
                field = value;
                OnPropertyChanged(propertyName);
                return true;
            }
        }
    }
