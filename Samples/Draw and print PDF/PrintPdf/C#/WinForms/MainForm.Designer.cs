namespace BitMiracle.Docotic.Samples.PrintPdf
{
    partial class MainForm
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
            if (disposing)
            {
                if (components != null)
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
            this.printButton = new System.Windows.Forms.Button();
            this.previewButton = new System.Windows.Forms.Button();
            this.printSize = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // printButton
            // 
            this.printButton.Location = new System.Drawing.Point(16, 15);
            this.printButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.printButton.Name = "printButton";
            this.printButton.Size = new System.Drawing.Size(132, 43);
            this.printButton.TabIndex = 0;
            this.printButton.Text = "Print";
            this.printButton.UseVisualStyleBackColor = true;
            this.printButton.Click += new System.EventHandler(this.printButton_Click);
            // 
            // previewButton
            // 
            this.previewButton.Location = new System.Drawing.Point(181, 15);
            this.previewButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.previewButton.Name = "previewButton";
            this.previewButton.Size = new System.Drawing.Size(132, 43);
            this.previewButton.TabIndex = 1;
            this.previewButton.Text = "Preview";
            this.previewButton.UseVisualStyleBackColor = true;
            this.previewButton.Click += new System.EventHandler(this.previewButton_Click);
            // 
            // printSize
            // 
            this.printSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.printSize.FormattingEnabled = true;
            this.printSize.Items.AddRange(new object[] {
            "Fit page",
            "Actual size"});
            this.printSize.Location = new System.Drawing.Point(16, 77);
            this.printSize.Name = "printSize";
            this.printSize.Size = new System.Drawing.Size(297, 24);
            this.printSize.TabIndex = 2;
            this.printSize.SelectedIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 497);
            this.Controls.Add(this.printSize);
            this.Controls.Add(this.previewButton);
            this.Controls.Add(this.printButton);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "MainForm";
            this.Text = "Print PDF";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button printButton;
        private System.Windows.Forms.Button previewButton;
        private System.Windows.Forms.ComboBox printSize;
    }
}

