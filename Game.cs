using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Tetris
{
    public partial class Game : Form
    {
        //***INICIALIZACE***
        Control[] aktivniPolozka = { null, null, null, null };
        Control[] aktivniPolozka2 = { null, null, null, null };
        List<int> pieceSequence = new List<int>();

        int momentalniPolozka;
        int dalsiPolozkaInt;
        int otaceni = 0;
        int ubehlyCas = 0;
        int skore = 0; //aktuální skóre
        int pocetPostavenychRad = 0;
        int pieceSequenceIteration = 0;
        bool konecHry = false;


        //Nastavení barev pro jednotlivé položky
        readonly Color[] seznamBarev ={
            Color.Cyan,     //I
            Color.Orange,   //L
            Color.Blue,     //J
            Color.Green,    //S
            Color.Red,      //Z
            Color.Yellow,   //O
            Color.Black     //T
        };

        //Hra
        public Game()
        {
            InitializeComponent();
            //Zapnutí timerů a vynulování label_updateSkore
            label_updateSkore.Text = "";
            timer1.Start();
            timer2.Start();

            //Generování položek
            Random rnd = new Random();
            while (pieceSequence.Count < 7)
            {
                int x = rnd.Next(7);
                if (!pieceSequence.Contains(x))
                {
                    pieceSequence.Add(x);
                }
            }

            //Vybere první položku
            dalsiPolozkaInt = pieceSequence[0];
            pieceSequenceIteration++;

            DropNewPiece();
        }

        public void DropNewPiece()
        {
            // Zresetuje počet otočení
            otaceni = 0;

            // Přesune další položku na momentální
            momentalniPolozka = dalsiPolozkaInt;

            // Pokud je poslední pieceSequence, vygeneruje novou
            if (pieceSequenceIteration == 7)
            {
                pieceSequenceIteration = 0;

                // 83
                pieceSequence.Clear();
                Random rnd = new Random();
                while (pieceSequence.Count < 7)
                {
                    int x = rnd.Next(7);
                    if (!pieceSequence.Contains(x))
                    {
                        pieceSequence.Add(x);
                    }
                }
            }
            // Zvolí další položku
            dalsiPolozkaInt = pieceSequence[pieceSequenceIteration];
            pieceSequenceIteration++;

            // Rozvržení padajícího kousku
            Control[,] activePieceArray =
            {
                { pictureBox6, pictureBox16, pictureBox26, pictureBox36 }, // I
                { pictureBox4, pictureBox14, pictureBox24, pictureBox25 }, // L
                { pictureBox5, pictureBox15, pictureBox25, pictureBox24 }, // J
                { pictureBox14, pictureBox15, pictureBox5, pictureBox6 },  // S
                { pictureBox5, pictureBox6, pictureBox16, pictureBox17 },  // Z
                { pictureBox5, pictureBox6, pictureBox15, pictureBox16 },  // O
                { pictureBox6, pictureBox15, pictureBox16, pictureBox17 }  // T
            };

            // Vybere padající kousek
            for (int x = 0; x < 4; x++)
            {
                aktivniPolozka[x] = activePieceArray[momentalniPolozka, x];
            }


            // Zkontroluje jestli není konec hry

            foreach (Control box in aktivniPolozka)
            {
                if (box.BackColor != Color.White)
                {
                    //Game over!
                    timer1.Stop();
                    timer2.Stop();
                    konecHry = true;
                    MessageBox.Show("Konec hry!");
                    return;
                }
            }


            // Populate falling piece squares with correct color
            foreach (Control square in aktivniPolozka)
            {
                square.BackColor = seznamBarev[momentalniPolozka];
            }
        }

        // Otestuje jestli budoucí pohyb (doprava,doleva,dolů) byl mimo tabulku nebo jiný kousek
        public bool TestMove(string smer)
        {
            int vrchniRadek = 21;
            int spodniRadek = 0;
            int levySloupec = 9;
            int pravySloupec = 0;

            int dalsiCtverec = 0;

            Control novyCtverec = new Control();

            // Určí potencionální místa k pohybu
            foreach (Control ctverec in aktivniPolozka)
            {
                if (tabulka.GetRow(ctverec) < vrchniRadek)
                {
                    vrchniRadek = tabulka.GetRow(ctverec);
                }
                if (tabulka.GetRow(ctverec) > spodniRadek)
                {
                    spodniRadek = tabulka.GetRow(ctverec);
                }
                if (tabulka.GetColumn(ctverec) < levySloupec)
                {
                    levySloupec = tabulka.GetColumn(ctverec);
                }
                if (tabulka.GetColumn(ctverec) > pravySloupec)
                {
                    pravySloupec = tabulka.GetColumn(ctverec);
                }
            }

            //Otestuje jestli by byli nějaké čtverce mimo tabulku
            foreach (Control ctverec in aktivniPolozka)
            {
                int radekCtverce = tabulka.GetRow(ctverec);
                int sloupecCtverce = tabulka.GetColumn(ctverec);

                //Vlevo
                if (smer == "left" & sloupecCtverce > 0)
                {
                    novyCtverec = tabulka.GetControlFromPosition(sloupecCtverce - 1, radekCtverce);
                    dalsiCtverec = levySloupec;
                }
                else if (smer == "left" & sloupecCtverce == 0)
                {
                    //Doleva ven
                    return false;
                }

                //Vpravo
                else if (smer == "right" & sloupecCtverce < 9)
                {
                    novyCtverec = tabulka.GetControlFromPosition(sloupecCtverce + 1, radekCtverce);
                    dalsiCtverec = pravySloupec;
                }
                else if (smer == "right" & sloupecCtverce == 9)
                {
                    //Doprava ven
                    return false;
                }

                //Dolů
                else if (smer == "down" & radekCtverce < 21)
                {
                    novyCtverec = tabulka.GetControlFromPosition(sloupecCtverce, radekCtverce + 1);
                    dalsiCtverec = spodniRadek;
                }
                else if (smer == "down" & radekCtverce == 21)
                {
                    return false;
                    // Pod tabulku
                }

                // Otestuje jestli překryje jiný kousek
                if ((novyCtverec.BackColor != Color.White) & aktivniPolozka.Contains(novyCtverec) == false & dalsiCtverec > 0)
                {
                    return false;
                }

            }

            // Když projde všemi testy
            return true;
        }

        public void MovePiece(string smer)
        {
            // Vymaže starou pozici a určí novou podle inputu
            int x = 0;
            foreach (PictureBox square in aktivniPolozka)
            {
                square.BackColor = Color.White;
                int radekCtverce = tabulka.GetRow(square);
                int sloupecCtverce = tabulka.GetColumn(square);
                int novyRadekCtverce = 0;
                int novySloupecCtverce = 0;
                if (smer == "left")
                {
                    novySloupecCtverce = sloupecCtverce - 1;
                    novyRadekCtverce = radekCtverce;
                }
                else if (smer == "right")
                {
                    novySloupecCtverce = sloupecCtverce + 1;
                    novyRadekCtverce = radekCtverce;
                }
                else if (smer == "down")
                {
                    novySloupecCtverce = sloupecCtverce;
                    novyRadekCtverce = radekCtverce + 1;
                }

                aktivniPolozka2[x] = tabulka.GetControlFromPosition(novySloupecCtverce, novyRadekCtverce);
                x++;
            }

            //Zkopíruje aktivniPolozka2 do aktivniPolozka
            x = 0;
            foreach (PictureBox ctverec in aktivniPolozka2)
            {

                aktivniPolozka[x] = ctverec;
                x++;
            }

            // Nakreslí kousek na nové pozici
            x = 0;
            foreach (PictureBox ctverec in aktivniPolozka2)
            {
                ctverec.BackColor = seznamBarev[momentalniPolozka];
                x++;
            }
        }

        // Otestuje jestli by při rotaci překryl položený kousek
        private bool TestOverlap()
        {
            foreach (PictureBox ctverec in aktivniPolozka2)
            {
                if ((ctverec.BackColor != Color.White) & aktivniPolozka.Contains(ctverec) == false)
                {
                    return false;
                }
            }
            return true;
        }

        // Timer pro rychlost pohybu kousků
        // Speed is controlled by LevelUp() method
        private void SpeedTimer_Tick(object sender, EventArgs e)
        {
            if (CheckGameOver() == true)
            {
                timer1.Stop();
                timer2.Stop();
                MessageBox.Show("Konec hry!");
            }

            else
            {
                //Posune dál nebo vytvoří nový, když se nemůže hýbat
                if (TestMove("down") == true)
                {
                    MovePiece("down");
                }
                else
                {
                    if (CheckGameOver() == true)
                    {
                        timer1.Stop();
                        timer2.Stop();
                        MessageBox.Show("Konec hry!");
                    }
                    if (CheckForCompleteRows() > -1)
                    {
                        ClearFullRow();
                    }
                    DropNewPiece();
                }
            }
        }

        // Hra skončí, když je položka ve vrchním řádku, když je vytvořena nová položka
        private bool CheckGameOver()
        {
            Control[] vrchniRadek = { pictureBox1, pictureBox2, pictureBox3, pictureBox4, pictureBox5, pictureBox6, pictureBox7, pictureBox8, pictureBox9, pictureBox10 };

            foreach (Control box in vrchniRadek)
            {
                if ((box.BackColor != Color.White) & !aktivniPolozka.Contains(box))
                {
                    //Konec hry
                    return true;
                }
            }

            if (konecHry == true)
            {
                return true;
            }

            return false;
        }

        // Vrací číslo nejnižšího plného řádku
        // Když není žádný plný, tak vrátí -1
        private int CheckForCompleteRows()
        {
            // Pro každý řádek
            for (int x = 21; x >= 2; x--)
            {
                // Pro každý čtverec v řádku
                for (int y = 0; y <= 9; y++)
                {
                    Control z = tabulka.GetControlFromPosition(y, x);
                    if (z.BackColor == Color.White)
                    {
                        break;
                    }
                    if (y == 9)
                    {
                        // Vrátí číslo plného řádku
                        return x;
                    }
                }
            }
            return -1; //Vrací -1
        }

        //Kolik uběhlo času
        private void GameTimer_Tick(object sender, EventArgs e)
        {
            ubehlyCas++;
            label_cas.Text = "Čas: " + ubehlyCas.ToString();
        }

        // Vyčistí nejnižší plný řádek
        private void ClearFullRow()
        {
            int hotovyRadek = CheckForCompleteRows();

            //Přebarvý hotový na bílo
            for (int x = 0; x <= 9; x++)
            {
                Control z = tabulka.GetControlFromPosition(x, hotovyRadek);
                z.BackColor = Color.White;
            }

            //Posune všechny ostatní čtverce o jedno níž
            for (int x = hotovyRadek - 1; x >= 0; x--) //Pro každý řádek nad vyčištěným řádkem
            {
                //Pro každý čtverec v řádku
                for (int y = 0; y <= 9; y++)
                {
                    //čtverec
                    Control z = tabulka.GetControlFromPosition(y, x);

                    //čtverec pod ním
                    Control zz = tabulka.GetControlFromPosition(y, x + 1);

                    zz.BackColor = z.BackColor;
                    z.BackColor = Color.White;
                }
            }

            UpdateScore();

            pocetPostavenychRad++;
            label_radky.Text = "Řádky: " + pocetPostavenychRad;


            if (CheckForCompleteRows() > -1)
            {
                ClearFullRow();
            }
        }
        //Přepíše skóre a ukáže notifikaci
        private void UpdateScore()
        {
            skore += 100;
            label_updateSkore.Text = "+100";
            label_skore.Text = "Skóre: " + skore;
            timer3.Start();
        }
        //Vymaže notifikaci
        private void ScoreUpdateTimer_Tick(object sender, EventArgs e)
        {
            label_updateSkore.Text = "";
            timer3.Stop();
        }


        private void tabulka_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
