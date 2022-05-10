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
        List<Player> PlayerList;
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

        private List<Player> CreatePlayerList()
        {
            Player player1 = new Player(1, "player 1", Player.Colors.Green, 0);
            Player player2 = new Player(2, "player 2", Player.Colors.Red, 0);
            Player player3 = new Player(3, "player 3", Player.Colors.Yellow, 0);
            Player player4 = new Player(4, "player 4", Player.Colors.Blue, 0);
            
            List<Player> PlayerList = new List<Player>();
            
            PlayerList.Add(player1);
            PlayerList.Add(player2);
            PlayerList.Add(player3);
            PlayerList.Add(player4);
            
            return PlayerList;
        }

        private List<Player> GameStart()
        {
            List<Player> NewGameList = CreatePlayerList();
            NewGameList[0].InitialDiceRoll = dice1;
            NewGameList[1].InitialDiceRoll = dice2;
            NewGameList[2].InitialDiceRoll = dice3;
            NewGameList[3].InitialDiceRoll = dice4;

            var HighestRoll = NewGameList.MaxBy(x => x.InitialDiceRoll);
            List<Player> temp = NewGameList.Where(x => x.InitialDiceRoll == HighestRoll.InitialDiceRoll).ToList();
            //if (temp.Count > 1)
            //{
            //    foreach (Player player in temp)
            //    {
            //        player.InitialDiceRoll = dice;
            //    }
            //    HighestRoll = PlayerList.MaxBy(x => x.InitialDiceRoll);
            //}

            int ActivePlayer = HighestRoll.Id;

            label2.Text = NewGameList[0].colors.ToString() + " has rolled : " + NewGameList[0].InitialDiceRoll +
                " \n " + NewGameList[1].colors.ToString() + " has rolled : " + NewGameList[1].InitialDiceRoll +
                " \n " + NewGameList[2].colors.ToString() + " has rolled : " + NewGameList[2].InitialDiceRoll +
                " \n " + NewGameList[3].colors.ToString() + " has rolled : " + NewGameList[3].InitialDiceRoll +
            "\n \n The player : " + HighestRoll.colors.ToString() + " starts the game";



            return NewGameList;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            GameStart();

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
        }

        private void GP2_Click(object sender, EventArgs e)
        {
            MovePawn(1);
        }

        private void GP3_Click(object sender, EventArgs e)
        {
            MovePawn(2);
        }

        private void GP4_Click(object sender, EventArgs e)
        {
            MovePawn(3);
        }

        private void RP1_Click(object sender, EventArgs e)
        {
            MovePawn(4);
        }

        private void RP2_Click(object sender, EventArgs e)
        {
            MovePawn(5);
        }

        private void RP3_Click(object sender, EventArgs e)
        {
            MovePawn(6);
        }

        private void RP4_Click(object sender, EventArgs e)
        {
            MovePawn(7);
        }

        private void YP1_Click(object sender, EventArgs e)
        {
            MovePawn(8);
        }

        private void YP2_Click(object sender, EventArgs e)
        {
            MovePawn(9);
        }

        private void YP3_Click(object sender, EventArgs e)
        {
            MovePawn(10);
        }

        private void YP4_Click(object sender, EventArgs e)
        {
            MovePawn(11);
        }

        private void BP1_Click(object sender, EventArgs e)
        {
            MovePawn(12);
        }

        private void BP2_Click(object sender, EventArgs e)
        {
            MovePawn(13);
        }

        private void BP3_Click(object sender, EventArgs e)
        {
            MovePawn(14);
        }

        private void BP4_Click(object sender, EventArgs e)
        {
            MovePawn(15);
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
            dice1 = dice;
        }

        private void Player2_Click(object sender, EventArgs e)
        {
            dice2 = dice;

        }

        private void Player3_Click(object sender, EventArgs e)
        {
            dice3 = dice;

        }

        private void Player4_Click(object sender, EventArgs e)
        {
            dice4 = dice;

        }

        private static int Nextplayer(int activePlayer, int maxplayers) //int activePlayer = player ID
        {
            return activePlayer % maxplayers + 1;
        }

    }
}