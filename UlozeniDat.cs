using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    internal class UlozeniDat
    {
        public void Ulozeni(string jmeno, int skore, bool rezimHry)
        {
            FileStream fs = new FileStream(@"../../skore.txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(jmeno+";");
            sw.Write(skore + ";");
            string herniMod;
            if(rezimHry)
            {
                herniMod = "Časově omezený";
            }
            else
            {
                herniMod = "Nekonečný";
            }
            sw.Write(herniMod+";");
        }
    }
}
