namespace Tetris
{
    partial class Skore
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Skore));
            this.button1 = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button2 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Jméno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaximalniDelkaHry = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Skóre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DelkaHry = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HerniMod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(34, 22);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 57);
            this.button1.TabIndex = 0;
            this.button1.Text = "Zpět";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(34, 214);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(532, 173);
            this.listBox1.TabIndex = 1;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(466, 173);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(100, 35);
            this.button2.TabIndex = 2;
            this.button2.Text = "Smazat skóre";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Jméno,
            this.MaximalniDelkaHry,
            this.Skóre,
            this.DelkaHry,
            this.HerniMod});
            this.dataGridView1.Location = new System.Drawing.Point(34, 441);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(542, 150);
            this.dataGridView1.TabIndex = 3;
            // 
            // Jméno
            // 
            this.Jméno.HeaderText = "Jméno";
            this.Jméno.Name = "Jméno";
            // 
            // MaximalniDelkaHry
            // 
            this.MaximalniDelkaHry.HeaderText = "Maximální délka hry";
            this.MaximalniDelkaHry.Name = "MaximalniDelkaHry";
            // 
            // Skóre
            // 
            this.Skóre.HeaderText = "Skóre";
            this.Skóre.Name = "Skóre";
            // 
            // DelkaHry
            // 
            this.DelkaHry.HeaderText = "Délka Hry";
            this.DelkaHry.Name = "DelkaHry";
            // 
            // HerniMod
            // 
            this.HerniMod.HeaderText = "Herní Mód";
            this.HerniMod.Name = "HerniMod";
            // 
            // Skore
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(657, 750);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Skore";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tetris";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Jméno;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaximalniDelkaHry;
        private System.Windows.Forms.DataGridViewTextBoxColumn Skóre;
        private System.Windows.Forms.DataGridViewTextBoxColumn DelkaHry;
        private System.Windows.Forms.DataGridViewTextBoxColumn HerniMod;
    }
}