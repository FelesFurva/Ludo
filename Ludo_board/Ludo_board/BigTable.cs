namespace Ludo_board
{
    public partial class Ludoboard : Form
    {
        /*Rectangle landingSpace; //to identify the landing location of pawns
        //fieldspace for ech pawn
        Rectangle player1pawn1;
        Rectangle player1pawn2;
        Rectangle player1pawn3;
        Rectangle player1pawn4;
        Rectangle player2pawn1;
        Rectangle player2pawn2;
        Rectangle player2pawn3;
        Rectangle player2pawn4;
        Rectangle player3pawn1;
        Rectangle player3pawn2;
        Rectangle player3pawn3;
        Rectangle player3pawn4;
        Rectangle player4pawn1;
        Rectangle player4pawn2;
        Rectangle player4pawn3;
        Rectangle player4pawn4;
        
        //lists for each player's game field
        List<Rectangle> gamePlayer1 = new List<Rectangle>();
        List<Rectangle> gamePlayer2 = new List<Rectangle>();
        List<Rectangle> gamePlayer3 = new List<Rectangle>();
        List<Rectangle> gamePlayer4 = new List<Rectangle>();
        */

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
        }

        //Dice
        Image[] diceImages;
        int dice;
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
            MovePawn(0,0);
        }

        private void GP2_Click(object sender, EventArgs e)
        {
            MovePawn(1,0);
        }

        private void GP3_Click(object sender, EventArgs e)
        {
            MovePawn(2,0);
        }

        private void GP4_Click(object sender, EventArgs e)
        {
            MovePawn(3,0);
        }

        private void RP1_Click(object sender, EventArgs e)
        {
            MovePawn(4,12);
        }

        private void RP2_Click(object sender, EventArgs e)
        {
            MovePawn(5,12);
        }

        private void RP3_Click(object sender, EventArgs e)
        {
            MovePawn(6,12);
        }

        private void RP4_Click(object sender, EventArgs e)
        {
            MovePawn(7, 12);
        }

        private void YP1_Click(object sender, EventArgs e)
        {
            MovePawn(8, 24);
        }

        private void YP2_Click(object sender, EventArgs e)
        {
            MovePawn(9,24);
        }

        private void YP3_Click(object sender, EventArgs e)
        {
            MovePawn(10,24);
        }

        private void YP4_Click(object sender, EventArgs e)
        {
            MovePawn(11,24);
        }

        private void BP1_Click(object sender, EventArgs e)
        {
            MovePawn(12,36);
        }

        private void BP2_Click(object sender, EventArgs e)
        {
            MovePawn(13,36);
        }

        private void BP3_Click(object sender, EventArgs e)
        {
            MovePawn(14,36);
        }

        private void BP4_Click(object sender, EventArgs e)
        {
            MovePawn(15,36);
        }

       public void MovePawn(int i, int j)
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

                if (pawnStepsMade[i]<=47)
                {
                    allPawns[i].MoveTo(boardtiles[pawnBoxnumber[i]]);
                    
                }
                if (pawnStepsMade[i] > 47 && pawnStepsMade[i] <52)
                {
                    allPawns[i].MoveTo(specialLanes[pawnStepsMade[i]-47+(playerIndex*4)]);
                   
                }

                if (pawnStepsMade[i] >= 52) 
                {
                    allPawns[i].MoveTo(Home);
                    allPawns[i].Visible = false;
                }
                dice = 0;
            }
            label1.Text = allPawns[i].Name.ToString() + " has made " + pawnStepsMade[i].ToString() + 
                " steps and is on field " + boardtiles[pawnBoxnumber[i]].Name.ToString();
           
       }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Box34_Click(object sender, EventArgs e)
        {

        }

        private void Box22_Click(object sender, EventArgs e)
        {

        }

        private void Box46_Click(object sender, EventArgs e)
        {

        }

        private void Box10_Click(object sender, EventArgs e)
        {

        }

        private void Box24_Click(object sender, EventArgs e)
        {

        }

        private void Box36_Click(object sender, EventArgs e)
        {

        }

        private void Box12_Click(object sender, EventArgs e)
        {

        }

        private void GN1_Click(object sender, EventArgs e)
        {

        }

        private void GN2_Click(object sender, EventArgs e)
        {

        }

        private void GN3_Click(object sender, EventArgs e)
        {

        }

        private void GN4_Click(object sender, EventArgs e)
        {

        }

        private void BN3_Click(object sender, EventArgs e)
        {

        }

        private void BN1_Click(object sender, EventArgs e)
        {

        }

        private void BN4_Click(object sender, EventArgs e)
        {

        }

        private void BN2_Click(object sender, EventArgs e)
        {

        }

        private void YN3_Click(object sender, EventArgs e)
        {

        }

        private void YN4_Click(object sender, EventArgs e)
        {

        }

        private void YN1_Click(object sender, EventArgs e)
        {

        }

        private void YN2_Click(object sender, EventArgs e)
        {

        }

        private void RN4_Click(object sender, EventArgs e)
        {

        }

        private void RN2_Click(object sender, EventArgs e)
        {

        }

        private void RN3_Click(object sender, EventArgs e)
        {

        }

        private void RN1_Click(object sender, EventArgs e)
        {

        }

        private void Box40_Click(object sender, EventArgs e)
        {

        }

        private void Box28_Click(object sender, EventArgs e)
        {

        }

        private void Box33_Click(object sender, EventArgs e)
        {

        }

        private void Box32_Click(object sender, EventArgs e)
        {

        }

        private void Box31_Click(object sender, EventArgs e)
        {

        }

        private void Box30_Click(object sender, EventArgs e)
        {

        }

        private void Box29_Click(object sender, EventArgs e)
        {

        }

        private void BoxB1_Click(object sender, EventArgs e)
        {

        }

        private void BoxB2_Click(object sender, EventArgs e)
        {

        }

        private void BoxB3_Click(object sender, EventArgs e)
        {

        }

        private void Box35_Click(object sender, EventArgs e)
        {

        }

        private void Box37_Click(object sender, EventArgs e)
        {

        }

        private void Box38_Click(object sender, EventArgs e)
        {

        }

        private void Box39_Click(object sender, EventArgs e)
        {

        }

        private void BoxB4_Click(object sender, EventArgs e)
        {

        }

        private void Box20_Click(object sender, EventArgs e)
        {

        }

        private void Box21_Click(object sender, EventArgs e)
        {

        }

        private void Box17_Click(object sender, EventArgs e)
        {

        }

        private void Box27_Click(object sender, EventArgs e)
        {

        }

        private void Box26_Click(object sender, EventArgs e)
        {

        }

        private void Box25_Click(object sender, EventArgs e)
        {

        }

        private void Box23_Click(object sender, EventArgs e)
        {

        }

        private void BoxY3_Click(object sender, EventArgs e)
        {

        }

        private void BoxY2_Click(object sender, EventArgs e)
        {

        }

        private void BoxY1_Click(object sender, EventArgs e)
        {

        }

        private void Box16_Click(object sender, EventArgs e)
        {

        }

        private void Box19_Click(object sender, EventArgs e)
        {

        }

        private void Box18_Click(object sender, EventArgs e)
        {

        }

        private void Box15_Click(object sender, EventArgs e)
        {

        }

        private void Box14_Click(object sender, EventArgs e)
        {

        }

        private void Box13_Click(object sender, EventArgs e)
        {

        }

        private void Box11_Click(object sender, EventArgs e)
        {

        }

        private void BoxR4_Click(object sender, EventArgs e)
        {

        }

        private void BoxR3_Click(object sender, EventArgs e)
        {

        }

        private void BoxR2_Click(object sender, EventArgs e)
        {

        }

        private void BoxR1_Click(object sender, EventArgs e)
        {

        }

        private void Box47_Click(object sender, EventArgs e)
        {

        }

        private void Box45_Click(object sender, EventArgs e)
        {

        }

        private void Box44_Click(object sender, EventArgs e)
        {

        }

        private void Box43_Click(object sender, EventArgs e)
        {

        }

        private void Box42_Click(object sender, EventArgs e)
        {

        }

        private void Box41_Click(object sender, EventArgs e)
        {

        }

        private void Box8_Click(object sender, EventArgs e)
        {

        }

        private void Box9_Click(object sender, EventArgs e)
        {

        }

        private void Box0_Click(object sender, EventArgs e)
        {

        }

        private void Box1_Click(object sender, EventArgs e)
        {

        }

        private void Box2_Click(object sender, EventArgs e)
        {

        }

        private void Box3_Click(object sender, EventArgs e)
        {

        }

        private void Box4_Click(object sender, EventArgs e)
        {

        }

        private void Home_Click(object sender, EventArgs e)
        {

        }

        private void Box5_Click(object sender, EventArgs e)
        {

        }

        private void Box6_Click(object sender, EventArgs e)
        {

        }

        private void Box7_Click(object sender, EventArgs e)
        {

        }

        private void BoxG1_Click(object sender, EventArgs e)
        {

        }

        private void BoxG2_Click(object sender, EventArgs e)
        {

        }

        private void BoxG3_Click(object sender, EventArgs e)
        {

        }

        private void BoxG4_Click(object sender, EventArgs e)
        {

        }

        private void BoxY4_Click(object sender, EventArgs e)
        {

        }
    }
}