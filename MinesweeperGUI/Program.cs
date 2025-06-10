namespace MinesweeperGUI
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
            ApplicationConfiguration.Initialize();

            // Show settings form first
            using (var settingsForm = new SettingsForm())
            {
                // Show the form as a dialog and check the result
                if (settingsForm.ShowDialog() == DialogResult.OK)
                {
                    // Get the selected settings from the form's properties
                    int size = settingsForm.SelectedSize;
                    float difficulty = settingsForm.SelectedDifficulty;

                    // Validate the settings
                    if (size < 3) size = 3;
                    if (size > 10) size = 10;
                    if (difficulty < 0.01f) difficulty = 0.01f;
                    if (difficulty > 0.5f) difficulty = 0.5f;

                    // Start the game with the selected settings
                    Application.Run(new Form1(size, difficulty));
                }
                else
                {
                    // User canceled the settings form
                    Application.Exit();
                }
            }
        }
    }
}