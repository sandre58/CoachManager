using System;
using System.Windows.Data;

namespace My.CoachManager.Presentation.Prism.Core.Converters
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
            if (!string.IsNullOrEmpty((string)value))
            {
                if (parameter != null)
                {
                    var str = value.ToString();
                    double res;
                    if (double.TryParse(str, out res))
                    {
                        return string.Format(parameter.ToString(), res);
                    }
                    else
                    {
                        return string.Format(parameter.ToString(), str);
                    }
                }
                else
                {
                    return value;
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