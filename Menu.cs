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
            //Zapíše false
            FileStream fs = new FileStream("pomocny.txt", FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine("false");
            sw.Close();
            fs.Close();

            this.Hide();
            TetrisMain main = new TetrisMain();
            main.ShowDialog();
        }


        private void button2_rezimNaCas_Click(object sender, EventArgs e)
        {
            //Zapíše do souboru true
            FileStream fs = new FileStream("pomocny.txt", FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine("true");
            sw.Close();
            fs.Close();

            this.Hide();
            TetrisMain main = new TetrisMain();
            main.ShowDialog();
        }
        private void button3_nastaveni_Click(object sender, EventArgs e)
        {
            this.Hide();
            Nastaveni nastaveni = new Nastaveni();
            nastaveni.ShowDialog();
        }

        private void button4_skore_Click(object sender, EventArgs e)
        {
            this.Hide();
            Skore skore = new Skore();
            skore.ShowDialog();
        }

        private void button5_konec_Click(object sender, EventArgs e)
        {
            //Dořešit,samotná hra stále běží
            TetrisMain main = new TetrisMain();
            Skore skore = new Skore();
            Nastaveni nastaveni = new Nastaveni();
            main.Close();
            skore.Close();
            nastaveni.Close();
            this.Close();
        }

        
    }
}
