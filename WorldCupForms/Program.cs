using WorldCupData.Service;

namespace WorldCupForms
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var settingsService = new SettingsService();
            settingsService.Load();

            if (!settingsService.WasLoaded)
            {
                using var startupForm = new StartupForm();
                if (startupForm.ShowDialog() != DialogResult.OK)
                    return;
            }

            Application.Run(new MainForm());
        }
    }
}