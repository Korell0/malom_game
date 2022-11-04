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

        public Jatekos(string nev, int jel)
        {
            Nev = nev;
            KorongJel = jel;
            KezdoKorongok = new List<PictureBox>();
        }


    }
}
