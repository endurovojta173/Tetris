using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Media; //pridano
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace Tetris
{
    public partial class TetrisMain : Form
    {

        //***INICIALIZACE***
        Control[] aktivniPolozka = { null, null, null, null };
        Control[] aktivniPolozka2 = { null, null, null, null };
        Control[] dalsiPolozka = {null,null, null, null};
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

        //Proměnné pro nastavení
        string jmeno = "Guest";
        int delkaHry = 0;
        bool herniMod = false;
        bool obtiznost = false;

        bool jedinecnyPruchod = false;

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

            //Načte nastavení 
            FileStream fs = new FileStream(@"../../nastaveni.txt", FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            jmeno = sr.ReadLine();
            delkaHry = int.Parse(sr.ReadLine());
            herniMod = bool.Parse(sr.ReadLine());
            obtiznost = bool.Parse(sr.ReadLine());

            fs.Close();

            label_jmeno.Text = jmeno;
            if (herniMod)
            {
                label_herniRezim.Text += " Časový mód";
                if(delkaHry>=60)
                {

                    label_delkaHry.Text += " " + delkaHry/60+" m " + delkaHry%60+" s";

                }
                else label_delkaHry.Text += " " + delkaHry + " s";
            }
            else
            {
                label_herniRezim.Text += " Nekonečný mód";
                label_delkaHry.Text += " Nekonečná";
            }
            if(obtiznost)
            {
                label_obtiznost.Text += " Těžká";
            }
            else
            {
                label_obtiznost.Text += " Lehká";
            }


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


            //**************************************************Box s napovědou

            if (!obtiznost)
            {

                // If not first move, clear next piece panel
                if (dalsiPolozka.Contains(null) == false)
                {
                    foreach (Control x in dalsiPolozka)
                    {
                        x.BackColor = Color.White;
                    }
                }

                //Rozložení
                Control[,] poleDalsiPolozky =
                {
                { box203, box207, box211, box215 }, // I piece
                { box202, box206, box210, box211 }, // L piece
                { box203, box207, box211, box210 }, // J piece
                { box206, box207, box203, box204 }, // S piece
                { box202, box203, box207, box208 }, // Z piece
                { box206, box207, box210, box211 }, // O piece
                { box207, box210, box211, box212 }  // T piece
                };

                for (int x = 0; x < 4; x++)
                {
                    dalsiPolozka[x] = poleDalsiPolozky[dalsiPolozkaInt, x];
                }

                // Populate next piece panel with correct color
                foreach (Control square in dalsiPolozka)
                {
                    square.BackColor = seznamBarev[dalsiPolozkaInt];
                }

            }
            //********************************Box s napovědou


            //Rozvržení padajícího kousku //1
            Control[,] activePieceArray =
            {
                { box6, box16, box26, box36 }, // I
                { box5, box15, box25, box26 }, // L
                { box6, box16, box26, box25 }, // J
                { box14, box15, box5, box6 },  // S
                { box5, box6, box16, box17 },  // Z
                { box5, box6, box15, box16 },  // O
                { box6, box15, box16, box17 }  // T
            };




            //Vybere padající kousek //1
            for (int x = 0; x < 4; x++)
            {
                aktivniPolozka[x] = activePieceArray[momentalniPolozka, x];
            }



            //Zkontroluje jestli není konec hry //1
            foreach (Control box in aktivniPolozka)
            {
                if (box.BackColor != Color.White & box.BackColor != Color.LightGray)
                {
                    //Konec hry //Zde je konec hry pokud je Nekonečný mód
                    rychlostHryTimer.Stop();
                    casHryTimer.Stop();
                    SaveScore();
                    DisableIfEnd();
                    konecHry = true;
                    MessageBox.Show("Game over!");
                    return;
                }
            }

            // Populate falling piece squares with correct color //1
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
                MessageBox.Show("Game over!X");
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
        //***!!!!Pokud je mód na čas tak může i po vypršení času
        private bool CheckGameOver()
        {
            Control[] topRow = { box1, box2, box3, box4, box5, box6, box7, box8, box9, box10 };

                foreach (Control box in topRow)
                {
                    if ((box.BackColor != Color.White & box.BackColor != Color.LightGray) & !aktivniPolozka.Contains(box))
                    {
                        konecHry = true;
                    }
                }

                if (herniMod)
                {
                    if (ubehlyCas == delkaHry)
                    {
                        konecHry = true;
                    }
                }
            //pořád se opakuje dokola, kvůli tomu se skore uloží 3x // vyřešení pomocí bool
            if (konecHry == true)
            {
                //uloží skóre

                SaveScore();
                DisableIfEnd();
                //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!Ukončí hru
                return true;
            }
            else return false;
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
        }

        //Uloží Nick, skore, čas hry a režim hry(pokud hra na čas tak jak dlouho mohl hrát) a obtižnost //předělat aby rovnou ukládalo do skore.txt //!!!!!!!!!!!!!!Už funguje ale je potřeba otestovat
        private void SaveScore()
        {
            if (!jedinecnyPruchod) //Pomocná podmínka, aby funkce neproběhla 3x // nevim proč sa opakuje
            {
                FileStream fs = new FileStream(@"../../skore.txt", FileMode.Open, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                string delkaHryString = delkaHry.ToString();
                string obtiznostString = "Lehká;";
                string herniModString = "Časově omezený;";
                if (!herniMod)
                {
                    herniModString = "Nekonečný mód;";
                    delkaHryString = "Nekonečná;";

                }
                else
                {
                    // delkaHryString = "Neomezená;";
                }
                if (obtiznost) obtiznostString = "Těžká;";
                sw.BaseStream.Seek(0, SeekOrigin.End);
                sw.WriteLine(jmeno + ";" + skore + ";" + ubehlyCas + ";" + herniModString + delkaHryString + ";" + obtiznostString);
                sw.Close();
                fs.Close();
                jedinecnyPruchod = true;
            }
        }


        //Cool feature -- Změní položené bloky na černé 
        private void DisableIfEnd()
        {
            foreach(PictureBox pbx in tabulka.Controls)
            {
                pbx.Enabled = false;
                if(pbx.BackColor==Color.White)
                {
                    pbx.BackColor = Color.Gray;
                }
                else //Cool feature
                {
                    pbx.BackColor = Color.Black;
                }
                button_novaHra.Enabled = true;
                button_menu.Enabled = true;
                button_konec.Enabled = true;
                button_skore.Enabled = true;

            }
            foreach(PictureBox pbx in tabulkaNapoveda.Controls)
            {
                pbx.Enabled=false;
                if (pbx.BackColor == Color.White)
                {
                    pbx.BackColor = Color.Gray;
                }
                else //Cool feature
                {
                    pbx.BackColor = Color.Black;
                }
            }
            tabulka.BackColor = Color.Black;
            tabulkaNapoveda.BackColor = Color.Black;
        }











































        //**************************Ovládání******************************
        //Pracuje s inputem, zatím pouzde WASD a šipky
        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            //Přesune do menu
            if(e.KeyCode==Keys.M)
            {
                soundtrack.Stop();
                this.Hide();
                menu.ShowDialog();
                this.Close();
            }
            //Vypne tetris
            if (e.KeyCode == Keys.K)
            {
                soundtrack.Stop();
                this.Hide();
                this.Close();
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
                soundtrack.Stop();
                pozastaveno = true;
            }
            else if (e.KeyCode == Keys.E)
            {
                pozastaveno = false;
                casHryTimer.Start();
                rychlostHryTimer.Start();
                soundtrack.PlayLooping();
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
                //Feature se shiftem
                /*else if (!CheckGameOver() & e.KeyCode == Keys.ShiftKey)
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
                }*/
            }
        }

        //Tlacitko pro menu
        private void button_menu_Click(object sender, EventArgs e)
        {
            this.Hide();
            Menu menu = new Menu();
            menu.ShowDialog();
            this.Dispose();
        }
        //ukonci app
        private void button_konec_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        //zacne novou hru
        private void button_novaHra_Click(object sender, EventArgs e)
        {
            this.Hide();
            TetrisMain main = new TetrisMain();
            main.ShowDialog();
            this.Dispose();
        }

        private void button_skore_Click(object sender, EventArgs e)
        {
            this.Hide();
            Skore skore = new Skore();
            skore.ShowDialog();
            this.Dispose();
        }
    }
}
