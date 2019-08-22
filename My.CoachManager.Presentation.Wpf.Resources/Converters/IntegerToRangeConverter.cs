using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace My.CoachManager.Presentation.Wpf.Resources.Converters
{
    public class IntegerToRangeConverter : IValueConverter
    {
        #region Static Fields

        private static IntegerToRangeConverter _instance;

        #endregion Static Fields

        #region Public Properties

        /// <summary>
        /// Return a unique instance of <see cref="IntegerToRangeConverter"/>.
        /// </summary>
        public static IntegerToRangeConverter Instance => _instance ?? (_instance = new IntegerToRangeConverter());

        #endregion Public Properties

        #region Public Methods and Operators

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var startValue = parameter!= null ? int.Parse(parameter.ToString()) : 1;
            var endValue = value != null ? int.Parse(value.ToString()) : startValue;

            return Enumerable.Range(startValue, endValue);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion Public Methods and Operators
    }
}