using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Media; //pridano

namespace Tetris
{
    public partial class TetrisMain : Form
    {

        //***INICIALIZACE***
        Control[] aktivniPolozka = { null, null, null, null };
        Control[] aktivniPolozka2 = { null, null, null, null };
        List<int> PieceSequence = new List<int>();
        int ubehlyCas = 0;
        int momentalniPolozka;
        int dalsiPolozkaInt;
        int otaceni = 0;
        int skore = 0;
        int vycistenychRadku = 0;
        bool konecHry = false;
        int PieceSequenceIteration = 0;

        readonly Color[] seznamBarev =
        {
            Color.Cyan,     // I piece
            Color.Orange,   // L piece
            Color.Blue,     // J piece
            Color.Green,    // S piece
            Color.Red,      // Z piece
            Color.Yellow,   // O piece
            Color.Purple    // T piece
        };

        System.Media.SoundPlayer soundtrack = new System.Media.SoundPlayer(); //Konstruktor zvuku //Soundtrack


        //Hra
        public TetrisMain()
        {
            InitializeComponent();

            soundtrack.SoundLocation=@"../../zvuky/soundtrack.wav";//Lokace soundtracku
            soundtrack.PlayLooping();//Opakuje soundtrack furt dokola

            label_updateSkore.Text = "";
            rychlostHryTimer.Start();
            casHryTimer.Start();


            //Generování položek
            Random random = new Random();
            while (PieceSequence.Count < 7)
            {
                int x = random.Next(7);
                if (!PieceSequence.Contains(x))
                {
                    PieceSequence.Add(x);
                }
            }

            //Vybere první položku
            dalsiPolozkaInt = PieceSequence[0];
            PieceSequenceIteration++;

            DropNewPiece();
        }

        public void DropNewPiece()
        {
            // Zresetuje počet otočení
            otaceni = 0;

            //Přesune další položku na momentální
            momentalniPolozka = dalsiPolozkaInt;

            //Pokud je poslední pieceSequence, vygeneruje novou
            if (PieceSequenceIteration == 7)
            {
                PieceSequenceIteration = 0;

                //83
                PieceSequence.Clear();
                System.Random random = new System.Random();
                while (PieceSequence.Count < 7)
                {
                    int x = random.Next(7);
                    if (!PieceSequence.Contains(x))
                    {
                        PieceSequence.Add(x);
                    }
                }
            }

            //Zvolí další položku
            dalsiPolozkaInt = PieceSequence[PieceSequenceIteration];
            PieceSequenceIteration++;




            //Rozvržení padajícího kousku
            Control[,] activePieceArray =
            {
                { box6, box16, box26, box36 }, // I
                { box4, box14, box24, box25 }, // L
                { box5, box15, box25, box24 }, // J
                { box14, box15, box5, box6 },  // S
                { box5, box6, box16, box17 },  // Z
                { box5, box6, box15, box16 },  // O
                { box6, box15, box16, box17 }  // T
            };

            //Vybere padající kousek
            for (int x = 0; x < 4; x++)
            {
                aktivniPolozka[x] = activePieceArray[momentalniPolozka, x];
            }


            //Zkontroluje jestli není konec hry
            foreach (Control box in aktivniPolozka)
            {
                if (box.BackColor != Color.White & box.BackColor != Color.LightGray)
                {
                    //Konec hry
                    rychlostHryTimer.Stop();
                    casHryTimer.Stop();
                    konecHry = true;
                    MessageBox.Show("Game over!");
                    return;
                }
            }


            // Populate falling piece squares with correct color
            foreach (Control square in aktivniPolozka)
            {
                square.BackColor = seznamBarev[momentalniPolozka];
            }
        }

        //Otestuje jestli budoucí pohyb (doprava,doleva,dolů) byl mimo tabulku nebo jiný kousek
        public bool TestMove(string direction)
        {
            int currentHighRow = 21;
            int currentLowRow = 0;
            int currentLeftCol = 9;
            int currentRightCol = 0;

            int nextSquare = 0;

            Control newSquare = new Control();
            //Určí potencionální místa k pohybu
            foreach (Control square in aktivniPolozka)
            {
                if (tabulka.GetRow(square) < currentHighRow)
                {
                    currentHighRow = tabulka.GetRow(square);
                }
                if (tabulka.GetRow(square) > currentLowRow)
                {
                    currentLowRow = tabulka.GetRow(square);
                }
                if (tabulka.GetColumn(square) < currentLeftCol)
                {
                    currentLeftCol = tabulka.GetColumn(square);
                }
                if (tabulka.GetColumn(square) > currentRightCol)
                {
                    currentRightCol = tabulka.GetColumn(square);
                }
            }

            //Otestuje jestli by byli nějaké čtverce mimo tabulku
            foreach (Control square in aktivniPolozka)
            {
                int squareRow = tabulka.GetRow(square);
                int squareCol = tabulka.GetColumn(square);

                //Vlevo
                if (direction == "left" & squareCol > 0)
                {
                    newSquare = tabulka.GetControlFromPosition(squareCol - 1, squareRow);
                    nextSquare = currentLeftCol;
                }
                else if (direction == "left" & squareCol == 0)
                {
                    //Otestuje jestli by nebyl objekt doleva mimo tabulku
                    return false;
                }

                //Vpravo
                else if (direction == "right" & squareCol < 9)
                {
                    newSquare = tabulka.GetControlFromPosition(squareCol + 1, squareRow);
                    nextSquare = currentRightCol;
                }
                else if (direction == "right" & squareCol == 9)
                {
                    //Otestuje jestli by nebyl objekt doprava mimo tabulku
                    return false;
                }

                //Dolů
                else if (direction == "down" & squareRow < 21)
                {
                    newSquare = tabulka.GetControlFromPosition(squareCol, squareRow + 1);
                    nextSquare = currentLowRow;
                }
                else if (direction == "down" & squareRow == 21)
                {
                    return false;
                    //Otestuje, jestli by nebyl pohyb pod tabulku
                }

                //Otestuje jestli překryje jiný kousek
                if ((newSquare.BackColor != Color.White & newSquare.BackColor != Color.LightGray) & aktivniPolozka.Contains(newSquare) == false & nextSquare > 0)
                {
                    return false;
                }

            }

            //Když projde všemi testy tak vrátí true
            return true;
        }
        public void MovePiece(string direction)
        {
            // Vymaže starou pozici a určí novou podle inputu
            int x = 0;
            foreach (PictureBox square in aktivniPolozka)
            {
                square.BackColor = Color.White;
                int squareRow = tabulka.GetRow(square);
                int squareCol = tabulka.GetColumn(square);
                int newSquareRow = 0;
                int newSquareCol = 0;
                if (direction == "left")
                {
                    newSquareCol = squareCol - 1;
                    newSquareRow = squareRow;
                }
                else if (direction == "right")
                {
                    newSquareCol = squareCol + 1;
                    newSquareRow = squareRow;
                }
                else if (direction == "down")
                {
                    newSquareCol = squareCol;
                    newSquareRow = squareRow + 1;
                }

                aktivniPolozka2[x] = tabulka.GetControlFromPosition(newSquareCol, newSquareRow);
                x++;
            }

            //Zkopíruje activePiece2 do activePiece
            x = 0;
            foreach (PictureBox square in aktivniPolozka2)
            {

                aktivniPolozka[x] = square;
                x++;
            }

            //Překreslí položku na nové pozici
            x = 0;
            foreach (PictureBox square in aktivniPolozka2)
            {
                square.BackColor = seznamBarev[momentalniPolozka];
                x++;
            }
        }

