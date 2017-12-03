using System.Windows;

namespace My.CoachManager.Presentation.Prism.Controls.ExtendedNumericUpDowns
{
    public class ExtendedNumericUpDownChangedRoutedEventArgs : RoutedEventArgs
    {
        public double Interval { get; set; }

        public ExtendedNumericUpDownChangedRoutedEventArgs(RoutedEvent routedEvent, double interval)
            : base(routedEvent)
        {
            Interval = interval;
        }
    }
}