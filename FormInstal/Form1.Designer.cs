namespace FormInstal
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
            label1 = new Label();
            label2 = new Label();
            Instal = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F);
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(112, 21);
            label1.TabIndex = 0;
            label1.Text = "Cesta instalace";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F);
            label2.Location = new Point(12, 39);
            label2.Name = "label2";
            label2.Size = new Size(302, 21);
            label2.TabIndex = 1;
            label2.Text = "Doplněná cesta..............................................................";
            // 
            // Instal
            // 
            Instal.Font = new Font("Segoe UI", 12F);
            Instal.Location = new Point(666, 80);
            Instal.Name = "Instal";
            Instal.Size = new Size(106, 42);
            Instal.TabIndex = 2;
            Instal.Text = "Instalovat";
            Instal.UseVisualStyleBackColor = true;
            Instal.Click += Instal_ClickAsync;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 134);
            Controls.Add(Instal);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Button Instal;
    }
}
