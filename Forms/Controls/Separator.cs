using System.Drawing;
using System.Windows.Forms;

namespace MouseNet.Forms.Controls
{
    /// <inheritdoc />
    /// <summary>
    ///     Represents a visual divider that can be used to separate groups of
    ///     controls.
    /// </summary>
    /// <seealso cref="T:System.Windows.Forms.UserControl" />
    public partial class Separator : UserControl
    {
        private const int MaxLineWidth = 8;
        private Color _color;
        private int _lineWidth;
        private bool _vertical;

        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="T:MouseNet.Forms.Controls.Separator" /> class.
        /// </summary>
        public Separator()
            {
            InitializeComponent();
            _lineWidth = 2;
            Color = SystemColors.ControlDark;
            }

        /// <summary>
        ///     Gets or sets the color of the separator line.
        /// </summary>
        /// <value>
        ///     The color.
        /// </value>
        public Color Color {
            get => _color;
            set {
                _color = value;
                Invalidate();
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether this <see cref="Separator" /> is
        ///     vertical.
        /// </summary>
        /// <value>
        ///     <c>true</c> if vertical; otherwise, <c>false</c>.
        /// </value>
        public bool Vertical {
            get => _vertical;
            set {
                _vertical = value;
                UpdateSize();
                Invalidate();
            }
        }

        /// <summary>
        ///     Gets or sets the width of the separator line.
        /// </summary>
        /// <value>
        ///     The width of the line.
        /// </value>
        public int LineWidth {
            get => _lineWidth;
            set {
                if (value < 1)
                    _lineWidth = 1;
                else if (value > MaxLineWidth)
                    _lineWidth = MaxLineWidth;
                else _lineWidth = value;
                UpdateSize();
                Invalidate();
            }
        }

        /// <summary>
        ///     Updates the size depending on whether the separator is vertical or
        ///     horizontal.
        /// </summary>
        private void UpdateSize()
            {
            if (_vertical) Width = _lineWidth + Padding.Horizontal;
            else Height = _lineWidth + Padding.Vertical;
            }

        /// <inheritdoc />
        protected override void SetBoundsCore
            (int x,
             int y,
             int width,
             int height,
             BoundsSpecified specified)
            {
            if (Vertical) width = _lineWidth + Padding.Horizontal;
            else height = _lineWidth + Padding.Vertical;

            base.SetBoundsCore(x, y, width, height, specified);
            }

        /// <inheritdoc />
        protected override void OnPaint
            (PaintEventArgs e)
            {
            base.OnPaint(e);
            using (var b = new SolidBrush(Color))
                {
                var sz = Vertical
                             ? new Size(_lineWidth,
                                        Height - Padding.Vertical)
                             : new Size(Width - Padding.Horizontal,
                                        _lineWidth);
                e.Graphics.FillRectangle(b,
                                         new Rectangle(
                                             e.ClipRectangle.Location
                                           + new Size(
                                                 Padding.Left,
                                                 Padding.Top),
                                             sz));
                }
            }
    }
}