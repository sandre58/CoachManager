using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace My.CoachManager.Presentation.Controls
{
    public class ToggleCommandButton : ToggleButton
    {
        /// <summary>
        /// Identifies the IconData property.
        /// </summary>
        public static readonly DependencyProperty IconDataProperty = DependencyProperty.Register("IconData", typeof(Geometry), typeof(ToggleCommandButton));

        /// <summary>
        /// Identifies the IconData property.
        /// </summary>
        public static readonly DependencyProperty CheckedIconDataProperty = DependencyProperty.Register("CheckedIconData", typeof(Geometry), typeof(ToggleCommandButton));

        /// <summary>
        /// Identifies the IconData property.
        /// </summary>
        public static readonly DependencyProperty CheckedContentProperty = DependencyProperty.Register("CheckedContent", typeof(object), typeof(ToggleCommandButton));

        /// <summary>
        /// Identifies the IconData property.
        /// </summary>
        public static readonly DependencyProperty ButtonDiameterProperty = DependencyProperty.Register("ButtonDiameter", typeof(double), typeof(ToggleCommandButton));

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandButton"/> class.
        /// </summary>
        public ToggleCommandButton()
        {
            DefaultStyleKey = typeof(ToggleCommandButton);
        }

        /// <summary>
        /// Gets or sets the icon path data.
        /// </summary>
        /// <value>
        /// The icon path data.
        /// </value>
        public Geometry CheckedIconData
        {
            get { return (Geometry)GetValue(CheckedIconDataProperty); }
            set { SetValue(CheckedIconDataProperty, value); }
        }

        /// <summary>
        /// Gets or sets the icon path data.
        /// </summary>
        /// <value>
        /// The icon path data.
        /// </value>
        public object CheckedContent
        {
            get { return GetValue(CheckedContentProperty); }
            set { SetValue(CheckedContentProperty, value); }
        }

        /// <summary>
        /// Gets or sets the icon path data.
        /// </summary>
        /// <value>
        /// The icon path data.
        /// </value>
        public Geometry IconData
        {
            get { return (Geometry)GetValue(IconDataProperty); }
            set { SetValue(IconDataProperty, value); }
        }

        /// <summary>
        /// Gets or sets the icon path data.
        /// </summary>
        /// <value>
        /// The icon path data.
        /// </value>
        public double ButtonDiameter
        {
            get
            {
                var value = GetValue(ButtonDiameterProperty);
                if (value != null) return (double)value;
                return 0;
            }
            set { SetValue(ButtonDiameterProperty, value); }
        }
    }
}