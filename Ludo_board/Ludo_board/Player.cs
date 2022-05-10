using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludo_board
{
    public class Player
    {
        public enum Colors
        {
            Green,
            Red,
            Yellow,
            Blue,
        }

        public string Name { get; set; }
        public int Id { get; set; }
        public Colors colors { get; set; }
        public int InitialDiceRoll { get; set; }

        public Player(int id, string name, Colors colors, int initialdiceroll)
        {
            this.Id = id;
            this.Name = name;
            this.colors = colors;
            this.InitialDiceRoll = initialdiceroll;
        }

        public void SetInitialDiceRoll(int dice1)
        {
            InitialDiceRoll = dice1;
        }

        //public void FirstRoll(List<Player> PlayerList, int dice)
        //{
        //    foreach (Player player in PlayerList)
        //    {
        //        player.InitialDiceRoll = dice;
        //    }

        //    var HighestRoll = PlayerList.MaxBy(x => x.InitialDiceRoll);
        //    List<Player> temp = PlayerList.Where(x => x.InitialDiceRoll == HighestRoll?.InitialDiceRoll).ToList();
        //    if (temp.Count > 1)
        //    {
        //        foreach (Player player in temp)
        //        {
        //            player.InitialDiceRoll = dice;
        //        }
        //        HighestRoll = PlayerList.MaxBy(x => x.InitialDiceRoll);
        //    }
        //}

        private static int Nextplayer(int activePlayer, int maxplayers) //int activePlayer = player ID
        {
            return activePlayer % maxplayers + 1;
        }
    } 
}