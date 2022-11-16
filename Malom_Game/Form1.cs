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
        static int korongszam = 9;
        static Mezo[,,] Palya = new Mezo[3, 3, 3]; // sor, oszlop, z_index
        static PictureBox Aktiv = new PictureBox();
        static Jatekos Player1;
        static Jatekos Player2;
        static Jatekos ActualPlayer;
        static bool Kezdes = true;
        static PictureBox Temp = CreateTemp();
        static int Malmok = 0;
        static int AnimSzam = 0;
        static PictureBox AnimaltKep = null;

        public Form1(List<string> nevek)
        {
            InitializeComponent();
            GeneratePalya();
            GeneratePlayers(nevek);
            this.Controls.Add(Temp);
        }

        private static PictureBox CreateTemp()
        {
            PictureBox temp = new PictureBox();
            temp.Visible = false;
            temp.Image = Image.FromFile("korong_a.png");
            temp.Size = new Size(korongSize + 15, korongSize + 15);
            temp.BackColor = Color.Transparent;
            temp.SizeMode = PictureBoxSizeMode.Zoom;
            return temp;
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

                kep.Click += delegate (object sender, EventArgs e) { if (Malmok < 1) { Kijelol(kep); } };
            }
        }

        private void Kijelol(PictureBox kep)
        {
            if (Kezdes)
            {
                ActualPlayer = new List<Jatekos>() { Player1, Player2 }.Find(x => x.KezdoKorongok.Count(y => y.Image == kep.Image) > 0);
                label1.Text = ActualPlayer.Nev;
                actualPicture.Image = Image.FromFile($"korong_{ActualPlayer.KorongJel}.png");
                Kezdes = false;
            }

            if (ActualPlayer.Lephet)
            {
                int row = Convert.ToInt32(kep.Name.Split('_')[1][0].ToString());
                int column = Convert.ToInt32(kep.Name.Split('_')[1][1].ToString());
                int z_coord = Convert.ToInt32(kep.Name.Split('_')[1][2].ToString());
                if (Palya[row, column, z_coord].JatekosNev == ActualPlayer.Nev)
                {
                    Kijelol_Jelol(kep);
                }
            }
            if (ActualPlayer.KezdoKorongok.FindAll(x => x == kep).ToList().Count > 0)
            {
                Kijelol_Jelol(kep);
                
            }
        }

        private void Kijelol_Jelol(PictureBox kep)
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
                            Kerekit(kep);
                            Palya[sor, oszlop, z_index] = new Mezo(kep);

                            kep.Click += delegate (object sender, EventArgs e) {
                                if (Malmok < 1)
                                {
                                    if (ActualPlayer.Ugorhat && Aktiv == null) { Kijelol(kep); }
                                    else if (Aktiv == null) { KijelolMozgat(kep); }
                                    else { Lerak(kep); }
                                }
                                else { Levesz(kep); }
                                Wincheck();
                            };
                        }
                        else
                        {
                            Palya[sor, oszlop, z_index] = null;
                        }
                    }
                }
            }
        }

        private void Wincheck()
        {
            if (ActualPlayer.Lephet && ActualPlayer.Korongszam < 3)
            {
                Jatekos ellenfel = new List<Jatekos>() { Player1, Player2 }.Find(x => x.Nev != ActualPlayer.Nev);
                if (DialogResult.Yes == MessageBox.Show($"Gratulálok ezt a játszmát {ellenfel.Nev} nyerte!\nSzeretnétek Újat játszani?", "Győzelem", MessageBoxButtons.YesNo))
                {
                    Application.Restart();
                }
                else
                {
                    Application.Exit();
                }
            }
        }

        private void Levesz(PictureBox kep)
        {
            int sor = Convert.ToInt32(kep.Name.Split('_')[1][0].ToString());
            int oszlop = Convert.ToInt32(kep.Name.Split('_')[1][1].ToString());
            int z_index = Convert.ToInt32(kep.Name.Split('_')[1][2].ToString());
            Jatekos ellenfel = new List<Jatekos>() { Player1, Player2 }.Find(x => x.Nev != ActualPlayer.Nev);
            if (Palya[sor, oszlop, z_index].JatekosNev == ellenfel.Nev)
            {
                if (!Malom_e(sor, oszlop, z_index))
                {
                    kep.Image = null;
                    Palya[sor, oszlop, z_index].JatekosNev = null;
                    ellenfel.Korongszam--;
                    Malmok--;
                    malomLabel.Text = $"Levehetsz {Malmok} korongot";
                    if (Malmok < 1)
                    {
                        malomLabel.Visible = false;
                        ActualPlayerCsere();
                    }
                    UgorhatBeallitas();
                }
            }
        }
        private bool Malom_e(int sor, int oszlop, int z_index)
        {
            if (HanyMalom(sor, oszlop, z_index, Palya[sor, oszlop, z_index].Kep) > 0)
            {
                return true;
            }
            return false;
        }

        private void KijelolMozgat(PictureBox kep)
        {
            int sor = Convert.ToInt32(kep.Name.Split('_')[1][0].ToString());
            int oszlop = Convert.ToInt32(kep.Name.Split('_')[1][1].ToString());
            int z_index = Convert.ToInt32(kep.Name.Split('_')[1][2].ToString());

            if (ActualPlayer.Lephet && ActualPlayer.Nev == Palya[sor, oszlop, z_index].JatekosNev)
            {
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

        public void Lerak(PictureBox kep)
        {
            int sor = Convert.ToInt32(kep.Name.Split('_')[1][0].ToString());
            int oszlop = Convert.ToInt32(kep.Name.Split('_')[1][1].ToString());
            int z_index = Convert.ToInt32(kep.Name.Split('_')[1][2].ToString());

            if (Palya[sor, oszlop, z_index].Szabad)
            {
                KepHelyCsere(kep);
                if (!ActualPlayer.Lephet)
                {
                    LephetBeallitas();
                }
                UgorhatBeallitas();
            }
            Normalize();
        }

        private void LephetBeallitas()
        {
            foreach (Jatekos jatekos in new List<Jatekos>() { Player1, Player2})
            {
                if (jatekos.KezdoKorongok.Count < 1)
                {
                    jatekos.Lephet = true;
                }
            }
        }
        private void UgorhatBeallitas()
        {
            foreach (Jatekos jatekos in new List<Jatekos>() { Player1, Player2 })
            {
                if (jatekos.Lephet && jatekos.Korongszam < 4)
                {
                    jatekos.Ugorhat = true;
                }
            }
        }

        private void KepHelyCsere(PictureBox kep)
        {
            int sor = Convert.ToInt32(kep.Name.Split('_')[1][0].ToString());
            int oszlop = Convert.ToInt32(kep.Name.Split('_')[1][1].ToString());
            int z_index = Convert.ToInt32(kep.Name.Split('_')[1][2].ToString());

            MegjelenesAnimacio(kep, Aktiv.Image);
            //kep.Image = Aktiv.Image;
            Palya[sor, oszlop, z_index].JatekosNev = ActualPlayer.Nev;
            if (!ActualPlayer.Lephet)
            {
                ActualPlayer.Korongszam++;
                this.Controls.Remove(Aktiv);
                ActualPlayer.KezdoKorongok.Remove(Aktiv);
            }
            else
            {
                JatekosNevVisszaallitas();
                Aktiv.Image = null;
            }
            ActualPlayerCsere();
            MalomCheck(kep);
        }

        private void MegjelenesAnimacio(PictureBox kep, Image image)
        {
            AnimaltKep = kep;
            AnimaltKep.Visible = false;
            AnimaltKep.Image = image;
            AnimaltKep.Location = new Point(AnimaltKep.Location.X + korongSize / 2, AnimaltKep.Location.Y + korongSize / 2);
            animation.Start();
        }
        private void animation_Tick(object sender, EventArgs e)
        {
            AnimSzam++;
            AnimaltKep.Location = new Point(AnimaltKep.Location.X - 1, AnimaltKep.Location.Y - 1);
            AnimaltKep.Size = new Size(AnimSzam*2, AnimSzam*2);
            AnimaltKep.Visible = true;


            if (AnimSzam == korongSize/2)
            {
                AnimSzam = 0;
                AnimaltKep = null;
                animation.Stop();
            }
        }

        private void JatekosNevVisszaallitas()
        {
            int sor = Convert.ToInt32(Aktiv.Name.Split('_')[1][0].ToString());
            int oszlop  = Convert.ToInt32(Aktiv.Name.Split('_')[1][1].ToString());
            int z_index = Convert.ToInt32(Aktiv.Name.Split('_')[1][2].ToString());
            Palya[sor, oszlop, z_index].JatekosNev = null;
        }

        private void MalomCheck(PictureBox kep)
        {
            int sor = Convert.ToInt32(kep.Name.Split('_')[1][0].ToString());
            int oszlop = Convert.ToInt32(kep.Name.Split('_')[1][1].ToString());
            int z_index = Convert.ToInt32(kep.Name.Split('_')[1][2].ToString());

            Malmok = HanyMalom(sor, oszlop, z_index, kep);

            if (Malmok > 0 && Levehet_e())
            {
                ActualPlayerCsere();
                Jatekos ellenfel = new List<Jatekos>() { Player1, Player2 }.Find(x => x.Nev != ActualPlayer.Nev);
                actualPicture.Image = Image.FromFile($"korong_{ellenfel.KorongJel}.png");
                malomLabel.Text = $"Levehetsz {Malmok} korongot";
                malomLabel.Visible = true;
            } 
            else
            {
                Malmok = 0;
            }
        }

        private bool Levehet_e()
        {
            for (int sor = 0; sor < Palya.GetLength(0); sor++)
            {
                for (int oszlop = 0; oszlop < Palya.GetLength(1); oszlop++)
                {
                    for (int z_index = 0; z_index < Palya.GetLength(2); z_index++)
                    {
                        if (Palya[sor, oszlop, z_index] == null) { continue; }
                        else if (Palya[sor, oszlop, z_index].JatekosNev == ActualPlayer.Nev && HanyMalom(sor, oszlop, z_index, Palya[sor, oszlop, z_index].Kep) == 0)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private int HanyMalom(int sor, int oszlop, int z_index, PictureBox kep)
        {
            int sor_i = 0;
            int oszlop_i = 0;
            int z_index_i = 0;

            for (int i = 0; i < Palya.GetLength(0); i++)
            {
                if (Ellenoriz(i, oszlop, z_index, kep)) { sor_i++; }

                if (Ellenoriz(sor, i, z_index, kep)) { oszlop_i++; }

                if ((sor % 2 < 1 && oszlop % 2 > 0) || (sor % 2 > 0 && oszlop % 2 < 1))
                {
                    if (Ellenoriz(sor, oszlop, i, kep)) { z_index_i++; }
                }
            }
            return new List<int>() { sor_i, oszlop_i, z_index_i }.Count(x => x == 3);
        }

        private bool Ellenoriz(int row, int column, int z_coord, PictureBox kep)
        {
            int sor = Convert.ToInt32(kep.Name.Split('_')[1][0].ToString());
            int oszlop = Convert.ToInt32(kep.Name.Split('_')[1][1].ToString());
            int z_index = Convert.ToInt32(kep.Name.Split('_')[1][2].ToString());

            if (Palya[row, column, z_coord] != null && Palya[sor, oszlop, z_index].JatekosNev == Palya[row, column, z_coord].JatekosNev)
            {
                return true;
            }
            return false;
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
        private void ActualPlayerCsere()
        {
            ActualPlayer = new List<Jatekos>() { Player1, Player2 }.Find(x => x != ActualPlayer);
            label1.Text = ActualPlayer.Nev;
            actualPicture.Image = Image.FromFile($"korong_{ActualPlayer.KorongJel}.png");
        }

        private void MakeBorder()
        {
            Temp.Location = new Point(Aktiv.Location.X - (Temp.Size.Width-Aktiv.Size.Width)/2, Aktiv.Location.Y - (Temp.Size.Height - Aktiv.Size.Height) / 2);
            Temp.Visible = true;
            Temp.BringToFront();
            Aktiv.BringToFront();
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
            Aktiv = null;
            Temp.Visible = false;
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