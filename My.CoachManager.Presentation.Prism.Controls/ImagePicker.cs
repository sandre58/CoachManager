using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace My.CoachManager.Presentation.Prism.Controls
{
    public class ImagePicker : ContentControl
    {
        #region Properties

        public static readonly DependencyProperty ImageProperty = DependencyProperty.Register("Image", typeof(BitmapSource), typeof(ImagePicker), new PropertyMetadata());

        public BitmapSource Image
        {
            get { return (BitmapSource)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        public static readonly DependencyProperty RemovePhotoCommandProperty = DependencyProperty.Register("RemovePhotoCommand", typeof(ICommand), typeof(ImagePicker));

        /// <summary>
        /// Get or set the command on the Filter button.
        /// </summary>
        public ICommand RemovePhotoCommand
        {
            get
            {
                return (ICommand)GetValue(RemovePhotoCommandProperty);
            }

            set
            {
                SetValue(RemovePhotoCommandProperty, value);
            }
        }

        public static readonly DependencyProperty SelectPhotoCommandProperty = DependencyProperty.Register("SelectPhotoCommand", typeof(ICommand), typeof(ImagePicker));

        /// <summary>
        /// Get or set the command on the Filter button.
        /// </summary>
        public ICommand SelectPhotoCommand
        {
            get
            {
                return (ICommand)GetValue(SelectPhotoCommandProperty);
            }

            set
            {
                SetValue(SelectPhotoCommandProperty, value);
            }
        }

        #endregion Properties
    }
}
