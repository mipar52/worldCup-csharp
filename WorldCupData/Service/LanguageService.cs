﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldCupData.Service
{
    public static class LanguageService
    {
        private static string _language = AppSettings.Language; 

        public static void SetLanguage(string language)
        {
            if (language == "hr" || language == "en")
            {
                _language = language;
            }
            else
            {
                throw new ArgumentException("Invalid language code. Use 'hr' for Croatian or 'en' for English.");
            }
        }

        public static string SetWelcomeMessage()
        {
            return _language switch
            {
                "hr" => "Dobrodošli u aplikaciju Svjetskog prvenstva!",
                "en" => "Welcome to the World Cup application!",
                _ => throw new InvalidOperationException("Language not set correctly.")
            };
        }

        public static string SetApplicationLangugeString()
        {
            return _language switch
            {
                "hr" => "Odaberite jezik aplikacije:",
                "en" => "Select application language",
                _ => throw new InvalidOperationException("Language not set correctly.")
            };
        }

        public static string SetWorldChampionShipPicker()
        {
            return _language switch
            {
                "hr" => "Odaberite svijetsko prvenstvo:",
                "en" => "World Championship Picker",
                _ => throw new InvalidOperationException("Language not set correctly.")
            };
        }

        public static string SetMenWorldChampion()
        {
            return _language switch
            {
                "hr" => "Muško svijetsko prvenstvo (2018)",
                "en" => "Men World Cup Championship (2018)",
                _ => throw new InvalidOperationException("Language not set correctly.")
            };
        }

        public static string SetWomenWorldChampion()
        {
            return _language switch
            {
                "hr" => "Žensko svijetsko prvenstvo (2019)",
                "en" => "Women World Cup Championship (2019)",
                _ => throw new InvalidOperationException("Language not set correctly.")
            };
        }

        public static string SetDataFetcher()
        {
            return _language switch
            {
                "hr" => "Dohvati podatke:",
                "en" => "Data fetcher:",
                _ => throw new InvalidOperationException("Language not set correctly.")
            };
        }

        public static string ViaApi()
        {
            return _language switch
            {
                "hr" => "Preko API-a (potreban internet)",
                "en" => "Via API (internet required)",
                _ => throw new InvalidOperationException("Language not set correctly.")
            };
        }

        public static string Locally()
        {
            return _language switch
            {
                "hr" => "Lokalno (preko lokalnih datoteka, nije potreban internet)",
                "en" => "Locally (via local files, no internet required)",
                _ => throw new InvalidOperationException("Language not set correctly.")
            };
        }

        public static string Confirm()
        {
            return _language switch
            {
                "hr" => "POTVRDI",
                "en" => "CONFIRM",
                _ => throw new InvalidOperationException("Language not set correctly.")
            };
        }

        public static string Cancel()
        {
            return _language switch
            {
                "hr" => "ODUSTANI",
                "en" => "CANCEL",
                _ => throw new InvalidOperationException("Language not set correctly.")
            };
        }

        public static string WantAChange()
        {
            return _language switch
            {
                "hr" => "Spremni za promjenu? Slobodno promijenite postavke aplikacije ovdje",
                "en" => "Want a change? Feel free to change your app preferences here",
                _ => throw new InvalidOperationException("Language not set correctly.")
            };
        }

        public static string File()
        {
            return _language switch
            {
                "hr" => "Datoteka",
                "en" => "File",
                _ => throw new InvalidOperationException("Language not set correctly.")
            };
        }

        public static string Settings()
        {
            return _language switch
            {
                "hr" => "Postavke",
                "en" => "Settings",
                _ => throw new InvalidOperationException("Language not set correctly.")
            };
        }

        public static string ChangeAppSettings()
        {
            return _language switch
            {
                "hr" => "Promijenite postavke aplikacije",
                "en" => "Change application settings",
                _ => throw new InvalidOperationException("Language not set correctly.")
            };
        }

        public static string ResetSettings()
        {
            return _language switch
            {
                "hr" => "Vrati zadane postavke",
                "en" => "Reset settings",
                _ => throw new InvalidOperationException("Language not set correctly.")
            };
        }

        public static string Exit()
        {
            return _language switch
            {
                "hr" => "Izlaz",
                "en" => "Exit",
                _ => throw new InvalidOperationException("Language not set correctly.")
            };
        }

        public static string Help()
        {
            return _language switch
            {
                "hr" => "Pomoć",
                "en" => "Help",
                _ => throw new InvalidOperationException("Language not set correctly.")
            };
        }

        public static string About()
        {
            return _language switch
            {
                "hr" => "O programu",
                "en" => "About",
                _ => throw new InvalidOperationException("Language not set correctly.")
            };
        }

        public static string FavoriteTeam()
        {
            return _language switch
            {
                "hr" => "Omiljena momčad:",
                "en" => "Favorite team:",
                _ => throw new InvalidOperationException("Language not set correctly.")
            };
        }

        public static string FavoritePlayers()
        {
            return _language switch
            {
                "hr" => "Omiljeni igrači",
                "en" => "Favorite players",
                _ => throw new InvalidOperationException("Language not set correctly.")
            };
        }

        public static string GetRankings()
        {
            return _language switch
            {
                "hr" => "Dohvati statistiku",
                "en" => "Get rankings",
                _ => throw new InvalidOperationException("Language not set correctly.")
            };
        }

        public static string ChooseFavoritePlayers()
        {
            return _language switch
            {
                "hr" => "Odaberi omiljene igrače",
                "en" => "Choose favorite players",
                _ => throw new InvalidOperationException("Language not set correctly.")
            };
        }

        public static string AreYouSureReset()
        {
            return _language switch
            {
                "hr" => "Jeste li sigurni da žeite vratiti aplikaciju na zadane postavke?\nAplikacija će se ponovno pokrenuti i pokazati će se početni zaslon.",
                "en" => "Are you sure you want to reset application settings?\nThe app will restart and show the startup screen.",
                _ => throw new InvalidOperationException("Language not set correctly.")
            };
        }

        public static string ConfirmReset()
        {
            return _language switch
            {
                "hr" => "Potvrda o vraćanju na zadane postavke",
                "en" => "Reset Confirmation",
                _ => throw new InvalidOperationException("Language not set correctly.")
            };
        }

        public static string ExitConfirmation()
        {
            return _language switch
            {
                "hr" => "Jeste li sigurni da želite izaći iz aplikacije?",
                "en" => "Are you sure you want to exit?",
                _ => throw new InvalidOperationException("Language not set correctly.")
            };
        }
        public static string Success()
        {
            return _language switch
            {
                "hr" => "Uspjeh",
                "en" => "Success",
                _ => throw new InvalidOperationException("Language not set correctly.")
            };
        }
        public static string SaveSuccess()
        {
            return _language switch
            {
                "hr" => "Postavke uspješno promjenjene.",
                "en" => "Settings changed successfully.",
                _ => throw new InvalidOperationException("Language not set correctly.")
            };
        }

        public static string AppInfo()
        {
            return _language switch
            {
                "hr" => "Svijetsko prvenstvo - Aplikacije\nVerzija 1.0",
                "en" => "World Cup App\nVersion 1.0",
                _ => throw new InvalidOperationException("Language not set correctly.")
            };
        }

        public static string SaveSelection()
        {
            return _language switch
            {
                "hr" => "Spremi odabir",
                "en" => "Save selecation",
                _ => throw new InvalidOperationException("Language not set correctly.")
            };
        }

        public static string MainFormTitle()
        {
            return _language switch
            {
                "hr" => "Upravitelj svjetskog prvenstva",
                "en" => "World Cup Manager",
                _ => throw new InvalidOperationException("Language not set correctly.")
            };
        }

        public static string SettingsTitle()
        {
            return _language switch
            {
                "hr" => "Postavke",
                "en" => "Settings",
                _ => throw new InvalidOperationException("Language not set correctly.")
            };
        }

        public static string StartupTitle()
        {
            return _language switch
            {
                "hr" => "Početne postavke",
                "en" => "Initial settings",
                _ => throw new InvalidOperationException("Language not set correctly.")
            };
        }

        public static string FavoritePlayersTitle()
        {
            return _language switch
            {
                "hr" => "Omiljeni Igrači - Postavke",
                "en" => "Select Your Favorite Players",
                _ => throw new InvalidOperationException("Language not set correctly.")
            };
        }

        public static string RankingsTitle()
        {
            return _language switch
            {
                "hr" => "Statistika momčadi",
                "en" => "Player rankings",
                _ => throw new InvalidOperationException("Language not set correctly.")
            };
        }

        public static string PlayerName()
        {
            return _language switch
            {
                "hr" => "Ime igrača: ",
                "en" => "Player name: ",
                _ => throw new InvalidOperationException("Language not set correctly.")
            };
        }

        public static string PlayerNumber()
        {
            return _language switch
            {
                "hr" => "Broj igrača: ",
                "en" => "Player number: ",
                _ => throw new InvalidOperationException("Language not set correctly.")
            };
        }

        public static string PlayerPosition()
        {
            return _language switch
            {
                "hr" => "Pozicija igrača: ",
                "en" => "Player position: ",
                _ => throw new InvalidOperationException("Language not set correctly.")
            };
        }

        public static string PlayerCaptin()
        {
            return _language switch
            {
                "hr" => "⭐ Kapetan",
                "en" => "⭐ Captain",
                _ => throw new InvalidOperationException("Language not set correctly.")
            };
        }

        public static string? PromoteToFavorite()
        {
            return _language switch
            {
                "hr" => "Dodaj u favorite",
                "en" => "Promote to favorites",
                _ => throw new InvalidOperationException("Language not set correctly.")
            };
        }

        public static string? DemoteToOther()
        {
            return _language switch
            {
                "hr" => "Makni iz favorita",
                "en" => "Demote to others",
                _ => throw new InvalidOperationException("Language not set correctly.")
            };
        }
        public static string? Print()
        {
            return _language switch
            {
                "hr" => "Postavke printanja",
                "en" => "Print",
                _ => throw new InvalidOperationException("Language not set correctly.")
            };
        }
        public static string? PrintPreview()
        {
            return _language switch
            {
                "hr" => "Prikaži pregled ispisa",
                "en" => "Print preview",
                _ => throw new InvalidOperationException("Language not set correctly.")
            };
        }
        public static string? Image()
        {
            return _language switch
            {
                "hr" => "Slika",
                "en" => "Image",
                _ => throw new InvalidOperationException("Language not set correctly.")
            };
        }
        public static string? Name()
        {
            return _language switch
            {
                "hr" => "Ime igrača",
                "en" => "Player name",
                _ => throw new InvalidOperationException("Language not set correctly.")
            };
        }
        public static string? Goals()
        {
            return _language switch
            {
                "hr" => "Golovi",
                "en" => "Goals",
                _ => throw new InvalidOperationException("Language not set correctly.")
            };
        }
        public static string? YellowCards()
        {
            return _language switch
            {
                "hr" => "Žuti karton(i)",
                "en" => "Yellow card(s)",
                _ => throw new InvalidOperationException("Language not set correctly.")
            };
        }
        public static string? Appearances()
        {
            return _language switch
            {
                "hr" => "Ulasi na teren",
                "en" => "Appearances",
                _ => throw new InvalidOperationException("Language not set correctly.")
            };
        }
        public static string? Location()
        {
            return _language switch
            {
                "hr" => "Lokacija",
                "en" => "Location",
                _ => throw new InvalidOperationException("Language not set correctly.")
            };
        }
        public static string? Attendance()
        {
            return _language switch
            {
                "hr" => "Broj posjetitelja",
                "en" => "Attendance",
                _ => throw new InvalidOperationException("Language not set correctly.")
            };
        }
        public static string? HomeTeam()
        {
            return _language switch
            {
                "hr" => "Domaći tim",
                "en" => "Home team",
                _ => throw new InvalidOperationException("Language not set correctly.")
            };
        }
        public static string? AwayTeam()
        {
            return _language switch
            {
                "hr" => "Strani tim",
                "en" => "Away team",
                _ => throw new InvalidOperationException("Language not set correctly.")
            };
        }
        public static string? PlayerRankings()
        {
            return _language switch
            {
                "hr" => "Statistika igrača",
                "en" => "Player statistics",
                _ => throw new InvalidOperationException("Language not set correctly.")
            };
        }

        public static string? MatchRankings()
        {
            return _language switch
            {
                "hr" => "Statistika utakmica",
                "en" => "Match Statistics",
                _ => throw new InvalidOperationException("Language not set correctly.")
            };
        }
        public static string? PrintingFinished()
        {
            return _language switch
            {
                "hr" => "Printanje završeno",
                "en" => "Printing finished",
                _ => throw new InvalidOperationException("Language not set correctly.")
            };
        }
        public static string? RankingReport()
        {
            return _language switch
            {
                "hr" => "Statistika igranja",
                "en" => "Ranking Report",
                _ => throw new InvalidOperationException("Language not set correctly.")
            };
        }
    }
}
