using System;
using System.Drawing;
using System.Windows.Forms;

namespace MouseNet.Forms.Controls
{
    /// <inheritdoc />
    /// <summary>
    ///     Represents a clickable area that shows a <see cref="T:System.Windows.Forms.ColorDialog" />
    ///     and displays the selected color.
    /// </summary>
    /// <seealso cref="T:System.Windows.Forms.UserControl" />
    public partial class ColorPicker : UserControl
    {
        private readonly ColorDialog _dialog;

        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:MouseNet.Forms.Controls.ColorPicker" /> class.
        /// </summary>
        public ColorPicker()
            {
            InitializeComponent();
            _dialog = new ColorDialog();
            }

        /// <summary>
        ///     Gets or sets the selected color.
        /// </summary>
        /// <value>
        ///     The color.
        /// </value>
        public Color Color {
            get => _dialog.Color;
            set {
                _dialog.Color = value;
                cDisplayBox.BackColor = value;
            }
        }

        /// <inheritdoc />
        /// <summary>
        ///     Displays the <see cref="ColorDialog" /> and sets the display's color if the
        ///     input is accepted.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnClick
            (EventArgs e)
            {
            base.OnClick(e);
            if (_dialog.ShowDialog(ParentForm) == DialogResult.OK)
                cDisplayBox.BackColor = _dialog.Color;
            }
    }
}