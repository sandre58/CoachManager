using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using My.CoachManager.Presentation.Prism.Controls.Buttons;
using My.CoachManager.Presentation.Prism.Controls.Extensions;
using My.CoachManager.Presentation.Prism.Controls.Helpers;
using My.CoachManager.Presentation.Prism.Controls.TimePickers;

namespace My.CoachManager.Presentation.Prism.Controls.Parameters
{
    public static class TextBoxParameters
    {
        #region Mask Property

        [AttachedPropertyBrowsableForType(typeof(TextBox))]
        public static MaskType GetMask(DependencyObject obj)
        {
            var value = obj.GetValue(MaskProperty);
            if (value != null) return (MaskType)value;
            return default(MaskType);
        }

        public static void SetMask(DependencyObject obj, MaskType value)
        {
            obj.SetValue(MaskProperty, value);
        }

        public static readonly DependencyProperty MaskProperty =
            DependencyProperty.RegisterAttached(
                "Mask",
                typeof(MaskType),
                typeof(TextBoxParameters),
                new FrameworkPropertyMetadata(MaskChangedCallback)
                );

        private static void MaskChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var box = e.OldValue as TextBox;
            if (box != null)
            {
                box.PreviewTextInput -= TextBox_PreviewTextInput;
                DataObject.RemovePastingHandler(box, TextBoxPastingEventHandler);
            }

            var _this = (d as TextBox);
            if (_this == null)
                return;

            if ((MaskType)e.NewValue != MaskType.Any)
            {
                _this.PreviewTextInput += TextBox_PreviewTextInput;
                DataObject.AddPastingHandler(_this, TextBoxPastingEventHandler);
            }

            ValidateTextBox(_this);
        }

        #endregion Mask Property

        #region Private Static Methods

        private static void ValidateTextBox(TextBox _this)
        {
            if (GetMask(_this) != MaskType.Any)
            {
                _this.Text = ValidateValue(GetMask(_this), _this.Text);
            }
        }

        private static void TextBoxPastingEventHandler(object sender, DataObjectPastingEventArgs e)
        {
            var _this = (sender as TextBox);
            var clipboard = e.DataObject.GetData(typeof(string)) as string;
            clipboard = ValidateValue(GetMask(_this), clipboard);
            if (!string.IsNullOrEmpty(clipboard))
            {
                if (_this != null) _this.Text = clipboard;
            }
            e.CancelCommand();
            e.Handled = true;
        }

        private static void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var _this = (sender as TextBox);
            var isValid = IsSymbolValid(GetMask(_this), e.Text);
            e.Handled = !isValid;
            //if (isValid)
            //{
            //    if (_this != null)
            //    {
            //        var caret = _this.CaretIndex;
            //        var text = _this.Text;
            //        var textInserted = false;
            //        var selectionLength = 0;

            //        if (_this.SelectionLength > 0)
            //        {
            //            text = text.Substring(0, _this.SelectionStart) +
            //                   text.Substring(_this.SelectionStart + _this.SelectionLength);
            //            caret = _this.SelectionStart;
            //        }

            //        if (e.Text == NumberFormatInfo.CurrentInfo.NumberDecimalSeparator)
            //        {
            //            while (true)
            //            {
            //                var ind = text.IndexOf(NumberFormatInfo.CurrentInfo.NumberDecimalSeparator, StringComparison.Ordinal);
            //                if (ind == -1)
            //                    break;

            //                text = text.Substring(0, ind) + text.Substring(ind + 1);
            //                if (caret > ind)
            //                    caret--;
            //            }

            //            if (caret == 0)
            //            {
            //                text = "0" + text;
            //                caret++;
            //            }
            //            else
            //            {
            //                if (caret == 1 && string.Empty + text[0] == NumberFormatInfo.CurrentInfo.NegativeSign)
            //                {
            //                    text = NumberFormatInfo.CurrentInfo.NegativeSign + "0" + text.Substring(1);
            //                    caret++;
            //                }
            //            }

