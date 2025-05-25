using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldCupData.Enum;
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
                return _fileService.LoadJson<List<Match>>("matches.json");
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
                return _fileService.LoadJson<List<TeamResult>>("results.json");
            }
        }

        public List<GroupResult> GetGroupResults(DataSourceMode mode)
        {
            return _fileService.LoadJson<List<GroupResult>>("group_results.json");
        }
    }
}
