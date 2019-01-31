using System.Windows;
using System.Windows.Media.Imaging;

namespace My.CoachManager.Presentation.Controls
{
    public class ImageContent : System.Windows.Controls.ContentControl
    {
        #region Properties

        public static readonly DependencyProperty ImageProperty = DependencyProperty.Register("Image", typeof(BitmapSource), typeof(ImageContent), new PropertyMetadata());

        public BitmapSource Image
        {
            get { return (BitmapSource)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        public static readonly DependencyProperty ImagePaddingProperty = DependencyProperty.Register("ImagePadding", typeof(Thickness), typeof(ImageContent), new PropertyMetadata());

        public Thickness ImagePadding
        {
            get { return (Thickness)GetValue(ImagePaddingProperty); }
            set { SetValue(ImagePaddingProperty, value); }
        }

        #endregion Properties
    }
}