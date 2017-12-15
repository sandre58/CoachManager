﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace My.CoachManager.Presentation.Prism.Resources.Converters
{
    /// <summary>
    /// Converts a null or empty string value to Visibility.Visible and any other value to Visibility.Collapsed
    /// </summary>
    public class NullToBooleanConverter
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
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var flag = value == null;
            if (value is string)
            {
                flag = string.IsNullOrEmpty((string)value);
            }

            if (value is Array)
            {
                var array = (Array)value;
                flag = array.Length == 0;
            }

            var inverse = (parameter as string) == "inverse";

            if (inverse)
            {
                return !flag;
            }
            return flag;
            
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
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}