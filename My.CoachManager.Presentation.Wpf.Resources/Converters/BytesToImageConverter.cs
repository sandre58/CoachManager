using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using My.CoachManager.Presentation.Wpf.Core.Extensions;

namespace My.CoachManager.Presentation.Wpf.Resources.Converters
{
    /// <summary>
    /// Am implementation of <see cref="IValueConverter"/> to convert Image to byte array and vice versa.
    /// </summary>
    public class BytesToImageConverter : IValueConverter
    {
        #region Static Fields

        private static BytesToImageConverter _instance;

        #endregion Static Fields

        #region Public Properties

        /// <summary>
        /// Return a unique instance of <see cref="BytesToImageConverter"/>.
        /// </summary>
        public static BytesToImageConverter Instance
        {
            get
            {
                return _instance ?? (_instance = new BytesToImageConverter());
            }
        }

        #endregion Public Properties

        #region Implementation of IValueConverter

        /// <summary>
        /// Converts byte[] to Image source.
        /// </summary>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        /// <param name="value">The value produced by the binding source.</param><param name="targetType">The type of the binding target property.</param><param name="parameter">The converter parameter to use.</param><param name="culture">The culture to use in the converter.</param>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var bytes = value as byte[];
            if (bytes != null)
            {
                return bytes.ToBitmap();
            }
            return null;
        }

        /// <summary>
        /// Converts ImageSource to byte[]
        /// </summary>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        /// <param name="value">The value that is produced by the binding target.</param><param name="targetType">The type to convert to.</param><param name="parameter">The converter parameter to use.</param><param name="culture">The culture to use in the converter.</param>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var renderTargetBitmap = value as BitmapImage;
            if (null != renderTargetBitmap)
            {
                return renderTargetBitmap.ToBytes();
            }
            return null;
        }

        #endregion Implementation of IValueConverter
    }
}