namespace Lexical_Analyzer2
{
    partial class Form1
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
            this.File_Path = new System.Windows.Forms.TextBox();
            this.Btn_Generate = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.showResult = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // File_Path
            // 
            this.File_Path.Location = new System.Drawing.Point(69, 28);
            this.File_Path.Name = "File_Path";
            this.File_Path.Size = new System.Drawing.Size(145, 20);
            this.File_Path.TabIndex = 0;
            this.File_Path.Text = "D:\\TestingFiles\\sd.txt";
            this.File_Path.TextChanged += new System.EventHandler(this.File_Path_TextChanged);
            // 
            // Btn_Generate
            // 
            this.Btn_Generate.Location = new System.Drawing.Point(267, 26);
            this.Btn_Generate.Name = "Btn_Generate";
            this.Btn_Generate.Size = new System.Drawing.Size(119, 23);
            this.Btn_Generate.TabIndex = 1;
            this.Btn_Generate.Text = "Generate Tokens";
            this.Btn_Generate.UseVisualStyleBackColor = true;
            this.Btn_Generate.Click += new System.EventHandler(this.Btn_Generate_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "File Path:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(267, 85);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(119, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Check Syntax";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // showResult
            // 
            this.showResult.AutoSize = true;
            this.showResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.showResult.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.showResult.Location = new System.Drawing.Point(64, 85);
            this.showResult.Name = "showResult";
            this.showResult.Size = new System.Drawing.Size(86, 25);
            this.showResult.TabIndex = 4;
            this.showResult.Text = "Result:";
            this.showResult.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(437, 136);
            this.Controls.Add(this.showResult);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Btn_Generate);
            this.Controls.Add(this.File_Path);
            this.Name = "Form1";
            this.Text = "Compiler";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox File_Path;
        private System.Windows.Forms.Button Btn_Generate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label showResult;
    }
}

