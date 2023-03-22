using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace Tetris
{
    public partial class TetrisMain : Form
    {


        //***INICIALIZACE***
        Control[] aktivniPolozka = { null, null, null, null }; 
        Control[] aktivniPolozka2 = { null, null, null, null };
        Control[] dalsiPolozka = {null,null, null, null};
        List<int> VygenerovaneBloky = new List<int>(); //Vygenerovaná čísla pro bloky - dle nich se budou spawnovat 
        int ubehlyCas = 0;
        int momentalniPolozka;
        int dalsiPolozkaInt;
        int otaceni = 0;
        int skore = 0;
        int vycistenychRadku = 0;
        bool konecHry = false;
        int vygenerovanyBlok = 0; //Kontroluje kolikrát se položí vygenerované bloky // Pokud 7, tak se nastaví znova od nuly a vygeneruje se nový seznam bloků

        //Nastavení šipek
        Keys dolevaKlavesa = Keys.Left;
        Keys dopravaKlavesa = Keys.Right;
        Keys doluKlavesa = Keys.Down;
        Keys nahoruKlavesa = Keys.Up;


        //Proměnné pro nastavení
        string jmeno = "Guest";
        int delkaHry = 0;
        int hlasitost = 50;
        bool herniMod = false;
        bool obtiznost = false;
        bool doporuceneOvladani = true;
        char doleva;
        char doprava;
        char dolu;
        char nahoru;

        bool jedinecnyPruchod = false;
        bool pozastaveno = false;

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

        //Hra
        public TetrisMain()
        {
            InitializeComponent();


            //Načte nastavení 
            FileStream fs = new FileStream(@"../../data/nastaveni.txt", FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            jmeno = sr.ReadLine();
            delkaHry = int.Parse(sr.ReadLine());
            hlasitost= int.Parse(sr.ReadLine());

            //Zvuk
            mediaPlayer.URL = "soundtrack.wav";
            mediaPlayer.Ctlcontrols.play();
            mediaPlayer.Ctlenabled = false;
            mediaPlayer.settings.volume = hlasitost;

            herniMod = bool.Parse(sr.ReadLine());
            obtiznost = bool.Parse(sr.ReadLine());
            doporuceneOvladani = bool.Parse(sr.ReadLine());
            if(doporuceneOvladani==false)
            {
                doleva = char.Parse(sr.ReadLine());
                doprava = char.Parse(sr.ReadLine());
                dolu = char.Parse(sr.ReadLine());
                nahoru = char.Parse(sr.ReadLine());
                label_typOvladani.Text = "Ovládání: Vlastní";

                //nastaveniOvladacich klaves
                if (!doporuceneOvladani)
                {
                    PrirazeniKlaves prirazeniKlaves = new PrirazeniKlaves(); //Instance třídy PrirazeniKlaves.cs
                    dolevaKlavesa = prirazeniKlaves.priraditKlavesu(doleva);
                    dopravaKlavesa = prirazeniKlaves.priraditKlavesu(doprava);
                    doluKlavesa = prirazeniKlaves.priraditKlavesu(dolu);
                    nahoruKlavesa = prirazeniKlaves.priraditKlavesu(nahoru);
                }
            }
            else
            {
                label_typOvladani.Text = "Ovládání: Doporučené";
            }

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

            //Generování položek //Vygeneruje se prvních 7 bloků do listu //Pokaždé se položí všech 7 a pak se generuje znova
            Random random = new Random();
            while (VygenerovaneBloky.Count < 7)
            {
                int x = random.Next(7);
                if (!VygenerovaneBloky.Contains(x))
                {
                    VygenerovaneBloky.Add(x);
                }
            }

            //Vybere první položku
            dalsiPolozkaInt = VygenerovaneBloky[0];
            vygenerovanyBlok++;

            DropNewPiece();
        }

        public void DropNewPiece()
        {
            // Zresetuje počet otočení
            otaceni = 0;

            //Přesune další položku na momentální
            momentalniPolozka = dalsiPolozkaInt;

            //Pokud je poslední pieceSequence, vygeneruje novou
            if (vygenerovanyBlok == 7)
            {
                vygenerovanyBlok = 0;

                VygenerovaneBloky.Clear();
                System.Random random = new System.Random();
                while (VygenerovaneBloky.Count < 7) 
                {
                    int x = random.Next(7);
                    if (!VygenerovaneBloky.Contains(x))
                    {
                        VygenerovaneBloky.Add(x);
                    }
                }
            }

            //Zvolí další položku
            dalsiPolozkaInt = VygenerovaneBloky[vygenerovanyBlok];
            vygenerovanyBlok++;


            //**************************************************Box s napovědou

            if (!obtiznost)
            {

                //Nápověda nic nezobrazí, pokud je teprve první blok
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

                //Obarví blok
                foreach (Control square in dalsiPolozka)
                {
                    square.BackColor = seznamBarev[dalsiPolozkaInt];
                }

            }
            //********************************Box s napovědou

            //Rozvržení padajícího kousku //1
            Control[,] aktivniPolozka =
            {
                { box6, box16, box26, box36 }, // I
                { box5, box15, box25, box26 }, // L
                { box6, box16, box26, box25 }, // J
                { box14, box15, box5, box6 },  // S
                { box5, box6, box16, box17 },  // Z
                { box5, box6, box15, box16 },  // O
                { box6, box15, box16, box17 }  // T
            };

            //Vybere padající kousek
            for (int x = 0; x < 4; x++)
            {
                this.aktivniPolozka[x] = aktivniPolozka[momentalniPolozka, x];
            }

            //Zkontroluje jestli není konec hry
            foreach (Control box in this.aktivniPolozka)
            {
                if (box.BackColor != Color.White & box.BackColor != Color.LightGray)
                {
                    //Konec hry //Zde je konec hry pokud je Nekonečný mód
                    rychlostHryTimer.Stop();
                    casHryTimer.Stop();
                    SaveScore();
                    BlackIfEnd();
                    konecHry = true;
                    return;
                }
            }

            //Přiřadí barvu k daným blokům
            foreach (Control square in this.aktivniPolozka)
            {
                square.BackColor = seznamBarev[momentalniPolozka];
            }
        }

        //Otestuje jestli budoucí pohyb (doprava,doleva,dolů) byl mimo tabulku nebo jiný kousek
        public bool TestMove(string smer)
        {
            int momentalniVrchniRadek = 21; 
            int momentalniSpodniRadek = 0;
            int momentalniLevySloupec = 9;
            int momentalniPravySloupec = 0;

            int dalsiCtverec = 0;

            Control novyCtverec = new Control();
            //Určí hraniční sloupce a řádky ve kterých se blok nachází
            foreach (Control ctverec in aktivniPolozka)
            {
                if (tabulka.GetRow(ctverec) < momentalniVrchniRadek)
                {
                    momentalniVrchniRadek = tabulka.GetRow(ctverec);
                }
                if (tabulka.GetRow(ctverec) > momentalniSpodniRadek)
                {
                    momentalniSpodniRadek = tabulka.GetRow(ctverec);
                }
                if (tabulka.GetColumn(ctverec) < momentalniLevySloupec)
                {
                    momentalniLevySloupec = tabulka.GetColumn(ctverec);
                }
                if (tabulka.GetColumn(ctverec) > momentalniPravySloupec)
                {
                    momentalniPravySloupec = tabulka.GetColumn(ctverec);
                }
            }

            //Otestuje jestli by byly nějaké čtverce mimo tabulku
            foreach (Control ctverec in aktivniPolozka)
            {
                int ctverecRadek = tabulka.GetRow(ctverec);
                int ctverecSloupec = tabulka.GetColumn(ctverec);

                //Vlevo
                if (smer == "left" && ctverecSloupec > 0)
                {
                    novyCtverec = tabulka.GetControlFromPosition(ctverecSloupec - 1, ctverecRadek);
                    dalsiCtverec = momentalniLevySloupec;
                }
                else if (smer == "left" && ctverecSloupec == 0)
                {
                    //Otestuje jestli by nebyl objekt doleva mimo tabulku
                    return false;
                }

                //Vpravo
                else if (smer == "right" && ctverecSloupec < 9)
                {
                    novyCtverec = tabulka.GetControlFromPosition(ctverecSloupec + 1, ctverecRadek);
                    dalsiCtverec = momentalniPravySloupec;
                }
                else if (smer == "right" && ctverecSloupec == 9)
                {
                    //Otestuje jestli by nebyl objekt doprava mimo tabulku
                    return false;
                }

                //Dolů
                else if (smer == "down" && ctverecRadek < 21)
                {
                    novyCtverec = tabulka.GetControlFromPosition(ctverecSloupec, ctverecRadek + 1);
                    dalsiCtverec = momentalniSpodniRadek;
                }
                else if (smer == "down" && ctverecRadek == 21)
                {
                    return false;
                    //Otestuje, jestli by nebyl pohyb pod tabulku
                }

                //Otestuje jestli překryje jiný kousek
                if ((novyCtverec.BackColor != Color.White) && !aktivniPolozka.Contains(novyCtverec))
                {
                    return false;
                }

            }

            //Když projde všemi testy tak vrátí true
            return true;
        }
        public void MovePiece(string smer)
        {
            // Vymaže starou pozici a určí novou podle směru ovládání
            int x = 0;
            foreach (PictureBox ctverec in aktivniPolozka)
            {
                ctverec.BackColor = Color.White;
                int ctverecRadek = tabulka.GetRow(ctverec);
                int ctverecSloupec = tabulka.GetColumn(ctverec);
                int novyCtverecRadek = 0;
                int novyCtverecSloupec = 0;
                if (smer == "left")
                {
                    novyCtverecSloupec = ctverecSloupec - 1;
                    novyCtverecRadek = ctverecRadek;
                }
                else if (smer == "right")
                {
                    novyCtverecSloupec = ctverecSloupec + 1;
                    novyCtverecRadek = ctverecRadek;
                }
                else if (smer == "down")
                {
                    novyCtverecSloupec = ctverecSloupec;
                    novyCtverecRadek = ctverecRadek + 1;
                }

                aktivniPolozka2[x] = tabulka.GetControlFromPosition(novyCtverecSloupec, novyCtverecRadek);
                x++;
            }

            //Zkopíruje aktivniPolozka2 do aktivniPolozka
            x = 0;
            foreach (PictureBox ctverec in aktivniPolozka2)
            {

                aktivniPolozka[x] = ctverec;
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
        private void RychlostHryTimer_Tick(object sender, EventArgs e)
        {
            if (CheckGameOver() == true)
            {
                rychlostHryTimer.Stop();
                casHryTimer.Stop();
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
                    }
                    if (CheckForCompleteRows() > -1)
                    {
                        ClearFullRow();
                        
                        Zrychleni();
                    }
                    DropNewPiece();
                }
            }
        }

        //Zrychleni hry každých 5 clear radku
        private void Zrychleni()
        {
            if(vycistenychRadku%5==0&&rychlostHryTimer.Interval>100)
            {
                rychlostHryTimer.Interval -= 50;
            }
        }

        //Vyčistí nejnižší plný řádek
        private void ClearFullRow()
        {
            int plnyRadek = CheckForCompleteRows();

            //Přebarví ho na bílo
            for (int x = 0; x <= 9; x++)
            {
                Control z = tabulka.GetControlFromPosition(x, plnyRadek);
                z.BackColor = Color.White;
            }

            //Posune všechny ostatní čtverce dolů
            for (int x = plnyRadek - 1; x >= 0; x--) //Každý řádek nad vymazaným
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
            Control[] vrchniRadekSpawn = {box5, box6}; //Opraven bug, kvůli kterému se hra ukončila i při vystavení bloků nahoru, i když se mohli dále spawnovat

            foreach (Control box in vrchniRadekSpawn)
            {
                if ((box.BackColor != Color.White) & !aktivniPolozka.Contains(box))
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

            if (konecHry == true)
            {
                //uloží skóre
                SaveScore();
                BlackIfEnd();
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
            if(ubehlyCas%84==0)
            {
                mediaPlayer.Ctlcontrols.stop();
                mediaPlayer.Ctlcontrols.play();
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
                FileStream fs = new FileStream(@"../../data/skore.txt", FileMode.Open, FileAccess.Write);
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
        private void BlackIfEnd()
        {
            foreach(PictureBox pbx in tabulka.Controls)
            {
                pbx.Enabled = false;
                if(pbx.BackColor==Color.White)
                {
                    pbx.BackColor = Color.Black;
                }
                else //Cool feature
                {
                    pbx.BackColor = Color.Black;
                }
            }
            foreach(PictureBox pbx in tabulkaNapoveda.Controls)
            {
                pbx.Enabled=false;
                if (pbx.BackColor == Color.White)
                {
                    pbx.BackColor = Color.Black;
                }
                else //Cool feature
                {
                    pbx.BackColor = Color.Black;
                }
            }
            tabulka.BackColor = Color.Red;
            tabulkaNapoveda.BackColor = Color.Red;
        }

        //Je potřeba udělat vypnutí soundtracku a pozastavení timeru //hotovo
        //Tlacitko pro menu
        private void button_menu_Click(object sender, EventArgs e)
        {
            rychlostHryTimer.Stop();
            casHryTimer.Stop();
            mediaPlayer.Ctlcontrols.stop();
            this.Hide();
            Menu menu = new Menu();
            menu.ShowDialog();
            this.Dispose();
        }
        //Tlacitko pro skore
        private void button_skore_Click(object sender, EventArgs e)
        {
            rychlostHryTimer.Stop();
            casHryTimer.Stop();
            mediaPlayer.Ctlcontrols.stop();
            this.Hide();
            Skore skore = new Skore();
            skore.ShowDialog();
            this.Dispose();
        }
        //ukonci app
        private void button_konec_Click(object sender, EventArgs e)
        {
            rychlostHryTimer.Stop();
            casHryTimer.Stop();
            mediaPlayer.Ctlcontrols.stop();
            this.Dispose();
        }
        //zacne novou hru
        private void button_novaHra_Click(object sender, EventArgs e)
        {
            rychlostHryTimer.Stop();
            casHryTimer.Stop();
            //soundtrack.Stop();
            mediaPlayer.Ctlcontrols.stop();

            this.Hide();
            TetrisMain main = new TetrisMain();
            main.ShowDialog();
            this.Dispose();
        }

        //**************************Ovládání******************************
        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            //Pozastavuje hru
            if (e.KeyCode == Keys.Escape&&!pozastaveno)
            {
                casHryTimer.Stop();
                rychlostHryTimer.Stop();
                mediaPlayer.Ctlcontrols.stop();

                pozastaveno = true;
                label_pozastaveni.Text = "Hra je POZASTAVENA - stiskněte ESC pro pokračování";
            }
            else if (e.KeyCode == Keys.Escape&&pozastaveno)
            {
                casHryTimer.Start();
                rychlostHryTimer.Start();
                mediaPlayer.Ctlcontrols.play();

                pozastaveno = false;
                label_pozastaveni.Text = "Pro pozastavení stiskněte ESC";
            }
            
            if (!pozastaveno)
            {
                if (!CheckGameOver() & ((e.KeyCode == dolevaKlavesa) & TestMove("left") == true))
                {
                    MovePiece("left");
                }
                else if (!CheckGameOver() & ((e.KeyCode == dopravaKlavesa) & TestMove("right") == true))
                {
                    MovePiece("right");
                }
                else if ((e.KeyCode == doluKlavesa) & TestMove("down") == true)
                {
                    MovePiece("down");
                }
                else if (e.KeyCode == nahoruKlavesa)
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
                    
                    //Nastaví starou pozici na bílo
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
            }
        }
    }
}
