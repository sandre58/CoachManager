using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace My.CoachManager.Presentation.Converters
{
    /// <summary>
    /// An implementation of <see cref="IValueConverter"/> contract that converts a string to a <see cref="Brush"/> object.
    /// </summary>
    public class StringToThemeConverter : IValueConverter
    {
        /// <summary>
        /// Converts the name of a color to a <see cref="System.Windows.Media.Brush"/> object.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var baseColorName = System.Convert.ToString(value);
            var converter = new BrushConverter();
            if (string.CompareOrdinal(baseColorName, "BaseLight") == 0)
            {
                var brush = (Brush)converter.ConvertFromString("#FFFFFFFF");
                return brush;
            }
            if (string.CompareOrdinal(baseColorName, "BaseDark") == 0)
            {
                var brush = (Brush)converter.ConvertFromString("#FF000000");
                return brush;
            }

            return null;
        }

        /// <summary>
        /// Not implemented!
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}