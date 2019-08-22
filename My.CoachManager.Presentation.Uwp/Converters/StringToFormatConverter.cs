using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace My.CoachManager.Presentation.Uwp.Converters
{
    public class StringToFormatConverter : IValueConverter
    {
        #region Public Methods and Operators

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null) return string.Empty;
            if (parameter == null) return value;

            if (double.TryParse(value.ToString(), out var res))
            {
                if (res != 0)
                    return string.Format(parameter.ToString(), res);
            }

            if (value is string s)
            {
                if (!string.IsNullOrEmpty(s))
                {
                    return string.Format(parameter.ToString(), s);
                }
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

        #endregion Public Methods and Operators
    }
}