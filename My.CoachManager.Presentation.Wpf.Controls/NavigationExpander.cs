using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace My.CoachManager.Presentation.Wpf.Controls
{
    public class NavigationExpander : Expander
    {
        /// <summary>
        /// Identifies the Icon dependency property.
        /// </summary>
        public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(object), typeof(NavigationExpander));

        /// <summary>
        /// Identifies the Target dependency property.
        /// </summary>
        public static readonly DependencyProperty ColorProperty = DependencyProperty.Register("Color", typeof(Brush), typeof(NavigationExpander));

        /// <summary>
        /// Gets or sets the Icon of this window instance.
        /// </summary>
        public object Icon
        {
            get { return GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        /// <summary>
        /// Gets or sets the Color of this window instance.
        /// </summary>
        public Brush Color
        {
            get { return (Brush)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }
    }
}