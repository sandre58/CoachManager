using System;
using System.Globalization;
using System.Windows.Data;

namespace My.CoachManager.Presentation.Prism.Core.Converters
{
    public class EnumToIntegerConverter : IValueConverter
    {
        #region Static Fields

        private static EnumToIntegerConverter _instance;

        #endregion Static Fields

        #region Public Properties

        /// <summary>
        /// Return a unique _instance of <see cref="EnumToIntegerConverter"/>.
        /// </summary>
        public static EnumToIntegerConverter Instance
        {
            get
            {
                return _instance ?? (_instance = new EnumToIntegerConverter());
            }
        }

        #endregion Public Properties

        #region Public Methods and Operators

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return 0;
            }

            return (int)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion Public Methods and Operators
    }
}