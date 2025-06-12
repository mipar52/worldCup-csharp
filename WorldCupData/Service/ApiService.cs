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
using System.Text.RegularExpressions;

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
            var groupResults = JsonConvert.DeserializeObject<List<GroupResults>>(json, Converter.Converter.GroupResultsSettings);
            if (groupResults == null)
                throw new Exception("Failed to deserialize group results! The result was null.");

            return groupResults;
        }

        public async Task<List<WorldCupData.Model.Match>> GetMatchesAsync(ChampionshipType type)
        {
            string url = $"{GetBaseUrl(type)}/matches";
            var json = await _httpClient.GetStringAsync(url);
            if (string.IsNullOrEmpty(json))
                throw new Exception("Failed to retrieve matches. The response was null or empty.");

            var matches = JsonConvert.DeserializeObject<List<WorldCupData.Model.Match>>(json, Converter.Converter.MatchSettings);
            if (matches == null)
                throw new Exception("Failed to deserialize matches. The result was null.");

            return matches;
        }

        public async Task<List<WorldCupData.Model.Match>> GetMatchesByCountryAsync(ChampionshipType type, string country)
        {
            string url = $"{GetBaseUrl(type)}/matches/country?fifa_code={country}";
            var json = await _httpClient.GetStringAsync(url);
            if (string.IsNullOrEmpty(json))
                throw new Exception("Failed to retrieve matches. The response was null or empty.");

            var matches = JsonConvert.DeserializeObject<List<WorldCupData.Model.Match>>(json, Converter.Converter.MatchSettings);
            if (matches == null)
                throw new Exception("Failed to deserialize matches. The result was null.");

            return matches;
        }

        public async Task<List<Team>> GetTeamsAsync(ChampionshipType type)
        {
            string url = $"{GetBaseUrl(type)}/teams";
            var json = await _httpClient.GetStringAsync(url);
            var teams = JsonConvert.DeserializeObject<List<Team>>(json, Converter.Converter.TeamSettings);
            if (teams == null)
                throw new Exception("Failed to deserialize teams. The result was null.");

            return teams;
        }

        public async Task<List<TeamResult>> GetTeamsResultsAsync(ChampionshipType type)
        {
            string url = $"{GetBaseUrl(type)}/teams/results";
            var json = await _httpClient.GetStringAsync(url);
            var teamResults = JsonConvert.DeserializeObject<List<TeamResult>>(json, Converter.Converter.TeamSettings);
            if (teamResults == null)
                throw new Exception("Failed to deserialize team results! The result was null.");

            return teamResults;
        }
    }
}
