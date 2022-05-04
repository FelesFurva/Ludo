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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
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
    }
}