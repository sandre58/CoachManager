using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace My.CoachManager.Presentation.Core.Converters
{
    public class AnyToVisibilityConverter : IValueConverter
    {
        #region Static Fields

        private static AnyToVisibilityConverter _instance;

        #endregion Static Fields

        #region Public Properties

        /// <summary>
        /// Return a unique instance of <see cref="AnyToVisibilityConverter"/>.
        /// </summary>
        public static AnyToVisibilityConverter Instance
        {
            get
            {
                return _instance ?? (_instance = new AnyToVisibilityConverter());
            }
        }

        #endregion Public Properties

        #region Public Methods and Operators

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var enumerable = (IEnumerable<object>)value;
            return (value == null || !enumerable.Any()) ? Visibility.Hidden : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion Public Methods and Operators
    }
}