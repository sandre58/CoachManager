using System;
using System.Globalization;
using System.Windows.Data;
using My.CoachManager.CrossCutting.Core.Constants;

namespace My.CoachManager.Presentation.Prism.Resources.Converters
{
    public sealed class VerticalRowToHorizontalColumnPositionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var row = (int) value;

                return PositionConstants.RowsCount - 1 - row;
            }

            return 0;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}