using System.Windows.Forms;

namespace EVEOver
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
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.nameInput = new System.Windows.Forms.TextBox();
            this.systemsLoaded = new System.Windows.Forms.Label();
            this.lowestTruesec = new System.Windows.Forms.Label();
            this.progress = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(-7, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(280, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Initiate Database";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(3, 142);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 23);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "70";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(173, 142);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 23);
            this.textBox2.TabIndex = 2;
            this.textBox2.Text = "800";
            // 
            // nameInput
            // 
            this.nameInput.Location = new System.Drawing.Point(3, 113);
            this.nameInput.Name = "nameInput";
            this.nameInput.Size = new System.Drawing.Size(270, 23);
            this.nameInput.TabIndex = 3;
            // 
            // systemsLoaded
            // 
            this.systemsLoaded.AutoSize = true;
            this.systemsLoaded.Location = new System.Drawing.Point(5, 172);
            this.systemsLoaded.Name = "systemsLoaded";
            this.systemsLoaded.Size = new System.Drawing.Size(0, 15);
            this.systemsLoaded.TabIndex = 4;
            // 
            // lowestTruesec
            // 
            this.lowestTruesec.AutoSize = true;
            this.lowestTruesec.Location = new System.Drawing.Point(5, 26);
            this.lowestTruesec.Name = "lowestTruesec";
            this.lowestTruesec.Size = new System.Drawing.Size(150, 15);
            this.lowestTruesec.TabIndex = 5;
            this.lowestTruesec.Text = "Lowest Truesec in Region: ?";
            // 
            // progress
            // 
            this.progress.AutoSize = true;
            this.progress.Location = new System.Drawing.Point(5, 205);
            this.progress.Name = "progress";
            this.progress.Size = new System.Drawing.Size(0, 15);
            this.progress.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(275, 600);
            this.Controls.Add(this.progress);
            this.Controls.Add(this.lowestTruesec);
            this.Controls.Add(this.systemsLoaded);
            this.Controls.Add(this.nameInput);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "EVEAltert";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button button1;
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox nameInput;
        private Label systemsLoaded;
        private Label lowestTruesec;
        private Label progress;

        //THIS HAS TO BE A JOKE
    }
}