using System;
using System.Globalization;
using System.Windows.Data;

namespace My.CoachManager.Presentation.Core.Converters
{
    public class StringToUpperConverter : IValueConverter
    {
        #region Static Fields

        private static StringToUpperConverter _instance;

        #endregion Static Fields

        #region Public Properties

        /// <summary>
        /// Return a unique instance of <see cref="StringToUpperConverter"/>.
        /// </summary>
        public static StringToUpperConverter Instance
        {
            get
            {
                return _instance ?? (_instance = new StringToUpperConverter());
            }
        }

        #endregion Public Properties

        #region Public Methods and Operators

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? null : value.ToString().ToUpper();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion Public Methods and Operators
    }
}