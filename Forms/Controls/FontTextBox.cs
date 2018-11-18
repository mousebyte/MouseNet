using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MouseNet.Forms.Controls
{
    /// <inheritdoc />
    /// <summary>
    ///     Represents a <see cref="T:System.Windows.Forms.TextBox" /> with a
    ///     configurable font value.
    /// </summary>
    /// <seealso cref="T:System.Windows.Forms.UserControl" />
    public partial class FontTextBox : UserControl
    {
        private readonly int _btnAreaWidth;
        private readonly Font _defaultFont;
        private readonly FontDialog _fontDialog = new FontDialog();
        private readonly int _minHeight;
        private readonly int _padding;
        private int _multiLineHeight;
        private bool _showFontSelect;

        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="T:MouseNet.Forms.Controls.FontTextBox" /> class.
        /// </summary>
        public FontTextBox()
            {
            InitializeComponent();
            PreviewFont = true;
            _showFontSelect = true;
            _btnAreaWidth = Width - _cText.Width;
            _defaultFont = _cText.Font;
            _padding = _cText.Margin.Top + _cText.Margin.Bottom;
            _minHeight = _cText.Height + _padding;
            _multiLineHeight = _minHeight;
            }

        /// <summary>
        ///     Gets or sets a value indicating whether or not the font
        ///     selection button is visible.
        /// </summary>
        [EditorBrowsable]
        public bool ShowFontSelect {
            get => _showFontSelect;
            set {
                _showFontSelect = value;
                _cEditFont.Enabled = value;
                _cEditFont.Visible = value;
                if (value)
                    {
                    _cText.Width = Width - _btnAreaWidth;
                    _cText.Font = _defaultFont;
                    } else
                    {
                    _cText.Width = Width;
                    }
            }
        }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the font associated with the <see cref="FontTextBox" />.
        /// </summary>
        public override Font Font {
            get => _fontDialog.Font;
            set {
                _fontDialog.Font = value;
                if (PreviewFont) SetTextFont(value);
            }
        }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the contents of the text box.
        /// </summary>
        public override string Text {
            get => _cText.Text;
            set => _cText.Text = value;
        }

        /// <summary>
        ///     Gets or sets a value indicating whether this <see cref="FontTextBox" /> is
        ///     multiline.
        /// </summary>
        /// <value>
        ///     <c>true</c> if multiline; otherwise, <c>false</c>.
        /// </value>
        public bool Multiline {
            get => _cText.Multiline;
            set {
                _cText.Multiline = value;
                SetBoundsCore(Left,
                              Top,
                              Width,
                              _multiLineHeight,
                              BoundsSpecified.Height);
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether the selected font will be applied
        ///     to the contents of the text box.
        /// </summary>
        /// <value>
        ///     <c>true</c> if the font is applied to the text box contents; otherwise,
        ///     <c>false</c>.
        /// </value>
        /// <remarks>
        ///     The size of the selected font, while reflected in the <c>Font</c> property,
        ///     does not affect the size of the text in the text box.
        /// </remarks>
        public bool PreviewFont { get; set; }

        /// <summary>
        ///     Gets or sets the image displayed on the font configuration button.
        /// </summary>
        /// <value>
        ///     The font button image.
        /// </value>
        public Image FontButtonImage {
            get => _cEditFont.Image;
            set => _cEditFont.Image = value;
        }

        /// <inheritdoc />
        protected override void SetBoundsCore
            (int x,
             int y,
             int width,
             int height,
             BoundsSpecified specified)
            {
            if ((specified & BoundsSpecified.Height) != 0)
                {
                if (Multiline && height >= _minHeight)
                    {
                    _multiLineHeight = height;
                    _cText.Height = _multiLineHeight - _padding;
                    } else
                    {
                    height = _minHeight;
                    }
                }

            base.SetBoundsCore(x, y, width, height, specified);
            }

        /// <summary>
        ///     Sets font of the text box contents.
        /// </summary>
        /// <param name="font">The font.</param>
        private void SetTextFont
            (Font font)
            {
            _cText.Font = new Font(font.Name,
                                   _cText.Font.Size,
                                   font.Style,
                                   font.Unit);
            }

        /// <summary>
        ///     Called when the font configuration button is clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">
        ///     The <see cref="EventArgs" /> instance containing the event
        ///     data.
        /// </param>
        private void OnEditFontClicked
            (object sender,
             EventArgs args)
            {
            if (_fontDialog.ShowDialog(ParentForm) == DialogResult.OK)
                SetTextFont(_fontDialog.Font);
            }
    }
}