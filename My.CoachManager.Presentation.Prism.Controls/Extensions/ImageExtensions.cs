using System.IO;
using System.Windows.Media.Imaging;

namespace My.CoachManager.Presentation.Prism.Controls.Extensions
{
    public static class ImageExtensions
    {
        #region Public Methods and Operators

        public static BitmapSource ToBitmap(this byte[] objSource)
        {
            if (objSource.Length > 0)
            {
                var stream = new MemoryStream(objSource);
                var image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = stream;
                image.EndInit();
                return image;
            }
            return null;
        }

        public static byte[] ToBytes(this BitmapSource objSource)
        {
            var bitmapImage = new BitmapImage();
            var bitmapEncoder = new BmpBitmapEncoder();
            bitmapEncoder.Frames.Add(BitmapFrame.Create(objSource));

            using (var stream = new MemoryStream())
            {
                bitmapEncoder.Save(stream);
                stream.Seek(0, SeekOrigin.Begin);
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = stream;
                bitmapImage.EndInit();
            }
            var encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
            byte[] data;
            using (var ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
            }
            return data;
        }

        #endregion Public Methods and Operators
    }
}