using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace My.CoachManager.Presentation.Resources.Converters
{
    public class AnyToBooleanConverter : IValueConverter
    {
        #region Static Fields

        private static AnyToBooleanConverter _instance;

        #endregion Static Fields

        #region Public Properties

        /// <summary>
        /// Return a unique instance of <see cref="AnyToBooleanConverter"/>.
        /// </summary>
        public static AnyToBooleanConverter Instance => _instance ?? (_instance = new AnyToBooleanConverter());

        #endregion Public Properties

        #region Public Methods and Operators

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var enumerable = (IEnumerable<object>)value;
            if (parameter != null && parameter.ToString() == "inverse")
            {
                return enumerable == null || !enumerable.Any();
            }
            return enumerable != null && enumerable.Any();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion Public Methods and Operators
    }
}