            //            if (caret == text.Length)
            //            {
            //                selectionLength = 1;
            //                textInserted = true;
            //                text = text + NumberFormatInfo.CurrentInfo.NumberDecimalSeparator + "0";
            //                caret++;
            //            }
            //        }
            //        else if (e.Text == NumberFormatInfo.CurrentInfo.NegativeSign)
            //        {
            //            textInserted = true;
            //            if (_this.Text.Contains(NumberFormatInfo.CurrentInfo.NegativeSign))
            //            {
            //                text = text.Replace(NumberFormatInfo.CurrentInfo.NegativeSign, string.Empty);
            //                if (caret != 0)
            //                    caret--;
            //            }
            //            else
            //            {
            //                text = NumberFormatInfo.CurrentInfo.NegativeSign + _this.Text;
            //                caret++;
            //            }
            //        }

            //        if (!textInserted)
            //        {
            //            text = text.Substring(0, caret) + e.Text +
            //                   ((caret < _this.Text.Length) ? text.Substring(caret) : string.Empty);

            //            caret++;
            //        }

            //        while (text.Length > 1 && text[0] == '0' && string.Empty + text[1] != NumberFormatInfo.CurrentInfo.NumberDecimalSeparator)
            //        {
            //            text = text.Substring(1);
            //            if (caret > 0)
            //                caret--;
            //        }

            //        while (text.Length > 2 && string.Empty + text[0] == NumberFormatInfo.CurrentInfo.NegativeSign && text[1] == '0' && string.Empty + text[2] != NumberFormatInfo.CurrentInfo.NumberDecimalSeparator)
            //        {
            //            text = NumberFormatInfo.CurrentInfo.NegativeSign + text.Substring(2);
            //            if (caret > 1)
            //                caret--;
            //        }

            //        if (caret > text.Length)
            //            caret = text.Length;

