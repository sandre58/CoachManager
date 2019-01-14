using System;
using System.Globalization;
using System.Windows.Data;

namespace My.CoachManager.Presentation.Prism.Resources.Converters
{
    public class EqualValuesToBooleanConverter : IMultiValueConverter
    {
        #region Static Fields

        private static EqualValuesToBooleanConverter _instance;

        #endregion Static Fields

        #region Public Properties

        /// <summary>
        /// Return a unique instance of <see cref="EqualValuesToBooleanConverter"/>.
        /// </summary>
        public static EqualValuesToBooleanConverter Instance => _instance ?? (_instance = new EqualValuesToBooleanConverter());

        #endregion Public Properties

        #region Public Methods and Operators

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            bool result = !(parameter != null && parameter.ToString() == "inverse");
            if (values.Length < 1) return result;
            for (int i = 1; i < values.Length; i++)
            {
                if (values[i] != null && !values[i].Equals(values[i - 1])) return !result;
            }
            return result;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion Public Methods and Operators
    }
}