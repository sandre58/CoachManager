using System;
using System.Windows.Controls;
using System.Windows.Data;
using My.CoachManager.Presentation.Wpf.Controls.ContentControls;

namespace My.CoachManager.Presentation.Wpf.Resources.Converters
{
    /// <summary>
    /// Converts a boolean value to a font weight (false: normal, true: bold)
    /// </summary>
    public class CharacterCaseToCharacterCasingConverter
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
            var val = value is CharacterCase casing ? casing : CharacterCase.Normal;

            switch (val)
            {
                case CharacterCase.Normal:
                    return CharacterCasing.Normal;
                case CharacterCase.Lower:
                    return CharacterCasing.Lower;
                case CharacterCase.Upper:
                case CharacterCase.FirstLetterUpper:
                    return CharacterCasing.Upper;
                default:
                    return CharacterCasing.Normal;
            }

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