        //Otestuje jestli by při rotaci překryl položený kousek
        private bool TestOverlap()
        {
            foreach (PictureBox square in aktivniPolozka2)
            {
                if ((square.BackColor != Color.White) & aktivniPolozka.Contains(square) == false)
                {
                    return false;
                }
            }
            return true;
        }

        // Timer pro rychlost pohybu kousků
        private void RychlostHryTimer_Tick(object sender, EventArgs e)
        {
            if (CheckGameOver() == true)
            {
                rychlostHryTimer.Stop();
                casHryTimer.Stop();
                MessageBox.Show("Game over!");
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
                        rychlostHryTimer.Stop();
                        casHryTimer.Stop();
                        MessageBox.Show("Game over!");
                    }
                    if (CheckForCompleteRows() > -1)
                    {
                        ClearFullRow();
                    }
                    DropNewPiece();
                }
            }
        }


        //Vyčistí nejnižší plný řádek
        private void ClearFullRow()
        {
            int completedRow = CheckForCompleteRows();

            //Přebarví ho na bílo
            for (int x = 0; x <= 9; x++)
            {
                Control z = tabulka.GetControlFromPosition(x, completedRow);
                z.BackColor = Color.White;
            }

            //Posune všechny ostatní čtverce dolů
            for (int x = completedRow - 1; x >= 0; x--) //Každý řádek nad vymazaným
            {
                //Každý čtverec v řádku
                for (int y = 0; y <= 9; y++)
                {
                    //Momentální čtverec
                    Control z = tabulka.GetControlFromPosition(y, x);

                    //Čtverec pod ním
                    Control zz = tabulka.GetControlFromPosition(y, x + 1);

                    zz.BackColor = z.BackColor;
                    z.BackColor = Color.White;
                }
            }

            UpdateScore();

            vycistenychRadku++;
            label_radky.Text = "Řádky: " + vycistenychRadku;

            if (CheckForCompleteRows() > -1)
            {
                ClearFullRow();
            }
        }
        // Vrací číslo nejnižšího plného řádku
        // Když není žádný plný, tak vrátí -1
        private int CheckForCompleteRows()
        {
            //Každý řádek
            for (int x = 21; x >= 2; x--)
            {
                //Každý čtverec
                for (int y = 0; y <= 9; y++)
                {
                    Control z = tabulka.GetControlFromPosition(y, x);
                    if (z.BackColor == Color.White)
                    {
                        break;
                    }
                    if (y == 9)
                    {
                        //Vrací číslo plného řádku, aby se mohl vybílit
                        return x;
                    }
                }
            }
            return -1; //Žádný plný řádek
        }



        //Hra skončí, když je položka ve vrchním řádku, když je vytvořena nová položka
        private bool CheckGameOver()
        {
            Control[] topRow = { box1, box2, box3, box4, box5, box6, box7, box8, box9, box10 };

            foreach (Control box in topRow)
            {
                if ((box.BackColor != Color.White & box.BackColor != Color.LightGray) & !aktivniPolozka.Contains(box))
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
        
        //Updatuje čas hry
        private void CasHryTimer_Tick(object sender, EventArgs e)
        {
            ubehlyCas++;
            if (ubehlyCas > 60)
            {
                label_cas.Text = "Čas: " + ubehlyCas/60+" m "+ubehlyCas%60+" s";
            }
            else
            {
                label_cas.Text = "Čas: " + ubehlyCas+" s";
            }
        }
        //Updatuje skóre
        private void UpdateScore()
        {
            skore += 100;
            label_skore.Text = "Skóre: " + skore;
            label_updateSkore.Text = "+";//Dodělat
            skoreTimer.Start();
        }
        //Každou sekundu smaže notifikaci o skóre
        private void SkoreTimer_Tick(object sender, EventArgs e)
        {
            label_updateSkore.Text = "";
            skoreTimer.Stop();
        }
    }
}
