using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ludo_board
{
    public partial class PlayerNumberSelect : Form
    {
        public PlayerNumberSelect()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int PlayerCount = 2;
            Ludoboard GameWindow = new(PlayerCount);
            GameWindow.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int PlayerCount = 3;
            Ludoboard GameWindow = new(PlayerCount);
            GameWindow.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int PlayerCount = 4;
            Ludoboard GameWindow = new(PlayerCount);
            GameWindow.Show();
            this.Hide();
        }
    }
}
