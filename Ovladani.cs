using System.Drawing;
using System.Windows.Forms;

namespace Tetris
{
    public partial class TetrisMain : Form
    {
        //Pracuje s inputem, zatím pouzde WASD a šipky
        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
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