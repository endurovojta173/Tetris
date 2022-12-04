namespace Tetris
{
    partial class Nastaveni
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Nastaveni));
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label_delkaModuSOmezenymCasem = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.radioButton_nekonecnyMod = new System.Windows.Forms.RadioButton();
            this.radioButton_casoveOmezenyMod = new System.Windows.Forms.RadioButton();
            this.label_aktualniJmeno = new System.Windows.Forms.Label();
            this.label_delka = new System.Windows.Forms.Label();
            this.label_herniMod = new System.Windows.Forms.Label();
            this.label_obtiznost = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.radioButton_lehka = new System.Windows.Forms.RadioButton();
            this.radioButton_tezka = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_ovladaniDoleva = new System.Windows.Forms.TextBox();
            this.textBox_ovladaniDoprava = new System.Windows.Forms.TextBox();
            this.textBox_ovladaniOtaceni = new System.Windows.Forms.TextBox();
            this.textBox_ovladaniDolu = new System.Windows.Forms.TextBox();
            this.label_nadpisOvladani = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.radioButton_ovladaniDoporucene = new System.Windows.Forms.RadioButton();
            this.radioButton_ovladaniVlastni = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label_typOvladani = new System.Windows.Forms.Label();
            this.pictureBox_nahoru = new System.Windows.Forms.PictureBox();
            this.pictureBox_dolu = new System.Windows.Forms.PictureBox();
            this.pictureBox_doleva = new System.Windows.Forms.PictureBox();
            this.pictureBox_doprava = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_nahoru)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_dolu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_doleva)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_doprava)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(77, 590);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Uložit změny";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(304, 161);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(171, 20);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "Guest";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(74, 168);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Jméno";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(77, 22);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 22);
            this.button2.TabIndex = 3;
            this.button2.Text = "Zpět";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBox2
            // 
            this.textBox2.Enabled = false;
            this.textBox2.Location = new System.Drawing.Point(304, 249);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(171, 20);
            this.textBox2.TabIndex = 4;
            this.textBox2.Text = "200";
            // 
            // label_delkaModuSOmezenymCasem
            // 
            this.label_delkaModuSOmezenymCasem.AutoSize = true;
            this.label_delkaModuSOmezenymCasem.Location = new System.Drawing.Point(74, 252);
            this.label_delkaModuSOmezenymCasem.Name = "label_delkaModuSOmezenymCasem";
            this.label_delkaModuSOmezenymCasem.Size = new System.Drawing.Size(224, 13);
            this.label_delkaModuSOmezenymCasem.TabIndex = 5;
            this.label_delkaModuSOmezenymCasem.Text = "Délka módu s omezeným časem v sekundách";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(74, 223);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Herní mód";
            // 
            // radioButton_nekonecnyMod
            // 
            this.radioButton_nekonecnyMod.AutoSize = true;
            this.radioButton_nekonecnyMod.Checked = true;
            this.radioButton_nekonecnyMod.Location = new System.Drawing.Point(3, 3);
            this.radioButton_nekonecnyMod.Name = "radioButton_nekonecnyMod";
            this.radioButton_nekonecnyMod.Size = new System.Drawing.Size(103, 17);
            this.radioButton_nekonecnyMod.TabIndex = 7;
            this.radioButton_nekonecnyMod.TabStop = true;
            this.radioButton_nekonecnyMod.Text = "Nekonečný mód";
            this.radioButton_nekonecnyMod.UseVisualStyleBackColor = true;
            this.radioButton_nekonecnyMod.Click += new System.EventHandler(this.radioButton_nekonecnyMod_Click);
            // 
            // radioButton_casoveOmezenyMod
            // 
            this.radioButton_casoveOmezenyMod.AutoSize = true;
            this.radioButton_casoveOmezenyMod.Location = new System.Drawing.Point(112, 3);
            this.radioButton_casoveOmezenyMod.Name = "radioButton_casoveOmezenyMod";
            this.radioButton_casoveOmezenyMod.Size = new System.Drawing.Size(129, 17);
            this.radioButton_casoveOmezenyMod.TabIndex = 8;
            this.radioButton_casoveOmezenyMod.Text = "Časově omezený mód";
            this.radioButton_casoveOmezenyMod.UseVisualStyleBackColor = true;
            this.radioButton_casoveOmezenyMod.Click += new System.EventHandler(this.radioButton_casoveOmezenyMod_Click);
            // 
            // label_aktualniJmeno
            // 
            this.label_aktualniJmeno.AutoSize = true;
            this.label_aktualniJmeno.Location = new System.Drawing.Point(74, 325);
            this.label_aktualniJmeno.Name = "label_aktualniJmeno";
            this.label_aktualniJmeno.Size = new System.Drawing.Size(41, 13);
            this.label_aktualniJmeno.TabIndex = 9;
            this.label_aktualniJmeno.Text = "Jméno:";
            // 
            // label_delka
            // 
            this.label_delka.AutoSize = true;
            this.label_delka.Location = new System.Drawing.Point(74, 364);
            this.label_delka.Name = "label_delka";
            this.label_delka.Size = new System.Drawing.Size(55, 13);
            this.label_delka.TabIndex = 10;
            this.label_delka.Text = "Délka hry:";
            // 
            // label_herniMod
            // 
            this.label_herniMod.AutoSize = true;
            this.label_herniMod.Location = new System.Drawing.Point(74, 351);
            this.label_herniMod.Name = "label_herniMod";
            this.label_herniMod.Size = new System.Drawing.Size(60, 13);
            this.label_herniMod.TabIndex = 11;
            this.label_herniMod.Text = "Herní mód:";
            // 
            // label_obtiznost
            // 
            this.label_obtiznost.AutoSize = true;
            this.label_obtiznost.Location = new System.Drawing.Point(74, 338);
            this.label_obtiznost.Name = "label_obtiznost";
            this.label_obtiznost.Size = new System.Drawing.Size(56, 13);
            this.label_obtiznost.TabIndex = 12;
            this.label_obtiznost.Text = "Obtížnost:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radioButton_nekonecnyMod);
            this.panel1.Controls.Add(this.radioButton_casoveOmezenyMod);
            this.panel1.Location = new System.Drawing.Point(304, 218);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(257, 25);
            this.panel1.TabIndex = 13;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.radioButton_lehka);
            this.panel2.Controls.Add(this.radioButton_tezka);
            this.panel2.Location = new System.Drawing.Point(304, 187);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(257, 25);
            this.panel2.TabIndex = 14;
            // 
            // radioButton_lehka
            // 
            this.radioButton_lehka.AutoSize = true;
            this.radioButton_lehka.Checked = true;
            this.radioButton_lehka.Location = new System.Drawing.Point(3, 3);
            this.radioButton_lehka.Name = "radioButton_lehka";
            this.radioButton_lehka.Size = new System.Drawing.Size(55, 17);
            this.radioButton_lehka.TabIndex = 7;
            this.radioButton_lehka.TabStop = true;
            this.radioButton_lehka.Text = "Lehká";
            this.radioButton_lehka.UseVisualStyleBackColor = true;
            // 
            // radioButton_tezka
            // 
            this.radioButton_tezka.AutoSize = true;
            this.radioButton_tezka.Location = new System.Drawing.Point(112, 3);
            this.radioButton_tezka.Name = "radioButton_tezka";
            this.radioButton_tezka.Size = new System.Drawing.Size(55, 17);
            this.radioButton_tezka.TabIndex = 8;
            this.radioButton_tezka.Text = "Těžká";
            this.radioButton_tezka.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(74, 192);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Obtížnost";
            // 
            // textBox_ovladaniDoleva
            // 
            this.textBox_ovladaniDoleva.Location = new System.Drawing.Point(416, 351);
            this.textBox_ovladaniDoleva.MaxLength = 1;
            this.textBox_ovladaniDoleva.Name = "textBox_ovladaniDoleva";
            this.textBox_ovladaniDoleva.Size = new System.Drawing.Size(20, 20);
            this.textBox_ovladaniDoleva.TabIndex = 16;
            // 
            // textBox_ovladaniDoprava
            // 
            this.textBox_ovladaniDoprava.Location = new System.Drawing.Point(468, 351);
            this.textBox_ovladaniDoprava.MaxLength = 1;
            this.textBox_ovladaniDoprava.Name = "textBox_ovladaniDoprava";
            this.textBox_ovladaniDoprava.Size = new System.Drawing.Size(20, 20);
            this.textBox_ovladaniDoprava.TabIndex = 17;
            // 
            // textBox_ovladaniOtaceni
            // 
            this.textBox_ovladaniOtaceni.Location = new System.Drawing.Point(442, 325);
            this.textBox_ovladaniOtaceni.MaxLength = 1;
            this.textBox_ovladaniOtaceni.Name = "textBox_ovladaniOtaceni";
            this.textBox_ovladaniOtaceni.Size = new System.Drawing.Size(20, 20);
            this.textBox_ovladaniOtaceni.TabIndex = 18;
            // 
            // textBox_ovladaniDolu
            // 
            this.textBox_ovladaniDolu.Location = new System.Drawing.Point(442, 351);
            this.textBox_ovladaniDolu.MaxLength = 1;
            this.textBox_ovladaniDolu.Name = "textBox_ovladaniDolu";
            this.textBox_ovladaniDolu.Size = new System.Drawing.Size(20, 20);
            this.textBox_ovladaniDolu.TabIndex = 19;
            // 
            // label_nadpisOvladani
            // 
            this.label_nadpisOvladani.AutoSize = true;
            this.label_nadpisOvladani.Location = new System.Drawing.Point(375, 303);
            this.label_nadpisOvladani.Name = "label_nadpisOvladani";
            this.label_nadpisOvladani.Size = new System.Drawing.Size(51, 13);
            this.label_nadpisOvladani.TabIndex = 20;
            this.label_nadpisOvladani.Text = "Ovládání";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.radioButton_ovladaniDoporucene);
            this.panel3.Controls.Add(this.radioButton_ovladaniVlastni);
            this.panel3.Location = new System.Drawing.Point(304, 275);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(257, 25);
            this.panel3.TabIndex = 15;
            // 
            // radioButton_ovladaniDoporucene
            // 
            this.radioButton_ovladaniDoporucene.AutoSize = true;
            this.radioButton_ovladaniDoporucene.Checked = true;
            this.radioButton_ovladaniDoporucene.Location = new System.Drawing.Point(3, 3);
            this.radioButton_ovladaniDoporucene.Name = "radioButton_ovladaniDoporucene";
            this.radioButton_ovladaniDoporucene.Size = new System.Drawing.Size(84, 17);
            this.radioButton_ovladaniDoporucene.TabIndex = 7;
            this.radioButton_ovladaniDoporucene.TabStop = true;
            this.radioButton_ovladaniDoporucene.Text = "Doporučené";
            this.radioButton_ovladaniDoporucene.UseVisualStyleBackColor = true;
            this.radioButton_ovladaniDoporucene.Click += new System.EventHandler(this.radioButton_ovladaniDoporucene_Click);
            // 
            // radioButton_ovladaniVlastni
            // 
            this.radioButton_ovladaniVlastni.AutoSize = true;
            this.radioButton_ovladaniVlastni.Location = new System.Drawing.Point(112, 3);
            this.radioButton_ovladaniVlastni.Name = "radioButton_ovladaniVlastni";
            this.radioButton_ovladaniVlastni.Size = new System.Drawing.Size(58, 17);
            this.radioButton_ovladaniVlastni.TabIndex = 8;
            this.radioButton_ovladaniVlastni.Text = "Vlastní";
            this.radioButton_ovladaniVlastni.UseVisualStyleBackColor = true;
            this.radioButton_ovladaniVlastni.Click += new System.EventHandler(this.radioButton_ovladaniVlastni_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(74, 280);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 21;
            this.label2.Text = "Typ ovládání";
            // 
            // label_typOvladani
            // 
            this.label_typOvladani.AutoSize = true;
            this.label_typOvladani.Location = new System.Drawing.Point(74, 377);
            this.label_typOvladani.Name = "label_typOvladani";
            this.label_typOvladani.Size = new System.Drawing.Size(73, 13);
            this.label_typOvladani.TabIndex = 22;
            this.label_typOvladani.Text = "Typ ovládání:";
            // 
            // pictureBox_nahoru
            // 
            this.pictureBox_nahoru.Location = new System.Drawing.Point(330, 325);
            this.pictureBox_nahoru.Name = "pictureBox_nahoru";
            this.pictureBox_nahoru.Size = new System.Drawing.Size(20, 20);
            this.pictureBox_nahoru.TabIndex = 23;
            this.pictureBox_nahoru.TabStop = false;
            // 
            // pictureBox_dolu
            // 
            this.pictureBox_dolu.Location = new System.Drawing.Point(330, 351);
            this.pictureBox_dolu.Name = "pictureBox_dolu";
            this.pictureBox_dolu.Size = new System.Drawing.Size(20, 20);
            this.pictureBox_dolu.TabIndex = 24;
            this.pictureBox_dolu.TabStop = false;
            // 
            // pictureBox_doleva
            // 
            this.pictureBox_doleva.Location = new System.Drawing.Point(304, 351);
            this.pictureBox_doleva.Name = "pictureBox_doleva";
            this.pictureBox_doleva.Size = new System.Drawing.Size(20, 20);
            this.pictureBox_doleva.TabIndex = 25;
            this.pictureBox_doleva.TabStop = false;
            // 
            // pictureBox_doprava
            // 
            this.pictureBox_doprava.Location = new System.Drawing.Point(356, 351);
            this.pictureBox_doprava.Name = "pictureBox_doprava";
            this.pictureBox_doprava.Size = new System.Drawing.Size(20, 20);
            this.pictureBox_doprava.TabIndex = 26;
            this.pictureBox_doprava.TabStop = false;
            // 
            // Nastaveni
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(657, 750);
            this.Controls.Add(this.pictureBox_doprava);
            this.Controls.Add(this.pictureBox_doleva);
            this.Controls.Add(this.pictureBox_dolu);
            this.Controls.Add(this.pictureBox_nahoru);
            this.Controls.Add(this.label_typOvladani);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.label_nadpisOvladani);
            this.Controls.Add(this.textBox_ovladaniDolu);
            this.Controls.Add(this.textBox_ovladaniOtaceni);
            this.Controls.Add(this.textBox_ovladaniDoprava);
            this.Controls.Add(this.textBox_ovladaniDoleva);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label_obtiznost);
            this.Controls.Add(this.label_herniMod);
            this.Controls.Add(this.label_delka);
            this.Controls.Add(this.label_aktualniJmeno);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label_delkaModuSOmezenymCasem);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(673, 789);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(673, 789);
            this.Name = "Nastaveni";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tetris";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_nahoru)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_dolu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_doleva)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_doprava)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label_delkaModuSOmezenymCasem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton radioButton_nekonecnyMod;
        private System.Windows.Forms.RadioButton radioButton_casoveOmezenyMod;
        private System.Windows.Forms.Label label_aktualniJmeno;
        private System.Windows.Forms.Label label_delka;
        private System.Windows.Forms.Label label_herniMod;
        private System.Windows.Forms.Label label_obtiznost;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton radioButton_lehka;
        private System.Windows.Forms.RadioButton radioButton_tezka;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_ovladaniDoleva;
        private System.Windows.Forms.TextBox textBox_ovladaniDoprava;
        private System.Windows.Forms.TextBox textBox_ovladaniOtaceni;
        private System.Windows.Forms.TextBox textBox_ovladaniDolu;
        private System.Windows.Forms.Label label_nadpisOvladani;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton radioButton_ovladaniDoporucene;
        private System.Windows.Forms.RadioButton radioButton_ovladaniVlastni;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label_typOvladani;
        private System.Windows.Forms.PictureBox pictureBox_nahoru;
        private System.Windows.Forms.PictureBox pictureBox_dolu;
        private System.Windows.Forms.PictureBox pictureBox_doleva;
        private System.Windows.Forms.PictureBox pictureBox_doprava;
    }
}