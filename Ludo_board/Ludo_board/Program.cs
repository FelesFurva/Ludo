namespace Ludo_board
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            ApplicationConfiguration.Initialize();
            //Application.Run(new Ludoboard());
            Application.Run(new StartMenu.StartGameScreen());
        }
    }
}