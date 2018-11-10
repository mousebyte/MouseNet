using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Windows.Forms.Design.Behavior;

namespace MouseNet.Forms.Controls
{
    /// <inheritdoc />
    /// <summary>
    ///     Represents a group of <see cref="T:System.Windows.Forms.RadioButton" />
    ///     objects and provides
    ///     methods to arrange and access them.
    /// </summary>
    /// <seealso cref="T:System.Windows.Forms.UserControl" />
    [Designer(typeof(RadioButtonGroupDesigner))]
    public partial class RadioButtonGroup : UserControl
    {
        private readonly List<RadioButton> _buttons;
        private RadioButtonLayout _buttonLayout;
        private Padding _buttonMargin;
        private int _checkedItemIndex;
        private RadioButtonCollection _items;

        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="T:MouseNet.Forms.Controls.RadioButtonGroup" /> class.
        /// </summary>
        public RadioButtonGroup()
            {
            InitializeComponent();
            _buttons = new List<RadioButton>();
            ButtonLayout = RadioButtonLayout.Vertical;
            ButtonMargin = Padding;
            PaddingChanged +=
                (sender,
                 args) => ArrangeButtons();
            Items.ObjectAdded += Add;
            }

        /// <summary>
        ///     Gets the collection of <see cref="RadioButton" /> objects contained in the
        ///     group.
        /// </summary>
        /// <value>
        ///     The items.
        /// </value>
        [Editor(
            "System.Windows.Forms.Design.StringCollectionEditor, System.Design",
            "System.Drawing.Design.UITypeEditor, System.Drawing")]
        [DesignerSerializationVisibility(
            DesignerSerializationVisibility.Content)]
        public RadioButtonCollection Items =>
            _items ?? (_items = new RadioButtonCollection(this));

        /// <summary>
        ///     Gets or sets the margin placed around each <see cref="RadioButton" />.
        /// </summary>
        /// <value>
        ///     The button margin.
        /// </value>
        public Padding ButtonMargin {
            get => _buttonMargin;
            set {
                _buttonMargin = value;
                ArrangeButtons();
            }
        }

        /// <summary>
        ///     Gets or sets the layout of the buttons.
        /// </summary>
        /// <value>
        ///     The button layout.
        /// </value>
        public RadioButtonLayout ButtonLayout {
            get => _buttonLayout;
            set {
                _buttonLayout = value;
                ArrangeButtons();
            }
        }

        /// <summary>
        ///     Gets or sets the index of the checked item.
        /// </summary>
        /// <value>
        ///     The index of the checked item.
        /// </value>
        public int CheckedItemIndex {
            get => _checkedItemIndex;
            set {
                if (value < 0 || value >= _buttons.Count) return;
                _buttons[value].Checked = true;
            }
        }

        /// <summary>
        ///     Gets or sets the checked state of the <see cref="RadioButton" /> at the
        ///     specified index.
        /// </summary>
        /// <value>
        ///     The checked state of the item at the specified index.
        /// </value>
        /// <param name="index">The index.</param>
        [Browsable(false)]
        public bool this
            [int index] {
            get => _buttons[index].Checked;
            set {
                if (index < _buttons.Count && index >= 0)
                    _buttons[index].Checked = value;
            }
        }

        /// <exclude />
        /// Internal remove method.
        private void RemoveAt
            (int index)
            {
            Controls.Remove(_buttons[index]);
            _buttons.RemoveAt(index);
            _items.RemoveAt(index);
            ArrangeButtons();
            }

        /// <exclude />
        /// Internal factory method.
        private RadioButton MakeButton
            (object value)
            {
            var button = new RadioButton
                {
                Text = value.ToString(),
                Margin = ButtonMargin,
                Anchor = AnchorStyles.Left | AnchorStyles.Top,
                AutoSize = true
                };
            if (_buttons.Count == 0) button.Checked = true;
            button.CheckedChanged += OnCheckedItemChanged;
            return button;
            }

