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

        public static void SaveSkore()
        {
            FileStream fs = new FileStream(@"../../skoreHry.txt", FileMode.OpenOrCreate, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            string radek = "";
            char[] separators = { ';' };
            while (!sr.EndOfStream)
            {
                string polozka = sr.ReadLine();
                if (polozka != "#")
                {
                    if (polozka == "True")
                    {
                        radek += " Časový mód";
                    }
                    else if (polozka == "False")
                    {
                        radek += " Nekonečný mód";
                    }
                    else
                    {
                        radek += polozka;
                    }
                }
                else
                {
                    FileStream fs1 = new FileStream(@"../../skore.txt", FileMode.Open, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(fs1);
                    string[] splitRadek = radek.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    radek = splitRadek[0] + ";" + splitRadek[1] + ";" + splitRadek[2] + ";" + splitRadek[3] + ";" + splitRadek[4] + ";" + splitRadek[5];
                    sw.BaseStream.Seek(0, SeekOrigin.End);
                    sw.WriteLine(radek);
                    sw.Close();
                    fs1.Close();
                }
            }
            fs.Close();
            FileStream fs2 = new FileStream(@"../../skoreHry.txt", FileMode.Create, FileAccess.Write);
            fs2.Close();
        }
    }
}
