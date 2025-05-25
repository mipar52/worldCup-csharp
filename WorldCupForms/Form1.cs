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
                var matches = await dataProvider.GetMatchesAsync(ChampionshipType.Men, DataSourceMode.Api);

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
