using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    public partial class Tetris : Form
    {
        //***INICIALIZACE***

        int kombo = 1; //násobitel skóre, podle toho kolikrát postavím řadu
        int skore = 0; //aktuální skóre
        int pocetPostavenychRad = 0;
        int rychlostHry = 0; //nastavuje rychlost hry
        bool konecHry = false;


        public Tetris()
        {
            InitializeComponent();
        }

        private void tabulka_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
