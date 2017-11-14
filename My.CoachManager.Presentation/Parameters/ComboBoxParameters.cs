using System.Windows;
using System.Windows.Controls;

namespace My.CoachManager.Presentation.Parameters
{
    public static class ComboBoxParameters
    {
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