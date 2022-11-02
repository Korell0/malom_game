using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Malom_Game
{
    class Mezo
    {
        public PictureBox Kep;
        public bool Szabad;
        public string JatekosNev;



        public Mezo(PictureBox kep)
        {
            Kep = kep;
            Szabad = true;
            JatekosNev = null;
        }

    }
}
