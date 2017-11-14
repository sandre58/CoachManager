using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace My.CoachManager.Presentation.Core.Converters
{
    public class NullToVisibilityConverter : IValueConverter
    {
        #region Static Fields

        private static NullToVisibilityConverter _instance;

        #endregion Static Fields

        #region Public Properties

        /// <summary>
        /// Return a unique instance of <see cref="NullToVisibilityConverter"/>.
        /// </summary>
        public static NullToVisibilityConverter Instance
        {
            get
            {
                return _instance ?? (_instance = new NullToVisibilityConverter());
            }
        }

        #endregion Public Properties

        #region Public Methods and Operators

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion Public Methods and Operators
    }
}