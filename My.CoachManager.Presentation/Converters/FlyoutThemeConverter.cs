using System;
using System.Globalization;
using System.Windows.Data;
using My.CoachManager.Presentation.Core.ViewModels.Screens.Enums;

namespace My.CoachManager.Presentation.Converters
{
    /// <summary>
    /// An implementation of <see cref="IValueConverter"/> to convert ViewModel friendly flyout theme to
    /// UI friendly flyout theme.
    /// </summary>
    public class FlyoutThemeConverter : IValueConverter
    {
        /// <summary>
        /// Converts a <see cref="FlyoutTheme"/> enum value to
        /// <see cref="FlyoutTheme"/> enum value.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var position = (FlyoutTheme)Enum.Parse(typeof(FlyoutTheme), System.Convert.ToString(value));
            switch (position)
            {
                case FlyoutTheme.AccentedTheme:
                    return MahApps.Metro.Controls.FlyoutTheme.Accent;

                case FlyoutTheme.BaseColorTheme:
                    return MahApps.Metro.Controls.FlyoutTheme.Adapt;

                case FlyoutTheme.InverseTheme:
                    return MahApps.Metro.Controls.FlyoutTheme.Inverse;

                default:
                    return MahApps.Metro.Controls.FlyoutTheme.Dark;
            }
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