using System.Windows;

namespace My.CoachManager.Presentation.Controls.TimePickers
{
    public class TimePickerBaseSelectionChangedEventArgs<T> : RoutedEventArgs
    {
        public TimePickerBaseSelectionChangedEventArgs(RoutedEvent eventId, T oldValue, T newValue) :
            base(eventId)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }

        public T OldValue { get; private set; }
        public T NewValue { get; private set; }
    }
}