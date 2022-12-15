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
            this.button1_novaHra = new System.Windows.Forms.Button();
            this.button3_nastaveni = new System.Windows.Forms.Button();
            this.button4_skore = new System.Windows.Forms.Button();
            this.button5_konec = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1_novaHra
            // 
            this.button1_novaHra.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button1_novaHra.Font = new System.Drawing.Font("Arial Narrow", 26.25F, System.Drawing.FontStyle.Bold);
            this.button1_novaHra.Location = new System.Drawing.Point(238, 264);
            this.button1_novaHra.MaximumSize = new System.Drawing.Size(3000, 60);
            this.button1_novaHra.MinimumSize = new System.Drawing.Size(230, 60);
            this.button1_novaHra.Name = "button1_novaHra";
            this.button1_novaHra.Size = new System.Drawing.Size(230, 60);
            this.button1_novaHra.TabIndex = 0;
            this.button1_novaHra.Text = "Nová hra";
            this.button1_novaHra.UseVisualStyleBackColor = true;
            this.button1_novaHra.Click += new System.EventHandler(this.button1_Click);
            // 
            // button3_nastaveni
            // 
            this.button3_nastaveni.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button3_nastaveni.Font = new System.Drawing.Font("Arial Narrow", 26.25F, System.Drawing.FontStyle.Bold);
            this.button3_nastaveni.Location = new System.Drawing.Point(238, 327);
            this.button3_nastaveni.MaximumSize = new System.Drawing.Size(3000, 60);
            this.button3_nastaveni.MinimumSize = new System.Drawing.Size(230, 60);
            this.button3_nastaveni.Name = "button3_nastaveni";
            this.button3_nastaveni.Size = new System.Drawing.Size(230, 60);
            this.button3_nastaveni.TabIndex = 2;
            this.button3_nastaveni.Text = "Nastavení";
            this.button3_nastaveni.UseVisualStyleBackColor = true;
            this.button3_nastaveni.Click += new System.EventHandler(this.button3_nastaveni_Click);
            // 
            // button4_skore
            // 
            this.button4_skore.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button4_skore.Font = new System.Drawing.Font("Arial Narrow", 26.25F, System.Drawing.FontStyle.Bold);
            this.button4_skore.Location = new System.Drawing.Point(238, 390);
            this.button4_skore.MaximumSize = new System.Drawing.Size(3000, 60);
            this.button4_skore.MinimumSize = new System.Drawing.Size(230, 60);
            this.button4_skore.Name = "button4_skore";
            this.button4_skore.Size = new System.Drawing.Size(230, 60);
            this.button4_skore.TabIndex = 3;
            this.button4_skore.Text = "Skóre";
            this.button4_skore.UseVisualStyleBackColor = true;
            this.button4_skore.Click += new System.EventHandler(this.button4_skore_Click);
            // 
            // button5_konec
            // 
            this.button5_konec.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button5_konec.Font = new System.Drawing.Font("Arial Narrow", 26.25F, System.Drawing.FontStyle.Bold);
            this.button5_konec.Location = new System.Drawing.Point(238, 453);
            this.button5_konec.MaximumSize = new System.Drawing.Size(3000, 60);
            this.button5_konec.MinimumSize = new System.Drawing.Size(230, 60);
            this.button5_konec.Name = "button5_konec";
            this.button5_konec.Size = new System.Drawing.Size(230, 60);
            this.button5_konec.TabIndex = 4;
            this.button5_konec.Text = "Konec";
            this.button5_konec.UseVisualStyleBackColor = true;
            this.button5_konec.Click += new System.EventHandler(this.button5_konec_Click);
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(684, 861);
            this.Controls.Add(this.button5_konec);
            this.Controls.Add(this.button4_skore);
            this.Controls.Add(this.button3_nastaveni);
            this.Controls.Add(this.button1_novaHra);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(700, 900);
            this.Name = "Menu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tetris";
            this.Load += new System.EventHandler(this.Menu_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1_novaHra;
        private System.Windows.Forms.Button button3_nastaveni;
        private System.Windows.Forms.Button button4_skore;
        private System.Windows.Forms.Button button5_konec;
    }
}