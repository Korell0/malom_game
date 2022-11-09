﻿using System;
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
        static int korongSize = 35;
        static PictureBox Aktiv = new PictureBox();
        static int korongszam = 1; 
        static Mezo[,,] Palya = new Mezo[3, 3, 3]; // sor, oszlop, z_index
        Jatekos Player1;
        Jatekos Player2;
        Jatekos ActualPlayer;
        public Form1(List<string> nevek)
        {
            InitializeComponent();
            GeneratePalya();
            GeneratePlayers(nevek);
            groupBox2.Enabled = false;           
        }


        private void GeneratePlayers(List<string> nevek)
        {
            List<Jatekos> players = new List<Jatekos>() { Player1, Player2 };
            List<GroupBox> boxok = new List<GroupBox>() { groupBox1, groupBox2 };
            Player1 = new Jatekos(nevek[0], 0, boxok[0]);
            GenerateKorongok(Player1, boxok[0]);
            Player2 = new Jatekos(nevek[1], 1, boxok[1]);
            GenerateKorongok(Player2, boxok[1]);

            for (int i = 0; i < players.Count; i++)
            {
                boxok[i].Text = nevek[i];                
            }
        }

        private void GenerateKorongok(Jatekos jatekos, GroupBox groupbox)
        {            
            for (int i = 0; i < korongszam; i++)
            {
                PictureBox kep = new PictureBox();
                kep.Size = new Size(korongSize, korongSize);
                int gap = 10; // px
                int xHelyzet = groupbox.Location.X + groupbox.Size.Width / 2 - kep.Size.Width / 2;
                int yHelyzet = groupbox.Location.Y + groupbox.Size.Height / 2 - (korongszam - 1) * gap / 2 - korongszam * kep.Size.Height / 2 + i*(gap + kep.Size.Height);
                kep.Location = new Point(xHelyzet, yHelyzet);
                kep.BackColor = Color.Transparent;                
                kep.Image = Image.FromFile($"korong_{jatekos.KorongJel}.png");
                kep.SizeMode = PictureBoxSizeMode.Zoom;
                this.Controls.Add(kep);
                kep.BringToFront();
                kep.Name = i.ToString();
                jatekos.KezdoKorongok.Add(kep);

                kep.Click += delegate (object sender, EventArgs e) { Kijelol(sender, e, kep); };
            }
        }

        private void Kijelol(object sender, EventArgs e, PictureBox kep)
        {
            if (ActualPlayer.KezdoKorongok.Contains(kep))
            {                
                Aktiv = kep;
                for (int sor = 0; sor < Palya.GetLength(0); sor++)
                {
                    for (int oszlop = 0; oszlop < Palya.GetLength(1); oszlop++)
                    {
                        for (int z_index = 0; z_index < Palya.GetLength(2); z_index++)
                        {
                            if (Palya[sor, oszlop, z_index] != null && Palya[sor, oszlop, z_index].Kep.Image == null)
                            {
                                Palya[sor, oszlop, z_index].Kep.BackColor = Color.Gray;
                                Palya[sor, oszlop, z_index].Szabad = true;
                            }
                        }
                    }
                }
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
                            kep.Size = new Size(korongSize, korongSize);
                            kep.Name = $"kep_{sor}{oszlop}{z_index}";
                            kep.SizeMode = PictureBoxSizeMode.Zoom;
                            kep.BackColor = Color.Transparent;
                            this.Controls.Add(kep);
                            Palya[sor, oszlop, z_index] = new Mezo(kep);

                            kep.Click += delegate (object sender, EventArgs e) { Lerak(sender, e, kep); Mozgat(sender, e, kep); };

                            Kerekit(kep);
                        } 
                        else
                        {
                            Palya[sor, oszlop, z_index] = null;
                        }
                    }
                }
            }
        }

        private void Mozgat(object sender, EventArgs e, PictureBox kep)
        {
            if (ActualPlayer.Lephet && ActualPlayer.JatszoKorongok.Count(x => x.Image == kep.Image) > 0)
            {
                int sor = Convert.ToInt32(kep.Name.Split('_')[1][0].ToString());
                int oszlop = Convert.ToInt32(kep.Name.Split('_')[1][1].ToString());
                int z_index = Convert.ToInt32(kep.Name.Split('_')[1][2].ToString());

                for (int row = 0; row < Palya.GetLength(0); row++)
                {
                    if (Palya[row, oszlop, z_index] != null && Palya[row, oszlop, z_index].Kep.Image == null)
                    {
                        MessageBox.Show($"{row},{oszlop},{z_index}");
                        Palya[row, oszlop, z_index].Szabad = true;
                        Palya[row, oszlop, z_index].Kep.BackColor = Color.Gray;
                    }
                }
                for (int column = 0; column < Palya.GetLength(1); column++)
                {
                    if (Palya[sor, column, z_index] != null && Palya[sor, column, z_index].Kep.Image == null)
                    {
                        Palya[sor, column, z_index].Szabad = true;
                        Palya[sor, column, z_index].Kep.BackColor = Color.Gray;
                    }
                }
                if ((sor % 2 < 1 && oszlop % 2 > 0) || (sor % 2 > 0 && oszlop % 2 < 1))
                {
                    for (int z_coord = 0; z_coord < Palya.GetLength(2); z_coord++)
                    {
                        if (Palya[sor, oszlop, z_coord] != null && Palya[sor, oszlop, z_coord].Kep.Image == null)
                        {
                            Palya[sor, oszlop, z_coord].Szabad = true;
                            Palya[sor, oszlop, z_coord].Kep.BackColor = Color.Gray;
                        }
                    }
                }
            }
        }

        public void Lerak(object sender, EventArgs e, PictureBox kep)
        {            
            if (!ActualPlayer.Lephet && Aktiv != null)
            {
                if (kep.Image == null)
                {
                    kep.Image = Aktiv.Image;
                    this.Controls.Remove(Aktiv);
                    ActualPlayer.JatszoKorongok.Add(Aktiv);
                    ActualPlayer.KezdoKorongok.Remove(Aktiv);
                    if (ActualPlayer.KezdoKorongok.Count < 1)
                    {
                        ActualPlayer.Lephet = true;
                    }
                    ActualPlayer.Groupbox.Enabled = false;
                    ActualPlayer = new List<Jatekos>() { Player1, Player2 }.Find(x => x != ActualPlayer);
                    ActualPlayer.Groupbox.Enabled = true;
                }
                Aktiv = null;
            }
            Normalize();
            
        }

        private void Normalize()
        {
            for (int sor = 0; sor < Palya.GetLength(0); sor++)
            {
                for (int oszlop = 0; oszlop < Palya.GetLength(1); oszlop++)
                {
                    for (int z_index = 0; z_index < Palya.GetLength(2); z_index++)
                    {
                        if (Palya[sor, oszlop, z_index] != null)
                        {
                            Palya[sor, oszlop, z_index].Kep.BackColor = Color.Transparent;
                            Palya[sor, oszlop, z_index].Szabad = false;
                        }
                    }
                }
            }
        }

        private void Kerekit(PictureBox pictureBox1)
        {
            Rectangle r = new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height);
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            int d = korongSize;
            gp.AddArc(r.X, r.Y, d, d, 180, 90);
            gp.AddArc(r.X + r.Width - d, r.Y, d, d, 270, 90);
            gp.AddArc(r.X + r.Width - d, r.Y + r.Height - d, d, d, 0, 90);
            gp.AddArc(r.X, r.Y + r.Height - d, d, d, 90, 90);
            pictureBox1.Region = new Region(gp);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
