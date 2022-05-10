namespace StartMenu
{
    partial class HelpScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HelpScreen));
            this.help = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // help
            // 
            this.help.BackColor = System.Drawing.SystemColors.Control;
            this.help.Font = new System.Drawing.Font("Palatino Linotype", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.help.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.help.Location = new System.Drawing.Point(12, 19);
            this.help.Name = "help";
            this.help.Size = new System.Drawing.Size(537, 563);
            this.help.TabIndex = 0;
            this.help.Text = resources.GetString("help.Text");
            // 
            // HelpScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(561, 591);
            this.Controls.Add(this.help);
            this.Name = "HelpScreen";
            this.Text = "HelpScreen";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label help;
    }
}