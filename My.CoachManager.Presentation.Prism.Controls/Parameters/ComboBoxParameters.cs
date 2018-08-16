using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using My.CoachManager.Presentation.Prism.Controls.Helpers;

namespace My.CoachManager.Presentation.Prism.Controls.Parameters
{
    /// <summary>
    /// A helper class that provides various attached properties for the ComboBox control.
    /// <see cref="ComboBox"/>
    /// </summary>
    public class ComboBoxParameters
    {
        public static readonly DependencyProperty EnableVirtualizationWithGroupingProperty = DependencyProperty.RegisterAttached("EnableVirtualizationWithGrouping", typeof(bool), typeof(ComboBoxParameters), new FrameworkPropertyMetadata(false, EnableVirtualizationWithGroupingPropertyChangedCallback));

        private static void EnableVirtualizationWithGroupingPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (dependencyObject is ComboBox comboBox && e.NewValue != e.OldValue)
            {
                comboBox.SetValue(VirtualizingStackPanel.IsVirtualizingProperty, e.NewValue);
                comboBox.SetValue(VirtualizingPanel.IsVirtualizingWhenGroupingProperty, e.NewValue);
                comboBox.SetValue(ScrollViewer.CanContentScrollProperty, e.NewValue);
            }
        }

        public static void SetEnableVirtualizationWithGrouping(DependencyObject obj, bool value)
        {
            obj.SetValue(EnableVirtualizationWithGroupingProperty, value);
        }

        [Category(Constants.ParameterCategory)]
        public static bool GetEnableVirtualizationWithGrouping(DependencyObject obj)
        {
            return (bool)obj.GetValue(EnableVirtualizationWithGroupingProperty);
        }

        public static readonly DependencyProperty MaxLengthProperty = DependencyProperty.RegisterAttached("MaxLength", typeof(int), typeof(ComboBoxParameters), new FrameworkPropertyMetadata(0), MaxLengthValidateValue);

        private static bool MaxLengthValidateValue(object value)
        {
            return ((int)value) >= 0;
        }

        /// <summary>
        /// Gets the Maximum number of characters the TextBox can accept.
        /// </summary>
        [Category(Constants.ParameterCategory)]
        [AttachedPropertyBrowsableForType(typeof(ComboBox))]
        public static int GetMaxLength(UIElement obj)
        {
            return (int)obj.GetValue(MaxLengthProperty);
        }

        /// <summary>
        /// Sets the Maximum number of characters the TextBox can accept.
        /// </summary>
        public static void SetMaxLength(UIElement obj, int value)
        {
            obj.SetValue(MaxLengthProperty, value);
        }

        public static readonly DependencyProperty CharacterCasingProperty = DependencyProperty.RegisterAttached("CharacterCasing", typeof(CharacterCasing), typeof(ComboBoxParameters), new FrameworkPropertyMetadata(CharacterCasing.Normal), CharacterCasingValidateValue);

        private static bool CharacterCasingValidateValue(object value)
        {
            return (CharacterCasing.Normal <= (CharacterCasing)value && (CharacterCasing)value <= CharacterCasing.Upper);
        }

        /// <summary>
        /// Gets the Character casing of the TextBox.
        /// </summary>
        [Category(Constants.ParameterCategory)]
        [AttachedPropertyBrowsableForType(typeof(ComboBox))]
        public static CharacterCasing GetCharacterCasing(UIElement obj)
        {
            return (CharacterCasing)obj.GetValue(CharacterCasingProperty);
        }

        /// <summary>
        /// Sets the Character casing of the TextBox.
        /// </summary>
        public static void SetCharacterCasing(UIElement obj, CharacterCasing value)
        {
            obj.SetValue(CharacterCasingProperty, value);
        }

        #region DropDownButtonVisible Property

        [AttachedPropertyBrowsableForType(typeof(ComboBox))]
        public static bool GetDropDownButtonVisible(ComboBox obj)
        {
            return (bool)obj.GetValue(DropDownButtonVisibleProperty);
        }

        public static void SetDropDownButtonVisible(ComboBox obj, bool value)
        {
            obj.SetValue(DropDownButtonVisibleProperty, value);
        }

        public static readonly DependencyProperty DropDownButtonVisibleProperty =
            DependencyProperty.RegisterAttached(
                "DropDownButtonVisible",
                typeof(bool),
                typeof(ComboBoxParameters),
                new FrameworkPropertyMetadata(null)
                );

        #endregion DropDownButtonVisible Property
    }
}