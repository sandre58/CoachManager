﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace My.CoachManager.Presentation.Wpf.Resources.Converters
{
    /// <summary>
    /// Converts boolean to visibility values.
    /// </summary>
    public class VisibilityToBooleanConverter
        : IValueConverter
    {
        #region Public Methods

        /// <summary>
        /// The convert.
        /// </summary>
        /// <param name="value"> The value. </param>
        /// <param name="targetType"> The target type. </param>
        /// <param name="parameter"> The parameter. </param>
        /// <param name="culture"> The culture. </param>
        /// <returns> The <see cref="object"/>. </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((Visibility)value == Visibility.Visible)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// The convert back.
        /// </summary>
        /// <param name="value"> The value. </param>
        /// <param name="targetType"> The target type. </param>
        /// <param name="parameter"> The parameter. </param>
        /// <param name="culture"> The culture. </param>
        /// <returns> The <see cref="object"/>. </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool boolean = (bool)value;

            if (boolean == true)
            {
                return Visibility.Visible;
            }

            return Visibility.Collapsed;
        }

        #endregion Public Methods
    }
}