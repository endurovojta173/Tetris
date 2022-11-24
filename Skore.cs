using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    public partial class Skore : Form
    {
        public Skore()
        {
            InitializeComponent();
            GetSkore();
            NacistSkore();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Menu menu = new Menu();
            menu.ShowDialog();
            this.Dispose();
        }

        private void GetSkore()
        {
            FileStream fs = new FileStream(@"../../skoreHry.txt", FileMode.OpenOrCreate, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            string radek="";
            char[] separators = { ' ' };
            while(!sr.EndOfStream)
            {
                string polozka = sr.ReadLine();
                if(polozka!=";")
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
                        radek += " " + polozka;
                    }
                }
                else
                {
                    FileStream fs1 = new FileStream(@"../../skore.txt", FileMode.Open, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(fs1);
                    string[] splitRadek = radek.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    radek = "Jméno: " + splitRadek[0] + " Skóre: " + splitRadek[1] + " Délka hry: " + splitRadek[2] + " s Herní mód: " + splitRadek[3] + " "+splitRadek[4] + " Maximální délka hry: " + splitRadek[5];
                    sw.BaseStream.Seek(0, SeekOrigin.End);
                    sw.WriteLine(radek);
                    sw.Close();
                    fs1.Close();
                    radek = "";
                }
            }
            fs.Close();
        }

        private void NacistSkore()
        {
            FileStream fs = new FileStream(@"../../skore.txt",FileMode.OpenOrCreate,FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            while(!sr.EndOfStream)
            {
                listBox1.Items.Add(sr.ReadLine());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //FileStream fs = new FileStream(@"../../skore.txt", FileMode.Create, FileAccess.Write);
            //fs.Close();
        }
    }
}
