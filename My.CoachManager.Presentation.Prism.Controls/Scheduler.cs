using My.CoachManager.CrossCutting.Core.Helpers;
using My.CoachManager.Presentation.Prism.Controls.Schedulers;
using My.CoachManager.Presentation.Prism.Core.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using SelectedDatesCollection = My.CoachManager.Presentation.Prism.Controls.Schedulers.SelectedDatesCollection;

namespace My.CoachManager.Presentation.Prism.Controls
{
    /// <summary>
    /// Represents a control that enables a user to select a date by using a visual Scheduler display.
    /// </summary>
    [TemplatePart(Name = ElementRoot, Type = typeof(Panel))]
    [TemplatePart(Name = ElementMonth, Type = typeof(SchedulerPanel))]
    [ContentProperty("Items")]
    public class Scheduler : ItemsControl
    {
        #region Constants

        private const string ElementRoot = "PART_Root";
        private const string ElementMonth = "PART_SchedulerPanel";

        public const int Cols = 7;
        public const int Rows = 7;
        private const int YearRows = 3;
        private const int YearCols = 4;
        private const int YearsPerDecade = 10;

        #endregion Constants

        #region Data

        private DateTime? _hoverStart;
        private DateTime? _hoverEnd;
        private bool _isShiftPressed;
        private DateTime? _currentDate;

        #endregion Data

        #region Public Events

        public static readonly RoutedEvent SelectedDatesChangedEvent = EventManager.RegisterRoutedEvent("SelectedDatesChanged", RoutingStrategy.Direct, typeof(EventHandler<SelectionChangedEventArgs>), typeof(Scheduler));

        /// <summary>
        /// Occurs when a date is selected.
        /// </summary>
        public event EventHandler<SelectionChangedEventArgs> SelectedDatesChanged
        {
            add => AddHandler(SelectedDatesChangedEvent, value);
            remove => RemoveHandler(SelectedDatesChangedEvent, value);
        }

        /// <summary>
        /// Occurs when the DisplayDate property is changed.
        /// </summary>
        public event EventHandler<SchedulerDateChangedEventArgs> DisplayDateChanged;

        /// <summary>
        /// Occurs when the DisplayMode property is changed.
        /// </summary>
        public event EventHandler<CalendarModeChangedEventArgs> DisplayModeChanged;

        /// <summary>
        /// Occurs when the SelectionMode property is changed.
        /// </summary>
        public event EventHandler<EventArgs> SelectionModeChanged;

        #endregion Public Events

