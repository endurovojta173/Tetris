namespace Tetris
{
    partial class Menu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Menu));
            this.button1_nekonecnyMod = new System.Windows.Forms.Button();
            this.button2_rezimNaCas = new System.Windows.Forms.Button();
            this.button3_nastaveni = new System.Windows.Forms.Button();
            this.button4_skore = new System.Windows.Forms.Button();
            this.button5_konec = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1_nekonecnyMod
            // 
            this.button1_nekonecnyMod.Location = new System.Drawing.Point(238, 264);
            this.button1_nekonecnyMod.Name = "button1_nekonecnyMod";
            this.button1_nekonecnyMod.Size = new System.Drawing.Size(152, 57);
            this.button1_nekonecnyMod.TabIndex = 0;
            this.button1_nekonecnyMod.Text = "Nová hra - Nekonečný mód";
            this.button1_nekonecnyMod.UseVisualStyleBackColor = true;
            this.button1_nekonecnyMod.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2_rezimNaCas
            // 
            this.button2_rezimNaCas.Location = new System.Drawing.Point(238, 327);
            this.button2_rezimNaCas.Name = "button2_rezimNaCas";
            this.button2_rezimNaCas.Size = new System.Drawing.Size(152, 57);
            this.button2_rezimNaCas.TabIndex = 1;
            this.button2_rezimNaCas.Text = "Nová hra - Režim s omezeným časem";
            this.button2_rezimNaCas.UseVisualStyleBackColor = true;
            this.button2_rezimNaCas.Click += new System.EventHandler(this.button2_rezimNaCas_Click);
            // 
            // button3_nastaveni
            // 
            this.button3_nastaveni.Location = new System.Drawing.Point(238, 390);
            this.button3_nastaveni.Name = "button3_nastaveni";
            this.button3_nastaveni.Size = new System.Drawing.Size(152, 57);
            this.button3_nastaveni.TabIndex = 2;
            this.button3_nastaveni.Text = "Nastavení";
            this.button3_nastaveni.UseVisualStyleBackColor = true;
            this.button3_nastaveni.Click += new System.EventHandler(this.button3_nastaveni_Click);
            // 
            // button4_skore
            // 
            this.button4_skore.Location = new System.Drawing.Point(238, 453);
            this.button4_skore.Name = "button4_skore";
            this.button4_skore.Size = new System.Drawing.Size(152, 57);
            this.button4_skore.TabIndex = 3;
            this.button4_skore.Text = "Skóre";
            this.button4_skore.UseVisualStyleBackColor = true;
            this.button4_skore.Click += new System.EventHandler(this.button4_skore_Click);
            // 
            // button5_konec
            // 
            this.button5_konec.Location = new System.Drawing.Point(238, 516);
            this.button5_konec.Name = "button5_konec";
            this.button5_konec.Size = new System.Drawing.Size(152, 57);
            this.button5_konec.TabIndex = 4;
            this.button5_konec.Text = "Konec";
            this.button5_konec.UseVisualStyleBackColor = true;
            this.button5_konec.Click += new System.EventHandler(this.button5_konec_Click);
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(657, 750);
            this.Controls.Add(this.button5_konec);
            this.Controls.Add(this.button4_skore);
            this.Controls.Add(this.button3_nastaveni);
            this.Controls.Add(this.button2_rezimNaCas);
            this.Controls.Add(this.button1_nekonecnyMod);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Menu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tetris";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1_nekonecnyMod;
        private System.Windows.Forms.Button button2_rezimNaCas;
        private System.Windows.Forms.Button button3_nastaveni;
        private System.Windows.Forms.Button button4_skore;
        private System.Windows.Forms.Button button5_konec;
    }
}