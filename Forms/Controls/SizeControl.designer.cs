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
            this._cHeight = new System.Windows.Forms.NumericUpDown();
            this._lblX = new System.Windows.Forms.Label();
            this._cWidth = new System.Windows.Forms.NumericUpDown();
            this._lblSize = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this._cHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._cWidth)).BeginInit();
            this.SuspendLayout();
            // 
            // _cHeight
            // 
            this._cHeight.Anchor = System.Windows.Forms.AnchorStyles.None;
            this._cHeight.Location = new System.Drawing.Point(90, 3);
            this._cHeight.Name = "_cHeight";
            this._cHeight.Size = new System.Drawing.Size(42, 20);
            this._cHeight.TabIndex = 16;
            // 
            // _lblX
            // 
            this._lblX.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this._lblX.AutoSize = true;
            this._lblX.Location = new System.Drawing.Point(77, 5);
            this._lblX.Margin = new System.Windows.Forms.Padding(0);
            this._lblX.Name = "_lblX";
            this._lblX.Size = new System.Drawing.Size(12, 13);
            this._lblX.TabIndex = 15;
            this._lblX.Text = "x";
            // 
            // _cWidth
            // 
            this._cWidth.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this._cWidth.Location = new System.Drawing.Point(33, 3);
            this._cWidth.Name = "_cWidth";
            this._cWidth.Size = new System.Drawing.Size(42, 20);
            this._cWidth.TabIndex = 14;
            this._cWidth.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // _lblSize
            // 
            this._lblSize.AutoSize = true;
            this._lblSize.Location = new System.Drawing.Point(3, 5);
            this._lblSize.Name = "_lblSize";
            this._lblSize.Size = new System.Drawing.Size(30, 13);
            this._lblSize.TabIndex = 13;
            this._lblSize.Text = "Size:";
            // 
            // SizeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._cHeight);
            this.Controls.Add(this._lblX);
            this.Controls.Add(this._cWidth);
            this.Controls.Add(this._lblSize);
            this.Name = "SizeControl";
            this.Size = new System.Drawing.Size(135, 26);
            ((System.ComponentModel.ISupportInitialize)(this._cHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._cWidth)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown _cHeight;
        private System.Windows.Forms.Label _lblX;
        private System.Windows.Forms.NumericUpDown _cWidth;
        private System.Windows.Forms.Label _lblSize;
    }
}
