using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace MouseNet.Forms.Controls {
    /// <summary>
    /// Represents a control that can be used to input a hotkey combination.
    /// </summary>
    public partial class HotkeyEdit : UserControl {

        private readonly InputControl _inputControl;

        /// <inheritdoc />
        public HotkeyEdit()
            {
            InitializeComponent();
            AllowShiftOnlyHotkeys = false;
            _inputControl = new InputControl(this)
                {
                Location = new Point(0, 4),
                Size = new Size(Width - 54, Height - 8),
                Anchor = AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right | AnchorStyles.Top,
                Visible = true
                };
            _inputControl.ValidHotkeyEntered += OnValidHotkeyEntered;
            _inputControl.InputCancelled += OnInputCancelled;
            _inputControl.InputStarted += OnInputStarted;
            Controls.Add(_inputControl);
            ShowButtons = false;
            _btnAccept.Enabled = false;
            }

        /// <summary>
        /// Gets or sets the value assigned to the hotkey control.
        /// </summary>
        public Keys Value {
            get => _inputControl.Value;
            set => _inputControl.Value = value;
        }

        
        private Keys _previousValue = Keys.None;

        private void OnInputCancelled(object sender, EventArgs args)
            {
            _inputControl.Value = _previousValue;
            _btnAccept.Enabled = false;
            ShowButtons = false;
            }

        private void OnValidHotkeyEntered(object sender, EventArgs args)
            {
            _btnAccept.Enabled = true;
            }

        private void OnAcceptClicked(object sender, EventArgs args)
            {
            _previousValue = _inputControl.Value;
            _btnAccept.Enabled = false;
            ShowButtons = false;
            }

        private void OnInputStarted(object sender, EventArgs args)
            {
            ShowButtons = true;
            _btnAccept.Enabled = false;
            }

        private bool ShowButtons {
            set {
                _btnAccept.Visible = value;
                _btnCancel.Visible = value;
            }
        }

        /// <summary>
        /// Clears the value of the hotkey control.
        /// </summary>
        public void Clear()
            {
            _inputControl.Value = Keys.None;
            }

        /// <summary>
        ///     Gets a list representing blacklisted key combinations. Blacklisted items
        ///     can be modifiers, key codes, or a combination of the two.
        /// </summary>
        public IList<Keys> Blacklist { get; } = new List<Keys>();


        /// <summary>
        ///     Gets or sets a value indicating wheather to allow hotkeys with
        ///     Shift as their only modifier.
        /// </summary>
        /// <remarks>
        ///     AllowShiftOnlyHotkeys uses the Blacklist property internally.
        /// </remarks>
        public bool AllowShiftOnlyHotkeys {
            get => !Blacklist.Contains(Keys.Shift);
            set {
                if (value == AllowShiftOnlyHotkeys) return;
                if (!value) Blacklist.Add(Keys.Shift);
                else Blacklist.Remove(Keys.Shift);
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating wheather to allow hotkeys with
        ///     Control as their only modifier.
        /// </summary>
        /// <remarks>
        ///     AllowCtrlOnlyHotkeys uses the Blacklist property internally.
        /// </remarks>
        public bool AllowCtrlOnlyHotkeys {
            get => !Blacklist.Contains(Keys.Control);
            set {
                if (value == AllowCtrlOnlyHotkeys) return;
                if (!value) Blacklist.Add(Keys.Control);
                else Blacklist.Remove(Keys.Control);
            }
        }

        private class InputControl : Control {
            private static readonly KeysConverter KeysConverter =
                new KeysConverter();

            private static readonly IList<Keys> NeedNonCtlAltMod =
                new List<Keys>();

            private static readonly IList<Keys> NeedNonShiftMod =
                new List<Keys>
                    {
                    Keys.Insert,
                    Keys.Help,
                    Keys.Multiply,
                    Keys.Add,
                    Keys.Subtract,
                    Keys.Divide,
                    Keys.Decimal,
                    Keys.Return,
                    Keys.Escape,
                    Keys.NumLock,
                    Keys.Scroll,
                    Keys.Pause
                    };

            private bool _inputting;
            private Keys _keyCode;
            private Keys _modifiers;
            private int _textTop;
            private readonly HotkeyEdit _parent;

            public InputControl(HotkeyEdit parent)
                {
                _parent = parent;
                Value = Keys.None;
                if (NeedNonCtlAltMod.Count == 0)
                    PopulateModifierLists();
                SetStyle(ControlStyles.UserPaint, true);
                SetStyle(ControlStyles.AllPaintingInWmPaint, true);
                SetStyle(ControlStyles.DoubleBuffer, true);
                UpdateTextTop();
                }

            public bool IsValid { get; private set; }

            public Keys Value {
                get => _keyCode | _modifiers;
                set {
                    _keyCode = value & Keys.KeyCode;
                    _modifiers = value & Keys.Modifiers;
                    UpdateText();
                }
            }

            private void UpdateTextTop()
                {
                _textTop = (Height - TextRenderer.MeasureText(Text, Font).Height) / 2 + 1;
                }

            public event EventHandler ValidHotkeyEntered;

            private void InvokeValidHotkeyEntered(object sender, EventArgs args)
                {
                ValidHotkeyEntered?.Invoke(sender, args);
                }

            public event EventHandler InputCancelled;

            private void InvokeInputCancelled(object sender, EventArgs args)
                {
                InputCancelled?.Invoke(sender, args);
                }

            public event EventHandler InputStarted;

            private void InvokeInputStarted(object sender, EventArgs args)
                {
                InputStarted?.Invoke(sender, args);
                }

            private void UpdateValidity()
                {
                IsValid = !_inputting
                       && _keyCode != Keys.None
                       && (_modifiers != Keys.Shift
                        || !NeedNonShiftMod.Contains(_keyCode))
                       && (_modifiers != (Keys.Control | Keys.Alt)
                        || !NeedNonCtlAltMod.Contains(_keyCode))
                       && !_parent.Blacklist.Any(
                              k => k == _modifiers
                                || k == _keyCode
                                || k == (_modifiers & _keyCode));
                }

            protected override void OnFontChanged(EventArgs e)
                {
                base.OnFontChanged(e);
                UpdateTextTop();
                }

            protected override void OnSizeChanged(EventArgs e)
                {
                base.OnSizeChanged(e);
                UpdateTextTop();
                }

            protected override void OnPaint(PaintEventArgs e)
                {
                base.OnPaint(e);
                var g = e.Graphics;
                var rect = new Rectangle(0, 0, Width - 1, Height - 1);
                g.FillRectangle(new SolidBrush(BackColor), rect);
                g.DrawString(Text, Font, new SolidBrush(ForeColor), 0, _textTop);
                if (Value == Keys.None || IsValid)
                    g.DrawRectangle(
                        Focused
                            ? SystemPens.Highlight
                            : SystemPens.ControlDarkDark, rect);
                else g.DrawRectangle(Pens.Red, rect);
                }

            protected override void OnClick(EventArgs e)
                {
                base.OnClick(e);
                Focus();
                Invalidate();
                }

            protected override bool ProcessCmdKey
            (ref Message msg,
             Keys keyData)
                {
                if (keyData != Keys.Delete
                 && keyData != (Keys.Control & Keys.Delete))
                    return keyData == (Keys.Shift | Keys.Insert)
                        || base.ProcessCmdKey(ref msg, keyData);
                Value = Keys.None;
                return true;
                }

            private static void PopulateModifierLists()
                {
                for (var k = Keys.D0; k <= Keys.D9; k++)
                    NeedNonCtlAltMod.Add(k);
                for (var k = Keys.D0; k <= Keys.Z; k++)
                    NeedNonShiftMod.Add(k);
                for (var k = Keys.NumPad0; k <= Keys.NumPad9; k++)
                    NeedNonShiftMod.Add(k);
                for (var k = Keys.Oem1; k <= Keys.OemBackslash; k++)
                    NeedNonShiftMod.Add(k);
                for (var k = Keys.Space; k <= Keys.Home; k++)
                    NeedNonShiftMod.Add(k);
                }

            private void UpdateText()
                {
                if (_keyCode == Keys.None
                 && _modifiers == Keys.None
                 && !_inputting)
                    {
                    Text = @"None";
                    return;
                    }

                var text = string.Empty;

                if ((_modifiers & Keys.Control) == Keys.Control)
                    text += "Ctrl+";
                if ((_modifiers & Keys.Shift) == Keys.Shift)
                    text += "Shift+";
                if ((_modifiers & Keys.Alt) == Keys.Alt)
                    text += "Alt+";
                if (!_inputting)
                    {
                    var keyString =
                        KeysConverter.ConvertToString(_keyCode);
                    if (keyString != null && keyString.Contains("Oem"))
                        text +=
                            User32Interop.ToAscii(_keyCode, Keys.None);
                    else text += keyString;
                    }

                Text = text;
                UpdateValidity();
                Invalidate();
                }

            protected override void OnKeyDown
                (KeyEventArgs e)
                {
                base.OnKeyDown(e);
                e.Handled = true;

                if (e.Modifiers == Keys.None)
                    {
                    _inputting = false;
                    return;
                    }

                switch (e.KeyCode)
                    {
                    case Keys.Back:
                    case Keys.Delete:
                        Value = Keys.None;
                        InvokeInputCancelled(this, EventArgs.Empty);
                        return;
                    case Keys.ControlKey:
                    case Keys.ShiftKey:
                    case Keys.Menu:
                        _modifiers = e.Modifiers;
                        UpdateText();
                        return;
                    default:
                        _keyCode = e.KeyCode;
                        _inputting = false;
                        break;
                    }

                UpdateText();
                if (IsValid) InvokeValidHotkeyEntered(this, EventArgs.Empty);
                }


            protected override void OnLostFocus
                (EventArgs e)
                {
                base.OnLostFocus(e);
                _inputting = false;
                Invalidate();
                }

            protected override void OnPreviewKeyDown
                (PreviewKeyDownEventArgs e)
                {
                base.OnPreviewKeyDown(e);
                if (!_inputting)
                    {
                    _inputting = true;
                    InvokeInputStarted(this, EventArgs.Empty);
                    }
                e.IsInputKey = e.Modifiers != Keys.None;
                }
        }
    }
    
    internal static class User32Interop {
        private const byte HighBit = 0x80;

        public static char ToAscii
        (Keys key,
         Keys modifiers)
            {
            var outputBuilder = new StringBuilder(2);
            var result = ToAscii(
                (uint) key,
                0,
                GetKeyState(modifiers),
                outputBuilder,
                0);
            if (result == 1)
                return outputBuilder[0];
            throw new Exception("Invalid key");
            }

        private static byte[] GetKeyState
            (Keys modifiers)
            {
            var keyState = new byte[256];
            foreach (Keys key in Enum.GetValues(typeof(Keys)))
                if ((modifiers & key) == key)
                    keyState[(int) key] = HighBit;

            return keyState;
            }

        [DllImport("user32.dll")]
        private static extern int ToAscii
        (uint uVirtKey,
         uint uScanCode,
         byte[] lpKeyState,
         [Out] StringBuilder lpChar,
         uint uFlags);
    }
}