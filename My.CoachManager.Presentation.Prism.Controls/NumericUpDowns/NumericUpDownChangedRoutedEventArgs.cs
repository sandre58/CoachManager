using System.Windows;

namespace My.CoachManager.Presentation.Prism.Controls.NumericUpDowns
{
    public class NumericUpDownChangedRoutedEventArgs : RoutedEventArgs
    {
        public double Interval { get; set; }

        public NumericUpDownChangedRoutedEventArgs(RoutedEvent routedEvent, double interval)
            : base(routedEvent)
        {
            Interval = interval;
        }
    }
}