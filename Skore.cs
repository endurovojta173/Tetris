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
        //Načte skore při otevření
        private void Skore_Load(object sender, EventArgs e)
        {
            CreateTable();
            SaveSkore();
            LoadData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Menu menu = new Menu();
            menu.ShowDialog();
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FileStream fs = new FileStream(@"../../skore.txt", FileMode.Create, FileAccess.Write);
            fs.Close();
            LoadData();
        }

        private void SaveSkore()
        {
            FileStream fs = new FileStream(@"../../skoreHry.txt", FileMode.OpenOrCreate, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            string radek = "";
            char[] separators = { ';' };
            while (!sr.EndOfStream)
            {
                string polozka = sr.ReadLine();
                if (polozka != "#")
                {
                    if (polozka == "True")
                    {
                        radek += " Časový mód";
                    }
                    else if (polozka == "False")
                    {
                        radek += " Nekonečný mód";
                    }
                    else
                    {
                        radek += polozka;
                    }
                }
                else
                {
                    FileStream fs1 = new FileStream(@"../../skore.txt", FileMode.Open, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(fs1);
                    string[] splitRadek = radek.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    radek = splitRadek[0]+";" +splitRadek[1]+";"+splitRadek[2]+";"+splitRadek[3] + ";" + splitRadek[4]+";" + splitRadek[5];
                    sw.BaseStream.Seek(0, SeekOrigin.End);
                    sw.WriteLine(radek);
                    sw.Close();
                    fs1.Close();
                }
            }
            fs.Close();
            FileStream fs2 = new FileStream(@"../../skoreHry.txt", FileMode.Create, FileAccess.Write);
            fs2.Close();
        }

        private void LoadData()
        {
            FileStream fs = new FileStream(@"../../skore.txt", FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            char[] separator = { ';' };
            while (!sr.EndOfStream)
            {
                string radek = sr.ReadLine();
                string[] splitRadek = radek.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                AddRow(splitRadek[0], splitRadek[1], splitRadek[2], splitRadek[3], splitRadek[4], splitRadek[5]);
            }
            fs.Close();
        }

        private void AddRow(string jmeno,string skore,string delkaHry,string herniMod, string maxDelkaHry, string obtiznost)
        {
            if(int.Parse(delkaHry)>60)
            {
                int delkaPomoc = int.Parse(delkaHry);
                delkaHry=delkaPomoc/60+" m " + delkaPomoc%60+" s";
            }
            else
            {
                delkaHry += " s";
            }
            String[] radek = {jmeno,skore,delkaHry,herniMod,maxDelkaHry,obtiznost};
            dataGridView1.Rows.Add(radek);
        }

        private void CreateTable()
        {
            dataGridView1.ColumnCount = 6;
            dataGridView1.Columns[0].Name = "Jméno";
            dataGridView1.Columns[1].Name = "Skóre";
            dataGridView1.Columns[2].Name = "Délka hry";
            dataGridView1.Columns[3].Name = "Herní mód";
            dataGridView1.Columns[4].Name = "Maximální délka hry";
            dataGridView1.Columns[5].Name = "Obtížnost";

            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

        }
    }
}
