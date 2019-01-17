using System.Windows;
using System.Windows.Controls;

namespace My.CoachManager.Presentation.Prism.Controls
{
    /// <summary>
    /// Represents a button control used in Scheduler Control, which reacts to the Click event.
    /// </summary>
    public sealed class SchedulerItem : ItemsControl
    {
        /// <summary>
        /// Static constructor
        /// </summary>
        static SchedulerItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SchedulerItem), new FrameworkPropertyMetadata(typeof(SchedulerItem)));
        }

        #region Public Properties

        #region Content

        public static readonly DependencyProperty ContentProperty = DependencyProperty.Register(
            "Content",
            typeof(object),
            typeof(SchedulerItem),
            new FrameworkPropertyMetadata(false));

        /// <summary>
        /// True if the SchedulerDay represents today
        /// </summary>
        public object Content
        {
            get => (bool)GetValue(ContentProperty);
            set => SetValue(ContentProperty, value);
        }

        #endregion Content

        #region HasSelectedDays

        internal static readonly DependencyPropertyKey HasSelectedDaysPropertyKey = DependencyProperty.RegisterReadOnly(
            "HasSelectedDays",
            typeof(bool),
            typeof(SchedulerItem),
            new FrameworkPropertyMetadata(false));

        /// <summary>
        /// Dependency property field for HasSelectedDays property
        /// </summary>
        public static readonly DependencyProperty HasSelectedDaysProperty = HasSelectedDaysPropertyKey.DependencyProperty;

        /// <summary>
        /// True if the SchedulerButton represents a date range containing the display date
        /// </summary>
        public bool HasSelectedDays
        {
            get => (bool)GetValue(HasSelectedDaysProperty);
            internal set => SetValue(HasSelectedDaysPropertyKey, value);
        }

        #endregion HasSelectedDays

        #region IsInactive

        internal static readonly DependencyPropertyKey IsInactivePropertyKey = DependencyProperty.RegisterReadOnly(
            "IsInactive",
            typeof(bool),
            typeof(SchedulerItem),
            new FrameworkPropertyMetadata(false));

        /// <summary>
        /// Dependency property field for IsInactive property
        /// </summary>
        public static readonly DependencyProperty IsInactiveProperty = IsInactivePropertyKey.DependencyProperty;

        /// <summary>
        /// True if the SchedulerButton represents
        ///     a month that falls outside the current year
        ///     or
        ///     a year that falls outside the current decade
        /// </summary>
        public bool IsInactive
        {
            get => (bool)GetValue(IsInactiveProperty);
            internal set => SetValue(IsInactivePropertyKey, value);
        }

        #endregion IsInactive

        #endregion Public Properties

        #region Internal Properties

        internal Scheduler Owner
        {
            get;
            set;
        }

        #endregion Internal Properties


        #region Internal Methods

        internal void SetContentInternal(string value)
        {
            SetCurrentValue(ContentProperty, value);
        }

        #endregion

    }
}
