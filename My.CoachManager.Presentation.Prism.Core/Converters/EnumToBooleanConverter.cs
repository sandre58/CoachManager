using System;
using System.Collections;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace My.CoachManager.Presentation.Prism.Core.Converters
{
    public class EnumToBooleanConverter : IValueConverter
    {
        #region Static Fields

        private static EnumToBooleanConverter _instance;

        #endregion Static Fields

        #region Public Properties

        /// <summary>
        /// Return a unique instance of <see cref="EnumToBooleanConverter"/>.
        /// </summary>
        public static EnumToBooleanConverter Instance
        {
            get
            {
                return _instance ?? (_instance = new EnumToBooleanConverter());
            }
        }

        #endregion Public Properties

        #region Public Methods and Operators

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null || value == null)
            {
                return false;
            }

            var parameters = parameter as IEnumerable;
            return
                (parameters != null)
                    ? parameters.Cast<object>().Any(parameter2 => (System.Convert.ToInt32(parameter2) == System.Convert.ToInt32(value)))
                    : (System.Convert.ToInt32(parameter) == System.Convert.ToInt32(value));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && ((parameter == null) || !(bool)value))
            {
                return DependencyProperty.UnsetValue;
            }

            var parameters = parameter as IEnumerable;
            return
                (parameters != null)
                    ? parameters.Cast<object>().FirstOrDefault()
                    : parameter;
        }

        #endregion Public Methods and Operators
    }
}