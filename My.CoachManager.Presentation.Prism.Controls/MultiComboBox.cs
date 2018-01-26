using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using My.CoachManager.Presentation.Prism.Controls.Helpers;
using My.CoachManager.Presentation.Prism.Core.Utilities;

namespace My.CoachManager.Presentation.Prism.Controls
{
    [TemplatePart(Name = PartPopup, Type = typeof(Popup))]
    public class MultiComboBox : SelectorControl
    {
        private const string PartPopup = "PART_Popup";

        #region Members

        private readonly ValueChangeHelper _displayMemberPathValuesChangeHelper;
        private bool _ignoreTextValueChanged;
        private Popup _popup;
        private readonly List<object> _initialValue = new List<object>();

        #endregion Members

        #region Constructors

        static MultiComboBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MultiComboBox), new FrameworkPropertyMetadata(typeof(MultiComboBox)));
        }

        public MultiComboBox()
        {
            Keyboard.AddKeyDownHandler(this, OnKeyDown);
            Mouse.AddPreviewMouseDownOutsideCapturedElementHandler(this, OnMouseDownOutsideCapturedElement);
            _displayMemberPathValuesChangeHelper = new ValueChangeHelper(OnDisplayMemberPathValuesChanged);
        }

        #endregion Constructors

        #region Properties

        #region IsEditable

        public static readonly DependencyProperty IsEditableProperty = DependencyProperty.Register("IsEditable", typeof(bool), typeof(MultiComboBox)
          , new UIPropertyMetadata(false));

        public bool IsEditable
        {
            get
            {
                return (bool)GetValue(IsEditableProperty);
            }
            set
            {
                SetValue(IsEditableProperty, value);
            }
        }

        #endregion IsEditable

        #region Text

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(MultiComboBox)
          , new UIPropertyMetadata(null, OnTextChanged));

        public string Text
        {
            get
            {
                return (string)GetValue(TextProperty);
            }
            set
            {
                SetValue(TextProperty, value);
            }
        }

        private static void OnTextChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var checkComboBox = o as MultiComboBox;
            if (checkComboBox != null)
                checkComboBox.OnTextChanged((string)e.OldValue, (string)e.NewValue);
        }

        protected virtual void OnTextChanged(string oldValue, string newValue)
        {
            if (!IsInitialized || _ignoreTextValueChanged)
                return;

            UpdateFromText();
        }

        #endregion Text

        #region IsDropDownOpen

        public static readonly DependencyProperty IsDropDownOpenProperty = DependencyProperty.Register("IsDropDownOpen", typeof(bool), typeof(MultiComboBox), new UIPropertyMetadata(false, OnIsDropDownOpenChanged));

        public bool IsDropDownOpen
        {
            get
            {
                return (bool)GetValue(IsDropDownOpenProperty);
            }
            set
            {
                SetValue(IsDropDownOpenProperty, value);
            }
        }

        private static void OnIsDropDownOpenChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            MultiComboBox comboBox = o as MultiComboBox;
            if (comboBox != null)
                comboBox.OnIsDropDownOpenChanged((bool)e.OldValue, (bool)e.NewValue);
        }

        protected virtual void OnIsDropDownOpenChanged(bool oldValue, bool newValue)
        {
            if (newValue)
            {
                _initialValue.Clear();
                foreach (object o in SelectedItems)
                    _initialValue.Add(o);
            }
            else
            {
                _initialValue.Clear();
            }

            // TODO: Add your property changed side-effects. Descendants can override as well.
        }

        #endregion IsDropDownOpen

        #region MaxDropDownHeight

        public static readonly DependencyProperty MaxDropDownHeightProperty = DependencyProperty.Register("MaxDropDownHeight", typeof(double), typeof(MultiComboBox), new UIPropertyMetadata(SystemParameters.PrimaryScreenHeight / 3.0, OnMaxDropDownHeightChanged));

        public double MaxDropDownHeight
        {
            get
            {
                return (double)GetValue(MaxDropDownHeightProperty);
            }
            set
            {
                SetValue(MaxDropDownHeightProperty, value);
            }
        }

        private static void OnMaxDropDownHeightChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            MultiComboBox comboBox = o as MultiComboBox;
            if (comboBox != null)
                comboBox.OnMaxDropDownHeightChanged((double)e.OldValue, (double)e.NewValue);
        }

        protected virtual void OnMaxDropDownHeightChanged(double oldValue, double newValue)
        {
            // TODO: Add your property changed side-effects. Descendants can override as well.
        }

        #endregion MaxDropDownHeight

        #endregion Properties

        #region Base Class Overrides

        protected override void OnSelectedValueChanged(string oldValue, string newValue)
        {
            base.OnSelectedValueChanged(oldValue, newValue);
            UpdateText();
        }

        protected override void OnDisplayMemberPathChanged(string oldDisplayMemberPath, string newDisplayMemberPath)
        {
            base.OnDisplayMemberPathChanged(oldDisplayMemberPath, newDisplayMemberPath);
            UpdateDisplayMemberPathValuesBindings();
        }

        protected override void OnItemsSourceChanged(System.Collections.IEnumerable oldValue, System.Collections.IEnumerable newValue)
        {
            base.OnItemsSourceChanged(oldValue, newValue);
            UpdateDisplayMemberPathValuesBindings();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (_popup != null)
                _popup.Opened -= Popup_Opened;

            _popup = GetTemplateChild(PartPopup) as Popup;

            if (_popup != null)
                _popup.Opened += Popup_Opened;
        }

        #endregion Base Class Overrides

        #region Event Handlers

        private void OnMouseDownOutsideCapturedElement(object sender, MouseButtonEventArgs e)
        {
            CloseDropDown(true);
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (!IsDropDownOpen)
            {
                if (KeyboardUtilities.IsKeyModifyingPopupState(e))
                {
                    IsDropDownOpen = true;
                    // Popup_Opened() will Focus on ComboBoxItem.
                    e.Handled = true;
                }
            }
            else
            {
                if (KeyboardUtilities.IsKeyModifyingPopupState(e))
                {
                    CloseDropDown(true);
                    e.Handled = true;
                }
                else if (e.Key == Key.Enter)
                {
                    CloseDropDown(true);
                    e.Handled = true;
                }
                else if (e.Key == Key.Escape)
                {
                    SelectedItems.Clear();
                    foreach (object o in _initialValue)
                        SelectedItems.Add(o);
                    CloseDropDown(true);
                    e.Handled = true;
                }
            }
        }

        private void Popup_Opened(object sender, EventArgs e)
        {
            UIElement item = ItemContainerGenerator.ContainerFromItem(SelectedItem) as UIElement;
            if ((item == null) && (Items.Count > 0))
                item = ItemContainerGenerator.ContainerFromItem(Items[0]) as UIElement;
            if (item != null)
                item.Focus();
        }

        #endregion Event Handlers

        #region Methods

        private void UpdateDisplayMemberPathValuesBindings()
        {
            _displayMemberPathValuesChangeHelper.UpdateValueSource(ItemsCollection, DisplayMemberPath);
        }

        private void OnDisplayMemberPathValuesChanged()
        {
            UpdateText();
        }

        private void UpdateText()
        {
            string newValue;
            if (SelectedItems.Count == Items.Count)
                newValue = TextAll;
            else
                newValue = string.Join(Delimiter, SelectedItems.Cast<object>().Select(GetItemDisplayValue));

            if (String.IsNullOrEmpty(Text) || !Text.Equals(newValue))
            {
                _ignoreTextValueChanged = true;
                Text = newValue;
                _ignoreTextValueChanged = false;
            }
        }

        /// <summary>
        /// Updates the SelectedItems collection based on the content of
        /// the Text property.
        /// </summary>
        private void UpdateFromText()
        {
            List<string> selectedValues = null;
            if (!String.IsNullOrEmpty(Text))
            {
                selectedValues = Text.Replace(" ", string.Empty).Split(new[] { Delimiter }, StringSplitOptions.RemoveEmptyEntries).ToList();
            }

            UpdateFromList(selectedValues, GetItemDisplayValue);
        }

        protected object GetItemDisplayValue(object item)
        {
            if (!String.IsNullOrEmpty(DisplayMemberPath))
            {
                var property = item.GetType().GetProperty(DisplayMemberPath);
                if (property != null)
                    return property.GetValue(item, null);
            }

            return item;
        }

        private void CloseDropDown(bool isFocusOnComboBox)
        {
            if (IsDropDownOpen)
                IsDropDownOpen = false;
            ReleaseMouseCapture();

            if (isFocusOnComboBox)
                Focus();
        }

        #endregion Methods
    }
}