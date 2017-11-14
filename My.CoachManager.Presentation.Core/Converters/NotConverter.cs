using System;
using System.Globalization;
using System.Windows.Data;

namespace My.CoachManager.Presentation.Core.Converters
{
    public class NotConverter : IValueConverter
    {
        #region Static Fields

        private static NotConverter _instance;

        #endregion Static Fields

        #region Public Properties

        /// <summary>
        /// Return a unique instance of <see cref="BooleanToVisibilityConverter"/>.
        /// </summary>
        public static NotConverter Instance
        {
            get
            {
                return _instance ?? (_instance = new NotConverter());
            }
        }

        #endregion Public Properties

        #region Public Methods and Operators

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var booleanValue = value != null && (bool)value;
            return !booleanValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null && !(bool)value;
        }

        #endregion Public Methods and Operators
    }
}