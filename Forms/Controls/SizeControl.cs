using System.Drawing;
using System.Windows.Forms;

namespace MouseNet.Forms.Controls
{
    /// <inheritdoc />
    /// <summary>
    ///     Represents a pair of <see cref="T:System.Windows.Forms.NumericUpDown" />
    ///     controls that
    ///     can be used to input dimensions.
    /// </summary>
    /// <seealso cref="T:System.Windows.Forms.UserControl" />
    public partial class SizeControl : UserControl
    {
        private const int CenterWidth = 15;
        private Size _maximumValue;
        private Size _minimumValue;

        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="T:MouseNet.Forms.Controls.SizeControl" /> class.
        /// </summary>
        public SizeControl()
            {
            InitializeComponent();
            var screenArea = Screen.PrimaryScreen.WorkingArea;
            MaximumValue =
                new Size(screenArea.Width, screenArea.Height);
            }

        private int WidthLeft => Padding.Left + lblSize.Width;

        /// <summary>
        ///     Gets or sets the value of the control.
        /// </summary>
        /// <value>
        ///     The value.
        /// </value>
        public Size Value {
            get => new Size((int) cWidth.Value, (int) cHeight.Value);
            set {
                WidthValue = value.Width;
                HeightValue = value.Height;
            }
        }

        /// <summary>
        ///     Gets or sets the width component of the control's value.
        /// </summary>
        /// <value>
        ///     The width value.
        /// </value>
        public int WidthValue {
            get => (int) cWidth.Value;
            set {
                if (value < MinimumValue.Width)
                    cWidth.Value = MinimumValue.Width;
                else if (value > MaximumValue.Width)
                    cWidth.Value = MaximumValue.Width;
                else cWidth.Value = value;
            }
        }

        /// <summary>
        ///     Gets or sets the height component of the control's value.
        /// </summary>
        /// <value>
        ///     The height value.
        /// </value>
        public int HeightValue {
            get => (int) cHeight.Value;
            set {
                if (value < MinimumValue.Height)
                    cWidth.Value = MinimumValue.Height;
                else if (value > MaximumValue.Height)
                    cWidth.Value = MaximumValue.Height;
                else cWidth.Value = value;
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether the lefthand label is shown.
        /// </summary>
        /// <value>
        ///     <c>true</c> if the label is shown; otherwise, <c>false</c>.
        /// </value>
        public bool ShowLabel {
            get => Controls.Contains(lblSize);
            set {
                if (value) Controls.Add(lblSize);
                else Controls.Remove(lblSize);
            }
        }

        /// <summary>
        ///     Gets or sets the minimum value.
        /// </summary>
        /// <value>
        ///     The minimum value.
        /// </value>
        public Size MinimumValue {
            get => _minimumValue;
            set {
                _minimumValue = value;
                cWidth.Minimum = value.Width;
                cHeight.Minimum = value.Height;
            }
        }

        /// <summary>
        ///     Gets or sets the maximum value.
        /// </summary>
        /// <value>
        ///     The maximum value.
        /// </value>
        public Size MaximumValue {
            get => _maximumValue;
            set {
                _maximumValue = value;
                cWidth.Maximum = value.Width;
                cHeight.Maximum = value.Height;
            }
        }

        /// <inheritdoc />
        protected override void SetBoundsCore
            (int x,
             int y,
             int width,
             int height,
             BoundsSpecified specified)
            {
            var setWidth = (specified & BoundsSpecified.Width)
                        == BoundsSpecified.Width;
            if (setWidth && width < 135) width = 135;
            base.SetBoundsCore(x, y, width, height, specified);
            if (!setWidth) return;
            var combindedWidth =
                Width - WidthLeft - CenterWidth - Padding.Right;
            var nudWidth = combindedWidth / 2;
            cWidth.Width = combindedWidth % 2 > 0
                               ? nudWidth + 1
                               : nudWidth;
            cHeight.Width = nudWidth;
            lblX.Left = cWidth.Right + 2;
            cHeight.Left = lblX.Right + 1;
            }
    }
}