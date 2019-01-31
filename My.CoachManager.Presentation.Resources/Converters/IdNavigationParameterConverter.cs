using System;
using System.Globalization;
using System.Windows.Data;

namespace My.CoachManager.Presentation.Resources.Converters
{
    public class IdNavigationParameterConverter : IValueConverter
    {
        #region Static Fields

        private static IdNavigationParameterConverter _instance;

        #endregion Static Fields

        #region Public Properties

        /// <summary>
        /// Return a unique instance of <see cref="IdNavigationParameterConverter"/>.
        /// </summary>
        public static IdNavigationParameterConverter Instance
        {
            get
            {
                return _instance ?? (_instance = new IdNavigationParameterConverter());
            }
        }

        #endregion Public Properties

        #region Public Methods and Operators

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && !string.IsNullOrEmpty((string)parameter))
            {
                return parameter + "?Id=" + value;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion Public Methods and Operators
    }
}