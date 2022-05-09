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
            
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Ludoboard());

            Player player1 = new Player(1, "player 1", Player.Colors.Green, 0);
            Player player2 = new Player(2, "player 2", Player.Colors.Red, 0);
            Player player3 = new Player(3, "player 3", Player.Colors.Yellow, 0);
            Player player4 = new Player(4, "player 4", Player.Colors.Blue, 0);

            List<Player> PlayerList = new List<Player>();
            PlayerList.Add(player1);
            PlayerList.Add(player2);
            PlayerList.Add(player3);
            PlayerList.Add(player4);






        }
    }
}