        /// <summary>
        /// Static constructor
        /// </summary>
        static Scheduler()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Scheduler), new FrameworkPropertyMetadata(typeof(Scheduler)));
            KeyboardNavigation.TabNavigationProperty.OverrideMetadata(typeof(Scheduler), new FrameworkPropertyMetadata(KeyboardNavigationMode.Once));
            KeyboardNavigation.DirectionalNavigationProperty.OverrideMetadata(typeof(Scheduler), new FrameworkPropertyMetadata(KeyboardNavigationMode.Contained));
            LanguageProperty.OverrideMetadata(typeof(Scheduler), new FrameworkPropertyMetadata(OnLanguageChanged));

            EventManager.RegisterClassHandler(typeof(Scheduler), GotFocusEvent, new RoutedEventHandler(OnGotFocus));
        }

        /// <summary>
        /// Initializes a new instance of the Scheduler class.
        /// </summary>
        public Scheduler()
        {
            BlackoutDates = new SchedulerBlackoutDatesCollection(this);
            SelectedDatesInternal = new SelectedDatesCollection(this);
            SetCurrentValue(DisplayDateProperty, DateTime.Today);
        }

        #region Public Properties

        #region AppointmentCommand

        /// <summary>
        /// Gets or sets the style for displaying a SchedulerButton.
        /// </summary>
        public ICommand AppointmentCommand
        {
            get => (ICommand)GetValue(AppointmentCommandProperty);
            set => SetValue(AppointmentCommandProperty, value);
        }

        /// <summary>
        /// Identifies the SchedulerButtonStyle dependency property.
        /// </summary>
        public static readonly DependencyProperty AppointmentCommandProperty =
            DependencyProperty.Register(
                "AppointmentCommand",
                typeof(ICommand),
                typeof(Scheduler));

        #endregion AppointmentCommand

        #region AddCommand

        /// <summary>
        /// Gets or sets the style for displaying a SchedulerButton.
        /// </summary>
        public ICommand AddCommand
        {
            get => (ICommand)GetValue(AddCommandProperty);
            set => SetValue(AddCommandProperty, value);
        }

        /// <summary>
        /// Identifies the SchedulerButtonStyle dependency property.
        /// </summary>
        public static readonly DependencyProperty AddCommandProperty =
            DependencyProperty.Register(
                "AddCommand",
                typeof(ICommand),
                typeof(Scheduler));

        #endregion AddCommand

        #region BlackoutDates

        /// <summary>
        /// Gets or sets the dates that are not selectable.
        /// </summary>
        public SchedulerBlackoutDatesCollection BlackoutDates { get; }

        #endregion BlackoutDates

        #region SchedulerPanelStyle

        /// <summary>
        /// Gets or sets the style for displaying a SchedulerButton.
        /// </summary>
        public Style SchedulerPanelStyle
        {
            get => (Style)GetValue(SchedulerPanelStyleProperty);
            set => SetValue(SchedulerPanelStyleProperty, value);
        }

        /// <summary>
        /// Identifies the SchedulerButtonStyle dependency property.
        /// </summary>
        public static readonly DependencyProperty SchedulerPanelStyleProperty =
            DependencyProperty.Register(
            "SchedulerPanelStyle",
            typeof(Style),
            typeof(Scheduler));

        #endregion SchedulerPanelStyle

        #region SchedulerDayStyle

        /// <summary>
        /// Gets or sets the style for displaying a day.
        /// </summary>
        public Style SchedulerDayStyle
        {
            get => (Style)GetValue(SchedulerDayStyleProperty);
            set => SetValue(SchedulerDayStyleProperty, value);
        }

        /// <summary>
        /// Identifies the DayButtonStyle dependency property.
        /// </summary>
        public static readonly DependencyProperty SchedulerDayStyleProperty =
            DependencyProperty.Register(
            "SchedulerDayStyle",
            typeof(Style),
            typeof(Scheduler));

        #endregion SchedulerDayStyle

        #region SchedulerItemStyle

        /// <summary>
        /// Gets or sets the style for a Month.
        /// </summary>
        public Style SchedulerItemStyle
        {
            get => (Style)GetValue(SchedulerItemStyleProperty);
            set => SetValue(SchedulerItemStyleProperty, value);
        }

        /// <summary>
        /// Identifies the MonthStyle dependency property.
        /// </summary>
        public static readonly DependencyProperty SchedulerItemStyleProperty =
            DependencyProperty.Register(
            "SchedulerItemStyle",
            typeof(Style),
            typeof(Scheduler));

        #endregion SchedulerItemStyle

        #region DisplayDate

        internal DateTime DisplayDateInternal
        {
            get;
            private set;
        }

        internal DateTime DisplayDateEndInternal => DisplayDateEnd.GetValueOrDefault(DateTime.MaxValue);

        internal DateTime DisplayDateStartInternal => DisplayDateStart.GetValueOrDefault(DateTime.MinValue);

        internal DateTime CurrentDate
        {
            get => _currentDate.GetValueOrDefault(DisplayDateInternal);
            set => _currentDate = value;
        }

        /// <summary>
        /// Gets or sets the date to display.
        /// </summary>
        ///
        public DateTime DisplayDate
        {
            get => (DateTime)GetValue(DisplayDateProperty);
            set => SetValue(DisplayDateProperty, value);
        }

        /// <summary>
        /// Identifies the DisplayDate dependency property.
        /// </summary>
        public static readonly DependencyProperty DisplayDateProperty =
            DependencyProperty.Register(
            "DisplayDate",
            typeof(DateTime),
            typeof(Scheduler),
            new FrameworkPropertyMetadata(DateTime.MinValue, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnDisplayDateChanged, CoerceDisplayDate));

        /// <summary>
        /// DisplayDateProperty property changed handler.
        /// </summary>
        /// <param name="d">Scheduler that changed its DisplayDate.</param>
        /// <param name="e">DependencyPropertyChangedEventArgs.</param>
        private static void OnDisplayDateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Scheduler c = d as Scheduler;
            Debug.Assert(c != null);

            c.DisplayDateInternal = DateTimeHelper.DiscardDayTime((DateTime)e.NewValue);
            // c.UpdateCellItems();
            c.OnDisplayDateChanged(new SchedulerDateChangedEventArgs((DateTime)e.OldValue, (DateTime)e.NewValue));
        }

        private static object CoerceDisplayDate(DependencyObject d, object value)
        {
            Scheduler c = d as Scheduler;

            DateTime date = (DateTime)value;
            if (c?.DisplayDateStart != null && (date < c.DisplayDateStart.Value))
            {
                value = c.DisplayDateStart.Value;
            }
            else if (c?.DisplayDateEnd != null && (date > c.DisplayDateEnd.Value))
            {
                value = c.DisplayDateEnd.Value;
            }

            return value;
        }

        #endregion DisplayDate

        #region DisplayDateEnd

        /// <summary>
        /// Gets or sets the last date to be displayed.
        /// </summary>
        ///
        public DateTime? DisplayDateEnd
        {
            get => (DateTime?)GetValue(DisplayDateEndProperty);
            set => SetValue(DisplayDateEndProperty, value);
        }

        /// <summary>
        /// Identifies the DisplayDateEnd dependency property.
        /// </summary>
        public static readonly DependencyProperty DisplayDateEndProperty =
            DependencyProperty.Register(
            "DisplayDateEnd",
            typeof(DateTime?),
            typeof(Scheduler),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnDisplayDateEndChanged, CoerceDisplayDateEnd));

        /// <summary>
        /// DisplayDateEndProperty property changed handler.
        /// </summary>
        /// <param name="d">Scheduler that changed its DisplayDateEnd.</param>
        /// <param name="e">DependencyPropertyChangedEventArgs.</param>
        private static void OnDisplayDateEndChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Scheduler c = d as Scheduler;
            Debug.Assert(c != null);

            c.CoerceValue(DisplayDateProperty);
            c.UpdateCellItems();
        }

        private static object CoerceDisplayDateEnd(DependencyObject d, object value)
        {
            Scheduler c = d as Scheduler;

            DateTime? date = (DateTime?)value;

            if (date.HasValue)
            {
                if (c?.DisplayDateStart != null && (date.Value < c.DisplayDateStart.Value))
                {
                    value = c.DisplayDateStart;
                }

                DateTime? maxSelectedDate = c?.SelectedDatesInternal.MaximumDate;
                if (maxSelectedDate != null && (date.Value < maxSelectedDate.Value))
                {
                    value = maxSelectedDate;
                }
            }

            return value;
        }

        #endregion DisplayDateEnd

        #region DisplayDateStart

        /// <summary>
        /// Gets or sets the first date to be displayed.
        /// </summary>
        ///
        public DateTime? DisplayDateStart
        {
            get => (DateTime?)GetValue(DisplayDateStartProperty);
            set => SetValue(DisplayDateStartProperty, value);
        }

        /// <summary>
        /// Identifies the DisplayDateStart dependency property.
        /// </summary>
        public static readonly DependencyProperty DisplayDateStartProperty =
            DependencyProperty.Register(
            "DisplayDateStart",
            typeof(DateTime?),
            typeof(Scheduler),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnDisplayDateStartChanged, CoerceDisplayDateStart));

        /// <summary>
        /// DisplayDateStartProperty property changed handler.
        /// </summary>
        /// <param name="d">Scheduler that changed its DisplayDateStart.</param>
        /// <param name="e">DependencyPropertyChangedEventArgs.</param>
        private static void OnDisplayDateStartChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Scheduler c = d as Scheduler;
            Debug.Assert(c != null);

            c.CoerceValue(DisplayDateEndProperty);
            c.CoerceValue(DisplayDateProperty);
            c.UpdateCellItems();
        }

        private static object CoerceDisplayDateStart(DependencyObject d, object value)
        {
            Scheduler c = d as Scheduler;

            DateTime? date = (DateTime?)value;

            if (date.HasValue)
            {
                DateTime? minSelectedDate = c?.SelectedDatesInternal.MinimumDate;
                if (minSelectedDate != null && (date.Value > minSelectedDate.Value))
                {
                    value = minSelectedDate;
                }
            }

            return value;
        }

        #endregion DisplayDateStart

        #region DisplayMode

        /// <summary>
        /// Gets or sets a value indicating whether the Scheduler is displayed in months or years.
        /// </summary>
        public CalendarMode DisplayMode
        {
            get => (CalendarMode)GetValue(DisplayModeProperty);
            set => SetValue(DisplayModeProperty, value);
        }

        /// <summary>
        /// Identifies the DisplayMode dependency property.
        /// </summary>
        public static readonly DependencyProperty DisplayModeProperty =
            DependencyProperty.Register(
            "DisplayMode",
            typeof(CalendarMode),
            typeof(Scheduler),
            new FrameworkPropertyMetadata(CalendarMode.Month, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnDisplayModePropertyChanged),
            IsValidDisplayMode);

        /// <summary>
        /// DisplayModeProperty property changed handler.
        /// </summary>
        /// <param name="d">Scheduler that changed its DisplayMode.</param>
        /// <param name="e">DependencyPropertyChangedEventArgs.</param>
        private static void OnDisplayModePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Scheduler c = d as Scheduler;
            Debug.Assert(c != null);
            CalendarMode mode = (CalendarMode)e.NewValue;
            CalendarMode oldMode = (CalendarMode)e.OldValue;

            switch (mode)
            {
                case CalendarMode.Month:
                    {
                        if (oldMode == CalendarMode.Year || oldMode == CalendarMode.Decade)
                        {
                            // Cancel highlight when switching to month display mode
                            c.HoverStart = c.HoverEnd = null;
                            c.CurrentDate = c.DisplayDate;
                        }

                        c.UpdateCellItems();
                        break;
                    }

                case CalendarMode.Year:
                case CalendarMode.Decade:
                    if (oldMode == CalendarMode.Month)
                    {
                        c.SetCurrentValue(DisplayDateProperty, c.CurrentDate);
                    }

                    c.UpdateCellItems();
                    break;

                default:
                    Debug.Assert(false);
                    break;
            }

            c.OnDisplayModeChanged(new CalendarModeChangedEventArgs((CalendarMode)e.OldValue, mode));
        }

        #endregion DisplayMode

        #region FirstDayOfWeek

        /// <summary>
        /// Gets or sets the day that is considered the beginning of the week.
        /// </summary>
        public DayOfWeek FirstDayOfWeek
        {
            get => (DayOfWeek)GetValue(FirstDayOfWeekProperty);
            set => SetValue(FirstDayOfWeekProperty, value);
        }

        /// <summary>
        /// Identifies the FirstDayOfWeek dependency property.
        /// </summary>
        public static readonly DependencyProperty FirstDayOfWeekProperty =
            DependencyProperty.Register(
            "FirstDayOfWeek",
            typeof(DayOfWeek),
            typeof(Scheduler),
            new FrameworkPropertyMetadata(DateTimeHelper.GetCurrentDateFormat().FirstDayOfWeek,
                                            OnFirstDayOfWeekChanged),
            IsValidFirstDayOfWeek);

        /// <summary>
        /// FirstDayOfWeekProperty property changed handler.
        /// </summary>
        /// <param name="d">Scheduler that changed its FirstDayOfWeek.</param>
        /// <param name="e">DependencyPropertyChangedEventArgs.</param>
        private static void OnFirstDayOfWeekChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Scheduler c = d as Scheduler;
            c?.UpdateCellItems();
        }

        #endregion FirstDayOfWeek

        #region IsTodayHighlighted

        /// <summary>
        /// Gets or sets a value indicating whether the current date is highlighted.
        /// </summary>
        public bool IsTodayHighlighted
        {
            get => (bool)GetValue(IsTodayHighlightedProperty);
            set => SetValue(IsTodayHighlightedProperty, value);
        }

        /// <summary>
        /// Identifies the IsTodayHighlighted dependency property.
        /// </summary>
        public static readonly DependencyProperty IsTodayHighlightedProperty =
            DependencyProperty.Register(
            "IsTodayHighlighted",
            typeof(bool),
            typeof(Scheduler),
            new FrameworkPropertyMetadata(true, OnIsTodayHighlightedChanged));

        /// <summary>
        /// IsTodayHighlightedProperty property changed handler.
        /// </summary>
        /// <param name="d">Scheduler that changed its IsTodayHighlighted.</param>
        /// <param name="e">DependencyPropertyChangedEventArgs.</param>
        private static void OnIsTodayHighlightedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Scheduler c)
            {
                int i = DateTimeHelper.CompareYearMonth(c.DisplayDateInternal, DateTime.Today);

                if (i <= -2 || i >= 2)
                {
                    c.UpdateCellItems();
                }
            }
        }

        #endregion IsTodayHighlighted

        #region Language

        private static void OnLanguageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Scheduler c = d as Scheduler;
            if (DependencyPropertyHelper.GetValueSource(d, FirstDayOfWeekProperty).BaseValueSource == BaseValueSource.Default)
            {
                if (c != null)
                {
                    c.SetCurrentValue(FirstDayOfWeekProperty,
                        DateTimeHelper.GetDateFormat(DateTimeHelper.GetCulture()).FirstDayOfWeek);
                    c.UpdateCellItems();
                }
            }
        }

        #endregion Language

        #region SelectedDate

        /// <summary>
        /// Gets or sets the currently selected date.
        /// </summary>
        ///
        public DateTime? SelectedDate
        {
            get => (DateTime?)GetValue(SelectedDateProperty);
            set => SetValue(SelectedDateProperty, value);
        }

        /// <summary>
        /// Identifies the SelectedDate dependency property.
        /// </summary>
        public static readonly DependencyProperty SelectedDateProperty =
            DependencyProperty.Register(
            "SelectedDate",
            typeof(DateTime?),
            typeof(Scheduler),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnSelectedDateChanged));

        /// <summary>
        /// SelectedDateProperty property changed handler.
        /// </summary>
        /// <param name="d">Scheduler that changed its SelectedDate.</param>
        /// <param name="e">DependencyPropertyChangedEventArgs.</param>
        private static void OnSelectedDateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Scheduler c = d as Scheduler;
            Debug.Assert(c != null);

            if (c.SelectionMode != CalendarSelectionMode.None || e.NewValue == null)
            {
                var addedDate = (DateTime?)e.NewValue;

                if (IsValidDateSelection(c, addedDate))
                {
                    if (!addedDate.HasValue)
                    {
                        c.SelectedDatesInternal.Clear();
                    }
                    else
                    {
                        if (!(c.SelectedDatesInternal.Count > 0 && c.SelectedDatesInternal[0] == addedDate.Value))
                        {
                            c.SelectedDatesInternal.Clear();
                            c.SelectedDatesInternal.Add(addedDate.Value);
                        }
                    }

                    // We update the current date for only the Single mode.For the other modes it automatically gets updated
                    if (c.SelectionMode == CalendarSelectionMode.SingleDate)
                    {
                        if (addedDate.HasValue)
                        {
                            c.CurrentDate = addedDate.Value;
                        }
                    }
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(d));
                }
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        #endregion SelectedDate

        #region SelectedDatesInternal

        /// <summary>
        /// Gets the dates that are currently selected.
        /// </summary>
        internal SelectedDatesCollection SelectedDatesInternal { get; set; }

        internal static readonly DependencyProperty SelectedDatesProperty = DependencyProperty.Register(
            "SelectedDates",
            typeof(IList<DateTime>),
            typeof(Scheduler),
            new FrameworkPropertyMetadata(new List<DateTime>()));

        /// <summary>
        /// True if the SchedulerDay represents a day that falls in the currently displayed month
        /// </summary>
        public IList<DateTime> SelectedDates
        {
            get => (IList<DateTime>)GetValue(SelectedDatesProperty);
            set => SetValue(SelectedDatesProperty, value);
        }

        #endregion SelectedDatesInternal

        #region SelectionMode

        /// <summary>
        /// Gets or sets the selection mode for the Scheduler.
        /// </summary>
        public CalendarSelectionMode SelectionMode
        {
            get => (CalendarSelectionMode)GetValue(SelectionModeProperty);
            set => SetValue(SelectionModeProperty, value);
        }

        /// <summary>
        /// Identifies the SelectionMode dependency property.
        /// </summary>
        public static readonly DependencyProperty SelectionModeProperty =
            DependencyProperty.Register(
            "SelectionMode",
            typeof(CalendarSelectionMode),
            typeof(Scheduler),
            new FrameworkPropertyMetadata(CalendarSelectionMode.SingleDate, OnSelectionModeChanged),
            IsValidSelectionMode);

        private static void OnSelectionModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Scheduler c = d as Scheduler;
            Debug.Assert(c != null);

            c.HoverStart = c.HoverEnd = null;
            c.SelectedDatesInternal.Clear();
            c.OnSelectionModeChanged(EventArgs.Empty);
        }

        #endregion SelectionMode

        #endregion Public Properties

        #region Internal Events

        internal event MouseButtonEventHandler DayButtonMouseUp;

        internal event RoutedEventHandler DayOrMonthPreviewKeyDown;

        #endregion Internal Events

        #region Internal Properties

        /// <summary>
        /// This flag is used to determine whether DatePicker should change its
        /// DisplayDate because of a SelectedDate change on its Scheduler
        /// </summary>
        internal bool DatePickerDisplayDateFlag
        {
            get;
            set;
        }

        internal DateTime? HoverStart
        {
            get => SelectionMode == CalendarSelectionMode.None ? null : _hoverStart;

            set => _hoverStart = value;
        }

        internal DateTime? HoverEnd
        {
            get => SelectionMode == CalendarSelectionMode.None ? null : _hoverEnd;

            set => _hoverEnd = value;
        }

        internal SchedulerPanel MonthControl { get; private set; }

        internal DateTime DisplayMonth => DateTimeHelper.DiscardDayTime(DisplayDate);

        internal DateTime DisplayYear => new DateTime(DisplayDate.Year, 1, 1);

        #endregion Internal Properties

        #region Public Methods

        /// <summary>
        /// Invoked whenever application code or an internal process,
        /// such as a rebuilding layout pass, calls the ApplyTemplate method.
        /// </summary>
        public override void OnApplyTemplate()
        {
            if (MonthControl != null)
            {
                MonthControl.Owner = null;
            }

            base.OnApplyTemplate();

            MonthControl = GetTemplateChild(ElementMonth) as SchedulerPanel;

            if (MonthControl != null)
            {
                MonthControl.Owner = this;
            }

            CurrentDate = DisplayDate;
        }

        /// <summary>
        /// Provides a text representation of the selected date.
        /// </summary>
        /// <returns>A text representation of the selected date, or an empty string if SelectedDate is a null reference.</returns>
        public override string ToString()
        {
            if (SelectedDate != null)
            {
                return SelectedDate.Value.ToString(DateTimeHelper.GetDateFormat(DateTimeHelper.GetCulture()));
            }
            else
            {
                return string.Empty;
            }
        }

        #endregion Public Methods

        #region Protected Methods

        protected virtual void OnSelectedDatesChanged(SelectionChangedEventArgs e)
        {
            RaiseEvent(e);
        }

        protected virtual void OnDisplayDateChanged(SchedulerDateChangedEventArgs e)
        {
            DisplayDateChanged?.Invoke(this, e);
        }

        protected virtual void OnDisplayModeChanged(CalendarModeChangedEventArgs e)
        {
            DisplayModeChanged?.Invoke(this, e);
        }

        protected virtual void OnSelectionModeChanged(EventArgs e)
        {
            EventHandler<EventArgs> handler = SelectionModeChanged;

            handler?.Invoke(this, e);
        }

        /// <summary>
        /// Creates the automation peer for this Scheduler Control.
        /// </summary>
        /// <returns></returns>
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new SchedulerAutomationPeer(this);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (!e.Handled)
            {
                e.Handled = ProcessSchedulerKey(e);
            }
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (!e.Handled)
            {
                if (e.Key == Key.LeftShift || e.Key == Key.RightShift)
                {
                    ProcessShiftKeyUp();
                }
            }
        }

        #endregion Protected Methods

        #region Internal Methods

        internal SchedulerDay FindDayButtonFromDay(DateTime day)
        {
            if (MonthControl != null)
            {
                foreach (SchedulerDay b in MonthControl.GetSchedulerDays())
                {
                    if (b.DataContext is DateTime)
                    {
                        if (DateTimeHelper.CompareDays((DateTime)b.DataContext, day) == 0)
                        {
                            return b;
                        }
                    }
                }
            }

            return null;
        }

        internal static bool IsValidDateSelection(Scheduler cal, object value)
        {
            return (value == null) || (!cal.BlackoutDates.Contains((DateTime)value));
        }

        internal void OnDayButtonMouseUp(MouseButtonEventArgs e)
        {
            MouseButtonEventHandler handler = DayButtonMouseUp;
            handler?.Invoke(this, e);
        }

        internal void OnDayOrMonthPreviewKeyDown(RoutedEventArgs e)
        {
            RoutedEventHandler handler = DayOrMonthPreviewKeyDown;
            handler?.Invoke(this, e);
        }

        // If the day is a trailing day, Update the DisplayDate
        internal void OnDayClick(DateTime selectedDate)
        {
            if (SelectionMode == CalendarSelectionMode.None)
            {
                CurrentDate = selectedDate;
            }

            //if (DateTimeHelper.CompareYearMonth(selectedDate, DisplayDateInternal) != 0)
            //{
            //    MoveDisplayTo(selectedDate);
            //}
            //else
            //{
            //UpdateCellItems();
            //UpdateSelection();
            FocusDate(selectedDate);
            //}
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);

            //switch (DisplayMode)
            //{
            //    case CalendarMode.Month:
            //        {
            //            var value = e.Delta < 0 ? 1 : -1;
            //            DateTime? selectedDate = BlackoutDates.GetNonBlackoutDate(DateTimeHelper.AddMonths(CurrentDate, value), value);
            //            ProcessSelection(false, selectedDate);
            //            break;
            //        }

            //    case CalendarMode.Year:
            //        {
            //            var value = e.Delta < 0 ? 1 : -1;
            //            DateTime? selectedMonth = DateTimeHelper.AddYears(DisplayDate, value);
            //            OnSelectedMonthChanged(selectedMonth);
            //            break;
            //        }

            //    case CalendarMode.Decade:
            //        {
            //            var value = e.Delta < 0 ? 10 : -10;
            //            DateTime? selectedYear = DateTimeHelper.AddYears(DisplayDate, value);
            //            OnSelectedYearChanged(selectedYear);
            //            break;
            //        }
            //}
        }

        internal void OnSchedulerItemPressed(SchedulerItem b, bool switchDisplayMode)
        {
            if (b.DataContext is DateTime)
            {
                DateTime d = (DateTime)b.DataContext;

                DateTime? newDate = null;
                CalendarMode newMode = CalendarMode.Month;

                switch (DisplayMode)
                {
                    case CalendarMode.Month:
                        {
                            Debug.Assert(false);
                            break;
                        }

                    case CalendarMode.Year:
                        {
                            newDate = DateTimeHelper.SetYearMonth(DisplayDate, d);
                            newMode = CalendarMode.Month;
                            break;
                        }

                    case CalendarMode.Decade:
                        {
                            newDate = DateTimeHelper.SetYear(DisplayDate, d.Year);
                            newMode = CalendarMode.Year;
                            break;
                        }

                    default:
                        Debug.Assert(false);
                        break;
                }

                if (newDate.HasValue)
                {
                    DisplayDate = newDate.Value;
                    if (switchDisplayMode)
                    {
                        SetCurrentValue(DisplayModeProperty, newMode);
                        FocusDate(DisplayMode == CalendarMode.Month ? CurrentDate : DisplayDate);
                    }
                }
            }
        }

        private DateTime? GetDateOffset(DateTime date, int offset, CalendarMode displayMode)
        {
            DateTime? result = null;
            switch (displayMode)
            {
                case CalendarMode.Month:
                    {
                        result = DateTimeHelper.AddMonths(date, offset);
                        break;
                    }

                case CalendarMode.Year:
                    {
                        result = DateTimeHelper.AddYears(date, offset);
                        break;
                    }

                case CalendarMode.Decade:
                    {
                        result = DateTimeHelper.AddYears(DisplayDate, offset * YearsPerDecade);
                        break;
                    }

                default:
                    Debug.Assert(false);
                    break;
            }

            return result;
        }

        private void MoveDisplayTo(DateTime? date)
        {
            if (date.HasValue)
            {
                DateTime d = date.Value.Date;
                switch (DisplayMode)
                {
                    case CalendarMode.Month:
                        {
                            SetCurrentValue(DisplayDateProperty, DateTimeHelper.DiscardDayTime(d));
                            CurrentDate = d;
                            UpdateCellItems();

                            break;
                        }

                    case CalendarMode.Year:
                    case CalendarMode.Decade:
                        {
                            SetCurrentValue(DisplayDateProperty, d);
                            UpdateCellItems();

                            break;
                        }

                    default:
                        Debug.Assert(false);
                        break;
                }

                FocusDate(d);
            }
        }

        internal void OnNextClick()
        {
            DateTime? nextDate = GetDateOffset(DisplayDate, 1, DisplayMode);
            if (nextDate.HasValue)
            {
                MoveDisplayTo(DateTimeHelper.DiscardDayTime(nextDate.Value));
            }
        }

        internal void OnPreviousClick()
        {
            DateTime? nextDate = GetDateOffset(DisplayDate, -1, DisplayMode);
            if (nextDate.HasValue)
            {
                MoveDisplayTo(DateTimeHelper.DiscardDayTime(nextDate.Value));
            }
        }

        internal void OnSelectedDatesCollectionChanged(SelectionChangedEventArgs e)
        {
            if (IsSelectionChanged(e))
            {
                if (AutomationPeer.ListenerExists(AutomationEvents.SelectionItemPatternOnElementSelected) ||
                    AutomationPeer.ListenerExists(AutomationEvents.SelectionItemPatternOnElementAddedToSelection) ||
                    AutomationPeer.ListenerExists(AutomationEvents.SelectionItemPatternOnElementRemovedFromSelection))
                {
                    if (UIElementAutomationPeer.FromElement(this) is SchedulerAutomationPeer peer)
                    {
                        peer.RaiseSelectionEvents(e);
                    }
                }

                CoerceFromSelection();

                SelectedDates = SelectedDatesInternal.ToList();
                SelectedDate = SelectedDatesInternal.FirstOrDefault();
                OnSelectedDatesChanged(e);
            }
        }

        internal void UpdateCellItems()
        {
            SchedulerPanel monthControl = MonthControl;
            if (monthControl != null)
            {
                switch (DisplayMode)
                {
                    case CalendarMode.Month:
                        {
                            monthControl.UpdateMonthMode();
                            break;
                        }

                    case CalendarMode.Year:
                        {
                            monthControl.UpdateYearMode();
                            break;
                        }

                    case CalendarMode.Decade:
                        {
                            monthControl.UpdateDecadeMode();
                            break;
                        }

                    default:
                        Debug.Assert(false);
                        break;
                }

                UpdateAppointments();
            }
        }

        #endregion Internal Methods

        #region Private Methods

        private void CoerceFromSelection()
        {
            CoerceValue(DisplayDateStartProperty);
            CoerceValue(DisplayDateEndProperty);
            CoerceValue(DisplayDateProperty);
        }

        // This method adds the days that were selected by Keyboard to the SelectedDays Collection
        private void AddKeyboardSelection()
        {
            if (HoverStart != null)
            {
                SelectedDatesInternal.Clear();

                // In keyboard selection, we are sure that the collection does not include any blackout days
                SelectedDatesInternal.AddRange(HoverStart.Value, CurrentDate);
            }
        }

        private static bool IsSelectionChanged(SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count != e.RemovedItems.Count)
            {
                return true;
            }

            foreach (DateTime addedDate in e.AddedItems)
            {
                if (!e.RemovedItems.Contains(addedDate))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool IsValidDisplayMode(object value)
        {
            CalendarMode mode = (CalendarMode)value;

            return mode == CalendarMode.Month
                || mode == CalendarMode.Year
                || mode == CalendarMode.Decade;
        }

        internal static bool IsValidFirstDayOfWeek(object value)
        {
            DayOfWeek day = (DayOfWeek)value;

            return day == DayOfWeek.Sunday
                || day == DayOfWeek.Monday
                || day == DayOfWeek.Tuesday
                || day == DayOfWeek.Wednesday
                || day == DayOfWeek.Thursday
                || day == DayOfWeek.Friday
                || day == DayOfWeek.Saturday;
        }

        private static bool IsValidKeyboardSelection(Scheduler cal, object value)
        {
            if (value == null)
            {
                return true;
            }
            else
            {
                if (cal.BlackoutDates.Contains((DateTime)value))
                {
                    return false;
                }
                else
                {
                    return DateTime.Compare((DateTime)value, cal.DisplayDateStartInternal) >= 0 && DateTime.Compare((DateTime)value, cal.DisplayDateEndInternal) <= 0;
                }
            }
        }

        private static bool IsValidSelectionMode(object value)
        {
            CalendarSelectionMode mode = (CalendarSelectionMode)value;

            return mode == CalendarSelectionMode.SingleDate
                || mode == CalendarSelectionMode.SingleRange
                || mode == CalendarSelectionMode.MultipleRange
                || mode == CalendarSelectionMode.None;
        }

        private void OnSelectedMonthChanged(DateTime? selectedMonth)
        {
            if (selectedMonth.HasValue)
            {
                Debug.Assert(DisplayMode == CalendarMode.Year);
                SetCurrentValue(DisplayDateProperty, selectedMonth.Value);

                UpdateCellItems();

                FocusDate(selectedMonth.Value);
            }
        }

        private void OnSelectedYearChanged(DateTime? selectedYear)
        {
            if (selectedYear.HasValue)
            {
                Debug.Assert(DisplayMode == CalendarMode.Decade);
                SetCurrentValue(DisplayDateProperty, selectedYear.Value);

                UpdateCellItems();

                FocusDate(selectedYear.Value);
            }
        }

        internal void FocusDate(DateTime date)
        {
            if (MonthControl != null)
            {
                MonthControl.FocusDate(date);
            }
        }

        /// <summary>
        ///     Called when this element gets focus.
        /// </summary>
        private static void OnGotFocus(object sender, RoutedEventArgs e)
        {
            // When Scheduler gets focus move it to the DisplayDate
            var c = (Scheduler)sender;
            if (!e.Handled && Equals(e.OriginalSource, c))
            {
                // This check is for the case where the DisplayDate is the first of the month
                // and the SelectedDate is in the middle of the month.  If you tab into the Scheduler
                // the focus should go to the SelectedDate, not the DisplayDate.
                if (c.SelectedDate.HasValue && DateTimeHelper.CompareYearMonth(c.SelectedDate.Value, c.DisplayDateInternal) == 0)
                {
                    c.FocusDate(c.SelectedDate.Value);
                }
                else
                {
                    c.FocusDate(c.DisplayDate);
                }

                e.Handled = true;
            }
        }

        private bool ProcessSchedulerKey(KeyEventArgs e)
        {
            if (DisplayMode == CalendarMode.Month)
            {
                // If a blackout day is inactive, when clicked on it, the previous inactive day which is not a blackout day can get the focus.
                // In this case we should allow keyboard functions on that inactive day
                SchedulerDay currentDayButton = MonthControl?.GetSchedulerDay(CurrentDate);

                if (DateTimeHelper.CompareYearMonth(CurrentDate, DisplayDateInternal) != 0 && currentDayButton != null && !currentDayButton.IsInactive)
                {
                    return false;
                }
            }

            bool ctrl, shift;
            SchedulerKeyboardHelper.GetMetaKeyState(out ctrl, out shift);

            switch (e.Key)
            {
                case Key.Up:
                    {
                        ProcessUpKey(ctrl, shift);
                        return true;
                    }

                case Key.Down:
                    {
                        ProcessDownKey(ctrl, shift);
                        return true;
                    }

                case Key.Left:
                    {
                        ProcessLeftKey(shift);
                        return true;
                    }

                case Key.Right:
                    {
                        ProcessRightKey(shift);
                        return true;
                    }

                case Key.PageDown:
                    {
                        ProcessPageDownKey(shift);
                        return true;
                    }

                case Key.PageUp:
                    {
                        ProcessPageUpKey(shift);
                        return true;
                    }

                case Key.Home:
                    {
                        ProcessHomeKey(shift);
                        return true;
                    }

                case Key.End:
                    {
                        ProcessEndKey(shift);
                        return true;
                    }

                case Key.Enter:
                case Key.Space:
                    {
                        return ProcessEnterKey();
                    }
            }

            return false;
        }

        private void ProcessDownKey(bool ctrl, bool shift)
        {
            switch (DisplayMode)
            {
                case CalendarMode.Month:
                    {
                        if (!ctrl || shift)
                        {
                            DateTime? selectedDate = BlackoutDates.GetNonBlackoutDate(DateTimeHelper.AddDays(CurrentDate, Cols), 1);
                            ProcessSelection(shift, selectedDate);
                        }

                        break;
                    }

                case CalendarMode.Year:
                    {
                        if (ctrl)
                        {
                            SetCurrentValue(DisplayModeProperty, CalendarMode.Month);
                            FocusDate(DisplayDate);
                        }
                        else
                        {
                            DateTime? selectedMonth = DateTimeHelper.AddMonths(DisplayDate, YearCols);
                            OnSelectedMonthChanged(selectedMonth);
                        }

                        break;
                    }

                case CalendarMode.Decade:
                    {
                        if (ctrl)
                        {
                            SetCurrentValue(DisplayModeProperty, CalendarMode.Year);
                            FocusDate(DisplayDate);
                        }
                        else
                        {
                            DateTime? selectedYear = DateTimeHelper.AddYears(DisplayDate, YearCols);
                            OnSelectedYearChanged(selectedYear);
                        }

                        break;
                    }
            }
        }

        private void ProcessEndKey(bool shift)
        {
            switch (DisplayMode)
            {
                case CalendarMode.Month:
                    {
                        {
                            DateTime? selectedDate = new DateTime(DisplayDateInternal.Year, DisplayDateInternal.Month, 1);

                            if (DateTimeHelper.CompareYearMonth(DateTime.MaxValue, selectedDate.Value) > 0)
                            {
                                // since DisplayDate is not equal to DateTime.MaxValue we are sure selectedDate is not null
                                selectedDate = DateTimeHelper.AddMonths(selectedDate.Value, 1).Value;
                                selectedDate = DateTimeHelper.AddDays(selectedDate.Value, -1).Value;
                            }
                            else
                            {
                                selectedDate = DateTime.MaxValue;
                            }

                            ProcessSelection(shift, selectedDate);
                        }

                        break;
                    }

                case CalendarMode.Year:
                    {
                        DateTime selectedMonth = new DateTime(DisplayDate.Year, 12, 1);
                        OnSelectedMonthChanged(selectedMonth);
                        break;
                    }

                case CalendarMode.Decade:
                    {
                        DateTime? selectedYear = new DateTime(DateTimeHelper.EndOfDecade(DisplayDate), 1, 1);
                        OnSelectedYearChanged(selectedYear);
                        break;
                    }
            }
        }

        private bool ProcessEnterKey()
        {
            switch (DisplayMode)
            {
                case CalendarMode.Year:
                    {
                        SetCurrentValue(DisplayModeProperty, CalendarMode.Month);
                        FocusDate(DisplayDate);
                        return true;
                    }

                case CalendarMode.Decade:
                    {
                        SetCurrentValue(DisplayModeProperty, CalendarMode.Year);
                        FocusDate(DisplayDate);
                        return true;
                    }
            }

            return false;
        }

        private void ProcessHomeKey(bool shift)
        {
            switch (DisplayMode)
            {
                case CalendarMode.Month:
                    {
                        //
                        DateTime? selectedDate = new DateTime(DisplayDateInternal.Year, DisplayDateInternal.Month, 1);
                        ProcessSelection(shift, selectedDate);
                        break;
                    }

                case CalendarMode.Year:
                    {
                        DateTime selectedMonth = new DateTime(DisplayDate.Year, 1, 1);
                        OnSelectedMonthChanged(selectedMonth);
                        break;
                    }

                case CalendarMode.Decade:
                    {
                        DateTime? selectedYear = new DateTime(DateTimeHelper.DecadeOfDate(DisplayDate), 1, 1);
                        OnSelectedYearChanged(selectedYear);
                        break;
                    }
            }
        }

        private void ProcessLeftKey(bool shift)
        {
            int moveAmmount = -1;
            switch (DisplayMode)
            {
                case CalendarMode.Month:
                    {
                        DateTime? selectedDate = BlackoutDates.GetNonBlackoutDate(DateTimeHelper.AddDays(CurrentDate, moveAmmount), moveAmmount);
                        ProcessSelection(shift, selectedDate);
                        break;
                    }

                case CalendarMode.Year:
                    {
                        DateTime? selectedMonth = DateTimeHelper.AddMonths(DisplayDate, moveAmmount);
                        OnSelectedMonthChanged(selectedMonth);
                        break;
                    }

                case CalendarMode.Decade:
                    {
                        DateTime? selectedYear = DateTimeHelper.AddYears(DisplayDate, moveAmmount);
                        OnSelectedYearChanged(selectedYear);
                        break;
                    }
            }
        }

        private void ProcessPageDownKey(bool shift)
        {
            switch (DisplayMode)
            {
                case CalendarMode.Month:
                    {
                        DateTime? selectedDate = BlackoutDates.GetNonBlackoutDate(DateTimeHelper.AddMonths(CurrentDate, 1), 1);
                        ProcessSelection(shift, selectedDate);
                        break;
                    }

                case CalendarMode.Year:
                    {
                        DateTime? selectedMonth = DateTimeHelper.AddYears(DisplayDate, 1);
                        OnSelectedMonthChanged(selectedMonth);
                        break;
                    }

                case CalendarMode.Decade:
                    {
                        DateTime? selectedYear = DateTimeHelper.AddYears(DisplayDate, 10);
                        OnSelectedYearChanged(selectedYear);
                        break;
                    }
            }
        }

        private void ProcessPageUpKey(bool shift)
        {
            switch (DisplayMode)
            {
                case CalendarMode.Month:
                    {
                        DateTime? selectedDate = BlackoutDates.GetNonBlackoutDate(DateTimeHelper.AddMonths(CurrentDate, -1), -1);
                        ProcessSelection(shift, selectedDate);
                        break;
                    }

                case CalendarMode.Year:
                    {
                        DateTime? selectedMonth = DateTimeHelper.AddYears(DisplayDate, -1);
                        OnSelectedMonthChanged(selectedMonth);
                        break;
                    }

                case CalendarMode.Decade:
                    {
                        DateTime? selectedYear = DateTimeHelper.AddYears(DisplayDate, -10);
                        OnSelectedYearChanged(selectedYear);
                        break;
                    }
            }
        }

        private void ProcessRightKey(bool shift)
        {
            switch (DisplayMode)
            {
                case CalendarMode.Month:
                    {
                        DateTime? selectedDate = BlackoutDates.GetNonBlackoutDate(DateTimeHelper.AddDays(CurrentDate, 1), 1);
                        ProcessSelection(shift, selectedDate);
                        break;
                    }

                case CalendarMode.Year:
                    {
                        DateTime? selectedMonth = DateTimeHelper.AddMonths(DisplayDate, 1);
                        OnSelectedMonthChanged(selectedMonth);
                        break;
                    }

                case CalendarMode.Decade:
                    {
                        DateTime? selectedYear = DateTimeHelper.AddYears(DisplayDate, 1);
                        OnSelectedYearChanged(selectedYear);
                        break;
                    }
            }
        }

        private void ProcessSelection(bool shift, DateTime? lastSelectedDate)
        {
            if (SelectionMode == CalendarSelectionMode.None && lastSelectedDate != null)
            {
                OnDayClick(lastSelectedDate.Value);
                return;
            }

            if (lastSelectedDate != null && IsValidKeyboardSelection(this, lastSelectedDate.Value))
            {
                if (SelectionMode == CalendarSelectionMode.SingleRange || SelectionMode == CalendarSelectionMode.MultipleRange)
                {
                    SelectedDatesInternal.Clear();
                    if (shift)
                    {
                        _isShiftPressed = true;
                        if (!HoverStart.HasValue)
                        {
                            HoverStart = HoverEnd = CurrentDate;
                        }

                        // If we hit a BlackOutDay with keyboard we do not update the HoverEnd
                        SchedulerDateRange range;

                        if (DateTime.Compare(HoverStart.Value, lastSelectedDate.Value) < 0)
                        {
                            range = new SchedulerDateRange(HoverStart.Value, lastSelectedDate.Value);
                        }
                        else
                        {
                            range = new SchedulerDateRange(lastSelectedDate.Value, HoverStart.Value);
                        }

                        if (!BlackoutDates.ContainsAny(range))
                        {
                            _currentDate = lastSelectedDate;
                            HoverEnd = lastSelectedDate;
                        }

                        OnDayClick(CurrentDate);
                    }
                    else
                    {
                        HoverStart = HoverEnd = CurrentDate = lastSelectedDate.Value;
                        AddKeyboardSelection();
                        OnDayClick(lastSelectedDate.Value);
                    }
                }
                else
                {
                    // ON CLEAR
                    CurrentDate = lastSelectedDate.Value;
                    HoverStart = HoverEnd = null;
                    if (SelectedDatesInternal.Count > 0)
                    {
                        SelectedDatesInternal[0] = lastSelectedDate.Value;
                    }
                    else
                    {
                        SelectedDatesInternal.Add(lastSelectedDate.Value);
                    }

                    OnDayClick(lastSelectedDate.Value);
                }

                //UpdateCellItems();
            }
        }

        private void ProcessShiftKeyUp()
        {
            if (_isShiftPressed && (SelectionMode == CalendarSelectionMode.SingleRange || SelectionMode == CalendarSelectionMode.MultipleRange))
            {
                AddKeyboardSelection();
                _isShiftPressed = false;
                HoverStart = HoverEnd = null;
            }
        }

        private void ProcessUpKey(bool ctrl, bool shift)
        {
            switch (DisplayMode)
            {
                case CalendarMode.Month:
                    {
                        if (ctrl)
                        {
                            SetCurrentValue(DisplayModeProperty, CalendarMode.Year);
                            FocusDate(DisplayDate);
                        }
                        else
                        {
                            DateTime? selectedDate = BlackoutDates.GetNonBlackoutDate(DateTimeHelper.AddDays(CurrentDate, -Cols), -1);
                            ProcessSelection(shift, selectedDate);
                        }

                        break;
                    }

                case CalendarMode.Year:
                    {
                        if (ctrl)
                        {
                            SetCurrentValue(DisplayModeProperty, CalendarMode.Decade);
                            FocusDate(DisplayDate);
                        }
                        else
                        {
                            DateTime? selectedMonth = DateTimeHelper.AddMonths(DisplayDate, -YearCols);
                            OnSelectedMonthChanged(selectedMonth);
                        }

                        break;
                    }

                case CalendarMode.Decade:
                    {
                        if (!ctrl)
                        {
                            DateTime? selectedYear = DateTimeHelper.AddYears(DisplayDate, -YearCols);
                            OnSelectedYearChanged(selectedYear);
                        }

                        break;
                    }
            }
        }

        #endregion Private Methods

        protected override void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            base.OnItemsSourceChanged(oldValue, newValue);

            UpdateAppointments();
        }

        protected void UpdateAppointments()
        {
            if (MonthControl != null)
            {
                switch (DisplayMode)
                {
                    case CalendarMode.Month:
                        var days = MonthControl.GetSchedulerDays();

                        foreach (var item in days)
                        {
                            item.ItemsSource = GetAppointmentsByDate((DateTime)item.DataContext);
                        }
                        break;

                    case CalendarMode.Year:
                        var months = MonthControl.GetSchedulerButtons();

                        foreach (var item in months)
                        {
                            var date = (DateTime)item.DataContext;
                            item.ItemsSource = GetAppointmentsByMonth(date.Month);
                        }
                        break;

                    case CalendarMode.Decade:
                        var years = MonthControl.GetSchedulerButtons();

                        foreach (var item in years)
                        {
                            var date = (DateTime)item.DataContext;
                            item.ItemsSource = GetAppointmentsByYear(date.Year);
                        }
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        internal IList<IAppointment> GetAppointments()
        {
            var list = new List<IAppointment>();
            foreach (var item in Items)
            {
                if (item is IAppointment appointment)
                    list.Add(appointment);
            }

            return list;
        }

        internal IList<IAppointment> GetAppointmentsByDate(DateTime date)
        {
            var list = GetAppointments();
            return list.Where(x => date.Date >= x.StartDate.Date && date.Date <= x.EndDate.Date).OrderBy(x => x.StartDate).ToList();
        }

        internal IList<IAppointment> GetAppointmentsByMonth(int month)
        {
            var list = GetAppointments();
            return list.Where(x => month >= x.StartDate.Month && month <= x.EndDate.Month).OrderBy(x => x.StartDate).ToList();
        }

        internal IList<IAppointment> GetAppointmentsByYear(int year)
        {
            var list = GetAppointments();
            return list.Where(x => year >= x.StartDate.Year && year <= x.EndDate.Year).OrderBy(x => x.StartDate).ToList();
        }
    }
}