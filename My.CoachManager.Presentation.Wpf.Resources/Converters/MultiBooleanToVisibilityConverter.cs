using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace My.CoachManager.Presentation.Wpf.Resources.Converters
{
    /// <summary>
    /// Converts boolean to visibility values.
    /// </summary>
    public class MultiBooleanToVisibilityConverter
        : IMultiValueConverter
    {
        public object Convert(object[] values,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            var visible = true;
            foreach (var value in values)
                if (value is bool b)
                    visible = visible && b;

            return visible ? Visibility.Visible : Visibility.Hidden;
        }

        public object[] ConvertBack(object value,
            Type[] targetTypes,
            object parameter,
            CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}