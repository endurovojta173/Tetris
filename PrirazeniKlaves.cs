using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    internal class PrirazeniKlaves
    {
        public Keys priraditKlavesu(char znak)
        {
            Keys klavesa=Keys.X;
            if (znak >= 'a' && znak <= 'z')
            {
                znak = char.ToUpper(znak);
                klavesa = (Keys)znak;
            }
            else if (znak >= 'A' && znak <= 'Z')
            {
                klavesa = (Keys)znak;
            }
            else if (znak >= '0' && znak <= '9')
            {
                if (znak == '0') klavesa = Keys.NumPad0;
                if (znak == '1') klavesa = Keys.NumPad1;
                if (znak == '2') klavesa = Keys.NumPad2;
                if (znak == '3') klavesa = Keys.NumPad3;
                if (znak == '4') klavesa = Keys.NumPad4;
                if (znak == '5') klavesa = Keys.NumPad5;
                if (znak == '6') klavesa = Keys.NumPad6;
                if (znak == '7') klavesa = Keys.NumPad7;
                if (znak == '8') klavesa = Keys.NumPad8;
                if (znak == '9') klavesa = Keys.NumPad9;
            }
            else if(znak=='-')klavesa= Keys.Subtract;
            else if (znak=='+')klavesa=Keys.Add;
            return klavesa;
        }
        public bool jeVstupValidni(string vstup)
        {
            bool jeValidni = false;
            if (vstup.Length > 0)
            {
                char znak = char.Parse(vstup);
                if (znak >= 'a' && znak <= 'z') jeValidni = true;
                else if (znak >= 'A' && znak <= 'Z') jeValidni = true;
                else if (znak >= '0' && znak <= '9') jeValidni = true;
                else if (znak == '-') jeValidni = true;
                else if (znak == '+') jeValidni = true;
            }
            return jeValidni;
        }

    }
}
