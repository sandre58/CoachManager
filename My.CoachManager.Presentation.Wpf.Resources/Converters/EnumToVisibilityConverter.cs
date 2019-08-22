using System;
using System.Globalization;
using System.Windows.Data;

namespace My.CoachManager.Presentation.Wpf.Resources.Converters
{
    public class EnumToVisibilityConverter : IValueConverter
    {
        #region Static Fields

        private static EnumToVisibilityConverter _instance;

        #endregion Static Fields

        #region Public Properties

        /// <summary>
        /// Return a unique instance of <see cref="EnumToVisibilityConverter"/>.
        /// </summary>
        public static EnumToVisibilityConverter Instance
        {
            get
            {
                return _instance ?? (_instance = new EnumToVisibilityConverter());
            }
        }

        #endregion Public Properties

        #region Public Methods and Operators

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var convert = EnumToBooleanConverter.Instance.Convert(value, targetType, parameter, culture);
            var isVisible = convert != null && (bool)convert;
            return BooleanToVisibilityConverter.Instance.Convert(isVisible, targetType, parameter, culture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var convertBack = BooleanToVisibilityConverter.Instance.ConvertBack(value, targetType, parameter, culture);
            var isVisible = convertBack != null && (bool)convertBack;
            return EnumToBooleanConverter.Instance.ConvertBack(isVisible, targetType, parameter, culture);
        }

        #endregion Public Methods and Operators
    }
}