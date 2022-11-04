using System;
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
    public partial class Form1 : Form
    {
        static PictureBox Aktiv = new PictureBox();
        static int korongszam = 9; 
        static Mezo[,,] Palya = new Mezo[3, 3, 3]; // sor, oszlop, z_index
        Jatekos Player1;
        Jatekos Player2;
        public Form1(List<string> nevek)
        {
            InitializeComponent();
            GeneratePalya();
            GeneratePlayers(nevek);
            groupBox2.Enabled = false;           
        }


        private void GeneratePlayers(List<string> nevek)
        {
            Player1 = new Jatekos(nevek[0], 0);
            groupBox1.Text = nevek[0];
            GenerateKorongok(Player1, groupBox1);

            Player2 = new Jatekos(nevek[1], 1);
            groupBox2.Text = nevek[1];
            GenerateKorongok(Player2, groupBox2);

        }

        private void GenerateKorongok(Jatekos jatekos, GroupBox groupbox)
        {            
            for (int i = 0; i < korongszam; i++)
            {
                PictureBox kep = new PictureBox();
                kep.Size = new Size(35, 35);
                int gap = 10; // px
                int xHelyzet = groupbox.Location.X + groupbox.Size.Width / 2 - kep.Size.Width / 2;
                int yHelyzet = groupbox.Location.Y + groupbox.Size.Height / 2 - (korongszam - 1) * gap / 2 - korongszam * kep.Size.Height / 2 + i*(gap + kep.Size.Height);
                kep.Location = new Point(xHelyzet, yHelyzet);
                kep.BackColor = Color.Transparent;
                kep.Image = Image.FromFile($"korong_{jatekos.KorongJel}.png");
                kep.SizeMode = PictureBoxSizeMode.Zoom;
                this.Controls.Add(kep);
                kep.BringToFront();
                jatekos.KezdoKorongok.Add(kep);
            }
        }

        private void GeneratePalya()
        {
            int nullpozX = 173;
            int nullpozY = 13;
            int gap = 97;

            for (int sor = 0; sor < Palya.GetLength(0); sor++)
            {
                for (int oszlop = 0; oszlop < Palya.GetLength(1); oszlop++)
                {
                    for (int z_index = 0; z_index < Palya.GetLength(2); z_index++)
                    {
                        if (!(oszlop == 1 && sor == 1))
                        {
                            PictureBox kep = new PictureBox();
                            int xHelyzet = nullpozX + z_index * gap + (3 - z_index) * gap * oszlop;
                            int yHelyzet = nullpozY + z_index * gap + (3 - z_index) * gap * sor;
                            kep.Location = new Point(xHelyzet, yHelyzet);
                            kep.Size = new Size(35, 35);
                            kep.Name = $"kep_{sor}{oszlop}{z_index}";
                            //kep.BackColor = Color.Transparent;
                            this.Controls.Add(kep);
                            Palya[sor, oszlop, z_index] = new Mezo(kep);
                        } 
                        else
                        {
                            Palya[sor, oszlop, z_index] = null;
                        }
                    }
                }
            }
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