        /// <exclude />
        /// Internal add method.
        private void Add
            (object item)
            {
            var button = MakeButton(item);
            Controls.Add(button);
            _buttons.Add(button);
            ArrangeButtons();
            }

        /// <exclude />
        /// Internal set method.
        private void SetItem
            (int index,
             object value)
            {
            _buttons[index].Text = value.ToString();
            _items[index] = value.ToString();
            }

        /// <exclude />
        /// Internal clear method.
        private void Clear()
            {
            foreach (var button in _buttons) Controls.Remove(button);
            _buttons.Clear();
            _items.Clear();
            }

        /// <exclude />
        /// Internal insert method.
        private void Insert
            (int index,
             object item)
            {
            var button = MakeButton(item);
            _buttons.Insert(index, button);
            Controls.Add(button);
            ArrangeButtons();
            }

        /// <summary>
        ///     Occurs when the checked item changes.
        /// </summary>
        public event EventHandler CheckedItemChanged;

        /// <exclude />
        /// Internal event invoker.
        private void OnCheckedItemChanged
            (object sender,
             EventArgs args)
            {
            if (!(sender is RadioButton btn)) return;
            if (!btn.Checked) return;
            _checkedItemIndex = _buttons.IndexOf(btn);
            CheckedItemChanged?.Invoke(sender, args);
            }

        /// <inheritdoc />
        protected override void SetBoundsCore
            (int x,
             int y,
             int width,
             int height,
             BoundsSpecified specified)
            {
            if (width < _rightMost + Padding.Right)
                width = _rightMost + Padding.Right;
            if (height < _bottomMost + Padding.Bottom)
                height = _bottomMost + Padding.Bottom;
            base.SetBoundsCore(x, y, width, height, specified);
            }

        private int _rightMost = 10;
        private int _bottomMost = 10;

        /// <exclude />
        /// Arranges the buttons and resizes the control.
        private void ArrangeButtons()
            {
            if (!_buttons.Any()) return;
            var loc = new Point(Padding.Left, Padding.Top);

            if (ButtonLayout == RadioButtonLayout.Horizontal)
                {
                foreach (var radioButton in _buttons)
                    {
                    radioButton.Location = loc;
                    loc = new Point(
                        radioButton.Right + ButtonMargin.Horizontal,
                        loc.Y);
                    _rightMost = radioButton.Right;
                    }
                } else
                {
                _rightMost = 0;
                foreach (var radioButton in _buttons)
                    {
                    radioButton.Location = loc;
                    loc = new Point(loc.X,
                                    radioButton.Bottom
                                  + ButtonMargin.Vertical);
                    if (radioButton.Right > _rightMost)
                        _rightMost = radioButton.Right;
                    }
                }

            _bottomMost = _buttons[_buttons.Count - 1].Bottom;
            SetBoundsCore(Left,
                          Top,
                          _rightMost + Padding.Right,
                          _bottomMost + Padding.Bottom,
                          BoundsSpecified.Size);
            }

        /// <inheritdoc />
        /// <summary>
        ///     Provides snaplines for the Windows Forms Designer.
        /// </summary>
        /// <seealso cref="T:System.Windows.Forms.Design.ControlDesigner" />
        private class RadioButtonGroupDesigner : ControlDesigner
        {
            private RadioButtonGroup _control;

            public override IList SnapLines {
                get {
                    var result = new List<SnapLine>();
                    _control = Control as RadioButtonGroup;
                    if (_control == null) return result;
                    if (base.SnapLines != null)
                        result.AddRange(
                            base.SnapLines.Cast<SnapLine>());
                    foreach (var button in _control._buttons)
                        result.AddRange(GetSnapLines(button));
                    return result;
                }
            }

            private static SnapLine AddOffset
                (SnapLine line,
                 int i)
                {
                return new SnapLine(line.SnapLineType,
                                    line.Offset + i,
                                    line.Priority);
                }

