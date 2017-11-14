using My.CoachManager.CrossCutting.Core.Extensions;
using My.CoachManager.Presentation.Core.Services.Settings;
using My.CoachManager.Presentation.Resources.Strings.Screens;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using My.CoachManager.Presentation.Core.Commands;

namespace My.CoachManager.Presentation.Controls
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

        public static readonly DependencyProperty DeletePhotoCommandProperty = DependencyProperty.Register("DeletePhotoCommand", typeof(ICommand), typeof(ImagePicker));

        /// <summary>
        /// Get or set the command on the Filter button.
        /// </summary>
        public ICommand DeletePhotoCommand
        {
            get
            {
                return (ICommand)GetValue(DeletePhotoCommandProperty);
            }

            set
            {
                SetValue(DeletePhotoCommandProperty, value);
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

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            SelectPhotoCommand = new DelegateCommand(SelectPhoto, CanSelectPhoto);
            DeletePhotoCommand = new DelegateCommand(DeletePhoto, CanDeletePhoto);
        }

        public void SelectPhoto()
        {
            var filename = ServiceLocator.DialogService.ShowOpenFileDialog(new OpenFileDialogSettings()
            {
                MultiSelect = false,
                Filter = DialogResources.AllImages,
            });

            if (!string.IsNullOrEmpty(filename))
            {
                Image = File.ReadAllBytes(filename).ToBitmap();
            }
        }

        public bool CanSelectPhoto()
        {
            return true;
        }

        public void DeletePhoto()
        {
            Image = null;
        }

        public bool CanDeletePhoto()
        {
            return Image != null;
        }
    }
}