using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Microsoft.Toolkit.Uwp.UI.Controls;

namespace My.CoachManager.Presentation.Uwp.Controls
{
    public class ExtendedHeaderedContentControl : HeaderedContentControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExtendedHeaderedContentControl"/> class.
        /// </summary>
        public ExtendedHeaderedContentControl()
        {
            DefaultStyleKey = typeof(ExtendedHeaderedContentControl);
        }
        #region IconData

        public static readonly DependencyProperty IconDataProperty = DependencyProperty.Register("IconData", typeof(Geometry), typeof(ExtendedHeaderedContentControl), new PropertyMetadata(null));
        public Geometry IconData
        {
            get => (Geometry)GetValue(IconDataProperty);
            set => SetValue(IconDataProperty, value);
        }

        #endregion

        #region Badge

        public static readonly DependencyProperty BadgeProperty = DependencyProperty.Register("Badge", typeof(string), typeof(ExtendedHeaderedContentControl), new PropertyMetadata(null));
        public string Badge
        {
            get => (string)GetValue(BadgeProperty);
            set => SetValue(BadgeProperty, value);
        }

        #endregion

        #region Command

        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(ExtendedHeaderedContentControl), new PropertyMetadata(null));
        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        #endregion

        #region CommandParameter

        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register("CommandParameter", typeof(object), typeof(ExtendedHeaderedContentControl), new PropertyMetadata(null));
        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        #endregion

    }
}
