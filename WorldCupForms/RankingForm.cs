using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorldCupData.Enums;
using WorldCupData.Model;
using WorldCupData.Service;

namespace WorldCupForms
{
    public partial class RankingForm : Form
    {
        private readonly DataProvider _dataProvider;
        private readonly DataSourceMode _mode;

        private List<Match> _teamMatches;

        private PrintDocument printDocument = new PrintDocument();
        private int currentPrintPage = 0;
        private List<string> printLines = new();

        public RankingForm( string code)
        {
            InitializeComponent();
            _dataProvider = new DataProvider();
            InitializeDataGrids();
        }

        private async void RankingForm_Load(object sender, EventArgs e)
        {
            var favoriteTeamCode = FavoriteService.Load(AppSettings.Championship)?.TeamCode;
            ChangeLanguageStrings();
            if (string.IsNullOrEmpty(favoriteTeamCode))
            {
                MessageBox.Show("Favorite team not selected.");
                Close();
                return;
            }

            _teamMatches = await _dataProvider.GetMatchesByCountryAsync(AppSettings.Championship, _mode, favoriteTeamCode);

            PopulatePlayerRanking(favoriteTeamCode);
            PopulateMatchRanking();
        }
        private void ChangeLanguageStrings()
        {
            this.Text = LanguageService.RankingsTitle();
            fileToolStripMenuItem.Text = LanguageService.File();
            printItem.Text = LanguageService.Print();
            printPreviewToolStripMenuItem.Text = LanguageService.PrintPreview();
        }

        private void InitializeDataGrids()
        {
            dgvPlayerRanking.Columns.Clear();
            dgvPlayerRanking.Columns.Add(new DataGridViewImageColumn { HeaderText = LanguageService.Image(), Name = "Image", Width = 80, ImageLayout = DataGridViewImageCellLayout.Zoom });
            dgvPlayerRanking.Columns.Add("Name", LanguageService.Name());
            dgvPlayerRanking.Columns.Add("Goals", LanguageService.Goals());
            dgvPlayerRanking.Columns.Add("YellowCards", LanguageService.YellowCards());
            dgvPlayerRanking.Columns.Add("Appearances", LanguageService.Appearances());

            dgvMatchRanking.Columns.Clear();
            dgvMatchRanking.Columns.Add("Location", LanguageService.Location());
            dgvMatchRanking.Columns.Add("Attendance", LanguageService.Attendance());
            dgvMatchRanking.Columns.Add("HomeTeam", LanguageService.HomeTeam());
            dgvMatchRanking.Columns.Add("AwayTeam", LanguageService.AwayTeam());
        }


        private void PopulatePlayerRanking(string teamCode)
        {
            var playerStats = new Dictionary<string, (StartingEleven Player, int Goals, int YellowCards, int Appearances)>();

            foreach (var match in _teamMatches)
            {
                var stats = match.HomeTeam.Code == teamCode ? match.HomeTeamStatistics : match.AwayTeamStatistics;
                if (stats == null) continue;

                foreach (var player in stats.StartingEleven.Concat(stats.Substitutes))
                {
                    if (!playerStats.ContainsKey(player.Name))
                        playerStats[player.Name] = (player, 0, 0, 0);

                    var tuple = playerStats[player.Name];

                    playerStats[player.Name] = (player, tuple.Goals, tuple.YellowCards, tuple.Appearances + 1);
                }

                foreach (var ev in match.HomeTeamEvents.Concat(match.AwayTeamEvents))
                {

                    Debug.WriteLine(ev.TypeOfEvent);
                    if (!playerStats.ContainsKey(ev.Player)) continue;

                    var tuple = playerStats[ev.Player];
                    if (ev.TypeOfEvent == TypeOfEvent.Goal)
                        playerStats[ev.Player] = (tuple.Player, tuple.Goals + 1, tuple.YellowCards, tuple.Appearances);
                    else if (ev.TypeOfEvent == TypeOfEvent.YellowCard)
                        playerStats[ev.Player] = (tuple.Player, tuple.Goals, tuple.YellowCards + 1, tuple.Appearances);
                }
            }

            var sorted = playerStats.Values
                .OrderByDescending(x => x.Goals)
                .ThenByDescending(x => x.YellowCards)
                .ThenByDescending(x => x.Appearances)
                .ToList();

            dgvPlayerRanking.Rows.Clear();
            foreach (var entry in sorted)
            {
                var img = ImageService.GetPlayerImagePath(AppSettings.Championship, entry.Player.Name);
                if (img == null || !File.Exists(img))
                {
                    img = ImageService.GetPlaceholderImagePath(AppSettings.Championship);
                }
                dgvPlayerRanking.Rows.Add(Image.FromFile(img), entry.Player.Name, entry.Goals, entry.YellowCards, entry.Appearances);
            }
        }