            private IEnumerable<SnapLine> GetSnapLines
                (Control control)
                {
                var result = new List<SnapLine>();
                var designer =
                    TypeDescriptor.CreateDesigner(
                        control,
                        typeof(IDesigner));
                designer.Initialize(control);
                if (!(designer is ControlDesigner controlDesigner)
                 || controlDesigner.SnapLines == null)
                    return result;
                var lines = controlDesigner
                           .SnapLines.Cast<SnapLine>()
                           .ToList();
                //always add right snap lines
                result.AddRange(
                    lines.Where(l => l.SnapLineType
                                  == SnapLineType.Right)
                         .Select(r => AddOffset(r, control.Left)));
                switch (_control.ButtonLayout)
                    {
                    case RadioButtonLayout.Vertical:
                        //add baselines, bottoms, and a single left
                        result.AddRange(
                            lines
                               .Where(
                                    l => l.SnapLineType
                                      == SnapLineType.Baseline
                                      || l.SnapLineType
                                      == SnapLineType.Bottom)
                               .Select(
                                    b => AddOffset(b, control.Top)));
                        result.Add(
                            AddOffset(
                                lines.First(
                                    l => l.SnapLineType
                                      == SnapLineType.Left),
                                control.Left));
                        break;
                    case RadioButtonLayout.Horizontal:
                        //add lefts and a single bottom and baseline
                        result.AddRange(
                            lines.Where(l => l.SnapLineType
                                          == SnapLineType.Left)
                                 .Select(
                                      r => AddOffset(
                                          r,
                                          control.Left)));
                        result.Add(
                            AddOffset(
                                lines.First(
                                    l => l.SnapLineType
                                      == SnapLineType.Bottom),
                                control.Top));
                        result.Add(
                            AddOffset(
                                lines.First(
                                    l => l.SnapLineType
                                      == SnapLineType.Baseline),
                                control.Top));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                    }

                designer.Dispose();
                return result;
                }
        }

        /// <inheritdoc />
        /// <summary>
        ///     Represents a collection of
        ///     <see cref="T:System.Windows.Forms.RadioButton" /> objects within a
        ///     <see cref="T:MouseNet.Forms.Controls.RadioButtonGroup" />.
        /// </summary>
        /// <seealso cref="T:System.Collections.IList" />
        public class RadioButtonCollection : IList
        {
            private readonly RadioButtonGroup _owner;
            private ArrayList _list;

            /// <summary>
            ///     Initializes a new instance of the <see cref="RadioButtonCollection" /> class.
            /// </summary>
            /// <param name="owner">The owner.</param>
            public RadioButtonCollection
                (RadioButtonGroup owner)
                {
                _owner = owner;
                }

            private ArrayList List =>
                _list ?? (_list = new ArrayList());
            /// <inheritdoc />
            public int Count => List.Count;
            /// <inheritdoc />
            public object SyncRoot => this;
            /// <inheritdoc />
            public bool IsSynchronized => false;
            /// <inheritdoc />
            public bool IsFixedSize => false;
            /// <inheritdoc />
            public bool IsReadOnly => false;
            /// <summary>
            /// Occurs when an item is added.
            /// </summary>
            public event Action<object> ObjectAdded;

            /// <summary>
            /// Adds the specified item to the collection.
            /// </summary>
            /// <param name="item">The item to add.</param>
            /// <returns>The item's index in the collection.</returns>
            int IList.Add
                (object item)
                {
                return Add(item);
                }

            /// <inheritdoc />
            public object this
                [int index] {
                get {
                    if (index < 0 || index >= List.Count)
                        throw new ArgumentOutOfRangeException(
                            nameof(index));
                    return List[index];
                }
                set => SetItemInternal(index, value);
            }

            /// <inheritdoc />
            public void Clear()
                {
                List.Clear();
                if (_owner.IsHandleCreated) _owner.Clear();
                }

            /// <inheritdoc />
            public bool Contains
                (object value)
                {
                return IndexOf(value) != -1;
                }

            /// <summary>
            ///     Copies the contents of the collection to the provided array.
            /// </summary>
            /// <param name="destination">The destination array.</param>
            /// <param name="index">The starting index of the copy operation.</param>
            void ICollection.CopyTo
                (Array destination,
                 int index)
                {
                List.CopyTo(destination, index);
                }

