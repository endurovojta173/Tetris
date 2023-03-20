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
            LoadData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Menu menu = new Menu();
            menu.ShowDialog();
            this.Dispose();
        }

        //Vymaže obsah tabulky a refreshne formulář
        private void button2_Click(object sender, EventArgs e)
        {
            FileStream fs = new FileStream(@"../../data/skore.txt", FileMode.Create, FileAccess.Write);
            fs.Close();
            LoadData();
            this.Hide();
            Skore skore = new Skore();
            skore.ShowDialog();
            this.Dispose();
        }

        private void LoadData()
        {
            FileStream fs = new FileStream(@"../../data/skore.txt", FileMode.Open, FileAccess.Read);
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
            String[] radek = {jmeno,skore,delkaHry,herniMod,maxDelkaHry,obtiznost};
            dataGridView1.Rows.Add(jmeno, int.Parse(skore), int.Parse(delkaHry), herniMod, maxDelkaHry,obtiznost); //Musí se nastavit jako určité typy, kvůli filtrování
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

        private void ShowData()
        {
            //Znovu zobrazí všechno
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Visible = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ShowData();
            string coChciVyhledat=textBox1.Text;
            int kategorie = comboBox1.SelectedIndex;
            
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                try
                {
                    if (dataGridView1.Rows[i].Cells[kategorie].Value.ToString() != coChciVyhledat)
                    {
                        dataGridView1.Rows[i].Visible = false;
                    }
                }
                catch(System.ArgumentOutOfRangeException)
                {
                    MessageBox.Show("Zadejte co chcete vyhledat");
                }
                
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ShowData();
        }
    }
}
