﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace My.CoachManager.Presentation.Prism.Core.Converters
{
    /// <summary>
    /// Converts boolean to visibility values.
    /// </summary>
    public class BooleanToVisibilityConverter
        : IValueConverter
    {
        private static BooleanToVisibilityConverter _instance;

        /// <summary>
        /// Return a unique instance of <see cref="BooleanToVisibilityConverter"/>.
        /// </summary>
        public static BooleanToVisibilityConverter Instance
        {
            get
            {
                return _instance ?? (_instance = new BooleanToVisibilityConverter());
            }
        }

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
            bool flag = false;
            if (value is bool)
            {
                flag = (bool)value;
            }

            bool inverse = (parameter as string) == "inverse";

            if (inverse)
            {
                return (flag ? Visibility.Collapsed : Visibility.Visible);
            }
            return (flag ? Visibility.Visible : Visibility.Collapsed);
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