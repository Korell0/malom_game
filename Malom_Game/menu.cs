using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Malom_Game
{
    public partial class menu : Form
    {
        public menu()
        {
            InitializeComponent();            
        }


        private void player1Name_TextChanged(object sender, EventArgs e)
        {
            NoiceCheck(player1Name);
        }

        private void player2Name_TextChanged(object sender, EventArgs e)
        {
            NoiceCheck(player2Name);
        }

        private void NoiceCheck(TextBox playerName)
        {
            if (playerName.Text.Length == 0 || player2Name.Text == player1Name.Text)
            {
                Start_Btn.Enabled = false;
            }
            else
            {
                Start_Btn.Enabled = true;
            }
        }
        private void Start_Btn_Click(object sender, EventArgs e)
        {
            List<string> nevek = new List<string>();
            foreach (TextBox item in new List<TextBox>() { player1Name, player2Name })
            {
                nevek.Add(item.Text);
            }
            new Form1(nevek).Show();
            this.Hide();
        }
    }
}
