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
            this._editFont = new System.Windows.Forms.Button();
            this._text = new System.Windows.Forms.TextBox();
            this._dialog = new System.Windows.Forms.FontDialog();
            this._toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // _editFont
            // 
            this._editFont.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._editFont.AutoSize = true;
            this._editFont.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._editFont.Location = new System.Drawing.Point(170, 0);
            this._editFont.Margin = new System.Windows.Forms.Padding(0);
            this._editFont.Name = "_editFont";
            this._editFont.Size = new System.Drawing.Size(30, 24);
            this._editFont.TabIndex = 0;
            this._toolTip.SetToolTip(this._editFont, "Edit Font");
            this._editFont.UseVisualStyleBackColor = true;
            this._editFont.Click += new System.EventHandler(this.OnFontClicked);
            // 
            // _text
            // 
            this._text.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._text.Location = new System.Drawing.Point(0, 2);
            this._text.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this._text.Name = "_text";
            this._text.Size = new System.Drawing.Size(160, 20);
            this._text.TabIndex = 1;
            // 
            // _dialog
            // 
            this._dialog.ShowColor = true;
            // 
            // FontTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._text);
            this.Controls.Add(this._editFont);
            this.MinimumSize = new System.Drawing.Size(150, 24);
            this.Name = "FontTextBox";
            this.Size = new System.Drawing.Size(200, 24);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _editFont;
        private System.Windows.Forms.TextBox _text;
        private System.Windows.Forms.FontDialog _dialog;
        private System.Windows.Forms.ToolTip _toolTip;
    }
}
