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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Menu menu = new Menu();
            FileStream fs = new FileStream("pomocny.txt", FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader("pomocny.txt");
            string s = sr.ReadLine();
            fs.Close();
            MessageBox.Show(s);
        }
    }
}
