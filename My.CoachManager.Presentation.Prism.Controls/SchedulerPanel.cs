using My.CoachManager.CrossCutting.Core.Helpers;
using My.CoachManager.Presentation.Prism.Controls.Schedulers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace My.CoachManager.Presentation.Prism.Controls
{
    [TemplatePart(Name = ElementRoot, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = ElementHeaderButton, Type = typeof(Button))]
    [TemplatePart(Name = ElementPreviousButton, Type = typeof(Button))]
    [TemplatePart(Name = ElementNextButton, Type = typeof(Button))]
    [TemplatePart(Name = ElementDayTitleTemplate, Type = typeof(DataTemplate))]
    [TemplatePart(Name = ElementMonthView, Type = typeof(Grid))]
    [TemplatePart(Name = ElementYearView, Type = typeof(Grid))]
    public sealed class SchedulerPanel : Control
    {
        #region Constants

        private const string ElementRoot = "PART_Root";
        private const string ElementHeaderButton = "PART_HeaderButton";
        private const string ElementPreviousButton = "PART_PreviousButton";
        private const string ElementNextButton = "PART_NextButton";
        private const string ElementDayTitleTemplate = "DayTitleTemplate";
        private const string ElementMonthView = "PART_MonthView";
        private const string ElementYearView = "PART_YearView";

        private const int Cols = 7;
        private const int Rows = 7;
        private const int YearCols = 4;
        private const int YearRows = 3;
        private const int NumberOfDaysInWeek = 7;

        #endregion Constants

        #region Data

        private static ComponentResourceKey _dayTitleTemplateResourceKey;

        private readonly System.Globalization.Calendar _scheduler = new GregorianCalendar();
        private DataTemplate _dayTitleTemplate;
        private bool _isDayPressed;

        #endregion Data

        static SchedulerPanel()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SchedulerPanel), new FrameworkPropertyMetadata(typeof(SchedulerPanel)));
            FocusableProperty.OverrideMetadata(typeof(SchedulerPanel), new FrameworkPropertyMetadata(false));
            KeyboardNavigation.TabNavigationProperty.OverrideMetadata(typeof(SchedulerPanel), new FrameworkPropertyMetadata(KeyboardNavigationMode.Once));
            KeyboardNavigation.DirectionalNavigationProperty.OverrideMetadata(typeof(SchedulerPanel), new FrameworkPropertyMetadata(KeyboardNavigationMode.Contained));

            IsEnabledProperty.OverrideMetadata(typeof(SchedulerPanel), new UIPropertyMetadata());
        }

        #region Internal Properties

        internal Grid MonthView { get; private set; }

        internal Scheduler Owner
        {
            get;
            set;
        }

        internal Grid YearView { get; private set; }

        #endregion Internal Properties

        #region Private Properties

        internal Button HeaderButton { get; private set; }

        internal Button NextButton { get; private set; }

        internal Button PreviousButton { get; private set; }

        private DateTime DisplayDate => Owner?.DisplayDateInternal ?? DateTime.Today;

        #endregion Private Properties

        #region Public Methods

        /// <summary>
        /// Invoked whenever application code or an internal process,
        /// such as a rebuilding layout pass, calls the ApplyTemplate method.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (PreviousButton != null)
            {
                PreviousButton.Click -= PreviousButton_Click;
            }

            if (NextButton != null)
            {
                NextButton.Click -= NextButton_Click;
            }

            if (HeaderButton != null)
            {
                HeaderButton.Click -= HeaderButton_Click;
            }

            MonthView = GetTemplateChild(ElementMonthView) as Grid;
            YearView = GetTemplateChild(ElementYearView) as Grid;
            PreviousButton = GetTemplateChild(ElementPreviousButton) as Button;
            NextButton = GetTemplateChild(ElementNextButton) as Button;
            HeaderButton = GetTemplateChild(ElementHeaderButton) as Button;

            // WPF Compat: Unlike SL, WPF is not able to get elements in template resources with GetTemplateChild()
            _dayTitleTemplate = null;
            if (Template != null && Template.Resources.Contains(DayTitleTemplateResourceKey))
            {
                _dayTitleTemplate = Template.Resources[DayTitleTemplateResourceKey] as DataTemplate;
            }

            if (PreviousButton != null)
            {
                // If the user does not provide a Content value in template, we provide a helper text that can be used in Accessibility
                // this text is not shown on the UI, just used for Accessibility purposes
                if (PreviousButton.Content == null)
                {
                    //PreviousButton.Content = SR.Get(SRID.SchedulerPreviousButtonName);
                }

                PreviousButton.Click += PreviousButton_Click;
            }

            if (NextButton != null)
            {
                // If the user does not provide a Content value in template, we provide a helper text that can be used in Accessibility
                // this text is not shown on the UI, just used for Accessibility purposes
                if (NextButton.Content == null)
                {
                    //NextButton.Content = SR.Get(SRID.SchedulerNextButtonName);
                }

                NextButton.Click += NextButton_Click;
            }

            if (HeaderButton != null)
            {
                HeaderButton.Click += HeaderButton_Click;
            }

            PopulateGrids();

            if (Owner != null)
            {
                switch (Owner.DisplayMode)
                {
                    case CalendarMode.Year:
                        UpdateYearMode();
                        break;

                    case CalendarMode.Decade:
                        UpdateDecadeMode();
                        break;

                    case CalendarMode.Month:
                        UpdateMonthMode();
                        break;

                    default:
                        Debug.Assert(false);
                        break;
                }
            }
            else
            {
                UpdateMonthMode();
            }
        }

        #endregion Public Methods

        #region Protected Methods

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);

            if (IsMouseCaptured)
            {
                ReleaseMouseCapture();
            }
            
            _isDayPressed = false;

            // In Month mode, we may need to end a drag selection even if  the mouse up isn't on the Scheduler.
            if (!e.Handled &&
                Owner.DisplayMode == CalendarMode.Month &&
                Owner.HoverEnd.HasValue)
            {
                FinishSelection(Owner.HoverEnd.Value);
            }
        }

        protected override void OnLostMouseCapture(MouseEventArgs e)
        {
            base.OnLostMouseCapture(e);

            if (!IsMouseCaptured)
            {
                _isDayPressed = false;
            }
        }

        #endregion Protected Methods

        #region Internal Methods

        internal void UpdateDecadeMode()
        {
            DateTime selectedYear;

            if (Owner != null)
            {
                selectedYear = Owner.DisplayYear;
            }
            else
            {
                selectedYear = DateTime.Today;
            }

            int decade = GetDecadeForDecadeMode(selectedYear);
            int decadeEnd = decade + 9;

            SetDecadeModeHeaderButton(decade);
            SetDecadeModePreviousButton(decade);
            SetDecadeModeNextButton(decadeEnd);

            if (YearView != null)
            {
                SetYearButtons(decade, decadeEnd);
            }
        }

        internal void UpdateMonthMode()
        {
            SetMonthModeHeaderButton();
            SetMonthModePreviousButton();
            SetMonthModeNextButton();

            if (MonthView != null)
            {
                SetMonthModeDayTitles();
                SetMonthModeSchedulerDays();
                AddMonthModeHighlight();
            }
        }

        internal void UpdateYearMode()
        {
            SetYearModeHeaderButton();
            SetYearModePreviousButton();
            SetYearModeNextButton();

            if (YearView != null)
            {
                SetYearModeMonthButtons();
            }
        }

        internal IEnumerable<SchedulerDay> GetSchedulerDays()
        {
            //
            int count = Rows * Cols;
            if (MonthView != null)
            {
                UIElementCollection dayButtonsHost = MonthView.Children;
                for (int childIndex = Cols; childIndex < count; childIndex++)
                {
                    if (dayButtonsHost[childIndex] is SchedulerDay b)
                    {
                        yield return b;
                    }
                }
            }
        }

        internal SchedulerDay GetSchedulerDay(DateTime date)
        {
            foreach (SchedulerDay b in GetSchedulerDays())
            {
                if (b != null && b.DataContext is DateTime)
                {
                    if (DateTimeHelper.CompareDays(date, (DateTime)b.DataContext) == 0)
                    {
                        return b;
                    }
                }
            }

            return null;
        }

        internal SchedulerItem GetSchedulerButton(DateTime date, CalendarMode mode)
        {
            Debug.Assert(mode != CalendarMode.Month);

            foreach (SchedulerItem b in GetSchedulerButtons())
            {
                if (b != null && b.DataContext is DateTime)
                {
                    if (mode == CalendarMode.Year)
                    {
                        if (DateTimeHelper.CompareYearMonth(date, (DateTime)b.DataContext) == 0)
                        {
                            return b;
                        }
                    }
                    else
                    {
                        if (date.Year == ((DateTime)b.DataContext).Year)
                        {
                            return b;
                        }
                    }
                }
            }

            return null;
        }

        internal IEnumerable<SchedulerItem> GetSchedulerButtons()
        {
            foreach (UIElement element in YearView.Children)
            {
                if (element is SchedulerItem b)
                {
                    yield return b;
                }
            }
        }


        #endregion Internal Methods

        #region Private Methods

        private int GetDecadeForDecadeMode(DateTime selectedYear)
        {
            int decade = DateTimeHelper.DecadeOfDate(selectedYear);

            // Adjust the decade value if the mouse move selection is on,
            // such that if first or last year among the children are selected
            // then return the current selected decade as is.
            if (YearView != null)
            {
                UIElementCollection yearViewChildren = YearView.Children;
                int count = yearViewChildren.Count;

                if (count > 0)
                {
                    if (yearViewChildren[0] is SchedulerItem child &&
                        child.DataContext is DateTime &&
                        ((DateTime)child.DataContext).Year == selectedYear.Year)
                    {
                        return (decade + 10);
                    }
                }

                if (count > 1)
                {
                    if (yearViewChildren[count - 1] is SchedulerItem child &&
                        child.DataContext is DateTime &&
                        ((DateTime)child.DataContext).Year == selectedYear.Year)
                    {
                        return (decade - 10);
                    }
                }
            }
            return decade;
        }

        private void EndDrag(bool ctrl, DateTime selectedDate)
        {
            if (Owner?.HoverStart != null)
            {
                if (
                    ctrl &&
                    DateTime.Compare(Owner.HoverStart.Value, selectedDate) == 0 &&
                    (Owner.SelectionMode == CalendarSelectionMode.SingleDate || Owner.SelectionMode == CalendarSelectionMode.MultipleRange))
                {
                    // Ctrl + single click = toggle
                    //Owner.Toggle(selectedDate);
                }
                else
                {
                    // this is selection with Mouse, we do not guarantee the range does not include BlackOutDates.
                    // Use the internal AddRange that omits BlackOutDates based on the SelectionMode
                    Owner.SelectedDatesInternal.AddRange(Owner.HoverStart.Value, selectedDate);
                }
            }
        }

        private void CellOrMonth_PreviewKeyDown(object sender, RoutedEventArgs e)
        {
            Debug.Assert(e != null);

            Owner?.OnDayOrMonthPreviewKeyDown(e);
        }

        private void Cell_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!(sender is SchedulerDay b))
            {
                return;
            }


            if (Owner == null || !(b.DataContext is DateTime))
            {
                return;
            }

            DateTime clickedDate = (DateTime)b.DataContext;

            if (b.IsBlackedOut)
            {
                Owner.HoverStart = null;
            }
            else
            {
                _isDayPressed = true;
                //Mouse.Capture(this, CaptureMode.SubTree);

                b.MoveFocus(new TraversalRequest(FocusNavigationDirection.First));

                SchedulerKeyboardHelper.GetMetaKeyState(out var ctrl, out var shift);

                switch (Owner.SelectionMode)
                {
                    case CalendarSelectionMode.None:
                    {
                        break;
                    }

                    case CalendarSelectionMode.SingleDate:
                    {
                        if (!ctrl)
                        {
                            Owner.SelectedDateInternal = clickedDate;
                        }
                        else
                        {
                            Owner.Toggle(clickedDate);
                        }

                        break;
                    }

                    case CalendarSelectionMode.SingleRange:
                    {
                        Owner.SelectedDatesInternal.ClearInternal();

                        if (shift)
                        {
                            if (!Owner.HoverStart.HasValue)
                            {
                                Owner.HoverStart = Owner.HoverEnd = Owner.CurrentDate;
                            }
                        }
                        else
                        {
                            Owner.SelectedDateInternal = clickedDate;
                                Owner.HoverStart = Owner.HoverEnd = clickedDate;
                        }


                        break;
                    }

                    case CalendarSelectionMode.MultipleRange:
                    {
                        if (!ctrl)
                        {
                            Owner.SelectedDatesInternal.Clear();
                        }

                        if (shift)
                        {
                            if (!Owner.HoverStart.HasValue)
                            {
                                Owner.HoverStart = Owner.HoverEnd = Owner.CurrentDate;
                            }
                        }
                        else
                        {
                            if (ctrl)
                            {
                                Owner.Toggle(clickedDate);
                                Owner.HoverStart = Owner.HoverEnd = clickedDate;
                                }
                            else
                            {
                                Owner.SelectedDateInternal = clickedDate;
                                Owner.HoverStart = Owner.HoverEnd = clickedDate;
                                }
                        }

                            break;
                    }
                }
            }
        }

        private void Cell_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!(sender is SchedulerDay b))
            {
                return;
            }

            if (b.IsBlackedOut)
            {
                return;
            }

            if (e.LeftButton == MouseButtonState.Pressed && _isDayPressed)
            {
                b.MoveFocus(new TraversalRequest(FocusNavigationDirection.First));

                if (Owner == null || !(b.DataContext is DateTime))
                {
                    return;
                }

                DateTime selectedDate = (DateTime)b.DataContext;

                switch (Owner.SelectionMode)
                {
                    case CalendarSelectionMode.SingleDate:
                        {
                            Owner.HoverStart = Owner.HoverEnd = null;
                            if (Owner.SelectedDatesInternal.Count == 0)
                            {
                                Owner.SelectedDatesInternal.Add(selectedDate);
                            }
                            else
                            {
                                Owner.SelectedDatesInternal[0] = selectedDate;
                            }

                            return;
                        }
                }

                Owner.HoverEnd = selectedDate;

                if (Owner.HoverStart.HasValue)
                {
                    Owner.SelectedDatesInternal.AddRange(Owner.HoverStart.Value, Owner.HoverEnd.Value);
                }
            }
        }

        private void Cell_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!(sender is SchedulerDay b))
            {
                return;
            }

            if (Owner == null)
            {
                return;
            }

            if (!b.IsBlackedOut)
            {
                Owner.OnDayButtonMouseUp(e);
            }

            if (!(b.DataContext is DateTime))
            {
                return;
            }

            b.MoveFocus(new TraversalRequest(FocusNavigationDirection.First));
            FinishSelection((DateTime)b.DataContext);
            e.Handled = true;
        }

        private void FinishSelection(DateTime selectedDate)
        {
            Owner.OnDayClick(selectedDate);
            SchedulerKeyboardHelper.GetMetaKeyState(out var ctrl, out var _);

            if (Owner.SelectionMode == CalendarSelectionMode.None || Owner.SelectionMode == CalendarSelectionMode.SingleDate)
            {
                return;
            }

            if (Owner.HoverStart.HasValue)
            {
                switch (Owner.SelectionMode)
                {
                    case CalendarSelectionMode.SingleRange:
                        {
                            // Update SelectedDatesInternal
                            Owner.SelectedDatesInternal.ClearInternal();
                            EndDrag(ctrl, selectedDate);
                            break;
                        }

                    case CalendarSelectionMode.MultipleRange:
                        {
                            // add the selection (either single day or SingleRange day)
                            EndDrag(ctrl, selectedDate);
                            break;
                        }
                }
            }
        }

        private void Month_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (sender is SchedulerItem b)
            {
                Owner?.OnSchedulerItemPressed(b);
            }
        }

        private void HeaderButton_Click(object sender, RoutedEventArgs e)
        {
            if (Owner != null)
            {
                if (Owner.DisplayMode == CalendarMode.Month)
                {
                    Owner.SetCurrentValue(Scheduler.DisplayModeProperty, CalendarMode.Year);
                }
                else
                {
                    Debug.Assert(Owner.DisplayMode == CalendarMode.Year);

                    Owner.SetCurrentValue(Scheduler.DisplayModeProperty, CalendarMode.Decade);
                }

            }
        }

        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            Owner?.OnPreviousClick();
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            Owner?.OnNextClick();
        }

        private void PopulateGrids()
        {
            if (MonthView != null)
            {
                for (int i = 0; i < Cols; i++)
                {
                    FrameworkElement titleCell = (_dayTitleTemplate != null) ? (FrameworkElement)_dayTitleTemplate.LoadContent() : new ContentControl();
                    titleCell.SetValue(Grid.RowProperty, 0);
                    titleCell.SetValue(Grid.ColumnProperty, i);
                    MonthView.Children.Add(titleCell);
                }

                for (int i = 1; i < Rows; i++)
                {
                    for (int j = 0; j < Cols; j++)
                    {
                        SchedulerDay dayCell = new SchedulerDay();

                        dayCell.Owner = Owner;
                        dayCell.SetValue(Grid.RowProperty, i);
                        dayCell.SetValue(Grid.ColumnProperty, j);
                        dayCell.SetBinding(StyleProperty, GetOwnerBinding("SchedulerDayStyle"));

                        dayCell.MouseLeftButtonDown += Cell_MouseLeftButtonDown;
                        dayCell.MouseLeftButtonUp += Cell_MouseLeftButtonUp;
                        dayCell.MouseEnter += Cell_MouseEnter;
                        dayCell.PreviewKeyDown += CellOrMonth_PreviewKeyDown;

                        MonthView.Children.Add(dayCell);
                    }
                }
            }

            if (YearView != null)
            {
                for (int i = 0; i < YearRows; i++)
                {
                    for (int j = 0; j < YearCols; j++)
                    {
                        var monthCell = new SchedulerItem { Owner = Owner };

                        monthCell.SetValue(Grid.RowProperty, i);
                        monthCell.SetValue(Grid.ColumnProperty, j);
                        monthCell.SetBinding(StyleProperty, GetOwnerBinding("SchedulerItemStyle"));

                        monthCell.MouseLeftButtonUp += Month_MouseLeftButtonUp;
                        monthCell.PreviewKeyDown += CellOrMonth_PreviewKeyDown;
                        YearView.Children.Add(monthCell);
                    }
                }
            }
        }

        #region Month Mode Display

        private void SetMonthModeDayTitles()
        {
            if (MonthView != null)
            {
                string[] shortestDayNames = DateTimeHelper.GetDateFormat(DateTimeHelper.GetCulture()).ShortestDayNames;

                for (int childIndex = 0; childIndex < Cols; childIndex++)
                {
                    if (MonthView.Children[childIndex] is FrameworkElement daytitle && shortestDayNames.Length > 0)
                    {
                        if (Owner != null)
                        {
                            daytitle.DataContext = shortestDayNames[(childIndex + (int)Owner.FirstDayOfWeek) % shortestDayNames.Length];
                        }
                        else
                        {
                            daytitle.DataContext = shortestDayNames[(childIndex + (int)DateTimeHelper.GetDateFormat(DateTimeHelper.GetCulture()).FirstDayOfWeek) % shortestDayNames.Length];
                        }
                    }
                }
            }
        }

        private void SetMonthModeSchedulerDays()
        {
            DateTime firstDayOfMonth = DateTimeHelper.DiscardDayTime(DisplayDate);
            int lastMonthToDisplay = GetNumberOfDisplayedDaysFromPreviousMonth(firstDayOfMonth);

            bool isMinMonth = DateTimeHelper.CompareYearMonth(firstDayOfMonth, DateTime.MinValue) <= 0;
            bool isMaxMonth = DateTimeHelper.CompareYearMonth(firstDayOfMonth, DateTime.MaxValue) >= 0;
            int daysInMonth = _scheduler.GetDaysInMonth(firstDayOfMonth.Year, firstDayOfMonth.Month);
            CultureInfo culture = DateTimeHelper.GetCulture();

            int count = Rows * Cols;
            for (int childIndex = Cols; childIndex < count; childIndex++)
            {
                SchedulerDay childButton = MonthView.Children[childIndex] as SchedulerDay;

                Debug.Assert(childButton != null);

                int dayOffset = childIndex - lastMonthToDisplay - Cols;
                if ((!isMinMonth || (dayOffset >= 0)) && (!isMaxMonth || (dayOffset < daysInMonth)))
                {
                    DateTime dateToAdd = _scheduler.AddDays(firstDayOfMonth, dayOffset);
                    SetMonthModeDayButtonState(childButton, dateToAdd);
                    childButton.DataContext = dateToAdd;
                    childButton.SetContentInternal(DateTimeHelper.ToDayString(dateToAdd, culture));
                }
                else
                {
                    SetMonthModeDayButtonState(childButton, null);
                    childButton.DataContext = null;
                    childButton.SetContentInternal(DateTimeHelper.ToDayString(null, culture));
                }
            }
        }

        private void SetMonthModeDayButtonState(SchedulerDay childButton, DateTime? dateToAdd)
        {
            if (Owner != null)
            {
                if (dateToAdd.HasValue)
                {
                    childButton.Visibility = Visibility.Visible;

                    // If the day is outside the DisplayDateStart/End boundary, do not show it
                    if (DateTimeHelper.CompareDays(dateToAdd.Value, Owner.DisplayDateStartInternal) < 0 || DateTimeHelper.CompareDays(dateToAdd.Value, Owner.DisplayDateEndInternal) > 0)
                    {
                        childButton.IsEnabled = false;
                        childButton.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        childButton.IsEnabled = true;

                        // SET IF THE DAY IS SELECTABLE OR NOT
                        childButton.SetValue(
                            SchedulerDay.IsBlackedOutPropertyKey,
                            Owner.BlackoutDates.Contains(dateToAdd.Value));

                        // SET IF THE DAY IS ACTIVE OR NOT: set if the day is a trailing day or not
                        childButton.SetValue(
                            SchedulerDay.IsInactivePropertyKey,
                            DateTimeHelper.CompareYearMonth(dateToAdd.Value, Owner.DisplayDateInternal) != 0);

                        // SET IF THE DAY IS TODAY OR NOT
                        if (DateTimeHelper.CompareDays(dateToAdd.Value, DateTime.Today) == 0)
                        {
                            childButton.SetValue(SchedulerDay.IsTodayPropertyKey, true);
                        }
                        else
                        {
                            childButton.SetValue(SchedulerDay.IsTodayPropertyKey, false);
                        }

                        // SET IF THE DAY IS SELECTED OR NOT
                        // Since we should be comparing the Date values not DateTime values, we can't use this.Owner.SelectedDatesInternal.Contains(dateToAdd) directly
                        bool isSelected = false;
                        foreach (DateTime item in Owner.SelectedDatesInternal)
                        {
                            isSelected |= (DateTimeHelper.CompareDays(dateToAdd.Value, item) == 0);
                        }

                        childButton.SetValue(SchedulerDay.IsSelectedPropertyKey, isSelected);
                    }
                }
                else
                {
                    childButton.Visibility = Visibility.Hidden;
                    childButton.IsEnabled = false;
                    childButton.SetValue(SchedulerDay.IsBlackedOutPropertyKey, false);
                    childButton.SetValue(SchedulerDay.IsInactivePropertyKey, true);
                    childButton.SetValue(SchedulerDay.IsTodayPropertyKey, false);
                    childButton.SetValue(SchedulerDay.IsSelectedPropertyKey, false);
                }
            }
        }

        private void AddMonthModeHighlight()
        {
            var owner = Owner;
            if (owner == null)
            {
                return;
            }

            if (owner.HoverStart.HasValue && owner.HoverEnd.HasValue)
            {
                DateTime hStart = owner.HoverEnd.Value;
                DateTime hEnd = owner.HoverEnd.Value;

                int daysToHighlight = DateTimeHelper.CompareDays(owner.HoverEnd.Value, owner.HoverStart.Value);
                if (daysToHighlight < 0)
                {
                    hEnd = owner.HoverStart.Value;
                }
                else
                {
                    hStart = owner.HoverStart.Value;
                }

                int count = Rows * Cols;

                for (int childIndex = Cols; childIndex < count; childIndex++)
                {
                    SchedulerDay childButton = MonthView.Children[childIndex] as SchedulerDay;
                    if (childButton?.DataContext is DateTime)
                    {
                        DateTime date = (DateTime)childButton.DataContext;
                        childButton.SetValue(
                            SchedulerDay.IsHighlightedPropertyKey,
                            (daysToHighlight != 0) && DateTimeHelper.InRange(date, hStart, hEnd));
                    }
                    else
                    {
                        childButton?.SetValue(SchedulerDay.IsHighlightedPropertyKey, false);
                    }
                }
            }
            else
            {
                int count = Rows * Cols;

                for (int childIndex = Cols; childIndex < count; childIndex++)
                {
                    SchedulerDay childButton = MonthView.Children[childIndex] as SchedulerDay;
                    childButton?.SetValue(SchedulerDay.IsHighlightedPropertyKey, false);
                }
            }
        }

        private void SetMonthModeHeaderButton()
        {
            if (HeaderButton != null)
            {
                HeaderButton.Content = DateTimeHelper.ToYearMonthPatternString(DisplayDate, DateTimeHelper.GetCulture());

                if (Owner != null)
                {
                    HeaderButton.IsEnabled = true;
                }
            }
        }

        private void SetMonthModeNextButton()
        {
            if (Owner != null && NextButton != null)
            {
                DateTime firstDayOfMonth = DateTimeHelper.DiscardDayTime(DisplayDate);

                // DisplayDate is equal to DateTime.MaxValue
                if (DateTimeHelper.CompareYearMonth(firstDayOfMonth, DateTime.MaxValue) == 0)
                {
                    NextButton.IsEnabled = false;
                }
                else
                {
                    // Since we are sure DisplayDate is not equal to DateTime.MaxValue,
                    // it is safe to use AddMonths
                    DateTime firstDayOfNextMonth = _scheduler.AddMonths(firstDayOfMonth, 1);
                    NextButton.IsEnabled = (DateTimeHelper.CompareDays(Owner.DisplayDateEndInternal, firstDayOfNextMonth) > -1);
                }
            }
        }

        private void SetMonthModePreviousButton()
        {
            if (Owner != null && PreviousButton != null)
            {
                DateTime firstDayOfMonth = DateTimeHelper.DiscardDayTime(DisplayDate);
                PreviousButton.IsEnabled = (DateTimeHelper.CompareDays(Owner.DisplayDateStartInternal, firstDayOfMonth) < 0);
            }
        }

        #endregion Month Mode Display

        #region Year Mode Display

        private void SetYearButtons(int decade, int decadeEnd)
        {
            int year;
            int count = -1;
            foreach (object child in YearView.Children)
            {
                SchedulerItem childButton = child as SchedulerItem;
                Debug.Assert(childButton != null);
                year = decade + count;

                if (year <= DateTime.MaxValue.Year && year >= DateTime.MinValue.Year)
                {
                    // There should be no time component. Time is 12:00 AM
                    DateTime day = new DateTime(year, 1, 1);
                    childButton.DataContext = day;
                    childButton.SetContentInternal(DateTimeHelper.ToYearString(day, DateTimeHelper.GetCulture()));
                    childButton.Visibility = Visibility.Visible;
                    childButton.ItemsSource = Owner.GetAppointmentsByYear(day.Year);

                    if (Owner != null)
                    {
                        childButton.HasSelectedDays = (Owner.DisplayDateInternal.Year == year);

                        if (year < Owner.DisplayDateStartInternal.Year || year > Owner.DisplayDateEndInternal.Year)
                        {
                            childButton.IsEnabled = false;
                            childButton.Opacity = 0;
                        }
                        else
                        {
                            childButton.IsEnabled = true;
                            childButton.Opacity = 1;
                        }
                    }

                    // SET IF THE YEAR IS INACTIVE OR NOT: set if the year is a trailing year or not
                    childButton.IsInactive = year < decade || year > decadeEnd;
                }
                else
                {
                    childButton.DataContext = null;
                    childButton.IsEnabled = false;
                    childButton.Opacity = 0;
                }

                count++;
            }
        }

        private void SetYearModeMonthButtons()
        {
            int count = 0;
            foreach (object child in YearView.Children)
            {
                SchedulerItem childButton = child as SchedulerItem;
                Debug.Assert(childButton != null);

                // There should be no time component. Time is 12:00 AM
                DateTime day = new DateTime(DisplayDate.Year, count + 1, 1);
                childButton.DataContext = day;
                childButton.SetContentInternal(DateTimeHelper.ToAbbreviatedMonthString(day, DateTimeHelper.GetCulture()));
                childButton.Visibility = Visibility.Visible;
                childButton.ItemsSource = Owner.GetAppointmentsByMonth(day.Month);

                if (Owner != null)
                {
                    Debug.Assert(Owner.DisplayDateInternal != null);
                    childButton.HasSelectedDays = (DateTimeHelper.CompareYearMonth(day, Owner.DisplayDateInternal) == 0);

                    if (DateTimeHelper.CompareYearMonth(day, Owner.DisplayDateStartInternal) < 0 || DateTimeHelper.CompareYearMonth(day, Owner.DisplayDateEndInternal) > 0)
                    {
                        childButton.IsEnabled = false;
                        childButton.Opacity = 0;
                    }
                    else
                    {
                        childButton.IsEnabled = true;
                        childButton.Opacity = 1;
                    }
                }

                childButton.IsInactive = false;
                count++;
            }
        }

        private void SetYearModeHeaderButton()
        {
            if (HeaderButton != null)
            {
                HeaderButton.IsEnabled = true;
                HeaderButton.Content = DateTimeHelper.ToYearString(DisplayDate, DateTimeHelper.GetCulture());
            }
        }

        private void SetYearModeNextButton()
        {
            if (Owner != null && NextButton != null)
            {
                NextButton.IsEnabled = (Owner.DisplayDateEndInternal.Year != DisplayDate.Year);
            }
        }

        private void SetYearModePreviousButton()
        {
            if (Owner != null && PreviousButton != null)
            {
                PreviousButton.IsEnabled = (Owner.DisplayDateStartInternal.Year != DisplayDate.Year);
            }
        }

        #endregion Year Mode Display

        #region Decade Mode Display

        private void SetDecadeModeHeaderButton(int decade)
        {
            if (HeaderButton != null)
            {
                HeaderButton.Content = DateTimeHelper.ToDecadeRangeString(decade, this);
                HeaderButton.IsEnabled = false;
            }
        }

        private void SetDecadeModeNextButton(int decadeEnd)
        {
            if (Owner != null && NextButton != null)
            {
                NextButton.IsEnabled = (Owner.DisplayDateEndInternal.Year > decadeEnd);
            }
        }

        private void SetDecadeModePreviousButton(int decade)
        {
            if (Owner != null && PreviousButton != null)
            {
                PreviousButton.IsEnabled = (decade > Owner.DisplayDateStartInternal.Year);
            }
        }

        #endregion Decade Mode Display

        // How many days of the previous month need to be displayed
        private int GetNumberOfDisplayedDaysFromPreviousMonth(DateTime firstOfMonth)
        {
            DayOfWeek day = _scheduler.GetDayOfWeek(firstOfMonth);
            int i;

            if (Owner != null)
            {
                i = ((day - Owner.FirstDayOfWeek + NumberOfDaysInWeek) % NumberOfDaysInWeek);
            }
            else
            {
                i = ((day - DateTimeHelper.GetDateFormat(DateTimeHelper.GetCulture()).FirstDayOfWeek + NumberOfDaysInWeek) % NumberOfDaysInWeek);
            }

            if (i == 0)
            {
                return NumberOfDaysInWeek;
            }
            else
            {
                return i;
            }
        }

        /// <summary>
        /// Gets a binding to a property on the owning Scheduler
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        private BindingBase GetOwnerBinding(string propertyName)
        {
            Binding result = new Binding(propertyName);
            result.Source = Owner;
            return result;
        }

        #endregion Private Methods

        #region Resource Keys

        /// <summary>
        ///     Resource key for DayTitleTemplate
        /// </summary>
        public static ComponentResourceKey DayTitleTemplateResourceKey
        {
            get
            {
                if (_dayTitleTemplateResourceKey == null)
                {
                    _dayTitleTemplateResourceKey = new ComponentResourceKey(typeof(SchedulerPanel), ElementDayTitleTemplate);
                }

                return _dayTitleTemplateResourceKey;
            }
        }

        #endregion Resource Keys
    }
}