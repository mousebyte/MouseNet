namespace MouseNet.Forms.Controls
{
    partial class HotkeyEdit
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
            this._btnAccept = new System.Windows.Forms.Button();
            this._btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _btnAccept
            // 
            this._btnAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._btnAccept.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this._btnAccept.Location = new System.Drawing.Point(104, 2);
            this._btnAccept.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this._btnAccept.Name = "_btnAccept";
            this._btnAccept.Size = new System.Drawing.Size(24, 24);
            this._btnAccept.TabIndex = 2;
            this._btnAccept.Text = "✔";
            this._btnAccept.UseVisualStyleBackColor = true;
            this._btnAccept.Click += new System.EventHandler(this.OnAcceptClicked);
            // 
            // _btnCancel
            // 
            this._btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this._btnCancel.Location = new System.Drawing.Point(130, 2);
            this._btnCancel.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this._btnCancel.Name = "_btnCancel";
            this._btnCancel.Size = new System.Drawing.Size(24, 24);
            this._btnCancel.TabIndex = 3;
            this._btnCancel.Text = "🗙";
            this._btnCancel.UseVisualStyleBackColor = true;
            this._btnCancel.Click += new System.EventHandler(this.OnInputCancelled);
            // 
            // HotkeyEdit2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._btnCancel);
            this.Controls.Add(this._btnAccept);
            this.Name = "HotkeyEdit2";
            this.Size = new System.Drawing.Size(156, 28);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button _btnAccept;
        private System.Windows.Forms.Button _btnCancel;
    }
}
