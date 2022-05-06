namespace LudoWinForm
{
    public partial class Form1 : Form
    {
        Image[] diceImages;
        int dice;
        Random random = new Random();
        Label[] boardtiles;
        int stepsmade1 = 0;
        int stepsmade2 = 0;
        int boxnumber1 = 0;
        int boxnumber2 = 0;

        int playerIndex = 0;

        const int startShift = 0;
        const int sharedCellsCount = 6;

        PictureBox[] pawns;

        public Form1()
        {
            InitializeComponent();
            boardtiles = new Label[7] { lbl_sqr0, lbl_sqr1, lbl_sqr2, lbl_sqr3, lbl_sqr4, lbl_sqr5, lbl_sqr6 };

        }

        private void Form1_Load(object sender, EventArgs e)
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

        private void btn_rollDice_Click(object sender, EventArgs e)
        {
            RollDice();
        }

        private void RollDice()
        {
            dice = random.Next(1, 7);
            lbl_dice.Image = diceImages[dice];
            
        }

        private void lbl_sqr0_Click(object sender, EventArgs e)
        {

        }

        private void lbl_sqr2_Click(object sender, EventArgs e)
        {

        }

        private void pawn1_Click(object sender, EventArgs e)
        {
            if (pawn1.Location == nest1.Location)
            {   
                if (dice == 6)
                {
                    pawn1.MoveTo(lbl_sqr0);
                }

            }
            else if (boxnumber1 >= 0 && dice > 0)
            {
                var individualT = nextPosition(stepsmade1, dice);

                var individualPosition = individualT;

                var globalT = globalPosition(0, individualPosition);

                boxnumber1 = globalT;

                pawn1.MoveTo(boardtiles[boxnumber1]);
                stepsmade1 = individualT;

                if (stepsmade1 == 6)
                {
                    pawn1.MoveTo(nest1);
                    pawn1.Visible = false;
                }
            }
        }

        private void pawn2_Click(object sender, EventArgs e)
        {
            if (pawn2.Location == nest2.Location)
            {
                if (dice == 6)
                {
                    pawn2.MoveTo(boardtiles[0]);
                }

            }
            else if (boxnumber2 >= 0 && dice > 0)
            {

                var individualT = nextPosition(stepsmade2, dice);

                var individualPosition = individualT;

                var globalT = globalPosition(0, individualPosition);

                boxnumber2 = globalT;

                pawn2.MoveTo(boardtiles[boxnumber2]);
                stepsmade2 = individualT;

                if (stepsmade2 == 6)
                {
                    pawn2.MoveTo(nest2);
                    pawn2.Visible = false;
                }
            }
        }

        private static int nextPosition(int currentPosition, int diceRollValue)
        {
            return sharedCellsCount - Math.Abs(sharedCellsCount - (currentPosition + diceRollValue));
        }

        private static int globalPosition(int playerIndex, int currentIndividualPosition)
        {
            return ((playerIndex * startShift) + currentIndividualPosition) % sharedCellsCount;
        }

        private void lbl_sqr1_Click(object sender, EventArgs e)
        {

        }
    }
}