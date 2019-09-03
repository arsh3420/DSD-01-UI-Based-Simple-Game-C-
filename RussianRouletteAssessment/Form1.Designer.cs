﻿namespace RussianRouletteAssessment
{
    partial class frm_Menu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Menu));
            this.btn_NewGame = new System.Windows.Forms.Button();
            this.btn_ExitGame = new System.Windows.Forms.Button();
            this.pb_ProfilePic = new System.Windows.Forms.PictureBox();
            this.lbl_UserName = new System.Windows.Forms.Label();
            this.lbl_DeiExMachina = new System.Windows.Forms.Label();
            this.lbl_Deaths = new System.Windows.Forms.Label();
            this.lbl_HighScore = new System.Windows.Forms.Label();
            this.lbl_TimesPlayed = new System.Windows.Forms.Label();
            this.lbl_BulletsShot = new System.Windows.Forms.Label();
            this.lbl_CloseCalls = new System.Windows.Forms.Label();
            this.txt_UserName = new System.Windows.Forms.TextBox();
            this.txt_DeiExMachina = new System.Windows.Forms.TextBox();
            this.txt_CloseCalls = new System.Windows.Forms.TextBox();
            this.txt_BulletsShot = new System.Windows.Forms.TextBox();
            this.txt_Deaths = new System.Windows.Forms.TextBox();
            this.txt_TimesPlayed = new System.Windows.Forms.TextBox();
            this.txt_HighScore = new System.Windows.Forms.TextBox();
            this.pnl_CheatMenu = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_MoveCheats = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lb_AvailableCheats = new System.Windows.Forms.ListBox();
            this.lbl_AvailableCheats = new System.Windows.Forms.Label();
            this.lb_ActiveCheats = new System.Windows.Forms.ListBox();
            this.btn_HighScores = new System.Windows.Forms.Button();
            this.btn_NewPlayer = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pb_ProfilePic)).BeginInit();
            this.pnl_CheatMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_NewGame
            // 
            this.btn_NewGame.Location = new System.Drawing.Point(12, 406);
            this.btn_NewGame.Name = "btn_NewGame";
            this.btn_NewGame.Size = new System.Drawing.Size(75, 23);
            this.btn_NewGame.TabIndex = 7;
            this.btn_NewGame.Text = "New Game";
            this.btn_NewGame.UseVisualStyleBackColor = true;
            this.btn_NewGame.Click += new System.EventHandler(this.btn_NewGame_Click);
            // 
            // btn_ExitGame
            // 
            this.btn_ExitGame.Location = new System.Drawing.Point(534, 406);
            this.btn_ExitGame.Name = "btn_ExitGame";
            this.btn_ExitGame.Size = new System.Drawing.Size(75, 23);
            this.btn_ExitGame.TabIndex = 11;
            this.btn_ExitGame.Text = "Exit Game";
            this.btn_ExitGame.UseVisualStyleBackColor = true;
            this.btn_ExitGame.Click += new System.EventHandler(this.btn_ExitGame_Click);
            // 
            // pb_ProfilePic
            // 
            this.pb_ProfilePic.Location = new System.Drawing.Point(356, 12);
            this.pb_ProfilePic.Name = "pb_ProfilePic";
            this.pb_ProfilePic.Size = new System.Drawing.Size(256, 256);
            this.pb_ProfilePic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_ProfilePic.TabIndex = 6;
            this.pb_ProfilePic.TabStop = false;
            // 
            // lbl_UserName
            // 
            this.lbl_UserName.AutoSize = true;
            this.lbl_UserName.Location = new System.Drawing.Point(9, 12);
            this.lbl_UserName.Name = "lbl_UserName";
            this.lbl_UserName.Size = new System.Drawing.Size(57, 13);
            this.lbl_UserName.TabIndex = 7;
            this.lbl_UserName.Text = "UserName";
            // 
            // lbl_DeiExMachina
            // 
            this.lbl_DeiExMachina.AutoSize = true;
            this.lbl_DeiExMachina.Location = new System.Drawing.Point(9, 252);
            this.lbl_DeiExMachina.Name = "lbl_DeiExMachina";
            this.lbl_DeiExMachina.Size = new System.Drawing.Size(82, 13);
            this.lbl_DeiExMachina.TabIndex = 8;
            this.lbl_DeiExMachina.Text = "Dei Ex Machina";
            // 
            // lbl_Deaths
            // 
            this.lbl_Deaths.AutoSize = true;
            this.lbl_Deaths.Location = new System.Drawing.Point(9, 132);
            this.lbl_Deaths.Name = "lbl_Deaths";
            this.lbl_Deaths.Size = new System.Drawing.Size(41, 13);
            this.lbl_Deaths.TabIndex = 9;
            this.lbl_Deaths.Text = "Deaths";
            // 
            // lbl_HighScore
            // 
            this.lbl_HighScore.AutoSize = true;
            this.lbl_HighScore.Location = new System.Drawing.Point(9, 52);
            this.lbl_HighScore.Name = "lbl_HighScore";
            this.lbl_HighScore.Size = new System.Drawing.Size(60, 13);
            this.lbl_HighScore.TabIndex = 10;
            this.lbl_HighScore.Text = "High Score";
            // 
            // lbl_TimesPlayed
            // 
            this.lbl_TimesPlayed.AutoSize = true;
            this.lbl_TimesPlayed.Location = new System.Drawing.Point(9, 92);
            this.lbl_TimesPlayed.Name = "lbl_TimesPlayed";
            this.lbl_TimesPlayed.Size = new System.Drawing.Size(70, 13);
            this.lbl_TimesPlayed.TabIndex = 11;
            this.lbl_TimesPlayed.Text = "Times Played";
            // 
            // lbl_BulletsShot
            // 
            this.lbl_BulletsShot.AutoSize = true;
            this.lbl_BulletsShot.Location = new System.Drawing.Point(9, 172);
            this.lbl_BulletsShot.Name = "lbl_BulletsShot";
            this.lbl_BulletsShot.Size = new System.Drawing.Size(64, 13);
            this.lbl_BulletsShot.TabIndex = 12;
            this.lbl_BulletsShot.Text = "Bullets Fired";
            // 
            // lbl_CloseCalls
            // 
            this.lbl_CloseCalls.AutoSize = true;
            this.lbl_CloseCalls.Location = new System.Drawing.Point(9, 212);
            this.lbl_CloseCalls.Name = "lbl_CloseCalls";
            this.lbl_CloseCalls.Size = new System.Drawing.Size(58, 13);
            this.lbl_CloseCalls.TabIndex = 13;
            this.lbl_CloseCalls.Text = "Close Calls";
            // 
            // txt_UserName
            // 
            this.txt_UserName.Location = new System.Drawing.Point(114, 9);
            this.txt_UserName.Name = "txt_UserName";
            this.txt_UserName.ReadOnly = true;
            this.txt_UserName.Size = new System.Drawing.Size(236, 20);
            this.txt_UserName.TabIndex = 0;
            // 
            // txt_DeiExMachina
            // 
            this.txt_DeiExMachina.Location = new System.Drawing.Point(114, 249);
            this.txt_DeiExMachina.Name = "txt_DeiExMachina";
            this.txt_DeiExMachina.ReadOnly = true;
            this.txt_DeiExMachina.Size = new System.Drawing.Size(236, 20);
            this.txt_DeiExMachina.TabIndex = 6;
            // 
            // txt_CloseCalls
            // 
            this.txt_CloseCalls.Location = new System.Drawing.Point(114, 209);
            this.txt_CloseCalls.Name = "txt_CloseCalls";
            this.txt_CloseCalls.ReadOnly = true;
            this.txt_CloseCalls.Size = new System.Drawing.Size(236, 20);
            this.txt_CloseCalls.TabIndex = 5;
            // 
            // txt_BulletsShot
            // 
            this.txt_BulletsShot.Location = new System.Drawing.Point(114, 169);
            this.txt_BulletsShot.Name = "txt_BulletsShot";
            this.txt_BulletsShot.ReadOnly = true;
            this.txt_BulletsShot.Size = new System.Drawing.Size(236, 20);
            this.txt_BulletsShot.TabIndex = 4;
            // 
            // txt_Deaths
            // 
            this.txt_Deaths.Location = new System.Drawing.Point(114, 129);
            this.txt_Deaths.Name = "txt_Deaths";
            this.txt_Deaths.ReadOnly = true;
            this.txt_Deaths.Size = new System.Drawing.Size(236, 20);
            this.txt_Deaths.TabIndex = 3;
            // 
            // txt_TimesPlayed
            // 
            this.txt_TimesPlayed.Location = new System.Drawing.Point(114, 89);
            this.txt_TimesPlayed.Name = "txt_TimesPlayed";
            this.txt_TimesPlayed.ReadOnly = true;
            this.txt_TimesPlayed.Size = new System.Drawing.Size(236, 20);
            this.txt_TimesPlayed.TabIndex = 2;
            // 
            // txt_HighScore
            // 
            this.txt_HighScore.Location = new System.Drawing.Point(114, 49);
            this.txt_HighScore.Name = "txt_HighScore";
            this.txt_HighScore.ReadOnly = true;
            this.txt_HighScore.Size = new System.Drawing.Size(236, 20);
            this.txt_HighScore.TabIndex = 1;
            // 
            // pnl_CheatMenu
            // 
            this.pnl_CheatMenu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_CheatMenu.Controls.Add(this.label1);
            this.pnl_CheatMenu.Controls.Add(this.lbl_MoveCheats);
            this.pnl_CheatMenu.Controls.Add(this.label2);
            this.pnl_CheatMenu.Controls.Add(this.lb_AvailableCheats);
            this.pnl_CheatMenu.Controls.Add(this.lbl_AvailableCheats);
            this.pnl_CheatMenu.Controls.Add(this.lb_ActiveCheats);
            this.pnl_CheatMenu.Enabled = false;
            this.pnl_CheatMenu.Location = new System.Drawing.Point(12, 275);
            this.pnl_CheatMenu.Name = "pnl_CheatMenu";
            this.pnl_CheatMenu.Size = new System.Drawing.Size(600, 125);
            this.pnl_CheatMenu.TabIndex = 21;
            this.pnl_CheatMenu.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(222, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(246, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Cheats will only be in effect if this window is visible.";
            // 
            // lbl_MoveCheats
            // 
            this.lbl_MoveCheats.AutoSize = true;
            this.lbl_MoveCheats.Location = new System.Drawing.Point(298, 39);
            this.lbl_MoveCheats.Name = "lbl_MoveCheats";
            this.lbl_MoveCheats.Size = new System.Drawing.Size(94, 13);
            this.lbl_MoveCheats.TabIndex = 6;
            this.lbl_MoveCheats.Text = "<- Move Cheats ->";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(410, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Active Cheats";
            // 
            // lb_AvailableCheats
            // 
            this.lb_AvailableCheats.FormattingEnabled = true;
            this.lb_AvailableCheats.Items.AddRange(new object[] {
            "Edit All",
            "Edit Scores",
            "Extra Chance",
            "Extra Life",
            "God Mode"});
            this.lb_AvailableCheats.Location = new System.Drawing.Point(95, 3);
            this.lb_AvailableCheats.Name = "lb_AvailableCheats";
            this.lb_AvailableCheats.Size = new System.Drawing.Size(106, 95);
            this.lb_AvailableCheats.Sorted = true;
            this.lb_AvailableCheats.TabIndex = 10;
            this.lb_AvailableCheats.SelectedIndexChanged += new System.EventHandler(this.lb_AvailableCheats_SelectedIndexChanged);
            // 
            // lbl_AvailableCheats
            // 
            this.lbl_AvailableCheats.AutoSize = true;
            this.lbl_AvailableCheats.Location = new System.Drawing.Point(3, 7);
            this.lbl_AvailableCheats.Name = "lbl_AvailableCheats";
            this.lbl_AvailableCheats.Size = new System.Drawing.Size(86, 13);
            this.lbl_AvailableCheats.TabIndex = 2;
            this.lbl_AvailableCheats.Text = "Available Cheats";
            // 
            // lb_ActiveCheats
            // 
            this.lb_ActiveCheats.FormattingEnabled = true;
            this.lb_ActiveCheats.Location = new System.Drawing.Point(489, 3);
            this.lb_ActiveCheats.Name = "lb_ActiveCheats";
            this.lb_ActiveCheats.Size = new System.Drawing.Size(106, 95);
            this.lb_ActiveCheats.Sorted = true;
            this.lb_ActiveCheats.TabIndex = 11;
            this.lb_ActiveCheats.SelectedIndexChanged += new System.EventHandler(this.lb_ActiveCheats_SelectedIndexChanged);
            // 
            // btn_HighScores
            // 
            this.btn_HighScores.Location = new System.Drawing.Point(360, 406);
            this.btn_HighScores.Name = "btn_HighScores";
            this.btn_HighScores.Size = new System.Drawing.Size(75, 23);
            this.btn_HighScores.TabIndex = 9;
            this.btn_HighScores.Text = "High Scores";
            this.btn_HighScores.UseVisualStyleBackColor = true;
            this.btn_HighScores.Click += new System.EventHandler(this.btn_HighScores_Click);
            // 
            // btn_NewPlayer
            // 
            this.btn_NewPlayer.Location = new System.Drawing.Point(186, 406);
            this.btn_NewPlayer.Name = "btn_NewPlayer";
            this.btn_NewPlayer.Size = new System.Drawing.Size(75, 23);
            this.btn_NewPlayer.TabIndex = 8;
            this.btn_NewPlayer.Text = "New Player";
            this.btn_NewPlayer.UseVisualStyleBackColor = true;
            this.btn_NewPlayer.Click += new System.EventHandler(this.btn_NewPlayer_Click);
            // 
            // frm_Menu
            // 
            this.AcceptButton = this.btn_NewGame;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 441);
            this.Controls.Add(this.btn_NewPlayer);
            this.Controls.Add(this.btn_HighScores);
            this.Controls.Add(this.pnl_CheatMenu);
            this.Controls.Add(this.txt_HighScore);
            this.Controls.Add(this.txt_TimesPlayed);
            this.Controls.Add(this.txt_Deaths);
            this.Controls.Add(this.txt_BulletsShot);
            this.Controls.Add(this.txt_CloseCalls);
            this.Controls.Add(this.txt_DeiExMachina);
            this.Controls.Add(this.txt_UserName);
            this.Controls.Add(this.lbl_CloseCalls);
            this.Controls.Add(this.lbl_BulletsShot);
            this.Controls.Add(this.lbl_TimesPlayed);
            this.Controls.Add(this.lbl_HighScore);
            this.Controls.Add(this.lbl_Deaths);
            this.Controls.Add(this.lbl_DeiExMachina);
            this.Controls.Add(this.lbl_UserName);
            this.Controls.Add(this.pb_ProfilePic);
            this.Controls.Add(this.btn_ExitGame);
            this.Controls.Add(this.btn_NewGame);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frm_Menu";
            this.Text = "Menu";
            this.Load += new System.EventHandler(this.frm_Menu_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_Menu_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pb_ProfilePic)).EndInit();
            this.pnl_CheatMenu.ResumeLayout(false);
            this.pnl_CheatMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_NewGame;
        private System.Windows.Forms.Button btn_ExitGame;
        private System.Windows.Forms.PictureBox pb_ProfilePic;
        private System.Windows.Forms.Label lbl_UserName;
        private System.Windows.Forms.Label lbl_DeiExMachina;
        private System.Windows.Forms.Label lbl_Deaths;
        private System.Windows.Forms.Label lbl_HighScore;
        private System.Windows.Forms.Label lbl_TimesPlayed;
        private System.Windows.Forms.Label lbl_BulletsShot;
        private System.Windows.Forms.Label lbl_CloseCalls;
        private System.Windows.Forms.TextBox txt_UserName;
        private System.Windows.Forms.TextBox txt_DeiExMachina;
        private System.Windows.Forms.TextBox txt_CloseCalls;
        private System.Windows.Forms.TextBox txt_BulletsShot;
        private System.Windows.Forms.TextBox txt_Deaths;
        private System.Windows.Forms.TextBox txt_TimesPlayed;
        private System.Windows.Forms.TextBox txt_HighScore;
        private System.Windows.Forms.Panel pnl_CheatMenu;
        private System.Windows.Forms.ListBox lb_ActiveCheats;
        private System.Windows.Forms.Label lbl_AvailableCheats;
        private System.Windows.Forms.Label lbl_MoveCheats;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lb_AvailableCheats;
        private System.Windows.Forms.Button btn_HighScores;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_NewPlayer;
    }
}

