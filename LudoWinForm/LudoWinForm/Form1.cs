namespace LudoWinForm
{
    public partial class Form1 : Form
    {
        Image[] diceImages;
        int dice;
        Random random = new Random();
        int[,] pawnposition;
        

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            diceImages = new Image[7];
            diceImages[0] = Properties.Resources.Dice_blank;
            diceImages[1] = Properties.Resources.Dice_1;
            diceImages[2] = Properties.Resources.Dice_2;
            diceImages[3] = Properties.Resources.Dice_3;
            diceImages[4] = Properties.Resources.Dice_4;
            diceImages[5] = Properties.Resources.Dice_5;
            diceImages[6] = Properties.Resources.Dice_6;

            pawnposition = new int[7,2] { 
                                        { lbl_sqr0.Location.X, lbl_sqr0.Location.Y },
                                        { lbl_sqr1.Location.X, lbl_sqr1.Location.Y },
                                        { lbl_sqr2.Location.X, lbl_sqr2.Location.Y },
                                        { lbl_sqr3.Location.X, lbl_sqr3.Location.Y },
                                        { lbl_sqr4.Location.X, lbl_sqr4.Location.Y },
                                        { lbl_sqr5.Location.X, lbl_sqr5.Location.Y },
                                        { lbl_sqr6.Location.X, lbl_sqr6.Location.Y },
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
            pawn1.Left = pawnposition[dice, 0];
            pawn1.Top = pawnposition[dice, 1];
        }

        private void lbl_sqr0_Click(object sender, EventArgs e)
        {

        }

        private void lbl_sqr2_Click(object sender, EventArgs e)
        {

        }

        private void pawn1_Click(object sender, EventArgs e)
        {

        }
    }
}