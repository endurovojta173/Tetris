using System.Drawing;
using System.Windows.Forms;

namespace Tetris
{
    public partial class Game : Form
    {
        // Handle inputs - triggered on any keypress
        // Cleanup needed
        // Handle inputs - triggered on any keypress
        // Cleanup needed
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
                //Rotate
                int square1Col = tabulka.GetColumn(aktivniPolozka[0]);
                int square1Row = tabulka.GetRow(aktivniPolozka[0]);

                int square2Col = tabulka.GetColumn(aktivniPolozka[1]);
                int square2Row = tabulka.GetRow(aktivniPolozka[1]);

                int square3Col = tabulka.GetColumn(aktivniPolozka[2]);
                int square3Row = tabulka.GetRow(aktivniPolozka[2]);

                int square4Col = tabulka.GetColumn(aktivniPolozka[3]);
                int square4Row = tabulka.GetRow(aktivniPolozka[3]);

                if (momentalniPolozka == 0) //The line piece
                {
                    //Test if piece is too close to edge of board
                    if (otaceni == 0 & (square1Col == 0 | square1Col == 1 | square1Col == 9))
                    {
                        return;
                    }
                    else if (otaceni == 1 & (square3Col == 0 | square3Col == 1 | square3Col == 9))
                    {
                        return;
                    }

                    //If test passes, rotate piece
                    if (otaceni == 0)
                    {
                        aktivniPolozka2[0] = tabulka.GetControlFromPosition(square1Col - 2, square1Row);
                        aktivniPolozka2[1] = tabulka.GetControlFromPosition(square2Col - 1, square2Row - 1);
                        aktivniPolozka2[2] = tabulka.GetControlFromPosition(square3Col, square3Row - 2);
                        aktivniPolozka2[3] = tabulka.GetControlFromPosition(square4Col + 1, square4Row - 3);

                        //Test if new position overlaps another piece. If it does, cancel rotation.
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
                        aktivniPolozka2[0] = tabulka.GetControlFromPosition(square1Col + 2, square1Row);
                        aktivniPolozka2[1] = tabulka.GetControlFromPosition(square2Col + 1, square2Row + 1);
                        aktivniPolozka2[2] = tabulka.GetControlFromPosition(square3Col, square3Row + 2);
                        aktivniPolozka2[3] = tabulka.GetControlFromPosition(square4Col - 1, square4Row + 3);

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
                else if (momentalniPolozka == 1) //The normal L
                {
                    //Test if piece is too close to edge of board
                    if (otaceni == 0 & (square1Col == 8 | square1Col == 9))
                    {
                        return;
                    }
                    else if (otaceni == 2 & (square1Col == 9))
                    {
                        return;
                    }

                    //If test passes, rotate piece
                    if (otaceni == 0)
                    {
                        aktivniPolozka2[0] = tabulka.GetControlFromPosition(square1Col, square1Row + 2);
                        aktivniPolozka2[1] = tabulka.GetControlFromPosition(square2Col + 1, square2Row + 1);
                        aktivniPolozka2[2] = tabulka.GetControlFromPosition(square3Col + 2, square3Row);
                        aktivniPolozka2[3] = tabulka.GetControlFromPosition(square4Col + 1, square4Row - 1);

                        //Test if new position overlaps another piece. If it does, cancel rotation.
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
                        aktivniPolozka2[0] = tabulka.GetControlFromPosition(square1Col + 1, square1Row);
                        aktivniPolozka2[1] = tabulka.GetControlFromPosition(square2Col, square2Row - 1);
                        aktivniPolozka2[2] = tabulka.GetControlFromPosition(square3Col - 1, square3Row - 2);
                        aktivniPolozka2[3] = tabulka.GetControlFromPosition(square4Col - 2, square4Row - 1);

                        //Test if new position overlaps another piece. If it does, cancel rotation.
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
                        aktivniPolozka2[0] = tabulka.GetControlFromPosition(square1Col + 1, square1Row - 1);
                        aktivniPolozka2[1] = tabulka.GetControlFromPosition(square2Col, square2Row);
                        aktivniPolozka2[2] = tabulka.GetControlFromPosition(square3Col - 1, square3Row + 1);
                        aktivniPolozka2[3] = tabulka.GetControlFromPosition(square4Col, square4Row + 2);

                        //Test if new position overlaps another piece. If it does, cancel rotation.
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
                        aktivniPolozka2[0] = tabulka.GetControlFromPosition(square1Col - 2, square1Row - 1);
                        aktivniPolozka2[1] = tabulka.GetControlFromPosition(square2Col - 1, square2Row);
                        aktivniPolozka2[2] = tabulka.GetControlFromPosition(square3Col, square3Row + 1);
                        aktivniPolozka2[3] = tabulka.GetControlFromPosition(square4Col + 1, square4Row);

                        //Test if new position overlaps another piece. If it does, cancel rotation.
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
                else if (momentalniPolozka == 2) //The backwards L
                {
                    //Test if piece is too close to edge of board
                    if (otaceni == 0 & (square1Col == 0 | square1Col == 1))
                    {
                        return;
                    }
                    else if (otaceni == 2 & square1Col == 0)
                    {
                        return;
                    }

                    //If test passes, rotate piece
                    if (otaceni == 0)
                    {
                        aktivniPolozka2[0] = tabulka.GetControlFromPosition(square1Col - 2, square1Row + 1);
                        aktivniPolozka2[1] = tabulka.GetControlFromPosition(square2Col - 1, square2Row);
                        aktivniPolozka2[2] = tabulka.GetControlFromPosition(square3Col, square3Row - 1);
                        aktivniPolozka2[3] = tabulka.GetControlFromPosition(square4Col + 1, square4Row);

                        //Test if new position overlaps another piece. If it does, cancel rotation.
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
                        aktivniPolozka2[0] = tabulka.GetControlFromPosition(square1Col + 1, square1Row + 1);
                        aktivniPolozka2[1] = tabulka.GetControlFromPosition(square2Col, square2Row);
                        aktivniPolozka2[2] = tabulka.GetControlFromPosition(square3Col - 1, square3Row - 1);
                        aktivniPolozka2[3] = tabulka.GetControlFromPosition(square4Col, square4Row - 2);

                        //Test if new position overlaps another piece. If it does, cancel rotation.
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
                        aktivniPolozka2[0] = tabulka.GetControlFromPosition(square1Col + 1, square1Row);
                        aktivniPolozka2[1] = tabulka.GetControlFromPosition(square2Col, square2Row + 1);
                        aktivniPolozka2[2] = tabulka.GetControlFromPosition(square3Col - 1, square3Row + 2);
                        aktivniPolozka2[3] = tabulka.GetControlFromPosition(square4Col - 2, square4Row + 1);

                        //Test if new position overlaps another piece. If it does, cancel rotation.
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
                        aktivniPolozka2[0] = tabulka.GetControlFromPosition(square1Col, square1Row - 2);
                        aktivniPolozka2[1] = tabulka.GetControlFromPosition(square2Col + 1, square2Row - 1);
                        aktivniPolozka2[2] = tabulka.GetControlFromPosition(square3Col + 2, square3Row);
                        aktivniPolozka2[3] = tabulka.GetControlFromPosition(square4Col + 1, square4Row + 1);

                        //Test if new position overlaps another piece. If it does, cancel rotation.
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
                else if (momentalniPolozka == 3) //The normal S
                {
                    //Test if piece is too close to edge of board
                    if (otaceni == 0 & (square1Row == 1 | square1Col == 9))
                    {
                        return;
                    }
                    else if (otaceni == 1 & square1Col == 0)
                    {
                        return;
                    }

                    //If test passes, rotate piece
                    if (otaceni == 0)
                    {

                        aktivniPolozka2[0] = tabulka.GetControlFromPosition(square1Col + 1, square1Row - 2);
                        aktivniPolozka2[1] = tabulka.GetControlFromPosition(square2Col, square2Row - 1);
                        aktivniPolozka2[2] = tabulka.GetControlFromPosition(square3Col + 1, square3Row);
                        aktivniPolozka2[3] = tabulka.GetControlFromPosition(square4Col, square4Row + 1);


                        //Test if new position overlaps another piece. If it does, cancel rotation.
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
                        aktivniPolozka2[0] = tabulka.GetControlFromPosition(square1Col - 1, square1Row + 2);
                        aktivniPolozka2[1] = tabulka.GetControlFromPosition(square2Col, square2Row + 1);
                        aktivniPolozka2[2] = tabulka.GetControlFromPosition(square3Col - 1, square3Row);
                        aktivniPolozka2[3] = tabulka.GetControlFromPosition(square4Col, square4Row - 1);

                        //Test if new position overlaps another piece. If it does, cancel rotation.
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
                else if (momentalniPolozka == 4) //The backwards S
                {
                    //Test if piece is too close to edge of board
                    if (otaceni == 1 & square1Col == 8)
                    {
                        return;
                    }

                    //If test passes, rotate piece
                    if (otaceni == 0)
                    {
                        aktivniPolozka2[0] = tabulka.GetControlFromPosition(square1Col, square1Row + 1);
                        aktivniPolozka2[1] = tabulka.GetControlFromPosition(square2Col - 1, square2Row);
                        aktivniPolozka2[2] = tabulka.GetControlFromPosition(square3Col, square3Row - 1);
                        aktivniPolozka2[3] = tabulka.GetControlFromPosition(square4Col - 1, square4Row - 2);

                        //Test if new position overlaps another piece. If it does, cancel rotation.
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
                        aktivniPolozka2[0] = tabulka.GetControlFromPosition(square1Col, square1Row - 1);
                        aktivniPolozka2[1] = tabulka.GetControlFromPosition(square2Col + 1, square2Row);
                        aktivniPolozka2[2] = tabulka.GetControlFromPosition(square3Col, square3Row + 1);
                        aktivniPolozka2[3] = tabulka.GetControlFromPosition(square4Col + 1, square4Row + 2);

                        //Test if new position overlaps another piece. If it does, cancel rotation.
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
                else if (momentalniPolozka == 5) //The square
                {
                    //The square cannot rotate
                    return;
                }
                else if (momentalniPolozka == 6) //The pyramid
                {
                    //Test if piece is too close to edge of board
                    if (otaceni == 1 & square1Col == 9)
                    {
                        return;
                    }
                    else if (otaceni == 3 & square1Col == 0)
                    {
                        return;
                    }

                    //If test passes, rotate piece
                    if (otaceni == 0)
                    {
                        aktivniPolozka2[0] = tabulka.GetControlFromPosition(square1Col, square1Row);
                        aktivniPolozka2[1] = tabulka.GetControlFromPosition(square2Col, square2Row - 2);
                        aktivniPolozka2[2] = tabulka.GetControlFromPosition(square3Col - 1, square3Row - 1);
                        aktivniPolozka2[3] = tabulka.GetControlFromPosition(square4Col - 2, square4Row);

                        //Test if new position overlaps another piece. If it does, cancel rotation.
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
                        aktivniPolozka2[0] = tabulka.GetControlFromPosition(square1Col, square1Row);
                        aktivniPolozka2[1] = tabulka.GetControlFromPosition(square2Col + 2, square2Row);
                        aktivniPolozka2[2] = tabulka.GetControlFromPosition(square3Col + 1, square3Row - 1);
                        aktivniPolozka2[3] = tabulka.GetControlFromPosition(square4Col, square4Row - 2);

                        //Test if new position overlaps another piece. If it does, cancel rotation.
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
                        aktivniPolozka2[0] = tabulka.GetControlFromPosition(square1Col, square1Row);
                        aktivniPolozka2[1] = tabulka.GetControlFromPosition(square2Col, square2Row + 2);
                        aktivniPolozka2[2] = tabulka.GetControlFromPosition(square3Col + 1, square3Row + 1);
                        aktivniPolozka2[3] = tabulka.GetControlFromPosition(square4Col + 2, square4Row);

                        //Test if new position overlaps another piece. If it does, cancel rotation.
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
                        aktivniPolozka2[0] = tabulka.GetControlFromPosition(square1Col, square1Row);
                        aktivniPolozka2[1] = tabulka.GetControlFromPosition(square2Col - 2, square2Row);
                        aktivniPolozka2[2] = tabulka.GetControlFromPosition(square3Col - 1, square3Row + 1);
                        aktivniPolozka2[3] = tabulka.GetControlFromPosition(square4Col, square4Row + 2);

                        //Test if new position overlaps another piece. If it does, cancel rotation.
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

                //Set old position of piece to white
                foreach (PictureBox square in aktivniPolozka)
                {
                    square.BackColor = Color.White;
                }


                //Set new position of piece to that piece's color
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


                // Layout options for falling piece
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

                // Erase falling piece
                foreach (Control x in aktivniPolozka)
                {
                    x.BackColor = Color.White;
                }
            }
        }
    }
}