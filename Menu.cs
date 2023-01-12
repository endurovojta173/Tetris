using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();           
        }        

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            try
            {
                TetrisMain main = new TetrisMain();
                main.ShowDialog();
                this.Dispose();
            }
            catch(FileNotFoundException)
            {
                MessageBox.Show("Přeinstalujte hru a nehrabte se v souborech :)");
            }
            
        }
        private void button3_nastaveni_Click(object sender, EventArgs e)
        {
            this.Hide();
            Nastaveni nastaveni = new Nastaveni();
            nastaveni.ShowDialog();
            this.Dispose();

        }

        private void button4_skore_Click(object sender, EventArgs e)
        {
            this.Hide();
            Skore skore = new Skore();
            skore.ShowDialog();
            this.Dispose();
        }

        private void button5_konec_Click(object sender, EventArgs e)
        {
            //Dořešit,samotná hra stále běží // Vyřešeno
            this.Dispose();
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            //Ošetření soundtracku a knihoven
            /*try
            {
                if (!File.Exists("Interop.WMPLib.dll")) throw new FileNotFoundException();
                else if (!File.Exists("AxInterop.WMPLib.dll")) throw new FileNotFoundException();
            }
            catch(FileNotFoundException)
            {
                MessageBox.Show("Hra se rozbila, prosím přeinstalujte ji a nezasahujte do souborů :)");
            }*/
            //Ošetření zvuku
            try
            {
                if (!File.Exists("soundtrack.wav")) throw new FileNotFoundException();
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Nebyl nalezen zvukový soubor, prosím přeinstalujte hru :), nebo si hrajte beze zvuku");
            }
            //Ošetření souboru nastaveni.txt
            if (!File.Exists(@"../../data/nastaveni.txt"))
            {
                FileStream fs = new FileStream(@"../../data/nastaveni.txt", FileMode.Create, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine("Guest");
                sw.WriteLine("100");
                sw.WriteLine("50");
                sw.WriteLine("false");
                sw.WriteLine("false");
                sw.WriteLine("true");
                sw.Close();
                fs.Close();
            }
            FileStream fs1 = new FileStream(@"../../data/nastaveni.txt", FileMode.Open, FileAccess.Read);
            if(fs1.Length<1)
            {
                fs1.Close();
                FileStream fs2 = new FileStream(@"../../data/nastaveni.txt", FileMode.Open, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs2);
                sw.WriteLine("Guest");
                sw.WriteLine("100");
                sw.WriteLine("50");
                sw.WriteLine("false");
                sw.WriteLine("false");
                sw.WriteLine("true");
                sw.Close();
                fs2.Close();
            }
            fs1.Close();

            //Ošetření souboru skore.txt
            if(!File.Exists(@"../../data/skore.txt"))
            {
                FileStream fs = new FileStream(@"../../data/skore.txt", FileMode.Create);
                fs.Close();
            }
        }
    }
}
