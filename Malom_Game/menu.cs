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
        static List<TextBox> jatekosnevek = new List<TextBox>();    
        public menu()
        {
            InitializeComponent();
            Input_Gen();
        }

        private void Input_Gen()
        {
            for (int i = 0; i < 2; i++)
            {
                PictureBox ujpicturebox = new PictureBox();
                this.Controls.Add(ujpicturebox);
                ujpicturebox.SizeMode = PictureBoxSizeMode.Zoom;
                ujpicturebox.Width = 20;
                if (i == 0)
                {
                    ujpicturebox.Image = Properties.Resources.korong_a;
                }
                else
                {
                    ujpicturebox.Image = Properties.Resources.korong_b;
                }

                Label ujlabel = new Label();
                ujlabel.Text = $"Player {(2 == 1 ? "" : (i + 1).ToString() + " ")}Name: ";
                ujlabel.Name = $"player{i + 1}";
                ujlabel.AutoSize = true;
                this.Controls.Add(ujlabel);

                TextBox ujtextbox = new TextBox();
                ujtextbox.Text = $"Player{i + 1}";
                this.Controls.Add(ujtextbox);

                int xHelyzet = this.Width / 2 - (ujlabel.Width + ujtextbox.Width) / 2 - 30;
                int yHelyzet = (this.Height - (ujlabel.Height * 2 + (2 - 1) * 30)) / 2 + (i * (30 + ujlabel.Height)) - 26;
                ujpicturebox.Location = new Point(xHelyzet - 20, yHelyzet - 18);
                ujlabel.Location = new Point(xHelyzet, yHelyzet);
                ujtextbox.Location = new Point(ujlabel.Width + ujlabel.Location.X, ujlabel.Location.Y - 3);

                jatekosnevek.Add(ujtextbox);
            }
        }

        private void Start_Btn_Click(object sender, EventArgs e)
        {
            List<string> nevek = new List<string>();
            foreach (TextBox item in jatekosnevek)
            {
                nevek.Add(item.Text);
            }
            new Form1(nevek.Count > 1 && new Random().Next(0, 1) > 0 ? new List<string> { nevek[1], nevek[0] } : nevek).Show();
            this.Hide();
        }
    }
}
