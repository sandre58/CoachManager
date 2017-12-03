using System.Windows;
using System.Windows.Media;

namespace My.CoachManager.Presentation.Prism.Controls
{
    public class CommandButton : System.Windows.Controls.Button
    {
        /// <summary>
        /// Identifies the IconData property.
        /// </summary>
        public static readonly DependencyProperty IconDataProperty = DependencyProperty.Register("IconData", typeof(Geometry), typeof(CommandButton));

        /// <summary>
        /// Identifies the IconData property.
        /// </summary>
        public static readonly DependencyProperty ButtonDiameterProperty = DependencyProperty.Register("ButtonDiameter", typeof(double), typeof(CommandButton));

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandButton"/> class.
        /// </summary>
        public CommandButton()
        {
            DefaultStyleKey = typeof(CommandButton);
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