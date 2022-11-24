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
            this.Close();
        }

        
    }
}
