using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace My.CoachManager.Presentation.Core.Converters
{
    public class StringToImageConverter : IValueConverter
    {
        #region Static Fields

        private static StringToImageConverter _instance;

        #endregion Static Fields

        #region Public Properties

        /// <summary>
        /// Return a unique instance of <see cref="StringToImageConverter"/>.
        /// </summary>
        public static StringToImageConverter Instance
        {
            get
            {
                return _instance ?? (_instance = new StringToImageConverter());
            }
        }

        #endregion Public Properties

        #region Public Methods and Operators

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;
            var path = value.ToString();
            var uri = new Uri(path, UriKind.Relative);
            var image = new BitmapImage(uri);
            return image;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion Public Methods and Operators
    }
}