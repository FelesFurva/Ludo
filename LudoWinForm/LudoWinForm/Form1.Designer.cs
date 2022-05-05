namespace LudoWinForm
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbl_dice = new System.Windows.Forms.Label();
            this.btn_rollDice = new System.Windows.Forms.Button();
            this.lbl_sqr1 = new System.Windows.Forms.Label();
            this.lbl_sqr2 = new System.Windows.Forms.Label();
            this.lbl_sqr3 = new System.Windows.Forms.Label();
            this.lbl_sqr4 = new System.Windows.Forms.Label();
            this.lbl_sqr5 = new System.Windows.Forms.Label();
            this.lbl_sqr6 = new System.Windows.Forms.Label();
            this.lbl_sqr0 = new System.Windows.Forms.Label();
            this.pawn1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbl_dice
            // 
            this.lbl_dice.AccessibleName = "Dice";
            this.lbl_dice.Image = global::LudoWinForm.Properties.Resources.Dice_1;
            this.lbl_dice.Location = new System.Drawing.Point(120, 100);
            this.lbl_dice.Name = "lbl_dice";
            this.lbl_dice.Size = new System.Drawing.Size(50, 50);
            this.lbl_dice.TabIndex = 0;
            // 
            // btn_rollDice
            // 
            this.btn_rollDice.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btn_rollDice.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btn_rollDice.Font = new System.Drawing.Font("Cascadia Code", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btn_rollDice.Location = new System.Drawing.Point(96, 168);
            this.btn_rollDice.Name = "btn_rollDice";
            this.btn_rollDice.Size = new System.Drawing.Size(100, 50);
            this.btn_rollDice.TabIndex = 1;
            this.btn_rollDice.Text = "Roll Dice";
            this.btn_rollDice.UseVisualStyleBackColor = false;
            this.btn_rollDice.Click += new System.EventHandler(this.btn_rollDice_Click);
            // 
            // lbl_sqr1
            // 
            this.lbl_sqr1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.lbl_sqr1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lbl_sqr1.Image = global::LudoWinForm.Properties.Resources.field;
            this.lbl_sqr1.Location = new System.Drawing.Point(393, 387);
            this.lbl_sqr1.Name = "lbl_sqr1";
            this.lbl_sqr1.Size = new System.Drawing.Size(50, 50);
            this.lbl_sqr1.TabIndex = 2;
            // 
            // lbl_sqr2
            // 
            this.lbl_sqr2.BackColor = System.Drawing.SystemColors.ControlDark;
            this.lbl_sqr2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lbl_sqr2.Image = global::LudoWinForm.Properties.Resources.field;
            this.lbl_sqr2.Location = new System.Drawing.Point(337, 387);
            this.lbl_sqr2.Name = "lbl_sqr2";
            this.lbl_sqr2.Size = new System.Drawing.Size(50, 50);
            this.lbl_sqr2.TabIndex = 3;
            // 
            // lbl_sqr3
            // 
            this.lbl_sqr3.BackColor = System.Drawing.SystemColors.ControlDark;
            this.lbl_sqr3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lbl_sqr3.Image = global::LudoWinForm.Properties.Resources.field;
            this.lbl_sqr3.Location = new System.Drawing.Point(281, 387);
            this.lbl_sqr3.Name = "lbl_sqr3";
            this.lbl_sqr3.Size = new System.Drawing.Size(50, 50);
            this.lbl_sqr3.TabIndex = 4;
            // 
            // lbl_sqr4
            // 
            this.lbl_sqr4.BackColor = System.Drawing.SystemColors.ControlDark;
            this.lbl_sqr4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lbl_sqr4.Image = global::LudoWinForm.Properties.Resources.field;
            this.lbl_sqr4.Location = new System.Drawing.Point(225, 387);
            this.lbl_sqr4.Name = "lbl_sqr4";
            this.lbl_sqr4.Size = new System.Drawing.Size(50, 50);
            this.lbl_sqr4.TabIndex = 5;
            // 
            // lbl_sqr5
            // 
            this.lbl_sqr5.BackColor = System.Drawing.SystemColors.ControlDark;
            this.lbl_sqr5.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lbl_sqr5.Image = global::LudoWinForm.Properties.Resources.field;
            this.lbl_sqr5.Location = new System.Drawing.Point(169, 387);
            this.lbl_sqr5.Name = "lbl_sqr5";
            this.lbl_sqr5.Size = new System.Drawing.Size(50, 50);
            this.lbl_sqr5.TabIndex = 6;
            // 
            // lbl_sqr6
            // 
            this.lbl_sqr6.BackColor = System.Drawing.SystemColors.ControlDark;
            this.lbl_sqr6.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lbl_sqr6.Image = global::LudoWinForm.Properties.Resources.field;
            this.lbl_sqr6.Location = new System.Drawing.Point(113, 387);
            this.lbl_sqr6.Name = "lbl_sqr6";
            this.lbl_sqr6.Size = new System.Drawing.Size(50, 50);
            this.lbl_sqr6.TabIndex = 7;
            // 
            // lbl_sqr0
            // 
            this.lbl_sqr0.BackColor = System.Drawing.SystemColors.ControlDark;
            this.lbl_sqr0.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lbl_sqr0.Image = global::LudoWinForm.Properties.Resources.field;
            this.lbl_sqr0.Location = new System.Drawing.Point(57, 387);
            this.lbl_sqr0.Name = "lbl_sqr0";
            this.lbl_sqr0.Size = new System.Drawing.Size(50, 50);
            this.lbl_sqr0.TabIndex = 8;
            this.lbl_sqr0.Click += new System.EventHandler(this.lbl_sqr0_Click);
            // 
            // pawn1
            // 
            this.pawn1.Image = global::LudoWinForm.Properties.Resources.Pawn;
            this.pawn1.Location = new System.Drawing.Point(57, 358);
            this.pawn1.Name = "pawn1";
            this.pawn1.Size = new System.Drawing.Size(27, 29);
            this.pawn1.TabIndex = 9;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 561);
            this.Controls.Add(this.pawn1);
            this.Controls.Add(this.lbl_sqr0);
            this.Controls.Add(this.lbl_sqr6);
            this.Controls.Add(this.lbl_sqr5);
            this.Controls.Add(this.lbl_sqr4);
            this.Controls.Add(this.lbl_sqr3);
            this.Controls.Add(this.lbl_sqr2);
            this.Controls.Add(this.lbl_sqr1);
            this.Controls.Add(this.btn_rollDice);
            this.Controls.Add(this.lbl_dice);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(800, 600);
            this.Name = "Form1";
            this.Text = "Dice";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Label lbl_dice;
        private Button btn_rollDice;
        private Label lbl_sqr1;
        private Label lbl_sqr2;
        private Label lbl_sqr3;
        private Label lbl_sqr4;
        private Label lbl_sqr5;
        private Label lbl_sqr6;
        private Label lbl_sqr0;
        private Label pawn1;
    }
}