﻿using System.Windows;
using System.Windows.Media.Imaging;

namespace My.CoachManager.Presentation.Prism.Controls
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

        #endregion Properties
    }
}