using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace My.CoachManager.Presentation.Wpf.Controls
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

        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(HeaderPanel), new PropertyMetadata());

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register("CommandParameter", typeof(object), typeof(HeaderPanel), new PropertyMetadata());

        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        public static readonly DependencyProperty HeaderForegroundProperty = DependencyProperty.Register("HeaderForeground", typeof(SolidColorBrush), typeof(HeaderPanel), new PropertyMetadata());

        public SolidColorBrush HeaderForeground
        {
            get { return (SolidColorBrush)GetValue(HeaderForegroundProperty); }
            set { SetValue(HeaderForegroundProperty, value); }
        }

        public static readonly DependencyProperty HeaderFontSizeProperty = DependencyProperty.Register("HeaderFontSize", typeof(double), typeof(HeaderPanel), new PropertyMetadata(12.0));

        public double HeaderFontSize
        {
            get { return (double)GetValue(HeaderFontSizeProperty); }
            set { SetValue(HeaderFontSizeProperty, value); }
        }
        #endregion Properties
    }
}