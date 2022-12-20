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
            TetrisMain main = new TetrisMain();
            main.ShowDialog();
            this.Dispose();
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
            //Ošetření souboru nastaveni.txt
            if(!File.Exists(@"../../nastaveni.txt"))
            {
                FileStream fs = new FileStream(@"../../nastaveni.txt", FileMode.Create, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine("Guest");
                sw.WriteLine("100");
                sw.WriteLine("700 x 900");
                sw.WriteLine("false");
                sw.WriteLine("false");
                sw.WriteLine("true");
                sw.Close();
                fs.Close();
            }
            FileStream fs1 = new FileStream(@"../../nastaveni.txt", FileMode.Open, FileAccess.Read);
            if(fs1.Length<1)
            {
                fs1.Close();
                FileStream fs2 = new FileStream(@"../../nastaveni.txt", FileMode.Open, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs2);
                sw.WriteLine("Guest");
                sw.WriteLine("100");
                sw.WriteLine("700 x 900");
                sw.WriteLine("false");
                sw.WriteLine("false");
                sw.WriteLine("true");
                sw.Close();
                fs2.Close();
            }
            fs1.Close();

            //Ošetření souboru skore.txt
            if(!File.Exists(@"../../skore.txt"))
            {
                FileStream fs = new FileStream(@"../../skore.txt", FileMode.Create);
                fs.Close();
            }
        }
    }
}
