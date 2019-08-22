using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using My.CoachManager.Presentation.Wpf.SkinManager;

namespace My.CoachManager.Presentation.Wpf.Tests.Converters
{
    /// <summary>
    /// An implementation of <see cref="IValueConverter"/> contract that converts a string to a <see cref="Brush"/> object.
    /// </summary>
    public class StringToThemeConverter : IValueConverter
    {
        #region Static Fields

        private static StringToThemeConverter _instance;

        #endregion Static Fields

        #region Public Properties

        /// <summary>
        /// Return a unique instance of <see cref="StringToThemeConverter"/>.
        /// </summary>
        public static StringToThemeConverter Instance
        {
            get
            {
                return _instance ?? (_instance = new StringToThemeConverter());
            }
        }

        #endregion Public Properties

        /// <summary>
        /// Converts the name of a color to a <see cref="System.Windows.Media.Brush"/> object.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var accent = value as Theme;
            if (accent != null)
            {
                var color = accent.Resources["LightColor"];
                if (null != color)
                {
                    return new SolidColorBrush((Color)color);
                }
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