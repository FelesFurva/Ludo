namespace LudoWinForm
{
    public partial class Form1 : Form
    {
        Image[] diceImages;
        int dice;
        Random random = new Random();


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
    }
}