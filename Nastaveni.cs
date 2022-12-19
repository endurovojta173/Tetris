using AudioSwitcher.AudioApi.CoreAudio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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

            //nastavení zvuk na určitou hlasitost už v nastavení // později budu moct umazat, protože bude nastavovat při spuštění nové hry
            int hlasitost = int.Parse(numericUpDown_hlasitost.Value.ToString());

            sw.WriteLine(textBox1.Text); //Zapisuje jméno hráče
            sw.WriteLine(numericUpDown_delkaOmezeneho.Value); //Zapisuje délku hry
            sw.WriteLine(hlasitost); //Zapisuje hlasitost
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
                PrirazeniKlaves prirazeniKlaves= new PrirazeniKlaves();
                if(!prirazeniKlaves.jeVstupValidni(doleva) || !prirazeniKlaves.jeVstupValidni(doprava) || !prirazeniKlaves.jeVstupValidni(dolu) || !prirazeniKlaves.jeVstupValidni(otaceni))
                {
                    MessageBox.Show("Chybně zadané ovládání, bylo nastaveno Doporučené nastavení");
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
            string hlasitostHry=sr.ReadLine();
            bool mod = bool.Parse(sr.ReadLine());
            bool obtiznost = bool.Parse(sr.ReadLine());
            bool doporuceneOvladani = bool.Parse(sr.ReadLine());
            label_aktualniJmeno.Text = "Jméno: " + jmeno;
            textBox1.Text = jmeno;
            //numericUpDown_delkaOmezeneho.Value = decimal.Parse(delka);

            //Zobrazuje hlasitost
            label_hlasitostHry.Text = "Hlasitost hry: " + hlasitostHry;
            //numericUpDown_hlasitost.Value=decimal.Parse(hlasitostHry);


            //nacteni sipek
            pictureBox_nahoru.Image = Tetris.Properties.Resources.sipkaNahoru;
            pictureBox_doleva.Image = Tetris.Properties.Resources.sipkaDoleva;
            pictureBox_doprava.Image = Tetris.Properties.Resources.sipkaDoprava;
            pictureBox_dolu.Image = Tetris.Properties.Resources.sipkaDolu;

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
                numericUpDown_delkaOmezeneho.Enabled = true;
            }
            else
            {
                label_herniMod.Text = "Herní mód: Nekonečný mód";
                label_delka.Text = "Délka hry: Neomezená";
                radioButton_nekonecnyMod.Checked = true;
                numericUpDown_delkaOmezeneho.Enabled = false;
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
            numericUpDown_delkaOmezeneho.Enabled = true;
        }

        private void radioButton_nekonecnyMod_Click(object sender, EventArgs e)
        {
            numericUpDown_delkaOmezeneho.Enabled = false;
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