            /// <inheritdoc />
            public IEnumerator GetEnumerator()
                {
                return List.GetEnumerator();
                }

            /// <inheritdoc />
            public int IndexOf
                (object value)
                {
                if (value == null)
                    throw new ArgumentException(nameof(value));
                return List.IndexOf(value);
                }

            /// <summary>
            ///     Inserts an object at the specified index.
            /// </summary>
            /// <param name="index">The index.</param>
            /// <param name="item">The item to add.</param>
            /// <exception cref="System.ArgumentNullException"><paramref name="item" /> was null.</exception>
            /// <exception cref="System.ArgumentOutOfRangeException"><paramref name="index" /> was out of range.</exception>
            public void Insert
                (int index,
                 object item)
                {
                if (item == null)
                    throw new ArgumentNullException(nameof(item));
                if (index < 0 || index > List.Count)
                    throw new ArgumentOutOfRangeException(
                        nameof(index));
                List.Insert(index, item);
                if (_owner.IsHandleCreated)
                    _owner.Insert(index, item);
                }

            /// <inheritdoc />
            public void RemoveAt
                (int index)
                {
                if (index < 0 || index >= List.Count)
                    throw new ArgumentOutOfRangeException(
                        nameof(index));
                List.RemoveAt(index);
                if (_owner.IsHandleCreated) _owner.RemoveAt(index);
                }

            /// <inheritdoc />
            public void Remove
                (object value)
                {
                var index = List.IndexOf(value);
                if (index != -1) RemoveAt(index);
                }

            /// <summary>
            ///     Adds the specified item to the collection.
            /// </summary>
            /// <param name="item">The item to add.</param>
            /// <returns>The item's index in the collection.</returns>
            public int Add
                (object item)
                {
                return AddInternal(item);
                }

            /// <exclude />
            /// Internal add method.
            private int AddInternal
                (object item)
                {
                if (item == null)
                    throw new ArgumentNullException(nameof(item));
                List.Add(item);
                //if (_owner.IsHandleCreated)
                SendObjectAdded(item);
                return List.Count - 1;
                }

            /// <summary>
            ///     Adds the specified range of items to the collection.
            /// </summary>
            /// <param name="items">The items to add.</param>
            /// <exception cref="System.ArgumentNullException"><c>items</c> was null.</exception>
            public void AddRange
                (object[] items)
                {
                if (items == null)
                    throw new ArgumentNullException(nameof(items));
                foreach (var item in items) AddInternal(item);
                }

            /// <exclude />
            /// Internal set method.
            private void SetItemInternal
                (int index,
                 object value)
                {
                if (index < 0 || index >= List.Count)
                    throw new ArgumentOutOfRangeException(
                        nameof(index));
                List[index] =
                    value
                 ?? throw new ArgumentNullException(nameof(value));
                if (_owner.IsHandleCreated)
                    _owner.SetItem(index, value);
                }

            /// <summary>
            ///     Copies the contents of the collection to the provided array starting at the
            ///     specified index.
            /// </summary>
            /// <param name="destination">The destination array.</param>
            /// <param name="arrayIndex">The starting index of the copy operation.</param>
            public void CopyTo
                (object[] destination,
                 int arrayIndex)
                {
                List.CopyTo(destination, arrayIndex);
                }

            /// <summary>
            /// Invokes the OnObjectAdded event.
            /// </summary>
            /// <param name="obj">The new object.</param>
            protected virtual void SendObjectAdded
                (object obj)
                {
                ObjectAdded?.Invoke(obj);
                }
        }
    }

    /// <summary>
    ///     Specifies arrangements of <see cref="RadioButton" /> objects within a
    ///     <see cref="RadioButtonGroup" />.
    /// </summary>
    public enum RadioButtonLayout
    {
        /// <summary>
        ///     Vertical arrangement.
        /// </summary>
        Vertical,

        /// <summary>
        ///     Horizontal arrangement.
        /// </summary>
        Horizontal
    }
}