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
        }
        private void button1_Click(object sender, EventArgs e)
        {
            FileStream fs = new FileStream(@"../../nastaveni.txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(textBox1.Text+";");
            sw.WriteLine(textBox2.Text+";");
            if(radioButton1.Checked)
            {
                sw.WriteLine("false");
            }
            else
            {
                sw.WriteLine("true");
            }
            sw.Close();
            fs.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Menu menu = new Menu();
            menu.ShowDialog();
        }
    }
}
