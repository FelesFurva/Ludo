using StartMenu;
using Ludo_board;
using System.Data.Common;
using System.Data.SqlClient;
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
        int ActivePlayerID;
        const int startShift = 12;
        const int sharedCellsCount = 48;
        const int maxplayers = 4;

        //coordinates for all board spaces to walk on [0-3 = green, 4-7 = red, 8-11 = yellow, 12-15 = blue]
        PictureBox[] boardtiles;
        PictureBox[] specialLanes;
        PictureBox[] allNests;
        PictureBox[] allPawns;
        Label[] PlayerLabelList;

        //Log Manager
        LogManager logManager;

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
            PlayerLabelList = new Label[4]
            { Player1,Player2,Player3,Player4};

            DatabaseConnection databaseConnection = new DatabaseConnection();
            logManager = new LogManager(databaseConnection);
            //logManager.CleanLog();
        }

        //Dice
        Image[] diceImages;
        int dice;
        int dice1;
        int dice2;
        int dice3;
        int dice4;
        Random random = new Random();
        List<Player> PlayerList;
        int playerCount;

        private void Ludoboard_Load(object sender, EventArgs e)
        {
            rollDice.Visible = false;
        }

        private void PwnClick(object sender, EventArgs e)
        {
            var pwnImageBox = sender as PictureBox;
            int i = 0;
            for (; allPawns[i] != pwnImageBox && i < allPawns.Length; i++);

            MovePawn(i);
            Nextplayer();
        }

        public void GameStart()
        {
            PlayerList = CreatePlayerList();
            FirstRoll(PlayerList);
            var HighestRoll = PlayerList.MaxBy(x => x.InitialDiceRoll);
            if (HighestRoll == null) { throw new Exception("Player not found"); }
            List<Player> temp = PlayerList.Where(x => x.InitialDiceRoll == HighestRoll?.InitialDiceRoll).ToList();
            if (temp.Count > 1)
            {
                ReRoll(temp);
                HighestRoll = PlayerList.MaxBy(x => x.InitialDiceRoll);
                if (HighestRoll == null) { throw new Exception("Player not found"); }
                int NewActivePlayer = HighestRoll.Id;
                string ReRollsLog = "These players got the same high roll! \nHere's the re-roll:\n" + string.Join("\n", temp.Select(T => $"{T.Color} has rolled : {T.InitialDiceRoll}")) +
                                              "\n \n The player : " + HighestRoll.Color.ToString() + " starts the game";
                label1.ForeColor = PlayerLabelList[HighestRoll.Id - 1].ForeColor;
                label1.Text = ReRollsLog;

                ActivePlayerID = HighestRoll.Id;
                PlayerList[ActivePlayerID - 1].IsActive = true;
            }
            else
            {
                ActivePlayerID = HighestRoll.Id;

                string RollsLog = string.Join("\n", PlayerList.Select(ngl => $"{ngl.Color} has rolled : {ngl.InitialDiceRoll}")) +
                                              "\n \n The player : " + HighestRoll.Color.ToString() + " starts the game";
                label1.Text = RollsLog;
                label1.ForeColor = PlayerLabelList[ActivePlayerID - 1].ForeColor;

                PlayerList[ActivePlayerID - 1].IsActive = true;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            GameStart();
            dice=0;
            rollDice.Visible = true;
            label2.Visible = false;
        }

        private void rollDice_Click(object sender, EventArgs e)
        {
            RollDice();
            rollDice.Visible = false;
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
            if (dice!=0)
            {
                MovePawn(0);
                Nextplayer();
            }
            else
            {
                label1.Text = "Roll the dice first!";
            }
        }

        private void GP2_Click(object sender, EventArgs e)
        {
            if (dice!=0)
            {
                MovePawn(1);
                Nextplayer();
            }
            else
            {
                label1.Text = "Roll the dice first!";
            }
        }

        private void GP3_Click(object sender, EventArgs e)
        {
            if (dice!=0)
            {
                MovePawn(2);
                Nextplayer();
            }
            else
            {
                label1.Text = "Roll the dice first!";
            }
        }

        private void GP4_Click(object sender, EventArgs e)
        {
            if (dice!=0)
            {
                MovePawn(3);
                Nextplayer();
            }
            else
            {
                label1.Text = "Roll the dice first!";
            }
        }

        private void RP1_Click(object sender, EventArgs e)
        {
            if (dice!=0)
            {
                MovePawn(4);
                Nextplayer();
            }
            else
            {
                label1.Text = "Roll the dice first!";
            }
        }

        private void RP2_Click(object sender, EventArgs e)
        {
            if (dice!=0)
            {
                MovePawn(5);
                Nextplayer();
            }
            else
            {
                label1.Text = "Roll the dice first!";
            }
        }

        private void RP3_Click(object sender, EventArgs e)
        {
            if (dice!=0)
            {
                MovePawn(6);
                Nextplayer();
            }
            else
            {
                label1.Text = "Roll the dice first!";
            }
        }

        private void RP4_Click(object sender, EventArgs e)
        {
            if (dice!=0)
            {
                MovePawn(7);
                Nextplayer();
            }
            else
            {
                label1.Text = "Roll the dice first!";
            }
        }

        private void YP1_Click(object sender, EventArgs e)
        {
            if (dice!=0)
            {
                MovePawn(8);
                Nextplayer();
            }
            else
            {
                label1.Text = "Roll the dice first!";
            }
        }

        private void YP2_Click(object sender, EventArgs e)
        {
            if (dice!=0)
            {
                MovePawn(9);
                Nextplayer();
            }
            else
            {
                label1.Text = "Roll the dice first!";
            }
        }

        private void YP3_Click(object sender, EventArgs e)
        {
            if (dice!=0)
            {
                MovePawn(10);
                Nextplayer();
            }
            else
            {
                label1.Text = "Roll the dice first!";
            }
        }

        private void YP4_Click(object sender, EventArgs e)
        {
            if (dice!=0)
            {
                MovePawn(11);
                Nextplayer();
            }
            else
            {
                label1.Text = "Roll the dice first!";
            }
        }

        private void BP1_Click(object sender, EventArgs e)
        {
            if (dice!=0)
            {
                MovePawn(12);
                Nextplayer();
            }
            else
            {
                label1.Text = "Roll the dice first!";
            }
        }

        private void BP2_Click(object sender, EventArgs e)
        {
            if (dice!=0)
            {
                MovePawn(13);
                Nextplayer();
            }
            else
            {
                label1.Text = "Roll the dice first!";
            }
        }

        private void BP3_Click(object sender, EventArgs e)
        {
            if (dice!=0)
            {
                MovePawn(14);
                Nextplayer();
            }
            else
            {
                label1.Text = "Roll the dice first!";
            }
        }

        private void BP4_Click(object sender, EventArgs e)
        {
            if (dice!=0)
            {
                MovePawn(15);
                Nextplayer();
            }
            else
            {
                label1.Text = "Roll the dice first!";
            }
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
            int id = i + 1;
            string LableText = allPawns[i].Name.ToString() + " has made " + pawnStepsMade[i].ToString() +
                               " steps";
            label1.Text = LableText;
            logManager.UpdateMovementLog(id, LableText);
        }

        private void Player1_Click(object sender, EventArgs e)
        {
            RollDice();
            dice1 = dice;
            Player1.Enabled = false;
            if (Player1.Enabled == false && Player2.Enabled == false && Player3.Enabled == false && Player4.Enabled == false)
            {
                label2.Enabled = true;
            }
        }

        private void Player2_Click(object sender, EventArgs e)
        {
            RollDice();
            dice2 = dice;
            Player2.Enabled = false;
            if (Player1.Enabled == false && Player2.Enabled == false && Player3.Enabled == false && Player4.Enabled == false)
            {
                label2.Enabled = true;
            }

        }

        private void Player3_Click(object sender, EventArgs e)
        {
            RollDice();
            dice3 = dice;
            Player3.Enabled = false;
            if (Player1.Enabled == false && Player2.Enabled == false && Player3.Enabled == false && Player4.Enabled == false)
            {
                label2.Enabled = true;
            }
        }

        private void Player4_Click(object sender, EventArgs e)
        {
            RollDice();
            dice4 = dice;
            Player4.Enabled = false;
            if (Player1.Enabled == false && Player2.Enabled == false && Player3.Enabled == false && Player4.Enabled == false)
            {
                label2.Enabled = true;
            }
        }

        public void Nextplayer() //int activePlayer = player ID
        {
            rollDice.Visible = true;

            PlayerList[ActivePlayerID - 1].IsActive = false; // de-activating previous player
            ActivePlayerID = ActivePlayerID % maxplayers + 1; // getting id for next player
            
            PlayerList[ActivePlayerID - 1].IsActive = true; // activating next player
            
            foreach (Player player in PlayerList)
            {
                if (player.IsAllPawnsHidden())
                {
                    foreach (PictureBox pawn in allPawns)
                    {
                        pawn.Enabled = false;
                    }
                    rollDice.Enabled = false;
                    label1.Visible = false;
                    Gameover.Text = "Game Over! \n" + player.Color.ToString() + " has won!";   //make it as messagebox?
                    return;
                }
            }
            label1.ForeColor = PlayerLabelList[ActivePlayerID-1].ForeColor;
            label1.Text = "It is " + PlayerList[ActivePlayerID - 1].Color.ToString() + "'s time to move.";
        }

        public List<Player> CreatePlayerList() //creates player list at the start of the game
        {
            Player player1 = new Player(1, "player 1", Player.PlayerColor.Green, 0, GP1, GP2, GP3, GP4);
            Player player2 = new Player(2, "player 2", Player.PlayerColor.Red, 0, RP1, RP2, RP3, RP4);
            Player player3 = new Player(3, "player 3", Player.PlayerColor.Yellow, 0, YP1, YP2, YP3, YP4);
            Player player4 = new Player(4, "player 4", Player.PlayerColor.Blue, 0, BP1, BP2, BP3, BP4);

            List<Player> AllPlayers = new List<Player>();

            AllPlayers.Add(player1);
            AllPlayers.Add(player2);
            AllPlayers.Add(player3);
            AllPlayers.Add(player4);

            List<Player> PlayerList = new List<Player>();

            for(int i = 0; i <= playerCount; i++)
            {
                PlayerList.Add(AllPlayers[i]);
            }

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

        private void LogInfo_Click(object sender, EventArgs e)
        {
            MessageBox.Show(string.Join("\n",logManager.GetAllRecords()), 
                                        "Confirmation", MessageBoxButtons.OK);
        }

       

        private void NewGame_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            playerCount = 2;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            playerCount = 3;
        }
    }
}