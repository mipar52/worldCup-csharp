using WorldCupData.Service;
using WorldCupData.Model;
using WorldCupData.Enums;
using System.Diagnostics;

namespace WorldCupForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var dataProvider = new DataProvider();

                var matches = await dataProvider.GetMatchesAsync(ChampionshipType.Women, DataSourceMode.File);
                var matchCount = matches.Count();
                foreach (var item in matches)
                {
                    Debug.WriteLine("Matches: " + item.ToString());
                }
                var teams = await dataProvider.GetTeamsAsync(ChampionshipType.Women, DataSourceMode.File);
                var teamCount = teams.Count();
                var matchesByTeamCountry = 0;
                foreach (var item in teams)
                {
                    Debug.WriteLine("TEAM: " + item.ToString());
                    var matchesByCountries = await dataProvider.GetMatchesByCountryAsync(ChampionshipType.Women, DataSourceMode.File, item.FifaCode);
                    matchesByTeamCountry += matchesByCountries.Count();
                    foreach (var match in matchesByCountries)
                    {
                        Debug.WriteLine($"Match for {item.Country}: {match.ToString()}");
                    }
                }
                var teamResults = await dataProvider.GetTeamResultsAsync(ChampionshipType.Women, DataSourceMode.File);
                var teamResultsCount = teamResults.Count();
                foreach (var item in teams)
                {
                    Debug.WriteLine("Team result: " + item.ToString());
                }
                var groupResults = await dataProvider.GetGroupResults(ChampionshipType.Women, DataSourceMode.File);
                var groupResultsCount = groupResults.Count();
                foreach (var item in groupResults)
                {
                    Debug.WriteLine("Group result" + item.ToString());
                }
                if (matches != null && matches.Count > 0)
                {
                    MessageBox.Show($"Fetched {matches.Count} matches, {groupResultsCount} groupResults, {teamCount} teams, {teamResultsCount} team results & {matchesByTeamCountry} by country.\nFirst match: {matches[0].Venue} vs {matches[0].AwayTeamCountry}");
                }
                else
                {
                    MessageBox.Show("No matches returned.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[FORMS DEBUG] Error fetching matches: {ex.Message}");
                MessageBox.Show("Error: " + ex.Message);
            }

        }
    }
}
