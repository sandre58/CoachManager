using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace My.CoachManager.Presentation.Resources.Converters
{
    public class EqualValuesToVisibilityConverter : IMultiValueConverter
    {
        #region Static Fields

        private static EqualValuesToVisibilityConverter _instance;

        #endregion Static Fields

        #region Public Properties

        /// <summary>
        /// Return a unique instance of <see cref="EqualValuesToVisibilityConverter"/>.
        /// </summary>
        public static EqualValuesToVisibilityConverter Instance => _instance ?? (_instance = new EqualValuesToVisibilityConverter());

        #endregion Public Properties

        #region Public Methods and Operators

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            bool result = !(parameter != null && parameter.ToString() == "inverse");
            if (values.Length < 1) return result ? Visibility.Visible : Visibility.Collapsed;
            for (int i = 1; i < values.Length; i++)
            {
                if (values[i] != null && !values[i].Equals(values[i - 1])) return result ? Visibility.Collapsed : Visibility.Visible;
            }
            return result ? Visibility.Visible : Visibility.Collapsed;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion Public Methods and Operators
    }
}