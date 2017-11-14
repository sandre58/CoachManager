using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace My.CoachManager.Presentation.Core.Converters
{
    public class NullOrEmptyToBooleanConverter : IValueConverter
    {
        #region Static Fields

        private static NullOrEmptyToBooleanConverter _instance;

        #endregion Static Fields

        #region Public Properties

        /// <summary>
        /// Return a unique instance of <see cref="NullOrEmptyToBooleanConverter"/>.
        /// </summary>
        public static NullOrEmptyToBooleanConverter Instance
        {
            get
            {
                return _instance ?? (_instance = new NullOrEmptyToBooleanConverter());
            }
        }

        #endregion Public Properties

        #region Public Methods and Operators

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value == null || string.IsNullOrEmpty(value.ToString()));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion Public Methods and Operators
    }
}