namespace MouseNet.Forms.Controls
{
    partial class FontTextBox
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
            this.components = new System.ComponentModel.Container();
            this._cEditFont = new System.Windows.Forms.Button();
            this._cText = new System.Windows.Forms.TextBox();
            this._cToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // _cEditFont
            // 
            this._cEditFont.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._cEditFont.AutoSize = true;
            this._cEditFont.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._cEditFont.Location = new System.Drawing.Point(194, 0);
            this._cEditFont.Margin = new System.Windows.Forms.Padding(0);
            this._cEditFont.Name = "_cEditFont";
            this._cEditFont.Size = new System.Drawing.Size(6, 6);
            this._cEditFont.TabIndex = 0;
            this._cToolTip.SetToolTip(this._cEditFont, "Edit Font");
            this._cEditFont.UseVisualStyleBackColor = true;
            this._cEditFont.Click += new System.EventHandler(this.OnEditFontClicked);
            // 
            // _cText
            // 
            this._cText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._cText.Location = new System.Drawing.Point(0, 2);
            this._cText.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this._cText.Name = "_cText";
            this._cText.Size = new System.Drawing.Size(160, 20);
            this._cText.TabIndex = 1;
            // 
            // FontTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._cText);
            this.Controls.Add(this._cEditFont);
            this.MinimumSize = new System.Drawing.Size(150, 24);
            this.Name = "FontTextBox";
            this.Size = new System.Drawing.Size(200, 24);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _cEditFont;
        private System.Windows.Forms.TextBox _cText;
        private System.Windows.Forms.ToolTip _cToolTip;
    }
}
