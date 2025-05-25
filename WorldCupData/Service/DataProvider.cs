using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldCupData.Enums;
using WorldCupData.Model;
using WorldCupData.Model.GroupResults;

namespace WorldCupData.Service
{
    public enum DataSourceMode
    {
        Api,
        File
    }

    public class DataProvider
    {
        private readonly ApiService _apiService;
        private readonly FileService _fileService;

        public DataProvider()
        {
            _apiService = new ApiService();
            _fileService = new FileService();
        }

        public async Task<List<Match>> GetMatchesAsync(ChampionshipType type, DataSourceMode mode)
        {
            if (mode == DataSourceMode.Api)
            {
                return await _apiService.GetMatchesAsync(type);
            }
            else
            {
                string path = $"Files\\worldcup.sfg.io\\{type.ToString().ToLower()}\\matches.json";
                Debug.WriteLine($"CLASS LIB DEBUG: Loading matches from: {path}");
                return _fileService.LoadJson<List<Match>>(path);
            }
        }

        public async Task<List<Match>> GetMatchesByCountryAsync(ChampionshipType type, DataSourceMode mode, String country)
        {
            if (mode == DataSourceMode.Api)
            {
                return await _apiService.GetMatchesByCountryAsync(type, country);
            }
            else
            {
                string path = $"Files\\worldcup.sfg.io\\{type.ToString().ToLower()}\\matches.json";
                Debug.WriteLine($"CLASS LIB DEBUG: Loading matches from: {path}");
                return _fileService.LoadJson<List<Match>>(path);
            }
        }

        public async Task<List<Team>> GetTeamsAsync(ChampionshipType type, DataSourceMode mode)
        {
            if (mode == DataSourceMode.Api)
            {
                return await _apiService.GetTeamsAsync(type);
            }
            else
            {
                return _fileService.LoadJson<List<Team>>($"worldcup.sfg.io/{type.ToString().ToLower()}/teams.json");
            }
        }

        public async Task<List<TeamResult>> GetTeamResultsAsync(ChampionshipType type, DataSourceMode mode)
        {
            if (mode == DataSourceMode.Api)
            {
                return await _apiService.GetTeamsResultsAsync(type);
            }
            else
            {
                return _fileService.LoadJson<List<TeamResult>>($"worldcup.sfg.io/{type.ToString().ToLower()}/results.json");
            }
        }

        public async Task<List<GroupResults>> GetGroupResults(ChampionshipType type, DataSourceMode mode)
        {
            if (mode == DataSourceMode.Api)
            {
                return await _apiService.GetGroupResultsAsync(type);
            }
            else
            {
                return _fileService.LoadJson<List<GroupResults>>($"worldcup.sfg.io/{type.ToString().ToLower()}/group_results.json");
            }
            
        }
    }
}
