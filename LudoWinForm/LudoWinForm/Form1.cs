namespace LudoWinForm
{
    public partial class Form1 : Form
    {
        Image[] diceImages;
        int dice;
        Random random = new Random();
        Label[] boardtiles;
        Label[] pawns;
        Label[] nest;
        int[] stepsmade;
        int[] boxnumber;
        
        int playerIndex = 0;

        const int startShift = 0;
        const int sharedCellsCount = 6;

        public Form1()
        {
            InitializeComponent();
            boardtiles = new Label[7] { lbl_sqr0, lbl_sqr1, lbl_sqr2, lbl_sqr3, lbl_sqr4, lbl_sqr5, lbl_sqr6 };
            pawns = new Label[2] { pawn1, pawn2 };
            nest = new Label[2] { nest1, nest2 };
            boxnumber = new int[2];
            stepsmade = new int[2];
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
            pawnpath(0);
        }

        private void pawn2_Click(object sender, EventArgs e)
        {
            pawnpath(1);
        }

        private void pawnpath(int pawnindex)
        {
            if (pawns[pawnindex].Location == nest[pawnindex].Location)
            {
                if (dice == 6)
                {
                    pawns[pawnindex].MoveTo(boardtiles[0]);
                }

            }
            else if (boxnumber[pawnindex] >= 0 && dice > 0)
            {

                var individualT = nextPosition(stepsmade[pawnindex], dice);

                var individualPosition = individualT;

                var globalT = globalPosition(0, individualPosition);

                boxnumber[pawnindex] = globalT;

                pawns[pawnindex].MoveTo(boardtiles[boxnumber[pawnindex]]);
                stepsmade[pawnindex] = individualT;

                if (stepsmade[pawnindex] == 6)
                {
                    pawns[pawnindex].MoveTo(nest[pawnindex]);
                    pawns[pawnindex].Visible = false;
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