            //        _this.Text = text;
            //        _this.CaretIndex = caret;
            //        _this.SelectionStart = caret;
            //        _this.SelectionLength = selectionLength;
            //    }
            //    e.Handled = true;
            //}
        }

        private static string ValidateValue(MaskType mask, string value)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            value = value.Trim();
            switch (mask)
            {
                case MaskType.Integer:
                case MaskType.PositiveInteger:
                    int res;
                    return int.TryParse(value, out res) ? value : string.Empty;

                case MaskType.Decimal:
                case MaskType.PositiveDecimal:
                    double res1;
                    return double.TryParse(value, out res1) ? value : string.Empty;

                default:
                    return value;
            }
        }

        private static bool IsSymbolValid(MaskType mask, string str)
        {
            switch (mask)
            {
                case MaskType.Any:
                    return true;

                case MaskType.Integer:
                    if (str == NumberFormatInfo.CurrentInfo.NegativeSign)
                        return true;
                    break;

                case MaskType.Decimal:
                    if (str == NumberFormatInfo.CurrentInfo.NumberDecimalSeparator ||
                        str == NumberFormatInfo.CurrentInfo.NegativeSign)
                        return true;
                    break;

                case MaskType.PositiveDecimal:
                    if (str == NumberFormatInfo.CurrentInfo.NumberDecimalSeparator)
                        return true;
                    break;
            }

            if (mask.Equals(MaskType.Integer) || mask.Equals(MaskType.Decimal) || mask.Equals(MaskType.PositiveInteger) || mask.Equals(MaskType.PositiveDecimal))
            {
                return str.All(char.IsDigit);
            }

            return false;
        }

        #endregion Private Static Methods

        public static readonly DependencyProperty IsMonitoringProperty = DependencyProperty.RegisterAttached("IsMonitoring", typeof(bool), typeof(TextBoxParameters), new UIPropertyMetadata(false, OnIsMonitoringChanged));
        public static readonly DependencyProperty WatermarkProperty = DependencyProperty.RegisterAttached("Watermark", typeof(string), typeof(TextBoxParameters), new UIPropertyMetadata(string.Empty));
        public static readonly DependencyProperty UseFloatingWatermarkProperty = DependencyProperty.RegisterAttached("UseFloatingWatermark", typeof(bool), typeof(TextBoxParameters), new FrameworkPropertyMetadata(false, ButtonCommandOrClearTextChanged));
        public static readonly DependencyProperty TextLengthProperty = DependencyProperty.RegisterAttached("TextLength", typeof(int), typeof(TextBoxParameters), new UIPropertyMetadata(0));
        public static readonly DependencyProperty ClearTextButtonProperty = DependencyProperty.RegisterAttached("ClearTextButton", typeof(bool), typeof(TextBoxParameters), new FrameworkPropertyMetadata(false, ButtonCommandOrClearTextChanged));
        public static readonly DependencyProperty TextButtonProperty = DependencyProperty.RegisterAttached("TextButton", typeof(bool), typeof(TextBoxParameters), new FrameworkPropertyMetadata(false, ButtonCommandOrClearTextChanged));
        public static readonly DependencyProperty ButtonsAlignmentProperty = DependencyProperty.RegisterAttached("ButtonsAlignment", typeof(ButtonsAlignment), typeof(TextBoxParameters), new FrameworkPropertyMetadata(ButtonsAlignment.Right, FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// The clear text button behavior property. It sets a click event to the button if the value is true.
        /// </summary>
        public static readonly DependencyProperty IsClearTextButtonBehaviorEnabledProperty = DependencyProperty.RegisterAttached("IsClearTextButtonBehaviorEnabled", typeof(bool), typeof(TextBoxParameters), new FrameworkPropertyMetadata(false, IsClearTextButtonBehaviorEnabledChanged));

        /// <summary>
        /// This property can be used to set the button width (PART_ClearText) of TextBox, PasswordBox, ComboBox, ExtendedNumericUpDown
        /// </summary>
        public static readonly DependencyProperty ButtonWidthProperty = DependencyProperty.RegisterAttached("ButtonWidth", typeof(double), typeof(TextBoxParameters), new FrameworkPropertyMetadata(22d, FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.Inherits));

        public static readonly DependencyProperty ButtonCommandProperty = DependencyProperty.RegisterAttached("ButtonCommand", typeof(ICommand), typeof(TextBoxParameters), new FrameworkPropertyMetadata(null, ButtonCommandOrClearTextChanged));
        public static readonly DependencyProperty ButtonCommandParameterProperty = DependencyProperty.RegisterAttached("ButtonCommandParameter", typeof(object), typeof(TextBoxParameters), new FrameworkPropertyMetadata(null));
        public static readonly DependencyProperty ButtonContentProperty = DependencyProperty.RegisterAttached("ButtonContent", typeof(object), typeof(TextBoxParameters), new FrameworkPropertyMetadata("r"));
        public static readonly DependencyProperty ButtonContentTemplateProperty = DependencyProperty.RegisterAttached("ButtonContentTemplate", typeof(DataTemplate), typeof(TextBoxParameters), new FrameworkPropertyMetadata((DataTemplate)null));
        public static readonly DependencyProperty ButtonTemplateProperty = DependencyProperty.RegisterAttached("ButtonTemplate", typeof(ControlTemplate), typeof(TextBoxParameters), new FrameworkPropertyMetadata(null));
        public static readonly DependencyProperty ButtonFontFamilyProperty = DependencyProperty.RegisterAttached("ButtonFontFamily", typeof(FontFamily), typeof(TextBoxParameters), new FrameworkPropertyMetadata(new FontFamilyConverter().ConvertFromString("Marlett")));
        public static readonly DependencyProperty ButtonFontSizeProperty = DependencyProperty.RegisterAttached("ButtonFontSize", typeof(double), typeof(TextBoxParameters), new FrameworkPropertyMetadata(SystemFonts.MessageFontSize));

        public static readonly DependencyProperty SelectAllOnFocusProperty = DependencyProperty.RegisterAttached("SelectAllOnFocus", typeof(bool), typeof(TextBoxParameters), new FrameworkPropertyMetadata(false));
        public static readonly DependencyProperty IsWaitingForDataProperty = DependencyProperty.RegisterAttached("IsWaitingForData", typeof(bool), typeof(TextBoxParameters), new UIPropertyMetadata(false));

        public static readonly DependencyProperty HasTextProperty = DependencyProperty.RegisterAttached("HasText", typeof(bool), typeof(TextBoxParameters), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// This property can be used to retrieve the watermark using the <see cref="DisplayAttribute"/> of bound property.
        /// </summary>
        /// <remarks>
        /// Setting this property to true will uses reflection.
        /// </remarks>
        public static readonly DependencyProperty AutoWatermarkProperty = DependencyProperty.RegisterAttached("AutoWatermark", typeof(bool), typeof(TextBoxParameters), new PropertyMetadata(default(bool), OnAutoWatermarkChanged));

        private static readonly Dictionary<Type, DependencyProperty> AutoWatermarkPropertyMapping = new Dictionary<Type, DependencyProperty>
                                                                                                    {
                                                                                                        { typeof(TextBox), TextBox.TextProperty },
                                                                                                        { typeof(ComboBox), Selector.SelectedItemProperty },
                                                                                                        { typeof(ExtendedNumericUpDown), ExtendedNumericUpDown.ValueProperty },
                                                                                                        { typeof(DatePicker), DatePicker.SelectedDateProperty },
                                                                                                        { typeof(ExtendedTimePicker), TimePickerBase.SelectedTimeProperty },
                                                                                                        { typeof(ExtendedDateTimePicker), ExtendedDateTimePicker.SelectedDateProperty }
                                                                                                    };

        [Category(Constants.ParameterCategory)]
        [AttachedPropertyBrowsableForType(typeof(TextBox))]
        [AttachedPropertyBrowsableForType(typeof(ComboBox))]
        [AttachedPropertyBrowsableForType(typeof(DatePicker))]
        [AttachedPropertyBrowsableForType(typeof(TimePickerBase))]
        [AttachedPropertyBrowsableForType(typeof(ExtendedNumericUpDown))]
        public static bool GetAutoWatermark(DependencyObject element)
        {
            var value = element.GetValue(AutoWatermarkProperty);
            return value != null && (bool)value;
        }

        ///  <summary>
        ///  Indicates if the watermark is automatically retrieved by using the <see cref="DisplayAttribute"/> of the bound property.
        ///  </summary>
        /// <remarks>This attached property uses reflection; thus it might reduce the performance of the application.
        /// The auto-watermak does work for the following controls:
        /// In the following case no custom watermark is shown
        /// <list type="bullet">
        /// <item>There is no binding</item>
        /// <item>Binding path errors</item>
        /// <item>Binding to a element of a collection without using a property of that element <c>Binding Path=Collection[0]</c> use: <c>Binding Path=Collection[0].SubProperty</c></item>
        /// <item>The bound property does not have a <see cref="DisplayAttribute"/></item>
        /// </list></remarks>
        public static void SetAutoWatermark(DependencyObject element, bool value)
        {
            element.SetValue(AutoWatermarkProperty, value);
        }

        private static void OnAutoWatermarkChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement element = d as FrameworkElement;
            bool? enable = e.NewValue as bool?;
            if (element != null)
            {
                if (enable.GetValueOrDefault())
                {
                    if (element.IsLoaded)
                    {
                        OnControlWithAutoWatermarkSupportLoaded(element, new RoutedEventArgs());
                    }
                    else
                    {
                        element.Loaded += OnControlWithAutoWatermarkSupportLoaded;
                    }
                }
                else
                {
                    element.Loaded -= OnControlWithAutoWatermarkSupportLoaded;
                }
            }
        }

        private static void OnControlWithAutoWatermarkSupportLoaded(object o, RoutedEventArgs routedEventArgs)
        {
            FrameworkElement obj = (FrameworkElement)o;
            obj.Loaded -= OnControlWithAutoWatermarkSupportLoaded;

            DependencyProperty dependencyProperty;

            if (!AutoWatermarkPropertyMapping.TryGetValue(obj.GetType(), out dependencyProperty))
            {
                throw new NotSupportedException("AutoWatermarkProperty is not supported for {obj.GetType()}");
            }

            var resolvedProperty = ResolvePropertyFromBindingExpression(obj.GetBindingExpression(dependencyProperty));
            if (resolvedProperty != null)
            {
                var attribute = resolvedProperty.GetCustomAttribute<DisplayAttribute>();

                if (attribute != null)
                {
                    obj.SetValue(WatermarkProperty, attribute.GetPrompt());
                }
            }
        }

        private static PropertyInfo ResolvePropertyFromBindingExpression(BindingExpression bindingExpression)
        {
            if (bindingExpression != null)
            {
                if (bindingExpression.Status == BindingStatus.PathError)
                {
                    return null;
                }

                var propertyName = bindingExpression.ResolvedSourcePropertyName;

                if (!string.IsNullOrEmpty(propertyName))
                {
                    var resolvedType = bindingExpression.ResolvedSource.GetType();

                    return resolvedType.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
                }
            }
            return null;
        }

        public static void SetIsWaitingForData(DependencyObject obj, bool value)
        {
            obj.SetValue(IsWaitingForDataProperty, value);
        }

        [Category(Constants.ParameterCategory)]
        [AttachedPropertyBrowsableForType(typeof(TextBoxBase))]
        [AttachedPropertyBrowsableForType(typeof(PasswordBox))]
        public static bool GetIsWaitingForData(DependencyObject obj)
        {
            var value = obj.GetValue(IsWaitingForDataProperty);
            return value != null && (bool)value;
        }

        public static void SetSelectAllOnFocus(DependencyObject obj, bool value)
        {
            obj.SetValue(SelectAllOnFocusProperty, value);
        }

        public static bool GetSelectAllOnFocus(DependencyObject obj)
        {
            var value = obj.GetValue(SelectAllOnFocusProperty);
            return value != null && (bool)value;
        }

        public static void SetIsMonitoring(DependencyObject obj, bool value)
        {
            obj.SetValue(IsMonitoringProperty, value);
        }

        [Category(Constants.ParameterCategory)]
        [AttachedPropertyBrowsableForType(typeof(TextBoxBase))]
        [AttachedPropertyBrowsableForType(typeof(PasswordBox))]
        [AttachedPropertyBrowsableForType(typeof(ComboBox))]
        [AttachedPropertyBrowsableForType(typeof(DatePicker))]
        [AttachedPropertyBrowsableForType(typeof(TimePickerBase))]
        [AttachedPropertyBrowsableForType(typeof(ExtendedNumericUpDown))]
        public static string GetWatermark(DependencyObject obj)
        {
            return (string)obj.GetValue(WatermarkProperty);
        }

        public static void SetWatermark(DependencyObject obj, string value)
        {
            obj.SetValue(WatermarkProperty, value);
        }

        [Category(Constants.ParameterCategory)]
        [AttachedPropertyBrowsableForType(typeof(TextBoxBase))]
        [AttachedPropertyBrowsableForType(typeof(PasswordBox))]
        [AttachedPropertyBrowsableForType(typeof(ComboBox))]
        [AttachedPropertyBrowsableForType(typeof(ExtendedNumericUpDown))]
        public static bool GetUseFloatingWatermark(DependencyObject obj)
        {
            var value = obj.GetValue(UseFloatingWatermarkProperty);
            return value != null && (bool)value;
        }

        public static void SetUseFloatingWatermark(DependencyObject obj, bool value)
        {
            obj.SetValue(UseFloatingWatermarkProperty, value);
        }

        /// <summary>
        /// Gets if the attached TextBox has text.
        /// </summary>
        [Category(Constants.ParameterCategory)]
        [AttachedPropertyBrowsableForType(typeof(TextBoxBase))]
        [AttachedPropertyBrowsableForType(typeof(PasswordBox))]
        [AttachedPropertyBrowsableForType(typeof(ComboBox))]
        [AttachedPropertyBrowsableForType(typeof(DatePicker))]
        [AttachedPropertyBrowsableForType(typeof(ExtendedNumericUpDown))]
        public static bool GetHasText(DependencyObject obj)
        {
            var value = obj.GetValue(HasTextProperty);
            return value != null && (bool)value;
        }

        public static void SetHasText(DependencyObject obj, bool value)
        {
            obj.SetValue(HasTextProperty, value);
        }

        private static void OnIsMonitoringChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TextBox)
            {
                var txtBox = (TextBox)d;
                if ((bool)e.NewValue)
                {
                    // Fixes #1343 and #2514: also triggers the show of the floating watermark if necessary
                    txtBox.BeginInvoke(() => TextChanged(txtBox, new TextChangedEventArgs(TextBoxBase.TextChangedEvent, UndoAction.None)));

                    txtBox.TextChanged += TextChanged;
                    txtBox.GotFocus += TextBoxGotFocus;
                }
                else
                {
                    txtBox.TextChanged -= TextChanged;
                    txtBox.GotFocus -= TextBoxGotFocus;
                }
            }
            else if (d is PasswordBox)
            {
                var passBox = (PasswordBox)d;
                if ((bool)e.NewValue)
                {
                    // Fixes #1343 and #2514: also triggers the show of the floating watermark if necessary
                    passBox.BeginInvoke(() => PasswordChanged(passBox, new RoutedEventArgs(PasswordBox.PasswordChangedEvent, passBox)));

                    passBox.PasswordChanged += PasswordChanged;
                    passBox.GotFocus += PasswordGotFocus;
                }
                else
                {
                    passBox.PasswordChanged -= PasswordChanged;
                    passBox.GotFocus -= PasswordGotFocus;
                }
            }
            else if (d is ExtendedNumericUpDown)
            {
                var numericUpDown = (ExtendedNumericUpDown)d;
                if ((bool)e.NewValue)
                {
                    // Fixes #1343 and #2514: also triggers the show of the floating watermark if necessary
                    numericUpDown.BeginInvoke(() => OnExtendedNumericUpDownValueChaged(numericUpDown, new RoutedEventArgs(ExtendedNumericUpDown.ValueChangedEvent, numericUpDown)));

                    numericUpDown.ValueChanged += OnExtendedNumericUpDownValueChaged;
                    //ExtendedNumericUpDown.GotFocus += ExtendedNumericUpDownGotFocus;
                }
                else
                {
                    numericUpDown.ValueChanged -= OnExtendedNumericUpDownValueChaged;
                    //ExtendedNumericUpDown.GotFocus -= ExtendedNumericUpDownGotFocus;
                }
            }
            else if (d is TimePickerBase)
            {
                var timePicker = (TimePickerBase)d;
                if ((bool)e.NewValue)
                {
                    timePicker.SelectedTimeChanged += OnTimePickerBaseSelectedTimeChanged;
                }
                else
                {
                    timePicker.SelectedTimeChanged -= OnTimePickerBaseSelectedTimeChanged;
                }
            }
            else if (d is DatePicker)
            {
                var timePicker = (DatePicker)d;
                if ((bool)e.NewValue)
                {
                    timePicker.SelectedDateChanged += OnDatePickerBaseSelectedDateChanged;
                }
                else
                {
                    timePicker.SelectedDateChanged -= OnDatePickerBaseSelectedDateChanged;
                }
            }
        }

        private static void SetTextLength<TDependencyObject>(TDependencyObject sender, Func<TDependencyObject, int> funcTextLength) where TDependencyObject : DependencyObject
        {
            if (sender != null)
            {
                var value = funcTextLength(sender);
                sender.SetValue(TextLengthProperty, value);
                sender.SetValue(HasTextProperty, value >= 1);
            }
        }

        private static void TextChanged(object sender, RoutedEventArgs e)
        {
            SetTextLength(sender as TextBox, textBox => textBox.Text.Length);
        }

        private static void OnExtendedNumericUpDownValueChaged(object sender, RoutedEventArgs e)
        {
            SetTextLength(sender as ExtendedNumericUpDown, numericUpDown => numericUpDown.Value.HasValue ? 1 : 0);
        }

        private static void PasswordChanged(object sender, RoutedEventArgs e)
        {
            SetTextLength(sender as PasswordBox, passwordBox => passwordBox.Password.Length);
        }

        private static void OnDatePickerBaseSelectedDateChanged(object sender, RoutedEventArgs e)
        {
            SetTextLength(sender as DatePicker, timePickerBase => timePickerBase.SelectedDate.HasValue ? 1 : 0);
        }

        private static void OnTimePickerBaseSelectedTimeChanged(object sender, RoutedEventArgs e)
        {
            SetTextLength(sender as TimePickerBase, timePickerBase => timePickerBase.SelectedTime.HasValue ? 1 : 0);
        }

        private static void TextBoxGotFocus(object sender, RoutedEventArgs e)
        {
            ControlGotFocus(sender as TextBox, textBox => textBox.SelectAll());
        }

        private static void PasswordGotFocus(object sender, RoutedEventArgs e)
        {
            ControlGotFocus(sender as PasswordBox, passwordBox => passwordBox.SelectAll());
        }

        private static void ControlGotFocus<TDependencyObject>(TDependencyObject sender, Action<TDependencyObject> action) where TDependencyObject : DependencyObject
        {
            if (sender != null)
            {
                if (GetSelectAllOnFocus(sender))
                {
                    sender.Dispatcher.BeginInvoke(action, sender);
                }
            }
        }

        /// <summary>
        /// Gets the clear text button visibility / feature. Can be used to enable text deletion.
        /// </summary>
        [Category(Constants.ParameterCategory)]
        public static bool GetClearTextButton(DependencyObject d)
        {
            var value = d.GetValue(ClearTextButtonProperty);
            return value != null && (bool)value;
        }

        /// <summary>
        /// Sets the clear text button visibility / feature. Can be used to enable text deletion.
        /// </summary>
        public static void SetClearTextButton(DependencyObject obj, bool value)
        {
            obj.SetValue(ClearTextButtonProperty, value);
        }

        /// <summary>
        /// Gets the text button visibility.
        /// </summary>
        [Category(Constants.ParameterCategory)]
        public static bool GetTextButton(DependencyObject d)
        {
            var value = d.GetValue(TextButtonProperty);
            return value != null && (bool)value;
        }

        /// <summary>
        /// Sets the text button visibility.
        /// </summary>
        public static void SetTextButton(DependencyObject obj, bool value)
        {
            obj.SetValue(TextButtonProperty, value);
        }

        /// <summary>
        /// Gets the buttons placement variant.
        /// </summary>
        [Category(Constants.ParameterCategory)]
        public static ButtonsAlignment GetButtonsAlignment(DependencyObject d)
        {
            var value = d.GetValue(ButtonsAlignmentProperty);
            if (value != null)
                return (ButtonsAlignment)value;
            return default(ButtonsAlignment);
        }

        /// <summary>
        /// Sets the buttons placement variant.
        /// </summary>
        public static void SetButtonsAlignment(DependencyObject obj, ButtonsAlignment value)
        {
            obj.SetValue(ButtonsAlignmentProperty, value);
        }

        /// <summary>
        /// Gets the clear text button behavior.
        /// </summary>
        [Category(Constants.ParameterCategory)]
        [AttachedPropertyBrowsableForType(typeof(ButtonBase))]
        public static bool GetIsClearTextButtonBehaviorEnabled(Button d)
        {
            var value = d.GetValue(IsClearTextButtonBehaviorEnabledProperty);
            return value != null && (bool)value;
        }

        /// <summary>
        /// Sets the clear text button behavior.
        /// </summary>
        [AttachedPropertyBrowsableForType(typeof(ButtonBase))]
        public static void SetIsClearTextButtonBehaviorEnabled(Button obj, bool value)
        {
            obj.SetValue(IsClearTextButtonBehaviorEnabledProperty, value);
        }

        [Category(Constants.ParameterCategory)]
        public static double GetButtonWidth(DependencyObject obj)
        {
            var value = obj.GetValue(ButtonWidthProperty);
            if (value != null) return (double)value;
            return 0;
        }

        public static void SetButtonWidth(DependencyObject obj, double value)
        {
            obj.SetValue(ButtonWidthProperty, value);
        }

        [Category(Constants.ParameterCategory)]
        public static ICommand GetButtonCommand(DependencyObject d)
        {
            return (ICommand)d.GetValue(ButtonCommandProperty);
        }

        public static void SetButtonCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(ButtonCommandProperty, value);
        }

        [Category(Constants.ParameterCategory)]
        public static object GetButtonCommandParameter(DependencyObject d)
        {
            return d.GetValue(ButtonCommandParameterProperty);
        }

        public static void SetButtonCommandParameter(DependencyObject obj, object value)
        {
            obj.SetValue(ButtonCommandParameterProperty, value);
        }

        [Category(Constants.ParameterCategory)]
        public static object GetButtonContent(DependencyObject d)
        {
            return d.GetValue(ButtonContentProperty);
        }

        public static void SetButtonContent(DependencyObject obj, object value)
        {
            obj.SetValue(ButtonContentProperty, value);
        }

        /// <summary>
        /// ButtonContentTemplate is the template used to display the content of the ClearText button.
        /// </summary>
        [Category(Constants.ParameterCategory)]
        public static DataTemplate GetButtonContentTemplate(DependencyObject d)
        {
            return (DataTemplate)d.GetValue(ButtonContentTemplateProperty);
        }

        public static void SetButtonContentTemplate(DependencyObject obj, DataTemplate value)
        {
            obj.SetValue(ButtonContentTemplateProperty, value);
        }

        [Category(Constants.ParameterCategory)]
        public static ControlTemplate GetButtonTemplate(DependencyObject d)
        {
            return (ControlTemplate)d.GetValue(ButtonTemplateProperty);
        }

        public static void SetButtonTemplate(DependencyObject obj, ControlTemplate value)
        {
            obj.SetValue(ButtonTemplateProperty, value);
        }

        [Category(Constants.ParameterCategory)]
        public static FontFamily GetButtonFontFamily(DependencyObject d)
        {
            return (FontFamily)d.GetValue(ButtonFontFamilyProperty);
        }

        public static void SetButtonFontFamily(DependencyObject obj, FontFamily value)
        {
            obj.SetValue(ButtonFontFamilyProperty, value);
        }

        [Category(Constants.ParameterCategory)]
        public static double GetButtonFontSize(DependencyObject d)
        {
            var value = d.GetValue(ButtonFontSizeProperty);
            if (value != null) return (double)value;
            return 0;
        }

        public static void SetButtonFontSize(DependencyObject obj, double value)
        {
            obj.SetValue(ButtonFontSizeProperty, value);
        }

        private static void IsClearTextButtonBehaviorEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var button = d as Button;
            if (e.OldValue != e.NewValue && button != null)
            {
                button.Click -= ButtonClicked;
                if ((bool)e.NewValue)
                {
                    button.Click += ButtonClicked;
                }
            }
        }

        public static void ButtonClicked(object sender, RoutedEventArgs e)
        {
            var button = ((Button)sender);
            var parent = VisualTreeHelper.GetParent(button);
            while (!(parent is TextBox || parent is PasswordBox || parent is ComboBox))
            {
                if (parent != null) parent = VisualTreeHelper.GetParent(parent);
            }

            var command = GetButtonCommand(parent);
            var commandParameter = GetButtonCommandParameter(parent) ?? parent;
            if (command != null && command.CanExecute(commandParameter))
            {
                command.Execute(commandParameter);
            }

            if (GetClearTextButton(parent))
            {
                if (parent is TextBox)
                {
                    ((TextBox)parent).Clear();
                }
                else if (parent is PasswordBox)
                {
                    ((PasswordBox)parent).Clear();
                }
                else if (parent is ComboBox)
                {
                    if (((ComboBox)parent).IsEditable)
                    {
                        ((ComboBox)parent).Text = string.Empty;
                    }
                    ((ComboBox)parent).SelectedItem = null;
                }
            }
        }

        private static void ButtonCommandOrClearTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var textbox = d as TextBox;
            if (textbox != null)
            {
                // only one loaded event
                textbox.Loaded -= TextChanged;
                textbox.Loaded += TextChanged;
                if (textbox.IsLoaded)
                {
                    TextChanged(textbox, new RoutedEventArgs());
                }
            }
            var passbox = d as PasswordBox;
            if (passbox != null)
            {
                // only one loaded event
                passbox.Loaded -= PasswordChanged;
                passbox.Loaded += PasswordChanged;
                if (passbox.IsLoaded)
                {
                    PasswordChanged(passbox, new RoutedEventArgs());
                }
            }
            var combobox = d as ComboBox;
            if (combobox != null)
            {
                // only one loaded event
                combobox.Loaded -= ComboBoxLoaded;
                combobox.Loaded += ComboBoxLoaded;
                if (combobox.IsLoaded)
                {
                    ComboBoxLoaded(combobox, new RoutedEventArgs());
                }
            }
        }

        private static void ComboBoxLoaded(object sender, RoutedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            if (comboBox != null)
            {
                comboBox.SetValue(HasTextProperty, !string.IsNullOrWhiteSpace(comboBox.Text) || comboBox.SelectedItem != null);
            }
        }
    }

    public enum MaskType
    {
        Any,
        Integer,
        PositiveInteger,
        Decimal,
        PositiveDecimal
    }
}