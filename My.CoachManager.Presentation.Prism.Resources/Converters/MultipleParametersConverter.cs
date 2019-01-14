using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace My.CoachManager.Presentation.Prism.Resources.Converters
{
    public class MultipleParametersConverter : IMultiValueConverter
    {
        #region Static Fields

        private static MultipleParametersConverter _instance;

        #endregion Static Fields

        #region Public Properties

        /// <summary>
        /// Return a unique instance of <see cref="MultipleParametersConverter"/>.
        /// </summary>
        public static MultipleParametersConverter Instance => _instance ?? (_instance = new MultipleParametersConverter());

        #endregion Public Properties

        #region Public Methods and Operators

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var parameters = new List<object>();
            foreach (var value in values)
            {
                parameters.Add(value);
            }

            return parameters;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion Public Methods and Operators
    }
}