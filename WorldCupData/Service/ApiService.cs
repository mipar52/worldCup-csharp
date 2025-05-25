using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WorldCupData.Enums;
using WorldCupData.Model;
using WorldCupData.Converter;
using WorldCupData.Model.GroupResults;

namespace WorldCupData.Service
{
    public class ApiService
    {
        private static readonly HttpClient _httpClient = new();

        private string GetBaseUrl(ChampionshipType type)
        {
            return type == ChampionshipType.Men
                ? "http://worldcup-vua.nullbit.hr/men"
                : "http://worldcup-vua.nullbit.hr/women";
        }

        public async Task<List<GroupResults>> GetGroupResultsAsync(ChampionshipType type)
        {
            string url = $"{GetBaseUrl(type)}/teams/group_results";
            var json = await _httpClient.GetStringAsync(url);
            return JsonConvert.DeserializeObject<List<GroupResults>>(json, Converter.Converter.GroupResultsSettings);
        }

        public async Task<List<Match>> GetMatchesAsync(ChampionshipType type)
        {
            string url = $"{GetBaseUrl(type)}/matches";
            var json = await _httpClient.GetStringAsync(url);
            return JsonConvert.DeserializeObject<List<Match>>(json, Converter.Converter.MatchSettings);
        }

        public async Task<List<Match>> GetMatchesByCountryAsync(ChampionshipType type, string country)
        {
            string url = $"{GetBaseUrl(type)}/matches/country?fifa_code={country}";
            var json = await _httpClient.GetStringAsync(url);
            return JsonConvert.DeserializeObject<List<Match>>(json, Converter.Converter.MatchSettings);
        }

        public async Task<List<Team>> GetTeamsAsync(ChampionshipType type)
        {
            string url = $"{GetBaseUrl(type)}/teams";
            var json = await _httpClient.GetStringAsync(url);
            return JsonConvert.DeserializeObject<List<Team>>(json, Converter.Converter.TeamSettings);
        }

        public async Task<List<TeamResult>> GetTeamsResultsAsync(ChampionshipType type)
        {
            string url = $"{GetBaseUrl(type)}/teams/results";
            var json = await _httpClient.GetStringAsync(url);
            return JsonConvert.DeserializeObject<List<TeamResult>>(json, Converter.Converter.TeamSettings);
        }
    }
}
