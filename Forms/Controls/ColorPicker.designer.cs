namespace MouseNet.Forms.Controls
{
    partial class ColorPicker
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cDisplayBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.cDisplayBox)).BeginInit();
            this.SuspendLayout();
            // 
            // cDisplayBox
            // 
            this.cDisplayBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.cDisplayBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cDisplayBox.Location = new System.Drawing.Point(0, 0);
            this.cDisplayBox.Name = "cDisplayBox";
            this.cDisplayBox.Padding = new System.Windows.Forms.Padding(4);
            this.cDisplayBox.Size = new System.Drawing.Size(22, 22);
            this.cDisplayBox.TabIndex = 0;
            this.cDisplayBox.TabStop = false;
            // 
            // ColorPicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cDisplayBox);
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximumSize = new System.Drawing.Size(28, 28);
            this.MinimumSize = new System.Drawing.Size(22, 22);
            this.Name = "ColorPicker";
            this.Size = new System.Drawing.Size(22, 22);
            ((System.ComponentModel.ISupportInitialize)(this.cDisplayBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox cDisplayBox;
    }
}
