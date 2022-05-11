namespace Ludo_board
{
    public partial class Ludoboard : Form
    {
              
        //step counter for each pawn, to compare to the whole number of steps to make [0-3 = green, 4-7 = red, 8-11 = yellow, 12-15 = blue]:
        int[] pawnStepsMade = new int[16]
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};


        //positions for each pawn [0-3 = green, 4-7 = red, 8-11 = yellow, 12-15 = blue]
        int[] pawnBoxnumber = new int[16]
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};


        //Start field shift determination initial components
        int playerIndex = 0;  //player index: 0=green, 1=red, 2=yellow, 3=blue

        const int startShift = 12;
        const int sharedCellsCount = 48;

        //coordinates for all board spaces to walk on [0-3 = green, 4-7 = red, 8-11 = yellow, 12-15 = blue]
        PictureBox[] boardtiles;
        PictureBox[] specialLanes;
        PictureBox[] allNests;
        PictureBox[] allPawns;
        public Ludoboard()
        {
            InitializeComponent();
            boardtiles = new PictureBox[48]
            { Box0, Box1, Box2, Box3, Box4, Box5, Box6, Box7, Box8, Box9, Box10,
                Box11, Box12, Box13, Box14, Box15, Box16, Box17, Box18, Box19, Box20, Box21, Box22, Box23,
                Box24, Box25, Box26, Box27, Box28, Box29, Box30, Box31, Box32, Box33, Box34, Box35, Box36,
                Box37, Box38, Box39, Box40, Box41, Box42, Box43, Box44, Box45, Box46, Box47
            };
            specialLanes = new PictureBox[16]
            { BoxG1, BoxG2, BoxG3, BoxG4, BoxR1, BoxR2, BoxR3, BoxR4,
               BoxY1, BoxY2, BoxY3, BoxY4, BoxB1, BoxB2, BoxB3, BoxB4
            };
            allNests = new PictureBox[16]
            { GN1, GN2, GN3, GN4, RN1, RN2, RN3, RN4, YN1, YN2, YN3, YN4, BN1, BN2, BN3, BN4 };
            allPawns = new PictureBox[16]
            { GP1, GP2, GP3, GP4, RP1, RP2, RP3, RP4, YP1, YP2, YP3, YP4, BP1, BP2, BP3, BP4 };
            diceImages = new Image[7]
            {
                Properties.Resources.Dice_blank,
                Properties.Resources.Dice_1,
                Properties.Resources.Dice_2,
                Properties.Resources.Dice_3,
                Properties.Resources.Dice_4,
                Properties.Resources.Dice_5,
                Properties.Resources.Dice_6
            };
        }

        //Dice
        Image[] diceImages;
        int dice;
        int dice1;
        int dice2;
        int dice3;
        int dice4;
        Random random = new Random();
        private void Ludoboard_Load(object sender, EventArgs e)
        {
            rollDice.Visible = false;
        }

        public void GameStart()
        {
            int ActivePlayerID = 0;
            List<Player> NewGameList = CreatePlayerList();
            FirstRoll(NewGameList);
            var HighestRoll = NewGameList.MaxBy(x => x.InitialDiceRoll);
            if (HighestRoll == null) { throw new Exception("Player not found"); }
            List<Player> temp = NewGameList.Where(x => x.InitialDiceRoll == HighestRoll?.InitialDiceRoll).ToList();
            if (temp.Count > 1)
            {
                ReRoll(temp);
                HighestRoll = NewGameList.MaxBy(x => x.InitialDiceRoll);
                if (HighestRoll == null) { throw new Exception("Player not found"); }
                int NewActivePlayer = HighestRoll.Id;
                label2.Text = "Oh-uh, some rolled the same! There was a re-roll \n \n The player : " 
                               + HighestRoll.colors.ToString() + " starts the game";

                ActivePlayerID = HighestRoll.Id;
                NewGameList[ActivePlayerID - 1].IsActive = true;
                foreach (Player player in NewGameList)
                {
                    if (player.IsActive == true)
                    {
                        player.PlayerPawns[0].Enabled = true;
                        player.PlayerPawns[1].Enabled = true;
                        player.PlayerPawns[2].Enabled = true;
                        player.PlayerPawns[3].Enabled = true;
                    }
                }
            }
            else
            {
                ActivePlayerID = HighestRoll.Id;
                label2.Text = NewGameList[0].colors.ToString() + " has rolled : " + NewGameList[0].InitialDiceRoll +
                " \n " + NewGameList[1].colors.ToString() + " has rolled : " + NewGameList[1].InitialDiceRoll +
                " \n " + NewGameList[2].colors.ToString() + " has rolled : " + NewGameList[2].InitialDiceRoll +
                " \n " + NewGameList[3].colors.ToString() + " has rolled : " + NewGameList[3].InitialDiceRoll +
                "\n \n The player : " + HighestRoll.colors.ToString() + " starts the game";
                
                NewGameList[ActivePlayerID - 1].IsActive = true;
                foreach (Player player in NewGameList)
                {
                    if (player.IsActive == true)
                    {
                        player.PlayerPawns[0].Enabled = true;
                        player.PlayerPawns[1].Enabled = true;
                        player.PlayerPawns[2].Enabled = true;
                        player.PlayerPawns[3].Enabled = true;
                    }
                }
            }
            
        }

        private void label2_Click(object sender, EventArgs e)
        {
            GameStart();
            rollDice.Visible = true;
            label2.Enabled = false;
        }

        private void rollDice_Click(object sender, EventArgs e)
        {
            RollDice();
        }

        private void RollDice()
        {
            dice = random.Next(1, 7);
            lbl_dice.Image = diceImages[dice];

        }

        //Pawn movements
        private static int nextPosition(int currentPosition, int diceRollValue)
        {
            return sharedCellsCount - Math.Abs(sharedCellsCount - (currentPosition + diceRollValue));
        }

        private static int globalPosition(int playerIndex, int currentIndividualPosition)
        {
            return ((playerIndex * startShift) + currentIndividualPosition) % sharedCellsCount;
        }


        private void GP1_Click(object sender, EventArgs e)
        {
            MovePawn(0);
            Nextplayer(1, 4);
        }

        private void GP2_Click(object sender, EventArgs e)
        {
            MovePawn(1);
            Nextplayer(1, 4);
        }

        private void GP3_Click(object sender, EventArgs e)
        {
            MovePawn(2);
            Nextplayer(1, 4);
        }

        private void GP4_Click(object sender, EventArgs e)
        {
            MovePawn(3);
            Nextplayer(1, 4);
        }

        private void RP1_Click(object sender, EventArgs e)
        {
            MovePawn(4);
            Nextplayer(2, 4);
        }

        private void RP2_Click(object sender, EventArgs e)
        {
            MovePawn(5);
            Nextplayer(2, 4);
        }

        private void RP3_Click(object sender, EventArgs e)
        {
            MovePawn(6);
            Nextplayer(2, 4);
        }

        private void RP4_Click(object sender, EventArgs e)
        {
            MovePawn(7);
            Nextplayer(2, 4);
        }

        private void YP1_Click(object sender, EventArgs e)
        {
            MovePawn(8);
            Nextplayer(3, 4);
        }

        private void YP2_Click(object sender, EventArgs e)
        {
            MovePawn(9);
            Nextplayer(3, 4);
        }

        private void YP3_Click(object sender, EventArgs e)
        {
            MovePawn(10);
            Nextplayer(3, 4);
        }

        private void YP4_Click(object sender, EventArgs e)
        {
            MovePawn(11);
            Nextplayer(3, 4);
        }

        private void BP1_Click(object sender, EventArgs e)
        {
            MovePawn(12);
            Nextplayer(4, 4);
        }

        private void BP2_Click(object sender, EventArgs e)
        {
            MovePawn(13);
            Nextplayer(4, 4);
        }

        private void BP3_Click(object sender, EventArgs e)
        {
            MovePawn(14);
            Nextplayer(4, 4);
        }

        private void BP4_Click(object sender, EventArgs e)
        {
            MovePawn(15);
            Nextplayer(4, 4);
        }

       public void MovePawn(int i)
        {
            if (allPawns[i].Location == allNests[i].Location)
            {
                if (dice == 6)
                {
                    if (i<=3)
                    {
                        allPawns[i].MoveTo(Box0);
                    }
                    if (i>3 && i<=7)
                    {
                        allPawns[i].MoveTo(Box12);
                    }
                    if (i>7 && i<=11)
                    {
                        allPawns[i].MoveTo(Box24);
                    }
                    if (i>11 && i<=15)
                    {
                        allPawns[i].MoveTo(Box36);
                    }
                }
                dice=0;
            }
            else if (pawnBoxnumber[i] >= 0 && dice > 0)
            {
                if (i<=3)
                {
                    playerIndex=0;
                }
                if (i>3 && i<=7)
                {
                    playerIndex=1;
                }
                if (i>7 && i<=11)
                {
                    playerIndex=2;
                }
                if (i>11 && i<=15)
                {
                    playerIndex=3;
                }
                var individualT = nextPosition(pawnStepsMade[i], dice);

                var individualPosition = individualT;

                var globalT = globalPosition(playerIndex, individualPosition);  //0=playerindex have to figure how to define

                pawnBoxnumber[i] = globalT;
                pawnStepsMade[i] += dice;
                    //individualT;

                if (pawnStepsMade[i]<=46)
                {
                    allPawns[i].MoveTo(boardtiles[pawnBoxnumber[i]]);
                    
                }
                if (pawnStepsMade[i] > 46 && pawnStepsMade[i] <51)
                {
                    allPawns[i].MoveTo(specialLanes[pawnStepsMade[i]-47+(playerIndex*4)]);
                   
                }

                if (pawnStepsMade[i] >= 51) 
                {
                    allPawns[i].MoveTo(Home);
                    allPawns[i].Visible = false;
                }
                dice = 0;
            }
            label1.Text = allPawns[i].Name.ToString() + " has made " + pawnStepsMade[i].ToString() + 
                " steps";
           
       }

        private void Player1_Click(object sender, EventArgs e)
        {
            RollDice();
            dice1 = dice;
            Player1.Enabled = false;
        }

        private void Player2_Click(object sender, EventArgs e)
        {
            RollDice();
            dice2 = dice;
            Player2.Enabled = false;

        }

        private void Player3_Click(object sender, EventArgs e)
        {
            RollDice();
            dice3 = dice;
            Player3.Enabled = false;
        }

        private void Player4_Click(object sender, EventArgs e)
        {
            RollDice();
            dice4 = dice;
            Player4.Enabled = false;
        }

        public void Nextplayer(int activePlayerID, int maxplayers) //int activePlayer = player ID
        {
            List<Player> PlayerList = CreatePlayerList();
            PlayerList[activePlayerID - 1].IsActive = false;
            int NewActivePlayerID = activePlayerID % maxplayers + 1;
            PlayerList[NewActivePlayerID - 1].IsActive = true;
            foreach (Player player in PlayerList)
            {
                if (player.IsActive == true)
                {
                    player.PlayerPawns[0].Enabled = true;
                    player.PlayerPawns[1].Enabled = true;
                    player.PlayerPawns[2].Enabled = true;
                    player.PlayerPawns[3].Enabled = true;
                }
                else
                {
                    player.PlayerPawns[0].Enabled = false;
                    player.PlayerPawns[1].Enabled = false;
                    player.PlayerPawns[2].Enabled = false;
                    player.PlayerPawns[3].Enabled = false;
                }
            }
            label2.Text = PlayerList[NewActivePlayerID - 1].colors.ToString() + " : rolls next";
        }

        private List<Player> CreatePlayerList() //creates player list at the start of the game
        {
            Player player1 = new Player(1, "player 1", Player.Colors.Green, 0, false, GP1, GP2, GP3, GP4);
            Player player2 = new Player(2, "player 2", Player.Colors.Red, 0, false, RP1, RP2, RP3, RP4);
            Player player3 = new Player(3, "player 3", Player.Colors.Yellow, 0, false, YP1, YP2, YP3, YP4);
            Player player4 = new Player(4, "player 4", Player.Colors.Blue, 0, false, BP1, BP2, BP3, BP4);

            List<Player> PlayerList = new List<Player>();

            PlayerList.Add(player1);
            PlayerList.Add(player2);
            PlayerList.Add(player3);
            PlayerList.Add(player4);

            return PlayerList;
        }

        private List<Player> FirstRoll(List<Player> NewGameList)
        {
            int[] DiceRoll = new[] { dice1, dice2, dice3, dice4 };

            NewGameList[0].InitialDiceRoll = DiceRoll[0];
            NewGameList[1].InitialDiceRoll = DiceRoll[1];
            NewGameList[2].InitialDiceRoll = DiceRoll[2];
            NewGameList[3].InitialDiceRoll = DiceRoll[3];

            return NewGameList;
        }

        private List<Player> ReRoll(List<Player> NewGameList)
        {
            var HighestRoll = NewGameList.MaxBy(x => x.InitialDiceRoll);
            List<Player> temp = NewGameList.Where(x => x.InitialDiceRoll == HighestRoll?.InitialDiceRoll).ToList();
            foreach (Player player in temp)
            {
                RollDice();
                player.InitialDiceRoll = dice;
            }
            return NewGameList;
        }

    }
}