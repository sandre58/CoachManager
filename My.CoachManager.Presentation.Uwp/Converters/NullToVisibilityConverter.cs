using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace My.CoachManager.Presentation.Uwp.Converters
{
    public class NullToVisibilityConverter : IValueConverter
    {
        #region Public Methods and Operators

        public object Convert(object value, Type targetType, object parameter, string language)
        {
                return !string.IsNullOrEmpty(value?.ToString()) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

        #endregion Public Methods and Operators
    }
}