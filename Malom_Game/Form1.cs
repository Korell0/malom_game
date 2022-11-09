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
        static int korongSize = 35;
        static int korongszam = 4;
        static Mezo[,,] Palya = new Mezo[3, 3, 3]; // sor, oszlop, z_index
        static PictureBox Aktiv = new PictureBox();
        static Jatekos Player1;
        static Jatekos Player2;
        static Jatekos ActualPlayer;
        static bool Kezdes = true;
        static PictureBox Temp = null;

        public Form1(List<string> nevek)
        {
            InitializeComponent();
            GeneratePalya();
            GeneratePlayers(nevek);
            ActualPlayer = Player1;
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
                int yHelyzet = groupbox.Location.Y + groupbox.Size.Height / 2 - (korongszam - 1) * gap / 2 - korongszam * kep.Size.Height / 2 + i * (gap + kep.Size.Height);
                kep.Location = new Point(xHelyzet, yHelyzet);
                kep.BackColor = Color.Transparent;
                kep.Image = Image.FromFile($"korong_{jatekos.KorongJel}.png");
                kep.SizeMode = PictureBoxSizeMode.Zoom;
                this.Controls.Add(kep);
                kep.BringToFront();
                kep.Name = i.ToString();
                jatekos.KezdoKorongok.Add(kep);
                Kerekit(kep);

                kep.Click += delegate (object sender, EventArgs e) { Kijelol(sender, e, kep); };
            }
        }

        private void Kijelol(object sender, EventArgs e, PictureBox kep)
        {
            if (Kezdes)
            {
                ActualPlayer = new List<Jatekos>() { Player1, Player2 }.Find(x => x.KezdoKorongok.Count(y => y.Image == kep.Image) > 0);
                Kezdes = false;
                label1.Text = ActualPlayer.Nev;
                actualPicture.Image = Image.FromFile($"korong_{ActualPlayer.KorongJel}.png");
            }

            if (ActualPlayer.KezdoKorongok.Count(x => x.Image == kep.Image) > 0)
            {
                for (int sor = 0; sor < Palya.GetLength(0); sor++)
                {
                    for (int oszlop = 0; oszlop < Palya.GetLength(1); oszlop++)
                    {
                        for (int z_index = 0; z_index < Palya.GetLength(2); z_index++)
                        {
                            Lehetoseg(sor, oszlop, z_index, kep);
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

                            kep.Click += delegate (object sender, EventArgs e) {
                                Lerak(sender, e, kep);
                                KijelolMozgat(sender, e, kep);
                            };

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

        private void KijelolMozgat(object sender, EventArgs e, PictureBox kep)
        {
            if (ActualPlayer.Lephet && ActualPlayer.JatszoKorongok.Count(x => x.Image == kep.Image) > 0)
            {
                int sor = Convert.ToInt32(kep.Name.Split('_')[1][0].ToString());
                int oszlop = Convert.ToInt32(kep.Name.Split('_')[1][1].ToString());
                int z_index = Convert.ToInt32(kep.Name.Split('_')[1][2].ToString());

                for (int i = -1; i <= 1; i++)
                {
                    Lehetoseg(sor + i, oszlop, z_index, kep);
                    Lehetoseg(sor, oszlop + i, z_index, kep);
                    if ((sor % 2 < 1 && oszlop % 2 > 0) || (sor % 2 > 0 && oszlop % 2 < 1))
                    {
                        Lehetoseg(sor, oszlop, z_index + i, kep);
                    }
                }
            }
        }



        public void Lerak(object sender, EventArgs e, PictureBox kep)
        {
            int sor = Convert.ToInt32(kep.Name.Split('_')[1][0].ToString());
            int oszlop = Convert.ToInt32(kep.Name.Split('_')[1][1].ToString());
            int z_index = Convert.ToInt32(kep.Name.Split('_')[1][2].ToString());

            if (Aktiv != null)
            {
                if (!ActualPlayer.Lephet)
                {
                    if (Palya[sor, oszlop, z_index].Szabad)
                    {
                        KepHelyCsere(kep);
                        if (ActualPlayer.KezdoKorongok.Count < 1)
                        {
                            ActualPlayer.Lephet = true;
                        }
                        ActualPlayerCsere();
                    }
                }
                else if (Palya[sor, oszlop, z_index].Szabad)
                {
                    KepHelyCsere(kep);
                }
            }
            Normalize();

        }

        private void KepHelyCsere(PictureBox kep)
        {
            if (kep.Image != Aktiv.Image)
            {
                kep.Image = Aktiv.Image;
                if (!ActualPlayer.Lephet)
                {
                    this.Controls.Remove(Aktiv);
                    ActualPlayer.JatszoKorongok.Add(Aktiv);
                    ActualPlayer.KezdoKorongok.Remove(Aktiv);
                }
                else
                {
                    Aktiv.Image = null;
                    ActualPlayerCsere();
                }
            }
        }

        private void Lehetoseg(int sor, int oszlop, int z_index, PictureBox kep)
        {
            if (!(sor < 0 || sor > Palya.GetLength(0) - 1 || oszlop < 0 || oszlop > Palya.GetLength(1) - 1 || z_index < 0 || z_index > Palya.GetLength(2) - 1))
            {
                if (Palya[sor, oszlop, z_index] != null && Palya[sor, oszlop, z_index].Kep.Image == null)
                {
                    Palya[sor, oszlop, z_index].Szabad = true;
                    Palya[sor, oszlop, z_index].Kep.BackColor = Color.Gray;
                    Aktiv = kep;

                    MakeBorder();
                }
            }
        }

        private void MakeBorder()
        {
            this.Controls.Remove(Temp);
            Temp = new PictureBox();
            Temp.Size = new Size(korongSize+15, korongSize+15);
            Temp.Location = new Point(Aktiv.Location.X - (Temp.Size.Width-Aktiv.Size.Width)/2, Aktiv.Location.Y - (Temp.Size.Height - Aktiv.Size.Height) / 2);
            Temp.Image = Properties.Resources.korong_a;
            Temp.BackColor = Color.Transparent;
            Temp.SizeMode = PictureBoxSizeMode.Zoom;

            this.Controls.Add(Temp);
            Temp.BringToFront();
            Aktiv.BringToFront();

        }

        private void ActualPlayerCsere()
        {
            ActualPlayer = new List<Jatekos>() { Player1, Player2 }.Find(x => x != ActualPlayer);
            label1.Text = ActualPlayer.Nev;
            actualPicture.Image = Image.FromFile($"korong_{ActualPlayer.KorongJel}.png");
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
                            Palya[sor, oszlop, z_index].Kep.BorderStyle = BorderStyle.None;
                        }
                    }
                }
            }
            Aktiv = null;
            this.Controls.Remove(Temp);
        }

        private void Kerekit(PictureBox pictureBox)
        {
            Rectangle r = new Rectangle(0, 0, pictureBox.Width, pictureBox.Height);
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            int d = pictureBox.Size.Width;
            gp.AddArc(r.X, r.Y, d, d, 180, 90);
            gp.AddArc(r.X + r.Width - d, r.Y, d, d, 270, 90);
            gp.AddArc(r.X + r.Width - d, r.Y + r.Height - d, d, d, 0, 90);
            gp.AddArc(r.X, r.Y + r.Height - d, d, d, 90, 90);
            pictureBox.Region = new Region(gp);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            Normalize();
        }

        private void groupBox1_MouseCaptureChanged(object sender, EventArgs e)
        {
            Normalize();
        }

        private void groupBox2_MouseCaptureChanged(object sender, EventArgs e)
        {
            Normalize();
        }
    }
}