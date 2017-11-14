using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace My.CoachManager.Presentation.Core.Converters
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        #region Static Fields

        private static BooleanToVisibilityConverter _instance;

        #endregion Static Fields

        #region Public Properties

        /// <summary>
        /// Return a unique instance of <see cref="BooleanToVisibilityConverter"/>.
        /// </summary>
        public static BooleanToVisibilityConverter Instance
        {
            get
            {
                return _instance ?? (_instance = new BooleanToVisibilityConverter());
            }
        }

        #endregion Public Properties

        #region Public Methods and Operators

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return Visibility.Collapsed;
            }

            var booleanValue = (bool)value;
            return booleanValue ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var visibilityValue = (Visibility)value;
                return (visibilityValue != Visibility.Collapsed) && (visibilityValue != Visibility.Hidden);
            }
            return null;
        }

        #endregion Public Methods and Operators
    }
}