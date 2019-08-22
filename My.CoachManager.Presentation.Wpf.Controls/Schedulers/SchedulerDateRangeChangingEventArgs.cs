using System;

namespace My.CoachManager.Presentation.Wpf.Controls.Schedulers
{
    /// <summary>
    /// Event arguments to notify clients that the range is changing and what the new range will be
    /// </summary>
    internal class SchedulerDateRangeChangingEventArgs : EventArgs
    {
        public SchedulerDateRangeChangingEventArgs(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }

        public DateTime Start { get; }

        public DateTime End { get; }
    }
}
