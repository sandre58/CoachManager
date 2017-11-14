using System;
using System.Globalization;
using System.Windows.Data;
using MahApps.Metro.Controls;
using My.CoachManager.Presentation.Core.ViewModels.Screens.Enums;

namespace My.CoachManager.Presentation.Converters
{
    /// <summary>
    /// A converter to convert a <see cref="FlyoutVisibilityPosition"/> enum to <see cref="Position"/> enum.
    /// </summary>
    public class FlyoutPositionConverter : IValueConverter
    {
        /// <summary>
        /// Conmverts a View model recognized enum to view ewcogniuzed enum.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var position = (FlyoutVisibilityPosition)Enum.Parse(typeof(FlyoutVisibilityPosition), System.Convert.ToString(value));
            switch (position)
            {
                case FlyoutVisibilityPosition.Bottom:
                    return Position.Bottom;

                case FlyoutVisibilityPosition.Right:
                    return Position.Right;

                case FlyoutVisibilityPosition.Left:
                    return Position.Right;

                case FlyoutVisibilityPosition.Top:
                    return Position.Top;
            }
            return null;
        }

        /// <summary>
        /// Not implemented!
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}