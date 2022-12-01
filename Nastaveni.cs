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
            string jmeno = sr.ReadLine();
            string delka = sr.ReadLine();
            bool mod = bool.Parse(sr.ReadLine());
            bool obtiznost = bool.Parse(sr.ReadLine());

            label_aktualniJmeno.Text = "Jméno: " + jmeno;
            textBox1.Text = jmeno;
            label_delka.Text = "Délka módu s omezeným časem:  " + delka +" s";
            textBox2.Text = delka;


            if (obtiznost)
            {
                label_obtiznost.Text = "Obtížnost: Těžká";
                radioButton4.Checked = true;
            }
            else
            {
                label_obtiznost.Text = "Obtížnost: Lehká";
                radioButton3.Checked = true;
            }
            if(mod)
            {
                label_herniMod.Text = "Herní mód: Nekonečný mód";
                label_delkaModuSOmezenymCasem.Visible = false;
                textBox2.Visible = false;
            }
            else
            {

            }
            fs.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Menu menu = new Menu();
            menu.ShowDialog();
            this.Dispose();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            label_herniMod.Text = "Herní mód: Nekonečný mód";
            label_delkaModuSOmezenymCasem.Visible = false;
            textBox2.Visible = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            label_herniMod.Text = "Herní mód: Časový mód";
            label_delkaModuSOmezenymCasem.Visible = true;
            textBox2.Visible = true;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            label_obtiznost.Text = "Obtížnost: Těžká";
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            label_obtiznost.Text = "Obtížnost: Lehká";
        }
    }
}
