namespace MouseNet.Forms.Controls
{
    partial class SizeControl
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
            this.cHeight = new System.Windows.Forms.NumericUpDown();
            this.lblX = new System.Windows.Forms.Label();
            this.cWidth = new System.Windows.Forms.NumericUpDown();
            this.lblSize = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.cHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cWidth)).BeginInit();
            this.SuspendLayout();
            // 
            // cHeight
            // 
            this.cHeight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cHeight.Location = new System.Drawing.Point(90, 3);
            this.cHeight.Name = "cHeight";
            this.cHeight.Size = new System.Drawing.Size(42, 20);
            this.cHeight.TabIndex = 16;
            // 
            // lblX
            // 
            this.lblX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblX.AutoSize = true;
            this.lblX.Location = new System.Drawing.Point(77, 5);
            this.lblX.Margin = new System.Windows.Forms.Padding(0);
            this.lblX.Name = "lblX";
            this.lblX.Size = new System.Drawing.Size(12, 13);
            this.lblX.TabIndex = 15;
            this.lblX.Text = "x";
            // 
            // cWidth
            // 
            this.cWidth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cWidth.Location = new System.Drawing.Point(33, 3);
            this.cWidth.Name = "cWidth";
            this.cWidth.Size = new System.Drawing.Size(42, 20);
            this.cWidth.TabIndex = 14;
            this.cWidth.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // lblSize
            // 
            this.lblSize.AutoSize = true;
            this.lblSize.Location = new System.Drawing.Point(3, 5);
            this.lblSize.Name = "lblSize";
            this.lblSize.Size = new System.Drawing.Size(30, 13);
            this.lblSize.TabIndex = 13;
            this.lblSize.Text = "Size:";
            // 
            // SizeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cHeight);
            this.Controls.Add(this.lblX);
            this.Controls.Add(this.cWidth);
            this.Controls.Add(this.lblSize);
            this.Name = "SizeControl";
            this.Size = new System.Drawing.Size(135, 26);
            ((System.ComponentModel.ISupportInitialize)(this.cHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cWidth)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown cHeight;
        private System.Windows.Forms.Label lblX;
        private System.Windows.Forms.NumericUpDown cWidth;
        private System.Windows.Forms.Label lblSize;
    }
}
