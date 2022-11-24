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
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Menu menu = new Menu();
            menu.ShowDialog();
        }

        private void NacistSkore()
        {
            FileStream fs = new FileStream(@"../../skore.txt", FileMode.OpenOrCreate, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            while(br.BaseStream.Position<br.BaseStream.Length)
            {
                string radek = br.ReadString();
                listBox1.Items.Add(radek);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FileStream fs = new FileStream(@"../../skore.txt", FileMode.Create, FileAccess.Write);
            //BinaryWriter bw = new BinaryWriter(fs);
        }
    }
}
