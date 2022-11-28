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
    public partial class Nastaveni : Form
    {
        public Nastaveni()
        {
            InitializeComponent();
            ZobrazitAktualniNastaveni();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            FileStream fs = new FileStream(@"../../nastaveni.txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(textBox1.Text);
            sw.WriteLine(textBox2.Text);
            if(radioButton1.Checked)
            {
                sw.WriteLine("false");
            }
            else
            {
                sw.WriteLine("true");
            }
            if(radioButton3.Checked)
            {
                sw.WriteLine("false");
            }
            else
            {
                sw.WriteLine("true");
            }
            sw.Close();
            fs.Close();
            ZobrazitAktualniNastaveni();
        }



        //Nutno vyřešit co když je nastavení prázdné // nastavit defaultní hodnoty

        private void ZobrazitAktualniNastaveni()
        {

            FileStream fs = new FileStream(@"../../nastaveni.txt", FileMode.OpenOrCreate, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            label_aktualniJmeno.Text = "Jméno: " + sr.ReadLine();
            label_delka.Text = "Délka módu s omezeným časem:  " + sr.ReadLine()+" s";
            bool mod = bool.Parse(sr.ReadLine());
            bool obtiznost = bool.Parse(sr.ReadLine());
            if (mod) label_herniMod.Text = "Herní mód: Časový mód";
            else label_herniMod.Text = "Herní mód: Nekonečný mód";
            if (obtiznost) label_obtiznost.Text = "Obtížnost: Těžká";
            else label_obtiznost.Text = "Obtížnost: Lehká";
            fs.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Menu menu = new Menu();
            menu.ShowDialog();
            this.Dispose();
        }
    }
}
