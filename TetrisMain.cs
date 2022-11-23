using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Media; //pridano
using System.IO;

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
        Menu menu = new Menu();

        readonly Color[] seznamBarev =
        {
            Color.Cyan,     // I 
            Color.Orange,   // L 
            Color.Blue,     // J
            Color.Green,    // S 
            Color.Red,      // Z 
            Color.Yellow,   // O 
            Color.Purple    // T
        };
        
        System.Media.SoundPlayer soundtrack = new System.Media.SoundPlayer(); //Konstruktor zvuku //Soundtrack

        //Hra
        public TetrisMain()
        {
            InitializeComponent();
            soundtrack.SoundLocation=@"../../zvuky/soundtrack.wav";//Lokace soundtracku
            soundtrack.PlayLooping();//Opakuje soundtrack furt dokola
            FileStream fs = new FileStream("pomocny.txt", FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            bool herniRezim = bool.Parse(sr.ReadLine());
            fs.Close();
            //rozhodnutí o herním režimu
            if(herniRezim)
            {
                label_herniRezim.Text += " Časový mód";
            }
            else 
            {
                label_herniRezim.Text += " Nekonečný mód";
            }
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

        //**************************Ovládání******************************
        //Pracuje s inputem, zatím pouzde WASD a šipky
        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            //*****************************Menu************************
            if(e.KeyCode==Keys.M)
            {
                soundtrack.Stop();
                this.Hide();
                this.Close();
                menu.ShowDialog();
            }


            bool pozastaveno = false;
            if (e.KeyCode == Keys.Q)
            {
                e.SuppressKeyPress = (e.KeyCode == Keys.Up);
                e.SuppressKeyPress = (e.KeyCode == Keys.Down);
                e.SuppressKeyPress = (e.KeyCode == Keys.Left);
                e.SuppressKeyPress = (e.KeyCode == Keys.Right);
                casHryTimer.Stop();
                rychlostHryTimer.Stop();
                pozastaveno = true;
            }
            else if (e.KeyCode == Keys.E)
            {
                pozastaveno = false;
                casHryTimer.Start();
                rychlostHryTimer.Start();
            }
            if (!pozastaveno)
            {
                if (!CheckGameOver() & ((e.KeyCode == Keys.Left | e.KeyCode == Keys.A) & TestMove("left") == true))
                {
                    MovePiece("left");
                }
                else if (!CheckGameOver() & ((e.KeyCode == Keys.Right | e.KeyCode == Keys.D) & TestMove("right") == true))
                {
                    MovePiece("right");
                }
                else if ((e.KeyCode == Keys.Down | e.KeyCode == Keys.S) & TestMove("down") == true)
                {
                    MovePiece("down");
                }
                else if (e.KeyCode == Keys.Up | e.KeyCode == Keys.W)
                {
                    //Otáčení

                    int ctverec1Sloupec = tabulka.GetColumn(aktivniPolozka[0]);
                    int ctverec1Radek = tabulka.GetRow(aktivniPolozka[0]);

                    int ctverec2Sloupec = tabulka.GetColumn(aktivniPolozka[1]);
                    int ctverec2Radek = tabulka.GetRow(aktivniPolozka[1]);

                    int ctverec3Sloupec = tabulka.GetColumn(aktivniPolozka[2]);
                    int ctverec3Radek = tabulka.GetRow(aktivniPolozka[2]);

                    int ctverec4Sloupec = tabulka.GetColumn(aktivniPolozka[3]);
                    int ctverec4Radek = tabulka.GetRow(aktivniPolozka[3]);

                    if (momentalniPolozka == 0) //I tvar
                    {
                        //Otestuje jestli není I tvar moc blízko ke kraji, aby se mohl otočit
                        if (otaceni == 0 & (ctverec1Sloupec == 0 | ctverec1Sloupec == 1 | ctverec1Sloupec == 9))
                        {
                            return;
                        }
                        else if (otaceni == 1 & (ctverec3Sloupec == 0 | ctverec3Sloupec == 1 | ctverec3Sloupec == 9))
                        {
                            return;
                        }

                        //Pokud test projde, tak se otočí
                        if (otaceni == 0)
                        {
                            aktivniPolozka2[0] = tabulka.GetControlFromPosition(ctverec1Sloupec - 2, ctverec1Radek);
                            aktivniPolozka2[1] = tabulka.GetControlFromPosition(ctverec2Sloupec - 1, ctverec2Radek - 1);
                            aktivniPolozka2[2] = tabulka.GetControlFromPosition(ctverec3Sloupec, ctverec3Radek - 2);
                            aktivniPolozka2[3] = tabulka.GetControlFromPosition(ctverec4Sloupec + 1, ctverec4Radek - 3);

                            //Otestuje jestli nepřekrývá jiný kousek, jestli ne tak inkrementuje, aby se mohl otočit
                            if (TestOverlap() == true)
                            {
                                otaceni++;
                            }
                            else
                            {
                                return;
                            }
                        }
                        else if (otaceni == 1)
                        {
                            aktivniPolozka2[0] = tabulka.GetControlFromPosition(ctverec1Sloupec + 2, ctverec1Radek);
                            aktivniPolozka2[1] = tabulka.GetControlFromPosition(ctverec2Sloupec + 1, ctverec2Radek + 1);
                            aktivniPolozka2[2] = tabulka.GetControlFromPosition(ctverec3Sloupec, ctverec3Radek + 2);
                            aktivniPolozka2[3] = tabulka.GetControlFromPosition(ctverec4Sloupec - 1, ctverec4Radek + 3);

                            if (TestOverlap() == true)
                            {
                                otaceni = 0;
                            }
                            else
                            {
                                return;
                            }
                        }
                    }
                    else if (momentalniPolozka == 1) //L
                    {
                        //Otestuje, jestli není moc blízko straně
                        if (otaceni == 0 & (ctverec1Sloupec == 8 | ctverec1Sloupec == 9))
                        {
                            return;
                        }
                        else if (otaceni == 2 & (ctverec1Sloupec == 9))
                        {
                            return;
                        }

                        //Otočí
                        if (otaceni == 0)
                        {
                            aktivniPolozka2[0] = tabulka.GetControlFromPosition(ctverec1Sloupec, ctverec1Radek + 2);
                            aktivniPolozka2[1] = tabulka.GetControlFromPosition(ctverec2Sloupec + 1, ctverec2Radek + 1);
                            aktivniPolozka2[2] = tabulka.GetControlFromPosition(ctverec3Sloupec + 2, ctverec3Radek);
                            aktivniPolozka2[3] = tabulka.GetControlFromPosition(ctverec4Sloupec + 1, ctverec4Radek - 1);

                            //Otestuje, jestli nepřekrývá, jestli jo tak zruší rotaci
                            if (TestOverlap() == true)
                            {
                                otaceni++;
                            }
                            else
                            {
                                return;
                            }
                        }
                        else if (otaceni == 1)
                        {
                            aktivniPolozka2[0] = tabulka.GetControlFromPosition(ctverec1Sloupec + 1, ctverec1Radek);
                            aktivniPolozka2[1] = tabulka.GetControlFromPosition(ctverec2Sloupec, ctverec2Radek - 1);
                            aktivniPolozka2[2] = tabulka.GetControlFromPosition(ctverec3Sloupec - 1, ctverec3Radek - 2);
                            aktivniPolozka2[3] = tabulka.GetControlFromPosition(ctverec4Sloupec - 2, ctverec4Radek - 1);

                            //Otestuje, jestli nepřekrývá, jestli jo tak zruší rotaci
                            if (TestOverlap() == true)
                            {
                                otaceni++;
                            }
                            else
                            {
                                return;
                            }
                        }
                        else if (otaceni == 2)
                        {
                            aktivniPolozka2[0] = tabulka.GetControlFromPosition(ctverec1Sloupec + 1, ctverec1Radek - 1);
                            aktivniPolozka2[1] = tabulka.GetControlFromPosition(ctverec2Sloupec, ctverec2Radek);
                            aktivniPolozka2[2] = tabulka.GetControlFromPosition(ctverec3Sloupec - 1, ctverec3Radek + 1);
                            aktivniPolozka2[3] = tabulka.GetControlFromPosition(ctverec4Sloupec, ctverec4Radek + 2);

                            //Otestuje, jestli nepřekrývá, jestli jo tak zruší rotaci
                            if (TestOverlap() == true)
                            {
                                otaceni++;
                            }
                            else
                            {
                                return;
                            }
                        }
                        else if (otaceni == 3)
                        {
                            aktivniPolozka2[0] = tabulka.GetControlFromPosition(ctverec1Sloupec - 2, ctverec1Radek - 1);
                            aktivniPolozka2[1] = tabulka.GetControlFromPosition(ctverec2Sloupec - 1, ctverec2Radek);
                            aktivniPolozka2[2] = tabulka.GetControlFromPosition(ctverec3Sloupec, ctverec3Radek + 1);
                            aktivniPolozka2[3] = tabulka.GetControlFromPosition(ctverec4Sloupec + 1, ctverec4Radek);

                            //Otestuje, jestli nepřekrývá, jestli jo tak zruší rotaci
                            if (TestOverlap() == true)
                            {
                                otaceni = 0;
                            }
                            else
                            {
                                return;
                            }
                        }
                    }
                    else if (momentalniPolozka == 2) //L ale naopak
                    {
                        if (otaceni == 0 & (ctverec1Sloupec == 0 | ctverec1Sloupec == 1))
                        {
                            return;
                        }
                        else if (otaceni == 2 & ctverec1Sloupec == 0)
                        {
                            return;
                        }

                        if (otaceni == 0)
                        {
                            aktivniPolozka2[0] = tabulka.GetControlFromPosition(ctverec1Sloupec - 2, ctverec1Radek + 1);
                            aktivniPolozka2[1] = tabulka.GetControlFromPosition(ctverec2Sloupec - 1, ctverec2Radek);
                            aktivniPolozka2[2] = tabulka.GetControlFromPosition(ctverec3Sloupec, ctverec3Radek - 1);
                            aktivniPolozka2[3] = tabulka.GetControlFromPosition(ctverec4Sloupec + 1, ctverec4Radek);

                            if (TestOverlap() == true)
                            {
                                otaceni++;
                            }
                            else
                            {
                                return;
                            }
                        }
                        else if (otaceni == 1)
                        {
                            aktivniPolozka2[0] = tabulka.GetControlFromPosition(ctverec1Sloupec + 1, ctverec1Radek + 1);
                            aktivniPolozka2[1] = tabulka.GetControlFromPosition(ctverec2Sloupec, ctverec2Radek);
                            aktivniPolozka2[2] = tabulka.GetControlFromPosition(ctverec3Sloupec - 1, ctverec3Radek - 1);
                            aktivniPolozka2[3] = tabulka.GetControlFromPosition(ctverec4Sloupec, ctverec4Radek - 2);

                            if (TestOverlap() == true)
                            {
                                otaceni++;
                            }
                            else
                            {
                                return;
                            }
                        }
                        else if (otaceni == 2)
                        {
                            aktivniPolozka2[0] = tabulka.GetControlFromPosition(ctverec1Sloupec + 1, ctverec1Radek);
                            aktivniPolozka2[1] = tabulka.GetControlFromPosition(ctverec2Sloupec, ctverec2Radek + 1);
                            aktivniPolozka2[2] = tabulka.GetControlFromPosition(ctverec3Sloupec - 1, ctverec3Radek + 2);
                            aktivniPolozka2[3] = tabulka.GetControlFromPosition(ctverec4Sloupec - 2, ctverec4Radek + 1);

                            if (TestOverlap() == true)
                            {
                                otaceni++;
                            }
                            else
                            {
                                return;
                            }
                        }
                        else if (otaceni == 3)
                        {
                            aktivniPolozka2[0] = tabulka.GetControlFromPosition(ctverec1Sloupec, ctverec1Radek - 2);
                            aktivniPolozka2[1] = tabulka.GetControlFromPosition(ctverec2Sloupec + 1, ctverec2Radek - 1);
                            aktivniPolozka2[2] = tabulka.GetControlFromPosition(ctverec3Sloupec + 2, ctverec3Radek);
                            aktivniPolozka2[3] = tabulka.GetControlFromPosition(ctverec4Sloupec + 1, ctverec4Radek + 1);

                            if (TestOverlap() == true)
                            {
                                otaceni = 0;
                            }
                            else
                            {
                                return;
                            }
                        }
                    }
                    else if (momentalniPolozka == 3) //S
                    {
                        if (otaceni == 0 & (ctverec1Radek == 1 | ctverec1Sloupec == 9))
                        {
                            return;
                        }
                        else if (otaceni == 1 & ctverec1Sloupec == 0)
                        {
                            return;
                        }

                        if (otaceni == 0)
                        {

                            aktivniPolozka2[0] = tabulka.GetControlFromPosition(ctverec1Sloupec + 1, ctverec1Radek - 2);
                            aktivniPolozka2[1] = tabulka.GetControlFromPosition(ctverec2Sloupec, ctverec2Radek - 1);
                            aktivniPolozka2[2] = tabulka.GetControlFromPosition(ctverec3Sloupec + 1, ctverec3Radek);
                            aktivniPolozka2[3] = tabulka.GetControlFromPosition(ctverec4Sloupec, ctverec4Radek + 1);


                            if (TestOverlap() == true)
                            {
                                otaceni++;
                            }
                            else
                            {
                                return;
                            }
                        }
                        else if (otaceni == 1)
                        {
                            aktivniPolozka2[0] = tabulka.GetControlFromPosition(ctverec1Sloupec - 1, ctverec1Radek + 2);
                            aktivniPolozka2[1] = tabulka.GetControlFromPosition(ctverec2Sloupec, ctverec2Radek + 1);
                            aktivniPolozka2[2] = tabulka.GetControlFromPosition(ctverec3Sloupec - 1, ctverec3Radek);
                            aktivniPolozka2[3] = tabulka.GetControlFromPosition(ctverec4Sloupec, ctverec4Radek - 1);

                            if (TestOverlap() == true)
                            {
                                otaceni = 0;
                            }
                            else
                            {
                                return;
                            }
                        }
                    }
                    else if (momentalniPolozka == 4)
                    {
                        if (otaceni == 1 & ctverec1Sloupec == 8)
                        {
                            return;
                        }

                        if (otaceni == 0)
                        {
                            aktivniPolozka2[0] = tabulka.GetControlFromPosition(ctverec1Sloupec, ctverec1Radek + 1);
                            aktivniPolozka2[1] = tabulka.GetControlFromPosition(ctverec2Sloupec - 1, ctverec2Radek);
                            aktivniPolozka2[2] = tabulka.GetControlFromPosition(ctverec3Sloupec, ctverec3Radek - 1);
                            aktivniPolozka2[3] = tabulka.GetControlFromPosition(ctverec4Sloupec - 1, ctverec4Radek - 2);

                            if (TestOverlap() == true)
                            {
                                otaceni++;
                            }
                            else
                            {
                                return;
                            }
                        }
                        else if (otaceni == 1)
                        {
                            aktivniPolozka2[0] = tabulka.GetControlFromPosition(ctverec1Sloupec, ctverec1Radek - 1);
                            aktivniPolozka2[1] = tabulka.GetControlFromPosition(ctverec2Sloupec + 1, ctverec2Radek);
                            aktivniPolozka2[2] = tabulka.GetControlFromPosition(ctverec3Sloupec, ctverec3Radek + 1);
                            aktivniPolozka2[3] = tabulka.GetControlFromPosition(ctverec4Sloupec + 1, ctverec4Radek + 2);

                            if (TestOverlap() == true)
                            {
                                otaceni = 0;
                            }
                            else
                            {
                                return;
                            }
                        }
                    }
                    else if (momentalniPolozka == 5) //Čtverec
                    {
                        return;
                    }
                    else if (momentalniPolozka == 6) //Pyramidka
                    {
                        if (otaceni == 1 & ctverec1Sloupec == 9)
                        {
                            return;
                        }
                        else if (otaceni == 3 & ctverec1Sloupec == 0)
                        {
                            return;
                        }

                        if (otaceni == 0)
                        {
                            aktivniPolozka2[0] = tabulka.GetControlFromPosition(ctverec1Sloupec, ctverec1Radek);
                            aktivniPolozka2[1] = tabulka.GetControlFromPosition(ctverec2Sloupec, ctverec2Radek - 2);
                            aktivniPolozka2[2] = tabulka.GetControlFromPosition(ctverec3Sloupec - 1, ctverec3Radek - 1);
                            aktivniPolozka2[3] = tabulka.GetControlFromPosition(ctverec4Sloupec - 2, ctverec4Radek);

                            if (TestOverlap() == true)
                            {
                                otaceni++;
                            }
                            else
                            {
                                return;
                            }
                        }
                        else if (otaceni == 1)
                        {
                            aktivniPolozka2[0] = tabulka.GetControlFromPosition(ctverec1Sloupec, ctverec1Radek);
                            aktivniPolozka2[1] = tabulka.GetControlFromPosition(ctverec2Sloupec + 2, ctverec2Radek);
                            aktivniPolozka2[2] = tabulka.GetControlFromPosition(ctverec3Sloupec + 1, ctverec3Radek - 1);
                            aktivniPolozka2[3] = tabulka.GetControlFromPosition(ctverec4Sloupec, ctverec4Radek - 2);

                            if (TestOverlap() == true)
                            {
                                otaceni++;
                            }
                            else
                            {
                                return;
                            }
                        }
                        else if (otaceni == 2)
                        {
                            aktivniPolozka2[0] = tabulka.GetControlFromPosition(ctverec1Sloupec, ctverec1Radek);
                            aktivniPolozka2[1] = tabulka.GetControlFromPosition(ctverec2Sloupec, ctverec2Radek + 2);
                            aktivniPolozka2[2] = tabulka.GetControlFromPosition(ctverec3Sloupec + 1, ctverec3Radek + 1);
                            aktivniPolozka2[3] = tabulka.GetControlFromPosition(ctverec4Sloupec + 2, ctverec4Radek);

                            if (TestOverlap() == true)
                            {
                                otaceni++;
                            }
                            else
                            {
                                return;
                            }
                        }
                        else if (otaceni == 3)
                        {
                            aktivniPolozka2[0] = tabulka.GetControlFromPosition(ctverec1Sloupec, ctverec1Radek);
                            aktivniPolozka2[1] = tabulka.GetControlFromPosition(ctverec2Sloupec - 2, ctverec2Radek);
                            aktivniPolozka2[2] = tabulka.GetControlFromPosition(ctverec3Sloupec - 1, ctverec3Radek + 1);
                            aktivniPolozka2[3] = tabulka.GetControlFromPosition(ctverec4Sloupec, ctverec4Radek + 2);

                            if (TestOverlap() == true)
                            {
                                otaceni = 0;
                            }
                            else
                            {
                                return;
                            }
                        }
                    }

                    //Nastaví starou poziic na bílo
                    foreach (PictureBox square in aktivniPolozka)
                    {
                        square.BackColor = Color.White;
                    }


                    //Nová pozidce se přebarví na barvu položky
                    int x = 0;
                    foreach (PictureBox square in aktivniPolozka2)
                    {
                        square.BackColor = seznamBarev[momentalniPolozka];
                        aktivniPolozka[x] = square;
                        x++;
                    }
                }
                else if (!CheckGameOver() & e.KeyCode == Keys.ShiftKey)
                {
                    otaceni = 0;


                    Control[,] activePieceArray =
                    {
                        { box6, box16, box26, box36 }, // I
                        { box4, box14,box24, box25 }, // L
                        { box5, box15, box25, box24 }, // J
                        { box14, box15, box5, box6 },  // S
                        { box5, box6, box16, box17 },  // Z
                        { box5, box6, box15, box16 },  // O
                        { box6, box15, box16, box17 }  // T
                };


                    foreach (Control x in aktivniPolozka)
                    {
                        x.BackColor = Color.White;
                    }
                }
            }
        }
    }
}
