using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Malom_Game
{
    class Jatekos
    {
        public string Nev;
        public int KorongJel;
        public List<PictureBox> KezdoKorongok;
        public GroupBox Groupbox;
        public bool Lephet;
        public List<PictureBox> JatszoKorongok;

        public Jatekos(string nev, int jel, GroupBox groupbox)
        {
            Nev = nev;
            KorongJel = jel;
            KezdoKorongok = new List<PictureBox>();
            Groupbox = groupbox;
            Lephet = false;
            JatszoKorongok = new List<PictureBox>();
        }


    }
}
