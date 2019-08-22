using System.Windows;
using System.Windows.Controls;

namespace My.CoachManager.Presentation.Wpf.Controls
{
    public class PositionsSelector : SelectorControl
    {
        /// <summary>
        /// Identifies the BackgroundContent dependency property.
        /// </summary>
        public static readonly DependencyProperty ItemHeightProperty = DependencyProperty.Register("ItemHeight", typeof(double), typeof(PositionsSelector));

        /// <summary>
        /// Gets or sets the background content of this window instance.
        /// </summary>
        public double ItemHeight
        {
            get
            {
                var value = GetValue(ItemHeightProperty);
                if (value != null) return (double)value;
                return 0;
            }
            set => SetValue(ItemHeightProperty, value);
        }

        /// <summary>
        /// Identifies the BackgroundContent dependency property.
        /// </summary>
        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register("Orientation", typeof(Orientation), typeof(PositionsSelector), new PropertyMetadata(Orientation.Vertical));

        /// <summary>
        /// Gets or sets the background content of this window instance.
        /// </summary>
        public Orientation Orientation
        {
            get
            {
                var value = GetValue(ItemHeightProperty);
                if (value != null) return (Orientation)value;
                return Orientation.Vertical;
            }
            set => SetValue(ItemHeightProperty, value);
        }
    }
}
