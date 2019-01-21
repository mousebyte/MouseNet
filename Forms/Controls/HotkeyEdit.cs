using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace MouseNet.Forms.Controls
{
    /// <inheritdoc />
    /// <summary>
    ///     Represents a text box that the user can use to input
    ///     a hotkey combination.
    /// </summary>
    public class HotkeyEdit : TextBox
    {
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

        /// <inheritdoc />
        /// <summary>
        ///     Constructs a <see cref="T:MouseNet.Forms.Controls.HotkeyEdit" /> control using default values.
        /// </summary>
        public HotkeyEdit()
            {
            if (NeedNonCtlAltMod.Count == 0)
                PopulateModifierLists();
            Blacklist = new List<Keys>();
            base.ShortcutsEnabled = false;
            ReadOnly = true;
            AllowShiftOnlyHotkeys = false;
            }

        /// <inheritdoc />
        /// <summary>
        ///     Constructs a <see cref="HotkeyEdit" /> control using the specified
        ///     key blacklist.
        /// </summary>
        /// <param name="modifierBlacklist">
        ///     An <see cref="System.Collections.IEnumerable" />
        ///     object containing blacklisted key combinations.
        /// </param>
        public HotkeyEdit
            (IEnumerable<Keys> modifierBlacklist)
            : this()
            {
            Blacklist = modifierBlacklist.ToList();
            }

        /// <inheritdoc />
        /// <summary>
        ///     Constructs a <see cref="HotkeyEdit" /> control using the specified
        ///     key blacklist.
        /// </summary>
        /// <param name="modifierBlacklist">An array of blacklisted key combinations.</param>
        public HotkeyEdit
            (params Keys[] modifierBlacklist)
            : this()
            {
            Blacklist = modifierBlacklist;
            }

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

        /// <summary>
        ///     Gets a list representing blacklisted key combinations. Blacklisted items
        ///     can be modifiers, key codes, or a combination of the two.
        /// </summary>
        public IList<Keys> Blacklist { get; }

        /// <summary>
        ///     Gets or sets the value of the <see cref="HotkeyEdit" /> control.
        /// </summary>
        public Keys Hotkey {
            get => _keyCode | _modifiers;
            set {
                _keyCode = value & Keys.KeyCode;
                _modifiers = value & Keys.Modifiers;
                UpdateText();
            }
        }

        /// <summary>
        ///     Gets or sets the modifier flags for the <see cref="HotkeyEdit" /> control.
        /// </summary>
        public Keys Modifiers {
            get => _modifiers;
            set {
                _modifiers = value;
                UpdateText();
            }
        }

        /// <summary>
        ///     Gets or sets the keyboard code for the <see cref="HotkeyEdit" /> control.
        /// </summary>
        public Keys KeyCode {
            get => _keyCode;
            set {
                _keyCode = value;
                UpdateText();
            }
        }

        /// <summary>
        ///     Clears the hotkey value from the HotkeyEdit control.
        /// </summary>
        public new void Clear()
            {
            _keyCode = Keys.None;
            _modifiers = Keys.None;
            UpdateText();
            }

        /// <inheritdoc />
        protected override bool ProcessCmdKey
            (ref Message msg,
             Keys keyData)
            {
            if (keyData != Keys.Delete
             && keyData != (Keys.Control & Keys.Delete))
                return keyData == (Keys.Shift | Keys.Insert)
                    || base.ProcessCmdKey(ref msg, keyData);
            Clear();
            return true;
            }

        private bool CheckBlacklist
            (Keys keyCode)
            {
            if (!Blacklist.Any(k => k == _modifiers
                                 || k == keyCode
                                 || k == (_modifiers & keyCode)))
                return true;

            Clear();
            _inputting = false;
            return false;
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
            }

        /// <inheritdoc />
        protected override void OnKeyDown
            (KeyEventArgs e)
            {
            base.OnKeyDown(e);
            e.Handled = true;

            if (e.Modifiers == Keys.None) return;

            switch (e.KeyCode)
                {
                case Keys.Back:
                case Keys.Delete:
                    Clear();
                    return;
                case Keys.ControlKey:
                case Keys.ShiftKey:
                case Keys.Menu:
                    _modifiers = e.Modifiers;
                    UpdateText();
                    return;
                }

            if (!CheckBlacklist(e.KeyCode)) return;

            switch (e.Modifiers)
                {
                case Keys.Shift
                    when NeedNonShiftMod.Contains(e.KeyCode):
                case Keys.Control | Keys.Alt
                    when NeedNonCtlAltMod.Contains(e.KeyCode):
                    Clear();
                    _inputting = false;
                    break;
                default:
                    _inputting = false;
                    KeyCode = e.KeyCode;
                    break;
                }
            }

        /// <inheritdoc />
        protected override void OnLostFocus
            (EventArgs e)
            {
            _inputting = false;
            UpdateText();
            base.OnLostFocus(e);
            }

        /// <inheritdoc />
        protected override void OnPreviewKeyDown
            (PreviewKeyDownEventArgs e)
            {
            base.OnPreviewKeyDown(e);
            _inputting = true;
            e.IsInputKey = e.Modifiers != Keys.None;
            }
    }

    internal static class User32Interop
    {
        private const byte HighBit = 0x80;

        public static char ToAscii
            (Keys key,
             Keys modifiers)
            {
            var outputBuilder = new StringBuilder(2);
            var result = ToAscii((uint) key,
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