﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
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
            public ObservableCollection<TeamViewModel> OpponentOptions { get; set; } = new();

        public ICommand ShowTeamInfo => new RelayCommand(ExecuteShowTeamInfo);
        public ICommand ShowOpponentTeamInfo => new RelayCommand(ExecuteShowOpponentTeamInfo);

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
                    OnPropertyChanged();
                    TryLoadMatchAsync();
                }
            }
        }

            private TeamViewModel _selectedOpponentCode;
            public TeamViewModel SelectedOpponentCode
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
            if (SelectedTeam == null || SelectedOpponentCode == null)
                return;

            HomeStartingEleven.Clear();
            AwayStartingEleven.Clear();

            var matches = await _dataProvider.GetMatchesByCountryAsync(AppSettings.Championship, AppSettings.DataSourceMode, SelectedTeam.Team.FifaCode);
            var match = matches.FirstOrDefault(m =>
                (m.HomeTeam.Code == SelectedTeam.Team.FifaCode && m.AwayTeam.Code == SelectedOpponentCode.Team.FifaCode) ||
                (m.AwayTeam.Code == SelectedTeam.Team.FifaCode && m.HomeTeam.Code == SelectedOpponentCode.Team.FifaCode));

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
            try
            {
                var teams = await _dataProvider.GetTeamsAsync(AppSettings.Championship, AppSettings.DataSourceMode);
                AvailableTeams.Clear();
                foreach (var team in teams)
                    AvailableTeams.Add(new TeamViewModel(team));

                var favorite = FavoriteService.Load(AppSettings.Championship);
                Debug.WriteLine($"[DEBUG] Image Folder: {PathHelper.GetImageFolderPath(AppSettings.Championship)}");
                Debug.WriteLine($"[DEBUG] Favorite File: {PathHelper.GetFavoritesFilePath(AppSettings.Championship)}");
                if (favorite.HasValue)
                {
                    var favoriteCode = favorite.Value.TeamCode;
                    SelectedTeam = AvailableTeams.FirstOrDefault(vm => vm.Team.FifaCode == favoriteCode);
                }
                else
                {
                    SelectedTeam = AvailableTeams.FirstOrDefault();
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading teams: {ex.Message}");
                MessageBox.Show($"Error: {ex.Message}\n\n{ex.StackTrace}");
            }
        }

        private async Task LoadOpponentsAsync()
        {
            OpponentOptions.Clear();
            if (SelectedTeam == null) return;

            var matches = await _dataProvider.GetMatchesByCountryAsync(
                AppSettings.Championship, AppSettings.DataSourceMode, SelectedTeam.Team.FifaCode);

            var allTeams = await _dataProvider.GetTeamsAsync(AppSettings.Championship, AppSettings.DataSourceMode);

            var opponentsCodes = matches
                .Select(m => m.HomeTeam.Code == SelectedTeam.Team.FifaCode ? m.AwayTeam.Code : m.HomeTeam.Code)
                .Distinct();

            foreach (var code in opponentsCodes)
            {
                var team = allTeams.FirstOrDefault(t => t.FifaCode == code);
                if (team != null)
                {
                    var teamOption = new TeamViewModel(team);
                    OpponentOptions.Add(teamOption);
                }

            }

            OnPropertyChanged(nameof(OpponentOptions));
        }


        private async Task LoadMatchResultAsync()
            {
                if (SelectedTeam == null || SelectedOpponentCode == null) return;

                var matches = await _dataProvider.GetMatchesByCountryAsync(AppSettings.Championship, AppSettings.DataSourceMode, SelectedTeam.Team.FifaCode);
                var match = matches.FirstOrDefault(m =>
                    (m.HomeTeam.Code == SelectedTeam.Team.FifaCode && m.AwayTeam.Code == SelectedOpponentCode.Team.FifaCode) ||
                    (m.AwayTeam.Code == SelectedTeam.Team.FifaCode && m.HomeTeam.Code == SelectedOpponentCode.Team.FifaCode));

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

        private void ExecuteShowOpponentTeamInfo()
        {
            if (SelectedOpponentCode == null)
            {
                MessageBox.Show(LanguageService.PickATeam(),"", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            ;

            var teamInfoWindow = new TeamInfoWindow
            {
                DataContext = new TeamInfoViewModel(SelectedOpponentCode.Team)
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
