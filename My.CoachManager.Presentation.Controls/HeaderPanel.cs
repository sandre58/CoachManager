using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace My.CoachManager.Presentation.Controls
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

        #endregion Properties
    }
}