using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        public bool rezimaNaCas = false;
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            TetrisMain main = new TetrisMain();
            rezimaNaCas = false;
            main.ShowDialog();
        }


        private void button2_rezimNaCas_Click(object sender, EventArgs e)
        {
            rezimaNaCas = true;
            this.Hide();
            TetrisMain main = new TetrisMain();
            rezimaNaCas = true;
            main.ShowDialog();
        }

        private void button3_nastaveni_Click(object sender, EventArgs e)
        {

        }

        private void button4_skore_Click(object sender, EventArgs e)
        {

        }

        private void button5_konec_Click(object sender, EventArgs e)
        {
            TetrisMain main = new TetrisMain();
            this.Close();
            main.Close();
        }

        public bool GetRezimNaCas()
        {
            return rezimaNaCas;
        }
    }
}
