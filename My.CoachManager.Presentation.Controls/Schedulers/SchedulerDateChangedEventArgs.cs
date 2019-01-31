using System;

namespace My.CoachManager.Presentation.Controls.Schedulers
{
    /// <summary>
    /// Provides data for the DateSelected and DisplayDateChanged events.
    /// </summary>
    public class SchedulerDateChangedEventArgs : System.Windows.RoutedEventArgs
    {
        internal SchedulerDateChangedEventArgs(DateTime? removedDate, DateTime? addedDate)
        {
            RemovedDate = removedDate;
            AddedDate = addedDate;
        }

        /// <summary>
        /// Gets the date to be newly displayed.
        /// </summary>
        public DateTime? AddedDate
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the date that was previously displayed.
        /// </summary>
        public DateTime? RemovedDate
        {
            get;
            private set;
        }
    }
}
