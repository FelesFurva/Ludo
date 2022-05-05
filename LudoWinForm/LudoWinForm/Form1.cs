namespace LudoWinForm
{
    public partial class Form1 : Form
    {
        Image[] diceImages;
        int dice;
        Random random = new Random();
        int[,] boardcoordinates;
        int stepsmade1 = 0;
        int stepsmade2 = 0;
        int boxnumber = 0;
        int boxnumber2 = 0;

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

            boardcoordinates = new int[7,2] { 
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


                //pawn1.Left = boardcoordinates[boxnumber, 0];
                //pawn1.Top = boardcoordinates[boxnumber, 1];

        }

        private void RollDice()
        {

                dice = random.Next(5, 7);
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
            boxnumber += dice;

            pawn1.Left = boardcoordinates[boxnumber, 0];
            pawn1.Top = boardcoordinates[boxnumber, 1];

            stepsmade1 += dice;
            dice = 0;
        }

        private void pawn2_Click(object sender, EventArgs e)
        {
            if (pawn2.Left == nest2.Location.X && pawn2.Top == nest2.Location.Y)
            {
                if (dice == 6)
                {
                    pawn2.Left = boardcoordinates[0, 0];
                    pawn2.Top = boardcoordinates[0, 1];
                }
                
            }
            else if (boxnumber2 >= 0 && dice > 0)
            {
                boxnumber2 += dice;

                pawn2.Left = boardcoordinates[boxnumber2, 0];
                pawn2.Top = boardcoordinates[boxnumber2, 1];

                stepsmade2 += dice;
                dice = 0;

                if (stepsmade2 == 6)
                {
                    pawn2.Left = nest2.Location.X;
                    pawn2.Top = nest2.Location.Y;
                    pawn2.Visible = false;
                }
            }
        }
    }
}