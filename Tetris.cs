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
        Control[] aktivniPolozka = { null, null, null, null };
        Control[] aktivniPolozka2 = { null, null, null, null };
        Control[] dalsiPolozka = { null, null, null, null };
        Control[] ulozenaPolozka = { null, null, null, null };
        List<int> pieceSequence = new List<int>();

        Color barvaPolozky=Color.White;
        Color ulozenaBarvaPolozky = Color.White;

        int intervalOpakovaniPolozky = 0; //int PieceSequenceIteration = 0;
        int momentalniPolozka;
        int dalsiPolozkaInt;
        int ulozenaPolozkaInt = -1;
        int otaceni = 0;
        int ubehlyCas = 0;
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
