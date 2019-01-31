using System.Windows;
using System.Windows.Media.Imaging;

namespace My.CoachManager.Presentation.Controls
{
    public class ImagePicker : System.Windows.Controls.Button
    {
        #region Properties

        public static readonly DependencyProperty ImageProperty = DependencyProperty.Register("Image", typeof(BitmapSource), typeof(ImagePicker), new PropertyMetadata());

        public BitmapSource Image
        {
            get { return (BitmapSource)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        #endregion Properties
    }
}
