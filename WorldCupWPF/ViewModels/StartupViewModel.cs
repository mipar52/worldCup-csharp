using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using WorldCupData.Enums;
using WorldCupData.Service;
using WorldCupWPF.Utils;

namespace WorldCupWPF.ViewModels
{

    public class StartupViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<string> Languages { get; } = new() { "English", "Croatian" };
        public ObservableCollection<ChampionshipType> Championships { get; } = new() { ChampionshipType.Men, ChampionshipType.Women };
        public ObservableCollection<string> DisplayModes { get; } = new() { "1024x768", "1366x768", "1920x1080", "Fullscreen" };

        private string _selectedLanguage;
        private ChampionshipType _selectedChampionship;
        private string _selectedDisplayMode;

        public string SelectedLanguage
        {
            get => _selectedLanguage;
            set { _selectedLanguage = value; OnPropertyChanged(); }
        }

        public ChampionshipType SelectedChampionship
        {
            get => _selectedChampionship;
            set { _selectedChampionship = value; OnPropertyChanged(); }
        }

        public string SelectedDisplayMode
        {
            get => _selectedDisplayMode;
            set { _selectedDisplayMode = value; OnPropertyChanged(); }
        }

        public ICommand ConfirmCommand => new RelayCommand(Confirm);
        public ICommand CancelCommand => new RelayCommand(Cancel);

        public event Action OnConfirmed;
        public event Action OnCanceled;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public StartupViewModel()
        {
            // Initialize with default values or previously saved settings
            SelectedLanguage = AppSettings.Language == "hr" ? "Croatian" : "English";
            SelectedChampionship = AppSettings.Championship;
            SelectedDisplayMode = AppSettings.DisplayMode ?? "Fullscreen";
            // Ensure the selected display mode is valid
            if (!DisplayModes.Contains(SelectedDisplayMode))
            {
                SelectedDisplayMode = "Fullscreen";
            }
        }
        private void Confirm()
        {
            AppSettings.Language = SelectedLanguage == "Croatian" ? "hr" : "en";
            AppSettings.Championship = SelectedChampionship;
            AppSettings.DisplayMode = SelectedDisplayMode;

            new SettingsService().Save();
            OnConfirmed?.Invoke();
        }

        private void Cancel()
        {
            OnCanceled?.Invoke();
        }
    }
}
