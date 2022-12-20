using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    internal class VelikostOkna
    {
        public void NastavitVelikostOkna(Form thisForm,string velikostOkna)
        {
            char[] sep = { ' ' };
            string[] velikost = velikostOkna.Split(sep, StringSplitOptions.RemoveEmptyEntries);
            thisForm.Size = new System.Drawing.Size(int.Parse(velikost[0]), int.Parse(velikost[2]));
            NastavitLokaciOkna(thisForm);
        }
        private void NastavitLokaciOkna(Form thisForm)
        {
            thisForm.StartPosition = FormStartPosition.CenterScreen;
        }
    }
}
