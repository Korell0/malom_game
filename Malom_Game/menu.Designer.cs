﻿
namespace Malom_Game
{
    partial class menu
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
            this.Start_Btn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.player1Name = new System.Windows.Forms.TextBox();
            this.player2Name = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // Start_Btn
            // 
            this.Start_Btn.Location = new System.Drawing.Point(304, 78);
            this.Start_Btn.Name = "Start_Btn";
            this.Start_Btn.Size = new System.Drawing.Size(75, 52);
            this.Start_Btn.TabIndex = 0;
            this.Start_Btn.Text = "START";
            this.Start_Btn.UseVisualStyleBackColor = true;
            this.Start_Btn.Click += new System.EventHandler(this.Start_Btn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(76, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Palyer 1 Name:";
            // 
            // player1Name
            // 
            this.player1Name.Location = new System.Drawing.Point(172, 59);
            this.player1Name.Name = "player1Name";
            this.player1Name.Size = new System.Drawing.Size(100, 20);
            this.player1Name.TabIndex = 3;
            this.player1Name.Text = "Player1";
            this.player1Name.TextChanged += new System.EventHandler(this.player1Name_TextChanged);
            // 
            // player2Name
            // 
            this.player2Name.Location = new System.Drawing.Point(172, 130);
            this.player2Name.Name = "player2Name";
            this.player2Name.Size = new System.Drawing.Size(100, 20);
            this.player2Name.TabIndex = 5;
            this.player2Name.Text = "Player2";
            this.player2Name.TextChanged += new System.EventHandler(this.player2Name_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(76, 133);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Palyer 2 Name:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox1.Image = global::Malom_Game.Properties.Resources.korong_a;
            this.pictureBox1.Location = new System.Drawing.Point(50, 59);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(20, 20);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox2.Image = global::Malom_Game.Properties.Resources.korong_b;
            this.pictureBox2.Location = new System.Drawing.Point(50, 130);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(20, 20);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 7;
            this.pictureBox2.TabStop = false;
            // 
            // menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 225);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.player2Name);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.player1Name);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Start_Btn);
            this.Name = "menu";
<<<<<<< HEAD
            this.Text = "Menu";
=======
            this.Text = "menu";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
>>>>>>> 24f39ee25f3dbd5c13c0538a20d0ca0ce1ed20fc
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Start_Btn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox player1Name;
        private System.Windows.Forms.TextBox player2Name;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}