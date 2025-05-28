using System;
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

    }
}
