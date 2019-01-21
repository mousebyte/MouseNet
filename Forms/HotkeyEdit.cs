using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MouseNet.Forms
{
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

        public HotkeyEdit()
            {
            if (NeedNonCtlAltMod.Count == 0)
                PopulateModifierLists();
            base.ShortcutsEnabled = false;
            ReadOnly = true;
            }

        public Keys Hotkey {
            get => _keyCode | _modifiers;
            set {
                _keyCode = value & Keys.KeyCode;
                _modifiers = value & Keys.Modifiers;
                UpdateText();
            }
        }

        public Keys Modifiers {
            get => _modifiers;
            set {
                _modifiers = value;
                UpdateText();
            }
        }

        public Keys KeyCode {
            get => _keyCode;
            set {
                _keyCode = value;
                UpdateText();
            }
        }

        public new void Clear()
            {
            _keyCode = Keys.None;
            _modifiers = Keys.None;
            UpdateText();
            }

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
            if (_keyCode == Keys.None && _modifiers == Keys.None)
                {
                Text = "None";
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
                text += KeysConverter.ConvertToString(_keyCode);
            
            Text = text;
            }

        protected override void OnKeyDown
            (KeyEventArgs e)
            {
            base.OnKeyDown(e);
            e.Handled = true;
            
            if (e.Modifiers == Keys.None) return;
            _modifiers = e.Modifiers;
            
            switch (e.KeyCode)
                {
                case Keys.Back:
                case Keys.Delete:
                    Clear();
                    return;
                case Keys.ControlKey:
                case Keys.ShiftKey:
                case Keys.Menu:
                    UpdateText();
                    return;
                }

            switch (e.Modifiers)
                {
                case Keys.Shift
                    when NeedNonShiftMod.Contains(e.KeyCode):
                case Keys.Control | Keys.Alt
                    when NeedNonCtlAltMod.Contains(e.KeyCode):
                    UpdateText();
                    break;
                default:
                    _inputting = false;
                    _keyCode = e.KeyCode;
                    UpdateText();
                    break;
                }
            }

        protected override void OnLostFocus
            (EventArgs e)
            {
            _inputting = false;
            base.OnLostFocus(e);
            }

        protected override void OnPreviewKeyDown
            (PreviewKeyDownEventArgs e)
            {
            base.OnPreviewKeyDown(e);
            _inputting = true;
            e.IsInputKey = e.Modifiers != Keys.None;
            }
    }
}