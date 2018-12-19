using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace My.CoachManager.Presentation.Prism.Controls
{
    public class HeaderPanel : HeaderedContentControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderPanel"/> class.
        /// </summary>
        public HeaderPanel()
        {
            DefaultStyleKey = typeof(HeaderPanel);
        }

        #region Properties

        public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(Geometry), typeof(HeaderPanel), new PropertyMetadata());

        public Geometry Icon
        {
            get { return (Geometry)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        #endregion Properties
    }
}