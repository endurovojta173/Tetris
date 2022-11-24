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
    public partial class Skore : Form
    {
        public Skore()
        {
            InitializeComponent();
            NacistSkore();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Menu menu = new Menu();
            menu.ShowDialog();
            this.Dispose();
        }

        private void NacistSkore()
        {
            FileStream fs = new FileStream(@"../../skore.txt", FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            string radek="";
            while(sr.BaseStream.Position<sr.BaseStream.Length)
            {
                string polozka = sr.ReadLine();
                if(polozka!=";")
                {
                    radek += " " + polozka;
                }
                else
                {
                    listBox1.Items.Add(radek);
                    radek = "";
                }
            }
            fs.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FileStream fs = new FileStream(@"../../skore.txt", FileMode.Create, FileAccess.Write);
        }
    }
}
