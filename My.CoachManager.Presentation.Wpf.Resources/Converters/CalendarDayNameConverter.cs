using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace My.CoachManager.Presentation.Wpf.Resources.Converters
{
    public class CalendarDayNameConverter : IValueConverter
    {
        #region Static Fields

        private static CalendarDayNameConverter _instance;

        #endregion Static Fields

        #region Public Properties

        /// <summary>
        /// Return a unique instance of <see cref="CalendarDayNameConverter"/>.
        /// </summary>
        public static CalendarDayNameConverter Instance => _instance ?? (_instance = new CalendarDayNameConverter());

        #endregion Public Properties

        #region Public Methods and Operators

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
                var daynames = CultureInfo.CurrentCulture.DateTimeFormat.DayNames;
            if (value != null)
            {
                var dayname = value.ToString();

                return daynames.First(t => t.StartsWith(dayname));
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion Public Methods and Operators
    }
}