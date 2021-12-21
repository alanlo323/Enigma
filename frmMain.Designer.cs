
namespace Enigma
{
    partial class frmMain
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
            this.textBoxInput = new System.Windows.Forms.TextBox();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.textBoxOutput = new System.Windows.Forms.TextBox();
            this.labelRing1 = new System.Windows.Forms.Label();
            this.labelRing2 = new System.Windows.Forms.Label();
            this.labelRing3 = new System.Windows.Forms.Label();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.numericUpDownRing1 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownRing2 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownRing3 = new System.Windows.Forms.NumericUpDown();
            this.buttonReset = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRing1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRing2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRing3)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxInput
            // 
            this.textBoxInput.Location = new System.Drawing.Point(12, 12);
            this.textBoxInput.Multiline = true;
            this.textBoxInput.Name = "textBoxInput";
            this.textBoxInput.Size = new System.Drawing.Size(776, 135);
            this.textBoxInput.TabIndex = 0;
            this.textBoxInput.TextChanged += new System.EventHandler(this.textBoxInput_TextChanged);
            // 
            // textBoxOutput
            // 
            this.textBoxOutput.Location = new System.Drawing.Point(12, 158);
            this.textBoxOutput.Multiline = true;
            this.textBoxOutput.Name = "textBoxOutput";
            this.textBoxOutput.ReadOnly = true;
            this.textBoxOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxOutput.Size = new System.Drawing.Size(776, 135);
            this.textBoxOutput.TabIndex = 1;
            // 
            // labelRing1
            // 
            this.labelRing1.AutoSize = true;
            this.labelRing1.Location = new System.Drawing.Point(174, 302);
            this.labelRing1.Name = "labelRing1";
            this.labelRing1.Size = new System.Drawing.Size(35, 13);
            this.labelRing1.TabIndex = 3;
            this.labelRing1.Text = "Ring1";
            // 
            // labelRing2
            // 
            this.labelRing2.AutoSize = true;
            this.labelRing2.Location = new System.Drawing.Point(93, 302);
            this.labelRing2.Name = "labelRing2";
            this.labelRing2.Size = new System.Drawing.Size(35, 13);
            this.labelRing2.TabIndex = 5;
            this.labelRing2.Text = "Ring2";
            // 
            // labelRing3
            // 
            this.labelRing3.AutoSize = true;
            this.labelRing3.Location = new System.Drawing.Point(12, 302);
            this.labelRing3.Name = "labelRing3";
            this.labelRing3.Size = new System.Drawing.Size(35, 13);
            this.labelRing3.TabIndex = 7;
            this.labelRing3.Text = "Ring3";
            // 
            // buttonLoad
            // 
            this.buttonLoad.Location = new System.Drawing.Point(713, 297);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(75, 23);
            this.buttonLoad.TabIndex = 8;
            this.buttonLoad.Text = "Load";
            this.buttonLoad.UseVisualStyleBackColor = true;
            this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(632, 297);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 9;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // numericUpDownRing1
            // 
            this.numericUpDownRing1.Location = new System.Drawing.Point(215, 300);
            this.numericUpDownRing1.Name = "numericUpDownRing1";
            this.numericUpDownRing1.Size = new System.Drawing.Size(34, 20);
            this.numericUpDownRing1.TabIndex = 10;
            this.numericUpDownRing1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDownRing1.ValueChanged += new System.EventHandler(this.numericUpDownRing1_ValueChanged);
            // 
            // numericUpDownRing2
            // 
            this.numericUpDownRing2.Location = new System.Drawing.Point(134, 299);
            this.numericUpDownRing2.Name = "numericUpDownRing2";
            this.numericUpDownRing2.Size = new System.Drawing.Size(34, 20);
            this.numericUpDownRing2.TabIndex = 11;
            this.numericUpDownRing2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDownRing2.ValueChanged += new System.EventHandler(this.numericUpDownRing2_ValueChanged);
            // 
            // numericUpDownRing3
            // 
            this.numericUpDownRing3.Location = new System.Drawing.Point(53, 299);
            this.numericUpDownRing3.Name = "numericUpDownRing3";
            this.numericUpDownRing3.Size = new System.Drawing.Size(34, 20);
            this.numericUpDownRing3.TabIndex = 12;
            this.numericUpDownRing3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDownRing3.ValueChanged += new System.EventHandler(this.numericUpDownRing3_ValueChanged);
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(255, 299);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(75, 23);
            this.buttonReset.TabIndex = 13;
            this.buttonReset.Text = "Reset";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 326);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.numericUpDownRing3);
            this.Controls.Add(this.numericUpDownRing2);
            this.Controls.Add(this.numericUpDownRing1);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonLoad);
            this.Controls.Add(this.labelRing3);
            this.Controls.Add(this.labelRing2);
            this.Controls.Add(this.labelRing1);
            this.Controls.Add(this.textBoxOutput);
            this.Controls.Add(this.textBoxInput);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Enigma Machine";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRing1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRing2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRing3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxInput;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.TextBox textBoxOutput;
        private System.Windows.Forms.Label labelRing1;
        private System.Windows.Forms.Label labelRing2;
        private System.Windows.Forms.Label labelRing3;
        private System.Windows.Forms.Button buttonLoad;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.NumericUpDown numericUpDownRing1;
        private System.Windows.Forms.NumericUpDown numericUpDownRing2;
        private System.Windows.Forms.NumericUpDown numericUpDownRing3;
        private System.Windows.Forms.Button buttonReset;
    }
}

