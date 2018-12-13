using System.Windows;

namespace My.CoachManager.Presentation.Prism.Controls
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
    }
}
