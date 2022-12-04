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
            //Ověřuje mód //Časově omezený mód == true
            if(radioButton_nekonecnyMod.Checked)
            {
                sw.WriteLine("false");
            }
            else
            {
                sw.WriteLine("true");
            }
            //Ověřuje obtížnost //Těžká == true
            if(radioButton_lehka.Checked)
            {
                sw.WriteLine("false");
            }
            else
            {
                sw.WriteLine("true");
            }
            //Ověřuje typ ovládání //Doporučené == true
            if (radioButton_ovladaniDoporucene.Checked)
            {
                sw.WriteLine("true");
            }
            else //Ověřuje rovnou vstupy 
            {
                //sw.WriteLine("false");
                string doleva=textBox_ovladaniDoleva.Text;
                string doprava=textBox_ovladaniDoprava.Text;
                string dolu=textBox_ovladaniDolu.Text;
                string otaceni=textBox_ovladaniOtaceni.Text;
                if(doleva.Length<1)
                {
                    MessageBox.Show("Chybně zadané ovládání doleva, bylo nastaveno Doporučené nastavení");
                    sw.WriteLine("true");
                }
                else if(doprava.Length<1)
                {
                    MessageBox.Show("Chybně zadané ovládání doprava, bylo nastaveno Doporučené nastavení");
                    sw.WriteLine("true");
                }
                else if(dolu.Length<1)
                {
                    MessageBox.Show("Chybně zadané ovládání dolů, bylo nastaveno Doporučené nastavení");
                    sw.WriteLine("true");
                }
                else if(otaceni.Length<1)
                {
                    MessageBox.Show("Chybně zadané ovládání otáčení, bylo nastaveno Doporučené nastavení");
                    sw.WriteLine("true");
                }
                else
                {
                    sw.WriteLine("false");
                    sw.WriteLine(doleva);
                    sw.WriteLine(doprava);
                    sw.WriteLine(dolu);
                    sw.WriteLine(otaceni);
                }
            }
            sw.Close();
            fs.Close();
            ZobrazitAktualniNastaveni();
        }



        //Nutno vyřešit co když je nastavení prázdné // nastavit defaultní hodnoty //Vyřešeno, nastavuje se v menu //Bude potřeba udělat ovládání

        private void ZobrazitAktualniNastaveni()
        {

            FileStream fs = new FileStream(@"../../nastaveni.txt", FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            string jmeno = sr.ReadLine();
            string delka = sr.ReadLine();
            bool mod = bool.Parse(sr.ReadLine());
            bool obtiznost = bool.Parse(sr.ReadLine());
            bool doporuceneOvladani = bool.Parse(sr.ReadLine());
            label_aktualniJmeno.Text = "Jméno: " + jmeno;
            textBox1.Text = jmeno;
            textBox2.Text = delka;

            //nacteni sipek
            pictureBox_nahoru.ImageLocation = @"../../sipkaNahoru.png";
            pictureBox_dolu.ImageLocation = @"../../sipkaDolu.png";
            pictureBox_doleva.ImageLocation = @"../../sipkaDoleva.png";
            pictureBox_doprava.ImageLocation = @"../../sipkaDoprava.png";


            if (obtiznost)
            {
                label_obtiznost.Text = "Obtížnost: Těžká";
                radioButton_tezka.Checked = true;
            }
            else
            {
                label_obtiznost.Text = "Obtížnost: Lehká";
                radioButton_lehka.Checked = true;
            }
            if(mod)
            {
                label_herniMod.Text = "Herní mód: Časový mód";
                label_delka.Text = "Délka hry: " + delka + " s";
                radioButton_casoveOmezenyMod.Checked = true;
                textBox2.Enabled = true;
            }
            else
            {
                label_herniMod.Text = "Herní mód: Nekonečný mód";
                label_delka.Text = "Délka hry: Neomezená";
                radioButton_nekonecnyMod.Checked = true;
                textBox2.Enabled = false;
            }
            if(doporuceneOvladani)
            {
                label_typOvladani.Text = "Typ ovládání: Doporučené";
                textBox_ovladaniDoleva.Enabled = false;
                textBox_ovladaniDolu.Enabled = false;
                textBox_ovladaniDoprava.Enabled = false;
                textBox_ovladaniOtaceni.Enabled = false;
                radioButton_ovladaniDoporucene.Checked = true;

                

                textBox_ovladaniDoleva.Text = "";
                textBox_ovladaniDoprava.Text = "";
                textBox_ovladaniDolu.Text = "";
                textBox_ovladaniOtaceni.Text = "";
            }
            else
            {
                label_typOvladani.Text = "Typ ovládání: Vlastní";
                textBox_ovladaniDoleva.Enabled = true;
                textBox_ovladaniDolu.Enabled = true;
                textBox_ovladaniDoprava.Enabled = true;
                textBox_ovladaniOtaceni.Enabled = true;
                radioButton_ovladaniVlastni.Checked = true;

                pictureBox_nahoru.Enabled = false;


                string doleva = sr.ReadLine();
                string doprava = sr.ReadLine();
                string dolu = sr.ReadLine();
                string otaceni = sr.ReadLine();
                textBox_ovladaniDoleva.Text = doleva;
                textBox_ovladaniDoprava.Text = doprava;
                textBox_ovladaniDolu.Text = dolu;
                textBox_ovladaniOtaceni.Text = otaceni;
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

        private void radioButton_casoveOmezenyMod_Click(object sender, EventArgs e)
        {
            textBox2.Enabled = true;
        }

        private void radioButton_nekonecnyMod_Click(object sender, EventArgs e)
        {
            textBox2.Enabled = false;
        }


        private void radioButton_ovladaniVlastni_Click(object sender, EventArgs e)
        {
            textBox_ovladaniDoleva.Enabled = true;
            textBox_ovladaniDolu.Enabled = true;
            textBox_ovladaniDoprava.Enabled = true;
            textBox_ovladaniOtaceni.Enabled = true;
        }

        private void radioButton_ovladaniDoporucene_Click(object sender, EventArgs e)
        {
            textBox_ovladaniDoleva.Enabled = false;
            textBox_ovladaniDolu.Enabled = false;
            textBox_ovladaniDoprava.Enabled = false;
            textBox_ovladaniOtaceni.Enabled = false;
        }
    }
}
