using System;
using System.ComponentModel;

namespace My.CoachManager.Presentation.Prism.Controls.Schedulers
{
    /// <summary>
    /// Specifies a DateTime range class which has a start and end.
    /// </summary>
    public sealed class SchedulerDateRange : INotifyPropertyChanged
    {
        #region Data
        private DateTime _end;
        private DateTime _start;
        #endregion Data

        /// <summary>
        /// Initializes a new instance of the SchedulerDateRange class.
        /// </summary>
        public SchedulerDateRange() :
            this(DateTime.MinValue, DateTime.MaxValue)
        {
        }

        /// <summary>
        /// Initializes a new instance of the SchedulerDateRange class which creates a range from a single DateTime value.
        /// </summary>
        /// <param name="day"></param>
        public SchedulerDateRange(DateTime day) :
            this(day, day)
        {
        }

        /// <summary>
        /// Initializes a new instance of the SchedulerDateRange class which accepts range start and end dates.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public SchedulerDateRange(DateTime start, DateTime end)
        {
            _start = start;
            _end = end;
        }

        #region Public Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Public Properties

        /// <summary>
        /// Specifies the End date of the SchedulerDateRange.
        /// </summary>
        public DateTime End
        {
            get => CoerceEnd(_start, _end);

            set
            {
                DateTime newEnd = CoerceEnd(_start, value);
                if (newEnd != End)
                {
                    OnChanging(new SchedulerDateRangeChangingEventArgs(_start, newEnd));
                    _end = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("End"));
                }
            }
        }

        /// <summary>
        /// Specifies the Start date of the SchedulerDateRange.
        /// </summary>
        public DateTime Start
        {
            get => _start;

            set
            {
                if (_start != value)
                {
                    DateTime oldEnd = End;
                    DateTime newEnd = CoerceEnd(value, _end);

                    OnChanging(new SchedulerDateRangeChangingEventArgs(value, newEnd));

                    _start = value;

                    OnPropertyChanged(new PropertyChangedEventArgs("Start"));

                    if (newEnd != oldEnd)
                    {
                        OnPropertyChanged(new PropertyChangedEventArgs("End"));
                    }
                }
            }
        }

        #endregion Public Properties

        #region Internal Events

        internal event EventHandler<SchedulerDateRangeChangingEventArgs> Changing;

        #endregion Internal Events

        #region Internal Methods

        /// <summary>
        /// Returns true if any day in the given DateTime range is contained in the current SchedulerDateRange.
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        internal bool ContainsAny(SchedulerDateRange range)
        {
            return (range.End >= Start) && (End >= range.Start);
        }

        #endregion Internal Methods

        #region Private Methods

        private void OnChanging(SchedulerDateRangeChangingEventArgs e)
        {
            Changing?.Invoke(this, e);
        }

        private void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            handler?.Invoke(this, e);
        }

        /// <summary>
        /// Coerced the end parameter to satisfy the start &lt;= end constraint
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns>If start &lt;= end the end parameter otherwise the start parameter</returns>
        private static DateTime CoerceEnd(DateTime start, DateTime end)
        {
            return (DateTime.Compare(start, end) <= 0) ? end : start;
        }

        #endregion Private Methods
    }
}
