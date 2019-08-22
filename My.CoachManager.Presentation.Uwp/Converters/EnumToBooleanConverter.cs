using System;
using Windows.UI.Xaml.Data;

namespace My.CoachManager.Presentation.Uwp.Converters
{
    public class EnumToBooleanConverter : IValueConverter
    {
        #region Public Methods and Operators

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (parameter == null || value == null)
            {
                return false;
            }

            if (Enum.TryParse(value.GetType(), parameter.ToString(), out var enumValueParameter))
            {
                return value.Equals(enumValueParameter);
            };

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

        #endregion Public Methods and Operators
    }
}