using System;
using System.Globalization;
using System.Windows.Data;

namespace My.CoachManager.Presentation.Prism.Resources.Converters
{
    [ValueConversion(typeof(TimeSpan?), typeof(string))]
    internal class TimeSpanToStringConverter : IValueConverter
    {
        /// <summary>Converts a value. </summary>
        /// <returns>A converted value. If the method returns null, the valid null value is used.</returns>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is TimeSpan timeSpan))
            {
                return null;
            }

            var convert = DateTime.MinValue.Add(((TimeSpan?) timeSpan).GetValueOrDefault()).ToString(culture.DateTimeFormat.LongTimePattern, culture);
            return convert;
        }

        /// <summary>Converts a value. </summary>
        /// <returns>A converted value. If the method returns null, the valid null value is used.</returns>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && DateTime.TryParseExact(value.ToString(), culture.DateTimeFormat.LongTimePattern, culture, DateTimeStyles.None, out var dateTime))
            {
                return dateTime.TimeOfDay;
            }

            return null;
        }
    }
}
