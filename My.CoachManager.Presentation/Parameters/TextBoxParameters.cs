using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace My.CoachManager.Presentation.Parameters
{
    public static class TextBoxParameters
    {
        #region Mask Property

        [AttachedPropertyBrowsableForType(typeof(TextBox))]
        public static MaskType GetMask(DependencyObject obj)
        {
            return (MaskType)obj.GetValue(MaskProperty);
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

        private static void TextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
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