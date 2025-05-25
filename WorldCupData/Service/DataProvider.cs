using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldCupData.Enums;
using WorldCupData.Model;

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

        public async Task<List<TeamResult>> GetTeamsAsync(ChampionshipType type, DataSourceMode mode)
        {
            if (mode == DataSourceMode.Api)
            {
                return await _apiService.GetTeamResultsAsync(type);
            }
            else
            {
                return _fileService.LoadJson<List<TeamResult>>($"worldcup.sfg.io/{type.ToString().ToLower()}/results.json");
            }
        }

        public List<GroupResult> GetGroupResults(ChampionshipType type, DataSourceMode mode)
        {
            return _fileService.LoadJson<List<GroupResult>>($"worldcup.sfg.io/{type.ToString().ToLower()}/group_results.json");
        }
    }
}
