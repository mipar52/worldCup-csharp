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

                // You can switch between DataSourceMode.Api or File
                var matches = await dataProvider.GetMatchesAsync(ChampionshipType.Women, DataSourceMode.Api);
                foreach (var item in matches)
                {
                    Debug.WriteLine("Matches: " + item.ToString());
                }
                var teams = await dataProvider.GetTeamsAsync(ChampionshipType.Women, DataSourceMode.Api);
                foreach (var item in teams)
                {
                    Debug.WriteLine("TEAM: " + item.ToString());
                    var matchesByCountries = await dataProvider.GetMatchesByCountryAsync(ChampionshipType.Women, DataSourceMode.Api, item.FifaCode);
                    foreach (var match in matchesByCountries)
                    {
                        Debug.WriteLine($"Match for {item.Country}: {match.ToString()}");
                    }
                }
                var teamResults = await dataProvider.GetTeamResultsAsync(ChampionshipType.Women, DataSourceMode.Api);
                foreach (var item in teams)
                {
                    Debug.WriteLine("Team result: " + item.ToString());
                }
                var groupResults = await dataProvider.GetGroupResults(ChampionshipType.Women, DataSourceMode.Api);

                foreach (var item in groupResults)
                {
                    Debug.WriteLine("Group result" + item.ToString());
                }
                if (matches != null && matches.Count > 0)
                {
                    MessageBox.Show($"Fetched {matches.Count} matches.\nFirst match: {matches[0].Venue} vs {matches[0].AwayTeamCountry}");
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
