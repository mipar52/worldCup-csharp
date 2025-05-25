using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WorldCupData.Enum;
using WorldCupData.Model;

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

        public async Task<List<TeamResult>> GetTeamResultsAsync(ChampionshipType type)
        {
            string url = $"{GetBaseUrl(type)}/teams/results";
            var json = await _httpClient.GetStringAsync(url);
            return JsonSerializer.Deserialize<List<TeamResult>>(json);
        }

        public async Task<List<Match>> GetMatchesAsync(ChampionshipType type)
        {
            string url = $"{GetBaseUrl(type)}/matches";
            var json = await _httpClient.GetStringAsync(url);
            return JsonSerializer.Deserialize<List<Match>>(json);
        }

        public async Task<List<Team>> GetTeamsAsync(ChampionshipType type)
        {
            string url = $"{GetBaseUrl(type)}/teams/results";
            var json = await _httpClient.GetStringAsync(url);
            return JsonSerializer.Deserialize<List<Team>>(json);
        }
    }
}
