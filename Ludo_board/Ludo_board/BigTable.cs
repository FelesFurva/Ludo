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
        int playerCount = 4;

        //coordinates for all board spaces to walk on [0-3 = green, 4-7 = red, 8-11 = yellow, 12-15 = blue]
        PictureBox[] boardtiles;
        PictureBox[] specialLanes;
        PictureBox[] allNests;
        PictureBox[] allPawns;
        Label[] PlayerLabelList;

        //Log Manager
        LogManager logManager;

        public Ludoboard(int PlayerCount)
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

            playerCount = PlayerCount;

            AllPlayers = CreatePlayerList();

            DatabaseConnection databaseConnection = new DatabaseConnection();
            logManager = new LogManager(databaseConnection);
            
            PlayerList = new List<Player>();

            for (int i = 0; i < playerCount; i++)
            {
                PlayerList.Add(AllPlayers[i]);
            }
            TurnPlayerButtonsOn();
            label2.Text = "Click on the player name for the initial dice roll!" +
              "\nThen click here to find out who starts first!";
        }

        //Dice
        Image[] diceImages;
        int dice;
        int dice1;
        int dice2;
        int dice3;
        int dice4;
        Random random = new Random();
        List<Player> AllPlayers;
        List<Player> PlayerList;

        private void Ludoboard_Load(object sender, EventArgs e)
        {
            rollDice.Visible = false;
        }

        //private void PwnClick(object sender, EventArgs e)
        //{
        //    var pwnImageBox = sender as PictureBox;
        //    int i = 0;
        //    for (; allPawns[i] != pwnImageBox && i < allPawns.Length; i++); 

        //    MovePawn(i);
        //    Nextplayer();
        //}

        public void GameStart()
        {
            FirstRoll(PlayerList);
            var HighestRoll = PlayerList.MaxBy(x => x.InitialDiceRoll);
            if (HighestRoll == null) { throw new Exception("Player not found"); }

            List<Player> temp = PlayerList.Where(x => x.InitialDiceRoll == HighestRoll?.InitialDiceRoll).ToList();
            if (temp.Count > 1)
            {         
                ReRoll(temp);
                int NewActivePlayer = HighestRoll.Id;
                string ReRollsLog = "These players got the same high roll! \nHere's the re-roll:\n" +
                                    string.Join("\n", temp.Select(T => $"{T.Color} has rolled : {T.InitialDiceRoll}")) +
                                              $"\n \n The player :{HighestRoll.Color} starts the game";
                label1.ForeColor = PlayerLabelList[HighestRoll.Id - 1].ForeColor;
                label1.Text = ReRollsLog;

                ActivePlayerID = HighestRoll.Id;
                dice1 = 0;
                dice2 = 0;
                dice3 = 0;
                dice4 = 0;
                label2.Visible = false;
                rollDice.Visible = true;
            }
            else
            {
                ActivePlayerID = HighestRoll.Id;

                string RollsLog = string.Join("\n", PlayerList.Select(ngl => $"{ngl.Color} has rolled : {ngl.InitialDiceRoll}")) +
                                              $"\n \n The player : {HighestRoll.Color} starts the game";
                label1.Text = RollsLog;
                label1.ForeColor = PlayerLabelList[ActivePlayerID - 1].ForeColor;
                label2.Visible = false;
                rollDice.Visible = true;
            }
            PlayerList[ActivePlayerID - 1].IsActive = true;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            GameStart();
            dice=0;
        }

        private void rollDice_Click(object sender, EventArgs e)
        {
            RollDice();
            rollDice.Visible = false;
        }

        private void RollDice()
        {
            dice = random.Next(4, 7);
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

        //pawn location offsets on the white squares
        private void GPoffset(int i)
        {
            if (allPawns[i].Location != allNests[i].Location)
            {
                allPawns[i].Left += 1;
                allPawns[i].Top +=1;
            }
        }
        private void RPoffset(int i)
        {
            if (allPawns[i].Location != allNests[i].Location)
            {
                allPawns[i].Left += 19;
                allPawns[i].Top +=1;
            }
        }
        private void YPoffset(int i)
        {
            if (allPawns[i].Location != allNests[i].Location)
            {
                allPawns[i].Left += 19;
                allPawns[i].Top +=19;
            }
        }
        private void BPoffset(int i)
        {
            if (allPawns[i].Location != allNests[i].Location)
            {
                allPawns[i].Left += 1;
                allPawns[i].Top +=19;
            }
        }

        //option to choose which pawn to move by clicking on them
        //can't accidentally skip a turn by clicking a pawn in nest if there are active pawns

        private void GP1_Click(object sender, EventArgs e)
        {
                       
            if (dice!=0)
            {
                if (GP1.Location == GN1.Location && dice!=6)
                {
                    if((GP2.Location != GN2.Location && GP2.Visible == true) ||
                        (GP3.Location != GN3.Location && GP3.Visible == true) ||
                        (GP4.Location != GN4.Location && GP4.Visible == true))
                    {
                        label1.Text = "Click on an active pawn!";
                    }
                    else
                    {
                        MovePawn(0);
                        Nextplayer();
                        GPoffset(0);
                    }
                }
                else 
                {
                    MovePawn(0);
                    Nextplayer();
                    GPoffset(0);
                }
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
                if (GP2.Location == GN2.Location && dice!=6) 
                {
                    if((GP1.Location != GN1.Location && GP1.Visible == true) ||
                        (GP3.Location != GN3.Location && GP3.Visible == true) ||
                        (GP4.Location != GN4.Location && GP4.Visible == true))
                    {
                        label1.Text = "Click on an active pawn!";
                    }
                    else
                    {
                        MovePawn(1);
                        Nextplayer();
                        GPoffset(1);
                    }
                }
                else
                {
                    MovePawn(1);
                    Nextplayer();
                    GPoffset(1);
                }
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
                if (GP3.Location == GN3.Location && dice!=6)
                {
                    if ((GP1.Location != GN1.Location && GP1.Visible == true) ||
                        (GP2.Location != GN2.Location && GP2.Visible == true) ||
                        (GP4.Location != GN4.Location && GP4.Visible == true))
                    {
                        label1.Text = "Click on an active pawn!";
                    }
                    else
                    {
                        MovePawn(2);
                        Nextplayer();
                        GPoffset(2);
                    }
                }
                else
                {
                    MovePawn(2);
                    Nextplayer();
                    GPoffset(2);
                }
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
                if (GP4.Location == GN4.Location && dice!=6)
                {
                    if ((GP1.Location != GN1.Location && GP1.Visible == true) ||
                         (GP3.Location != GN3.Location && GP3.Visible == true) ||
                         (GP4.Location != GN4.Location && GP4.Visible == true))
                    {
                        label1.Text = "Click on an active pawn!";
                    }
                    else
                    {
                        MovePawn(3);
                        Nextplayer();
                        GPoffset(3);
                    }
                }
                else
                {
                    MovePawn(3);
                    Nextplayer();
                    GPoffset(3);
                }
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
                if (RP1.Location == RN1.Location && dice!=6)
                {
                    if((RP4.Location != RN4.Location && RP4.Visible ==true) ||
                        (RP2.Location != RN2.Location && RP2.Visible == true) ||
                        (RP3.Location != RN3.Location && RP3.Visible == true))
                    {
                        label1.Text = "Click on an active pawn!";
                    }
                    else
                    {
                        MovePawn(4);
                        Nextplayer();
                        RPoffset(4);
                    }
                }
                else
                {
                    MovePawn(4);
                    Nextplayer();
                    RPoffset(4);
                }
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
                if (RP2.Location == RN2.Location && dice!=6)
                {
                    if((RP4.Location != RN4.Location && RP4.Visible ==true) ||
                        (RP1.Location != RN1.Location && RP1.Visible == true) ||
                        (RP3.Location != RN3.Location && RP3.Visible == true))
                    {
                        label1.Text = "Click on an active pawn!";
                    }
                    else
                    {
                        MovePawn(5);
                        Nextplayer();
                        RPoffset(5);
                    }
                }
                else
                {
                    MovePawn(5);
                    Nextplayer();
                    RPoffset(5);
                }
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
                if (RP3.Location == RN3.Location && dice!=6)
                {
                    if ((RP4.Location != RN4.Location && RP4.Visible ==true) ||
                        (RP1.Location != RN1.Location && RP1.Visible == true) ||
                        (RP2.Location != RN2.Location && RP2.Visible == true))
                    {
                        label1.Text = "Click on an active pawn!";
                    }
                    else
                    {
                        MovePawn(6);
                        Nextplayer();
                        RPoffset(6);
                    }
                }
                else
                {
                    MovePawn(6);
                    Nextplayer();
                    RPoffset(6);
                }
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
                if(RP4.Location == RN4.Location && dice!=6)
                {
                    if ((RP3.Location != RN3.Location && RP3.Visible ==true) ||
                        (RP1.Location != RN1.Location && RP1.Visible == true) ||
                        (RP2.Location != RN2.Location && RP2.Visible == true))
                    {
                        label1.Text = "Click on an active pawn!";
                    }
                    else
                    {
                        MovePawn(7);
                        Nextplayer();
                        RPoffset(7);
                    }
                }
                else
                {
                    MovePawn(7);
                    Nextplayer();
                    RPoffset(7);
                }
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
                if (YP1.Location == YN1.Location && dice!=6)
                {
                    if ((YP4.Location != YN4.Location && YP4.Visible ==true) ||
                        (YP2.Location != YN2.Location && YP2.Visible == true) ||
                        (YP3.Location != YN3.Location && YP3.Visible == true))
                    {
                        label1.Text = "Click on an active pawn!";
                    }
                    else
                    {
                        MovePawn(8);
                        Nextplayer();
                        RPoffset(8);
                    }
                }
                else
                {
                    MovePawn(8);
                    Nextplayer();
                    YPoffset(8);
                }
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
                if (YP2.Location == YN2.Location && dice!=6)
                {
                    if ((YP4.Location != YN4.Location && YP4.Visible ==true) ||
                        (YP1.Location != YN1.Location && YP1.Visible == true) ||
                        (YP3.Location != YN3.Location && YP3.Visible == true))
                    {
                        label1.Text = "Click on an active pawn!";
                    }
                    else
                    {
                        MovePawn(9);
                        Nextplayer();
                        RPoffset(9);
                    }
                }
                else
                {
                    MovePawn(9);
                    Nextplayer();
                    YPoffset(9);
                }
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
                if (YP3.Location == YN3.Location && dice!=6)
                {
                    if ((YP4.Location != YN4.Location && YP4.Visible ==true) ||
                        (YP2.Location != YN2.Location && YP2.Visible == true) ||
                        (YP1.Location != YN1.Location && YP1.Visible == true))
                    {
                        label1.Text = "Click on an active pawn!";
                    }
                    else
                    {
                        MovePawn(10);
                        Nextplayer();
                        RPoffset(10);
                    }
                }
                else
                {
                    MovePawn(10);
                    Nextplayer();
                    YPoffset(10);
                }
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
                if (YP4.Location == YN4.Location && dice!=6)
                {
                    if ((YP1.Location != YN1.Location && YP1.Visible ==true) ||
                        (YP2.Location != YN2.Location && YP2.Visible == true) ||
                        (YP3.Location != YN3.Location && YP3.Visible == true))
                    {
                        label1.Text = "Click on an active pawn!";
                    }
                    else
                    {
                        MovePawn(11);
                        Nextplayer();
                        RPoffset(11);
                    }
                }
                else
                {
                    MovePawn(11);
                    Nextplayer();
                    YPoffset(11);
                }
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
                if (BP1.Location == BN1.Location && dice!=6)
                {
                    if ((BP4.Location != BN4.Location && BP4.Visible ==true) ||
                        (BP2.Location != BN2.Location && BP2.Visible == true) ||
                        (BP3.Location != BN3.Location && BP3.Visible == true))
                    {
                        label1.Text = "Click on an active pawn!";
                    }
                    else
                    {
                        MovePawn(12);
                        Nextplayer();
                        RPoffset(12);
                    }
                }
                else
                {
                    MovePawn(12);
                    Nextplayer();
                    BPoffset(12);
                }
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
                if (BP2.Location == BN2.Location && dice!=6)
                {
                    if ((BP4.Location != BN4.Location && BP4.Visible ==true) ||
                        (BP1.Location != BN1.Location && BP1.Visible == true) ||
                        (BP3.Location != BN3.Location && BP3.Visible == true))
                    {
                        label1.Text = "Click on an active pawn!";
                    }
                    else
                    {
                        MovePawn(13);
                        Nextplayer();
                        RPoffset(13);
                    }
                }
                else
                {
                    MovePawn(13);
                    Nextplayer();
                    BPoffset(13);
                }
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
                if (BP3.Location == BN3.Location && dice!=6)
                {
                    if ((BP4.Location != BN4.Location && BP4.Visible ==true) ||
                        (BP2.Location != BN2.Location && BP2.Visible == true) ||
                        (BP1.Location != BN1.Location && BP1.Visible == true))
                    {
                        label1.Text = "Click on an active pawn!";
                    }
                    else
                    {
                        MovePawn(14);
                        Nextplayer();
                        RPoffset(14);
                    }
                }
                else
                {
                    MovePawn(14);
                    Nextplayer();
                    BPoffset(14);
                }
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
                if (BP4.Location == BN4.Location && dice!=6)
                {
                    if ((BP1.Location != BN1.Location && BP1.Visible ==true) ||
                        (BP2.Location != BN2.Location && BP2.Visible == true) ||
                        (BP3.Location != BN3.Location && BP3.Visible == true))
                    {
                        label1.Text = "Click on an active pawn!";
                    }
                    else
                    {
                        MovePawn(15);
                        Nextplayer();
                        RPoffset(15);
                    }
                }
                else
                {
                    MovePawn(15);
                    Nextplayer();
                    BPoffset(15);
                }
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
                        allPawns[i].BackColor = Box0.BackColor;                       
                    }
                    if (i>3 && i<=7)
                    {
                        allPawns[i].MoveTo(Box12);
                        allPawns[i].BackColor = Box12.BackColor;
                    }
                    if (i>7 && i<=11)
                    {
                        allPawns[i].MoveTo(Box24);
                        allPawns[i].BackColor = Box24.BackColor;
                    }
                    if (i>11 && i<=15)
                    {
                        allPawns[i].MoveTo(Box36);
                        allPawns[i].BackColor = Box36.BackColor;
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
                    allPawns[i].BackColor = boardtiles[pawnBoxnumber[i]].BackColor;
                }
                if (pawnStepsMade[i] > 46 && pawnStepsMade[i] <51)
                {
                    allPawns[i].MoveTo(specialLanes[pawnStepsMade[i]-47+(playerIndex*4)]);
                    allPawns[i].BackColor = specialLanes[pawnStepsMade[i]-47+(playerIndex*4)].BackColor;
                }

                if (pawnStepsMade[i] >= 51) 
                {
                    allPawns[i].MoveTo(Home);
                    allPawns[i].Visible = false;
                }
                dice = 0;
            }
            int id = i + 1;
            string LableText = $"{allPawns[i].Name} has made {pawnStepsMade[i]} steps";
            label1.Text = LableText;
            logManager.UpdateMovementLog(id, LableText);
        }

        public bool IsPlayerLableStatus => Player1.Enabled == false && Player2.Enabled == false && Player3.Enabled == false && Player4.Enabled == false;

        private void Player1_Click(object sender, EventArgs e)
        {
            RollDice();
            dice1 = dice;
            Player1.Enabled = false;
            if (IsPlayerLableStatus)
            { 
                label2.Enabled = true;
            }
        }

        private void Player2_Click(object sender, EventArgs e)
        {
            RollDice();
            dice2 = dice;
            Player2.Enabled = false;
            if (IsPlayerLableStatus)
            {
                label2.Enabled = true;
            }
        }

        private void Player3_Click(object sender, EventArgs e)
        {
            RollDice();
            dice3 = dice;
            Player3.Enabled = false;
            if (IsPlayerLableStatus)
            {
                label2.Enabled = true;
            }
        }

        private void Player4_Click(object sender, EventArgs e)
        {
            RollDice();
            dice4 = dice;
            Player4.Enabled = false;
            if (IsPlayerLableStatus)
            {
                label2.Enabled = true;
            }
        }

        public void Nextplayer() //int activePlayer = player ID
        {
            rollDice.Visible = true;

            PlayerList[ActivePlayerID - 1].IsActive = false; // de-activating previous player
            ActivePlayerID = ActivePlayerID % playerCount + 1; // getting id for next player
            
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
                    Gameover.Text = $"Game Over! \n {player.Color} has won!";   //make it as messagebox?
                    return;
                }
            }
            label1.ForeColor = PlayerLabelList[ActivePlayerID-1].ForeColor;
            label1.Text = $"It is {PlayerList[ActivePlayerID - 1].Color}'s time to move.";
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
            return AllPlayers;
        }

        private void TurnPlayerButtonsOn()
        {
            foreach (Player player in PlayerList)
            {
                PlayerLabelList[player.Id - 1].Enabled = true;
            }
        }

        private List<Player> FirstRoll(List<Player> NewGameList)
        {
            int[] DiceRoll = new[] { dice1, dice2, dice3, dice4 };

            foreach (Player player in NewGameList)
            {
                player.InitialDiceRoll = DiceRoll[player.Id-1];
            }
            return NewGameList;
        }

        private List<Player> ReRoll(List<Player> NewGameList)
        {
            rollDice.Visible = false;
            var HighestRoll = NewGameList.MaxBy(x => x.InitialDiceRoll);
            List<Player> temp = NewGameList.Where(x => x.InitialDiceRoll == HighestRoll?.InitialDiceRoll).ToList();
    
            foreach (Player player in temp)
            {
                PlayerLabelList[player.Id - 1].Enabled = true;
            }
            label2.Visible = true;
            string ReRolls = "These players got the same high roll! :\n" + string.Join("\n", temp.Select(T => $"{T.Color}")) + "\nPlease roll again!";
            label2.Text = ReRolls;
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
    }
}