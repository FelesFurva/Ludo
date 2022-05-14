namespace Ludo_board
{
    public class Player
    {
        public enum PlayerColor
        {
            Green,
            Red,
            Yellow,
            Blue,
        }
        public string Name { get; set; }
        public int Id { get; set; }
        public PlayerColor Color { get; set; }
        public int InitialDiceRoll { get; set; }

        public PictureBox[] playerPawns;

        public PictureBox[] PlayerPawns
        {
            get => playerPawns;
            set
            {
                playerPawns = value;
                PawnStateReset();
            }
        }

        private bool isActive = false;

        public PictureBox[] PlayerPawnNests;

        public bool IsActive
        {
            get => isActive;
            set
            {
                isActive = value;
                PawnStateReset();
            }
        }
        public Player(int id,
                      string name,
                      PlayerColor color,
                      int initialdiceroll,
                      PictureBox pb1,
                      PictureBox pb2,
                      PictureBox pb3,
                      PictureBox pb4,
                      PictureBox nb1,
                      PictureBox nb2,
                      PictureBox nb3,
                      PictureBox nb4)
        {
            Id = id;
            Name = name;
            Color = color;
            InitialDiceRoll = initialdiceroll;
            PlayerPawns = new PictureBox[4]
            {
                pb1 ?? throw new ArgumentNullException(nameof(pb1)),
                pb2 ?? throw new ArgumentNullException(nameof(pb2)),
                pb3 ?? throw new ArgumentNullException(nameof(pb3)),
                pb4 ?? throw new ArgumentNullException(nameof(pb4)),
            };
            PlayerPawnNests = new PictureBox[4]
            {
                nb1 ?? throw new ArgumentNullException(nameof(pb1)),
                nb2 ?? throw new ArgumentNullException(nameof(pb2)),
                nb3 ?? throw new ArgumentNullException(nameof(pb3)),
                nb4 ?? throw new ArgumentNullException(nameof(pb4)),
            };

        }

        private void PawnStateReset()
        {
            Array.ForEach(PlayerPawns, p => p.Enabled = isActive);
            
        }

        //public bool PawnCheckVisibility()
        //{
        //    bool check = true;
        //    int count = 0;
        //    for (int i = 0; i < PlayerPawnNests.Length; i++)
        //    {
        //        if (PlayerPawns[i].Location != PlayerPawnNests[i].Location)
        //        {
        //            if (PlayerPawns[i].Visible == true)
        //            {
        //                count++;
        //            }
        //        }
        //        if (count > 0)
        //        {
        //            check = true;
        //        }
        //        else
        //        {
        //            check = false;
        //        }
        //    }
        //    return check;
        //}

        public bool HasPawnsInGame()
        {
            for (int i = 0; i < PlayerPawnNests.Length; i++)
            {
                if((PlayerPawns[i].Location != PlayerPawnNests[i].Location) && (PlayerPawns[i].Visible == true))
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsAllPawnsHidden()
        {
            return PlayerPawns.All(p => !p.Visible);
        }
    }
}