        private void PopulateMatchRanking()
        {
            var sorted = _teamMatches
                .OrderByDescending(m => m.Attendance)
                .ToList();

            dgvMatchRanking.Rows.Clear();
            foreach (var match in sorted)
            {
                dgvMatchRanking.Rows.Add(
                    match.Location,
                    match.Attendance,
                    match.HomeTeam.Country,
                    match.AwayTeam.Country);
            }
        }

        private void PreparePrintContent()
        {
            printLines.Clear();
            printLines.Add($"=== {LanguageService.PlayerRankings()} ===");
            printLines.Add($"{LanguageService.Name().PadRight(25)}{LanguageService.Goals(),7}{LanguageService.YellowCards(),10}{LanguageService.Appearances(),7}");

            foreach (DataGridViewRow row in dgvPlayerRanking.Rows)
            {
                if (row.IsNewRow) continue;

                string name = (row.Cells[1].Value?.ToString() ?? "").PadRight(25);  // fixed width
                string goals = (row.Cells[2].Value?.ToString() ?? "0").PadLeft(7);
                string yellows = (row.Cells[3].Value?.ToString() ?? "0").PadLeft(10);
                string apps = (row.Cells[4].Value?.ToString() ?? "0").PadLeft(7);

                printLines.Add($"{name}{goals}{yellows}{apps}");
            }

            printLines.Add("");
            printLines.Add("=== MATCH RANKINGS ===");
            printLines.Add(string.Format("{0,-30}{1,10}   {2,-25}{3,-25}", LanguageService.Location(), LanguageService.Attendance(), LanguageService.HomeTeam(), LanguageService.AwayTeam()));

            foreach (DataGridViewRow row in dgvMatchRanking.Rows)
            {
                if (row.IsNewRow) continue;

                string location = row.Cells[0].Value?.ToString() ?? "";
                string visitors = row.Cells[1].Value?.ToString() ?? "0";
                string home = row.Cells[2].Value?.ToString() ?? "";
                string away = row.Cells[3].Value?.ToString() ?? "";

                printLines.Add(string.Format("{0,-30}{1,10}   {2,-25}{3,-25}", location, visitors, home, away));
            }
        }



        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Font font = new Font("Consolas", 10);
            int lineHeight = (int)font.GetHeight(e.Graphics);
            int margin = 50;
            int y = margin;
            int linesPerPage = (e.MarginBounds.Height - margin) / lineHeight;

            while (currentPrintPage < printLines.Count)
            {
                string line = printLines[currentPrintPage];
                e.Graphics.DrawString(line, font, Brushes.Black, new RectangleF(margin, y, e.MarginBounds.Width, lineHeight));
                y += lineHeight;
                currentPrintPage++;

                if ((y + lineHeight) >= e.MarginBounds.Bottom)
                {
                    e.HasMorePages = true;
                    return;
                }
            }

            e.HasMorePages = false;
            currentPrintPage = 0;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

        }

        private void printItem_Click(object sender, EventArgs e)
        {
            PreparePrintContent();

            printDialogRankings.Document = printDocumentRankings;

            if (printDialogRankings.ShowDialog() == DialogResult.OK)
            {
                currentPrintPage = 0;
                printDocumentRankings.Print();
            }
        }

        private void printDocumentRankings_EndPrint(object sender, PrintEventArgs e)
        {
            if (e.PrintAction == PrintAction.PrintToPrinter)
            {
                MessageBox.Show(LanguageService.PrintingFinished());
            }
        }

        private void printDocumentRankings_PrintPage(object sender, PrintPageEventArgs e)
        {
            Font font = new Font("Consolas", 10);
            int lineHeight = (int)font.GetHeight(e.Graphics);
            int margin = 50;
            int y = margin;
            int linesPerPage = (e.MarginBounds.Height - margin) / lineHeight;

            while (currentPrintPage < printLines.Count)
            {
                string line = printLines[currentPrintPage];
                e.Graphics.DrawString(line, font, Brushes.Black, new RectangleF(margin, y, e.MarginBounds.Width, lineHeight));
                y += lineHeight;
                currentPrintPage++;

                if ((y + lineHeight) >= e.MarginBounds.Bottom)
                {
                    e.HasMorePages = true;
                    return;
                }
            }

            e.HasMorePages = false;
            currentPrintPage = 0;
        }

        private void printDocumentRankings_BeginPrint(object sender, PrintEventArgs e)
        {
        }

        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PreparePrintContent();
            printDocumentRankings.DocumentName = LanguageService.RankingReport();
            printPreviewDialog.Document = printDocumentRankings;
            printPreviewDialog.Width = 1000;
            printPreviewDialog.Height = 800;

            currentPrintPage = 0;
            printPreviewDialog.ShowDialog();
        }
    }
}
