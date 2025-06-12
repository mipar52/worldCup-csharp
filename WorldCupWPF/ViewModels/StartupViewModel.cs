using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
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
        public ObservableCollection<string> Championships { get; set; } = new() { LanguageService.SetMenWorldChampion(), LanguageService.SetWomenWorldChampion() };
        public ObservableCollection<string> DisplayModes { get; set; } = new() { "1250x768", "1366x768", "1920x1080", LanguageService.FullScreen() };
        private SettingsService settings = new SettingsService();

        private string _selectedLanguage;
        private string _selectedChampionship;
        private string _selectedDisplayMode;

        public string SelectedLanguage
        {
            get => _selectedLanguage;
            set
            {
                if (_selectedLanguage != value)
                {
                    _selectedLanguage = value;
                    AppSettings.Language = SelectedLanguage == "Croatian" ? "hr" : "en";
                    LanguageService.SetLanguage(AppSettings.Language);
                    ChangeLanugage();
                    OnPropertyChanged();
                }
            }
        }

        public string SelectedChampionship
        {
            get => _selectedChampionship;
            set 
            { 
                _selectedChampionship = value; 
                
                OnPropertyChanged(); }
        }

        public string SelectedDisplayMode
        {
            get => _selectedDisplayMode;
            set { _selectedDisplayMode = value; OnPropertyChanged(); }
        }

        public ICommand ConfirmCommand => new RelayCommand(Confirm);
        public ICommand CancelCommand => new RelayCommand(Cancel);

        public ICommand LanguageChangeCommand => new RelayCommand(ChangeLanugage);


        public event Action OnLanguageChange;
        public event Action OnConfirmed;
        public event Action OnCanceled;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public StartupViewModel()
        {
            SelectedLanguage = AppSettings.Language == "hr" ? "Croatian" : "English";
            SelectedChampionship = AppSettings.Championship == ChampionshipType.Men ? LanguageService.SetMenWorldChampion() : LanguageService.SetWomenWorldChampion();
            SelectedDisplayMode = AppSettings.DisplayMode ?? "Fullscreen";
            if (!DisplayModes.Contains(SelectedDisplayMode))
            {
                SelectedDisplayMode = "FullScreen";
            }
        }
        private void Confirm()
        {
            AppSettings.Language = SelectedLanguage == "Croatian" ? "hr" : "en";
            AppSettings.Championship = SelectedChampionship == LanguageService.SetMenWorldChampion() ? ChampionshipType.Men : ChampionshipType.Women;
            AppSettings.DisplayMode = SelectedDisplayMode;

            new SettingsService().Save();
            OnConfirmed?.Invoke();
        }

        private void Cancel()
        {
            OnCanceled?.Invoke();
        }
        private void ReloadComboBoxes()
        {
            Championships.Clear();
            Championships.Add(LanguageService.SetMenWorldChampion());
            Championships.Add(LanguageService.SetWomenWorldChampion());
            DisplayModes.Clear();
            DisplayModes.Add("1024x768");
            DisplayModes.Add("1366x768");
            DisplayModes.Add("1920x1080");
            DisplayModes.Add(LanguageService.FullScreen());

            SelectedLanguage = AppSettings.Language == "hr" ? "Croatian" : "English";
            SelectedChampionship = AppSettings.Championship == ChampionshipType.Men ? LanguageService.SetMenWorldChampion() : LanguageService.SetWomenWorldChampion();
            SelectedDisplayMode = AppSettings.DisplayMode ?? "Fullscreen";
        }
        private void ChangeLanugage()
        {
            ReloadComboBoxes();
        OnLanguageChange?.Invoke();

        }
    }
}
