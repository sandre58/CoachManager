using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace My.CoachManager.Presentation.Wpf.Controls.Schedulers
{
    public class SchedulerAppointment : Button
    {

        #region Properties

        #region Color

        /// <summary>
        /// Gets or sets the style for displaying a SchedulerButton.
        /// </summary>
        public SolidColorBrush Color
        {
            get => (SolidColorBrush)GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }

        /// <summary>
        /// Identifies the SchedulerButtonStyle dependency property.
        /// </summary>
        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.Register(
                "Color",
                typeof(SolidColorBrush),
                typeof(SchedulerAppointment));

        #endregion SchedulerPanelStyle

        #endregion
    }
}
