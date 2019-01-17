using System.Windows;
using System.Windows.Controls;
using My.CoachManager.Presentation.Prism.Controls.Schedulers;

namespace My.CoachManager.Presentation.Prism.Controls
{

        /// <summary>
        /// Represents a button control used in Scheduler Control, which reacts to the Click event.
        /// </summary>
        public sealed class SchedulerDay : ItemsControl
        {

            /// <summary>
            /// Static constructor
            /// </summary>
            static SchedulerDay()
            {
                DefaultStyleKeyProperty.OverrideMetadata(typeof(SchedulerDay),
                    new FrameworkPropertyMetadata(typeof(SchedulerDay)));
            }

        #region Public Properties

            #region Content

            public static readonly DependencyProperty ContentProperty = DependencyProperty.Register(
                "Content",
                typeof(object),
                typeof(SchedulerDay),
                new FrameworkPropertyMetadata());

            /// <summary>
            /// True if the SchedulerDay represents today
            /// </summary>
            public object Content
            {
              get => (bool)GetValue(ContentProperty);
                set => SetValue(ContentProperty, value);
            } 

        #endregion Content

        #region IsToday

        internal static readonly DependencyPropertyKey IsTodayPropertyKey = DependencyProperty.RegisterReadOnly(
                "IsToday",
                typeof(bool),
                typeof(SchedulerDay),
                new FrameworkPropertyMetadata(false));

            /// <summary>
            /// Dependency property field for IsToday property
            /// </summary>
            public static readonly DependencyProperty IsTodayProperty = IsTodayPropertyKey.DependencyProperty;

            /// <summary>
            /// True if the SchedulerDay represents today
            /// </summary>
            public bool IsToday => (bool) GetValue(IsTodayProperty);

            #endregion IsToday

            #region IsSelected

            internal static readonly DependencyPropertyKey IsSelectedPropertyKey = DependencyProperty.RegisterReadOnly(
                "IsSelected",
                typeof(bool),
                typeof(SchedulerDay),
                new FrameworkPropertyMetadata(false));

            /// <summary>
            /// Dependency property field for IsSelected property
            /// </summary>
            public static readonly DependencyProperty IsSelectedProperty = IsSelectedPropertyKey.DependencyProperty;

            /// <summary>
            /// True if the SchedulerDay is selected
            /// </summary>
            public bool IsSelected => (bool) GetValue(IsSelectedProperty);

            #endregion IsSelected

            #region IsInactive

            internal static readonly DependencyPropertyKey IsInactivePropertyKey = DependencyProperty.RegisterReadOnly(
                "IsInactive",
                typeof(bool),
                typeof(SchedulerDay),
                new FrameworkPropertyMetadata(false));

            /// <summary>
            /// Dependency property field for IsActive property
            /// </summary>
            public static readonly DependencyProperty IsInactiveProperty = IsInactivePropertyKey.DependencyProperty;

            /// <summary>
            /// True if the SchedulerDay represents a day that falls in the currently displayed month
            /// </summary>
            public bool IsInactive => (bool) GetValue(IsInactiveProperty);

            #endregion IsInactive

            #region IsBlackedOut

            internal static readonly DependencyPropertyKey IsBlackedOutPropertyKey =
                DependencyProperty.RegisterReadOnly(
                    "IsBlackedOut",
                    typeof(bool),
                    typeof(SchedulerDay),
                    new FrameworkPropertyMetadata(false));

            /// <summary>
            /// Dependency property field for IsBlackedOut property
            /// </summary>
            public static readonly DependencyProperty IsBlackedOutProperty = IsBlackedOutPropertyKey.DependencyProperty;

            /// <summary>
            /// True if the SchedulerDay represents a blackout date
            /// </summary>
            public bool IsBlackedOut => (bool) GetValue(IsBlackedOutProperty);

            #endregion IsBlackedOut

            #region IsHighlighted

            internal static readonly DependencyPropertyKey IsHighlightedPropertyKey =
                DependencyProperty.RegisterReadOnly(
                    "IsHighlighted",
                    typeof(bool),
                    typeof(SchedulerDay),
                    new FrameworkPropertyMetadata(false));

            /// <summary>
            /// Dependency property field for IsHighlighted property
            /// </summary>
            public static readonly DependencyProperty IsHighlightedProperty =
                IsHighlightedPropertyKey.DependencyProperty;

            /// <summary>
            /// True if the SchedulerDay represents a highlighted date
            /// </summary>
            public bool IsHighlighted => (bool) GetValue(IsHighlightedProperty);

            #endregion IsHighlighted

            #endregion Public Properties

            #region Internal Properties

            internal Scheduler Owner { get; set; }

            #endregion Internal Properties

            #region Public Methods

            #endregion Public Methods

            #region Internal Methods

            internal void SetContentInternal(string value)
            {
                SetCurrentValue(ContentProperty, value);
            }

        #endregion Internal Methods

            protected override DependencyObject GetContainerForItemOverride()
            {
                return new SchedulerAppointment();
            }
        }
}
