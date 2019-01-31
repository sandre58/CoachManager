using System;
using System.Windows.Data;

namespace My.CoachManager.Presentation.Resources.Converters
{
    /// <summary>
    /// Converts string values to upper case.
    /// </summary>
    public class ToFormatConverter
        : IValueConverter
    {
        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return string.Empty;
            if (parameter == null) return value;

            double res;
            if (double.TryParse(value.ToString(), out res))
            {
                if (res != 0)
                    return string.Format(parameter.ToString(), res);
            }

            if (value is string)
            {
                if (!string.IsNullOrEmpty((string)value))
                {
                    return string.Format(parameter.ToString(), value.ToString());
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}