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
        Mezo[,,] Palya = new Mezo[3, 3, 3]; // sor, oszlop, z_index
        public Form1(List<string> nevek)
        {
            InitializeComponent();
            GeneratePalya();
            GeneratePlayers(nevek);

        }

        private void GeneratePlayers(List<string> nevek)
        {
            groupBox1.Text = nevek[0];
            groupBox2.Text = nevek[1];

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
                            kep.BackColor = Color.Transparent;
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
