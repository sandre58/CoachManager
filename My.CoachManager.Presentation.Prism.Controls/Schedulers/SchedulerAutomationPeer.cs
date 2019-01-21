using My.CoachManager.CrossCutting.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;

namespace My.CoachManager.Presentation.Prism.Controls.Schedulers
{
    /// <summary>
    /// AutomationPeer for Scheduler Control
    /// </summary>
    public sealed class SchedulerAutomationPeer : FrameworkElementAutomationPeer, IMultipleViewProvider, ISelectionProvider, ITableProvider, IItemContainerProvider
    {
        /// <summary>
        /// Initializes a new instance of the SchedulerAutomationPeer class.
        /// </summary>
        /// <param name="owner">Owning Scheduler</param>
        public SchedulerAutomationPeer(Scheduler owner)
            : base(owner)
        {
        }

        #region Private Properties

        private Scheduler OwningScheduler
        {
            get
            {
                return Owner as Scheduler;
            }
        }

        private Grid OwningGrid
        {
            get
            {
                if (OwningScheduler != null && OwningScheduler.MonthControl != null)
                {
                    if (OwningScheduler.DisplayMode == CalendarMode.Month)
                    {
                        return OwningScheduler.MonthControl.MonthView;
                    }
                    else
                    {
                        return OwningScheduler.MonthControl.YearView;
                    }
                }

                return null;
            }
        }

        #endregion Private Properties

        #region Public Methods

        /// <summary>
        /// Gets the control pattern that is associated with the specified System.Windows.Automation.Peers.PatternInterface.
        /// </summary>
        /// <param name="patternInterface">A value from the System.Windows.Automation.Peers.PatternInterface enumeration.</param>
        /// <returns>The object that supports the specified pattern, or null if unsupported.</returns>
        public override object GetPattern(PatternInterface patternInterface)
        {
            switch (patternInterface)
            {
                case PatternInterface.Grid:
                case PatternInterface.Table:
                case PatternInterface.MultipleView:
                case PatternInterface.Selection:
                case PatternInterface.ItemContainer:
                    {
                        if (OwningGrid != null)
                        {
                            return this;
                        }

                        break;
                    }
            }

            return base.GetPattern(patternInterface);
        }

        #endregion Public Methods

        #region Protected Methods

        /// <summary>
        /// Gets the control type for the element that is associated with the UI Automation peer.
        /// </summary>
        /// <returns>The control type.</returns>
        protected override AutomationControlType GetAutomationControlTypeCore()
        {
            return AutomationControlType.Calendar;
        }

        protected override List<AutomationPeer> GetChildrenCore()
        {
            if (OwningScheduler.MonthControl == null)
            {
                return null;
            }

            List<AutomationPeer> peers = new List<AutomationPeer>();
            Dictionary<DateTimeCalendarModePair, DateTimeAutomationPeer> newChildren = new Dictionary<DateTimeCalendarModePair, DateTimeAutomationPeer>();

            // Step 1: Add previous, header and next buttons
            var buttonPeer = CreatePeerForElement(OwningScheduler.MonthControl.PreviousButton);
            if (buttonPeer != null)
            {
                peers.Add(buttonPeer);
            }
            buttonPeer = CreatePeerForElement(OwningScheduler.MonthControl.HeaderButton);
            if (buttonPeer != null)
            {
                peers.Add(buttonPeer);
            }
            buttonPeer = CreatePeerForElement(OwningScheduler.MonthControl.NextButton);
            if (buttonPeer != null)
            {
                peers.Add(buttonPeer);
            }

            // Step 2: Add Scheduler Buttons depending on the Scheduler.DisplayMode
            DateTime date;
            DateTimeAutomationPeer peer;
            foreach (UIElement child in OwningGrid.Children)
            {
                int childRow = (int)child.GetValue(Grid.RowProperty);
                // first row is day titles
                if (OwningScheduler.DisplayMode == CalendarMode.Month && childRow == 0)
                {
                    AutomationPeer dayTitlePeer = CreatePeerForElement(child);
                    if (dayTitlePeer != null)
                    {
                        peers.Add(dayTitlePeer);
                    }
                }
                else
                {
                    if (child is Button owningButton && owningButton.DataContext is DateTime)
                    {
                        date = (DateTime)owningButton.DataContext;
                        peer = GetOrCreateDateTimeAutomationPeer(date, OwningScheduler.DisplayMode, /*addParentInfo*/ false);
                        peers.Add(peer);

                        DateTimeCalendarModePair key = new DateTimeCalendarModePair(date, OwningScheduler.DisplayMode);
                        newChildren.Add(key, peer);
                    }
                }
            }

            DateTimePeers = newChildren;
            return peers;
        }

        /// <summary>
        /// Called by GetClassName that gets a human readable name that, in addition to AutomationControlType,
        /// differentiates the control represented by this AutomationPeer.
        /// </summary>
        /// <returns>The string that contains the name.</returns>
        protected override string GetClassNameCore()
        {
            return Owner.GetType().Name;
        }

        protected override void SetFocusCore()
        {
            Scheduler owner = OwningScheduler;
            if (owner.Focusable)
            {
                if (!owner.Focus())
                {
                    DateTime focusedDate;
                    // Focus should have moved to either SelectedDate or DisplayDate
                    if (owner.SelectedDate.HasValue && DateTimeHelper.CompareYearMonth(owner.SelectedDate.Value, owner.DisplayDateInternal) == 0)
                    {
                        focusedDate = owner.SelectedDate.Value;
                    }
                    else
                    {
                        focusedDate = owner.DisplayDate;
                    }

                    DateTimeAutomationPeer focusedItem = GetOrCreateDateTimeAutomationPeer(focusedDate, owner.DisplayMode, /*addParentInfo*/ false);
                    FrameworkElement focusedButton = focusedItem.OwningButton;

                    if (focusedButton == null || !focusedButton.IsKeyboardFocused)
                    {
                        throw new InvalidOperationException();
                    }
                }
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        #endregion Protected Methods

        #region InternalMethods

        private DateTimeAutomationPeer GetOrCreateDateTimeAutomationPeer(DateTime date, CalendarMode buttonMode)
        {
            return GetOrCreateDateTimeAutomationPeer(date, buttonMode, /*addParentInfo*/ true);
        }

        ///<SecurityNote>
        /// Security Critical - Calls a Security Critical operation AddParentInfo which adds parent peer and provides
        ///                     security critical Hwnd value for this peer created asynchronously.
        /// SecurityTreatAsSafe - It's being called from this object which is real parent for the item peer.
        /// </SecurityNote>
        [SecurityCritical, SecurityTreatAsSafe]
        private DateTimeAutomationPeer GetOrCreateDateTimeAutomationPeer(DateTime date, CalendarMode buttonMode, bool addParentInfo)
        {
            // try to reuse old peer if it exists either in Current AT or in WeakRefStorage of Peers being sent to Client
            DateTimeCalendarModePair key = new DateTimeCalendarModePair(date, buttonMode);
            DateTimeAutomationPeer peer;
            DateTimePeers.TryGetValue(key, out peer);

            if (peer == null)
            {
                peer = GetPeerFromWeakRefStorage(key);
                //if (peer != null && !addParentInfo)
                //{
                //    // As cached peer is getting used it must be invalidated. addParentInfo check ensures that call is coming from GetChildrenCore
                //    peer.AncestorsInvalid = false;
                //    peer.ChildrenValid = false;
                //}
            }

            if (peer == null)
            {
                peer = new DateTimeAutomationPeer(date, OwningScheduler, buttonMode);

                // Sets hwnd and parent info
                if (addParentInfo)
                {
                    //if (peer != null)
                    //    peer.TrySetParentInfo(this);
                }
            }
            // Set EventsSource if visual exists
            AutomationPeer wrapperPeer = peer.WrapperPeer;
            if (wrapperPeer != null)
            {
                wrapperPeer.EventsSource = peer;
            }

            return peer;
        }

        // Provides Peer if exist in Weak Reference Storage
        private DateTimeAutomationPeer GetPeerFromWeakRefStorage(DateTimeCalendarModePair dateTimeCalendarModePairKey)
        {
            DateTimeAutomationPeer returnPeer = null;
            WeakReference weakRefEp;
            WeakRefElementProxyStorage.TryGetValue(dateTimeCalendarModePairKey, out weakRefEp);

            if (weakRefEp != null)
            {
                //ElementProxy provider = weakRefEp.Target as ElementProxy;
                //if (provider != null)
                //{
                //    returnPeer = PeerFromProvider(provider as IRawElementProviderSimple) as DateTimeAutomationPeer;
                //    if (returnPeer == null)
                //        WeakRefElementProxyStorage.Remove(dateTimeCalendarModePairKey);
                //}
                //else
                //    WeakRefElementProxyStorage.Remove(dateTimeCalendarModePairKey);
            }

            return returnPeer;
        }

        // Called by DateTimeAutomationPeer
        internal void AddProxyToWeakRefStorage(WeakReference wr, DateTimeAutomationPeer dateTimePeer)
        {
            DateTimeCalendarModePair key = new DateTimeCalendarModePair(dateTimePeer.Date, dateTimePeer.ButtonMode);

            if (GetPeerFromWeakRefStorage(key) == null)
                WeakRefElementProxyStorage.Add(key, wr);
        }

        internal void RaiseSelectionEvents(SelectionChangedEventArgs e)
        {
            int numSelected = OwningScheduler.SelectedDatesInternal.Count;
            int numAdded = e.AddedItems.Count;

            if (ListenerExists(AutomationEvents.SelectionItemPatternOnElementSelected) && numSelected == 1 && numAdded == 1)
            {
                DateTimeAutomationPeer peer = GetOrCreateDateTimeAutomationPeer((DateTime)e.AddedItems[0], CalendarMode.Month);
                if (peer != null)
                {
                    peer.RaiseAutomationEvent(AutomationEvents.SelectionItemPatternOnElementSelected);
                }
            }
            else
            {
                if (ListenerExists(AutomationEvents.SelectionItemPatternOnElementAddedToSelection))
                {
                    foreach (DateTime date in e.AddedItems)
                    {
                        DateTimeAutomationPeer peer = GetOrCreateDateTimeAutomationPeer(date, CalendarMode.Month);
                        if (peer != null)
                        {
                            peer.RaiseAutomationEvent(AutomationEvents.SelectionItemPatternOnElementAddedToSelection);
                        }
                    }
                }
            }

            if (ListenerExists(AutomationEvents.SelectionItemPatternOnElementRemovedFromSelection))
            {
                foreach (DateTime date in e.RemovedItems)
                {
                    DateTimeAutomationPeer peer = GetOrCreateDateTimeAutomationPeer(date, CalendarMode.Month);
                    if (peer != null)
                    {
                        peer.RaiseAutomationEvent(AutomationEvents.SelectionItemPatternOnElementRemovedFromSelection);
                    }
                }
            }
        }

        #endregion InternalMethods

        #region IGridProvider

        int IGridProvider.ColumnCount
        {
            get
            {
                if (OwningGrid != null)
                {
                    return OwningGrid.ColumnDefinitions.Count;
                }

                return 0;
            }
        }

        int IGridProvider.RowCount
        {
            get
            {
                if (OwningGrid != null)
                {
                    if (OwningScheduler.DisplayMode == CalendarMode.Month)
                    {
                        // In Month DisplayMode, since first row is DayTitles, we return the RowCount-1
                        return Math.Max(0, OwningGrid.RowDefinitions.Count - 1);
                    }
                    else
                    {
                        return OwningGrid.RowDefinitions.Count;
                    }
                }

                return 0;
            }
        }

        IRawElementProviderSimple IGridProvider.GetItem(int row, int column)
        {
            if (OwningScheduler.DisplayMode == CalendarMode.Month)
            {
                // In Month DisplayMode, since first row is DayTitles, we increment the row number by 1
                row++;
            }

            if (OwningGrid != null && row >= 0 && row < OwningGrid.RowDefinitions.Count && column >= 0 && column < OwningGrid.ColumnDefinitions.Count)
            {
                foreach (UIElement child in OwningGrid.Children)
                {
                    int childRow = (int)child.GetValue(Grid.RowProperty);
                    int childColumn = (int)child.GetValue(Grid.ColumnProperty);
                    if (childRow == row && childColumn == column)
                    {
                        object dataContext = (child as FrameworkElement)?.DataContext;
                        if (dataContext is DateTime)
                        {
                            DateTime date = (DateTime)dataContext;
                            AutomationPeer peer = GetOrCreateDateTimeAutomationPeer(date, OwningScheduler.DisplayMode);
                            return ProviderFromPeer(peer);
                        }
                    }
                }
            }

            return null;
        }

        #endregion IGridProvider

        #region IMultipleViewProvider

        int IMultipleViewProvider.CurrentView
        {
            get
            {
                return (int)OwningScheduler.DisplayMode;
            }
        }

        int[] IMultipleViewProvider.GetSupportedViews()
        {
            int[] supportedViews = new int[3];

            supportedViews[0] = (int)CalendarMode.Month;
            supportedViews[1] = (int)CalendarMode.Year;
            supportedViews[2] = (int)CalendarMode.Decade;

            return supportedViews;
        }

        string IMultipleViewProvider.GetViewName(int viewId)
        {
            //switch (viewId)
            //{
            //    case 0:
            //        {
            //            return System.SR.Get(SRID.SchedulerAutomationPeer_MonthMode);
            //        }

            //    case 1:
            //        {
            //            return System.SR.Get(SRID.SchedulerAutomationPeer_YearMode);
            //        }

            //    case 2:
            //        {
            //            return System.SR.Get(SRID.SchedulerAutomationPeer_DecadeMode);
            //        }
            //}

            //

            return String.Empty;
        }

        void IMultipleViewProvider.SetCurrentView(int viewId)
        {
            OwningScheduler.DisplayMode = (CalendarMode)viewId;
        }

        #endregion IMultipleViewProvider

        #region ISelectionProvider

        bool ISelectionProvider.CanSelectMultiple
        {
            get
            {
                return OwningScheduler.SelectionMode == CalendarSelectionMode.SingleRange || OwningScheduler.SelectionMode == CalendarSelectionMode.MultipleRange;
            }
        }

        bool ISelectionProvider.IsSelectionRequired
        {
            get
            {
                return false;
            }
        }

        IRawElementProviderSimple[] ISelectionProvider.GetSelection()
        {
            List<IRawElementProviderSimple> providers = new List<IRawElementProviderSimple>();

            foreach (DateTime date in OwningScheduler.SelectedDatesInternal)
            {
                AutomationPeer peer = GetOrCreateDateTimeAutomationPeer(date, CalendarMode.Month);
                providers.Add(ProviderFromPeer(peer));
            }

            if (providers.Count > 0)
            {
                return providers.ToArray();
            }

            return null;
        }

        #endregion ISelectionProvider

        #region IItemContainerProvider

        IRawElementProviderSimple IItemContainerProvider.FindItemByProperty(IRawElementProviderSimple startAfterProvider, int propertyId, object value)
        {
            DateTimeAutomationPeer startAfterDatePeer = null;

            if (startAfterProvider != null)
            {
                startAfterDatePeer = PeerFromProvider(startAfterProvider) as DateTimeAutomationPeer;
                // if provider is not null, peer must exist
                if (startAfterDatePeer == null)
                {
                    throw new InvalidOperationException();
                }
            }

            DateTime? nextDate = null;
            CalendarMode currentMode;

            if (propertyId == SelectionItemPatternIdentifiers.IsSelectedProperty.Id)
            {
                currentMode = CalendarMode.Month;
                nextDate = GetNextSelectedDate(startAfterDatePeer, (bool)value);
            }
            else if (propertyId == AutomationElementIdentifiers.NameProperty.Id)
            {
                // finds the button for the given DateTime
                DateTimeFormatInfo format = DateTimeHelper.GetCurrentDateFormat();
                DateTime parsedDate;
                if (DateTime.TryParse((value as string), format, DateTimeStyles.None, out parsedDate))
                {
                    nextDate = parsedDate;
                }

                if (!nextDate.HasValue || (startAfterDatePeer != null && nextDate <= startAfterDatePeer.Date))
                {
                    throw new InvalidOperationException();
                }

                currentMode = (startAfterDatePeer != null) ? startAfterDatePeer.ButtonMode : OwningScheduler.DisplayMode;
            }
            else if (propertyId == 0 || propertyId == AutomationElementIdentifiers.ControlTypeProperty.Id)
            {
                // propertyId = 0 returns the button next to the startAfter or the DisplayDate if startAfter is null
                // All items here are buttons, so same behaviour as propertyId = 0
                if (propertyId == AutomationElementIdentifiers.ControlTypeProperty.Id && (int)value != ControlType.Button.Id)
                {
                    return null;
                }
                currentMode = (startAfterDatePeer != null) ? startAfterDatePeer.ButtonMode : OwningScheduler.DisplayMode;
                nextDate = GetNextDate(startAfterDatePeer, currentMode);
            }
            else
            {
                throw new ArgumentException();
            }

            if (nextDate.HasValue)
            {
                AutomationPeer nextPeer = GetOrCreateDateTimeAutomationPeer(nextDate.Value, currentMode);
                if (nextPeer != null)
                {
                    return ProviderFromPeer(nextPeer);
                }
            }
            return null;
        }

        private DateTime? GetNextDate(DateTimeAutomationPeer currentDatePeer, CalendarMode currentMode)
        {
            DateTime? nextDate = null;

            DateTime startDate = (currentDatePeer != null) ? currentDatePeer.Date : OwningScheduler.DisplayDate;

            if (currentMode == CalendarMode.Month)
                nextDate = startDate.AddDays(1);
            else if (currentMode == CalendarMode.Year)
                nextDate = startDate.AddMonths(1);
            else if (currentMode == CalendarMode.Decade)
                nextDate = startDate.AddYears(1);

            return nextDate;
        }

        private DateTime? GetNextSelectedDate(DateTimeAutomationPeer currentDatePeer, bool isSelected)
        {
            DateTime startDate = (currentDatePeer != null) ? currentDatePeer.Date : OwningScheduler.DisplayDate;

            if (isSelected)
            {
                // If SelectedDatesInternal is empty or startDate is beyond last SelectedDate
                if (OwningScheduler.SelectedDatesInternal.MaximumDate == DateTime.MinValue || OwningScheduler.SelectedDatesInternal.MaximumDate <= startDate)
                {
                    return null;
                }
                // startDate is before first SelectedDate
                if (OwningScheduler.SelectedDatesInternal.MinimumDate != DateTime.MinValue && startDate < OwningScheduler.SelectedDatesInternal.MinimumDate)
                {
                    return OwningScheduler.SelectedDatesInternal.MinimumDate;
                }
            }
            while (true)
            {
                startDate = startDate.AddDays(1);
                if (OwningScheduler.SelectedDatesInternal.Contains(startDate) == isSelected)
                {
                    break;
                }
            }

            return startDate;
        }

        #endregion IItemContainerProvider

        #region ITableProvider

        RowOrColumnMajor ITableProvider.RowOrColumnMajor
        {
            get
            {
                return RowOrColumnMajor.RowMajor;
            }
        }

        IRawElementProviderSimple[] ITableProvider.GetColumnHeaders()
        {
            if (OwningScheduler.DisplayMode == CalendarMode.Month)
            {
                List<IRawElementProviderSimple> providers = new List<IRawElementProviderSimple>();

                foreach (UIElement child in OwningGrid.Children)
                {
                    int childRow = (int)child.GetValue(Grid.RowProperty);

                    if (childRow == 0)
                    {
                        AutomationPeer peer = CreatePeerForElement(child);

                        if (peer != null)
                        {
                            providers.Add(ProviderFromPeer(peer));
                        }
                    }
                }

                if (providers.Count > 0)
                {
                    return providers.ToArray();
                }
            }

            return null;
        }

        // If WeekNumber functionality is supported by Scheduler in the future,
        // this method should return weeknumbers
        IRawElementProviderSimple[] ITableProvider.GetRowHeaders()
        {
            return null;
        }

        #endregion ITableProvider

        /// <summary>
        /// Used to cache realized peers. We donot store references to virtualized peers.
        /// </summary>
        private Dictionary<DateTimeCalendarModePair, DateTimeAutomationPeer> DateTimePeers
        {
            get { return _dataChildren; }

            set { _dataChildren = value; }
        }

        private Dictionary<DateTimeCalendarModePair, WeakReference> WeakRefElementProxyStorage
        {
            get { return _weakRefElementProxyStorage; }
        }

        #region Private Data

        private Dictionary<DateTimeCalendarModePair, DateTimeAutomationPeer> _dataChildren = new Dictionary<DateTimeCalendarModePair, DateTimeAutomationPeer>();
        private readonly Dictionary<DateTimeCalendarModePair, WeakReference> _weakRefElementProxyStorage = new Dictionary<DateTimeCalendarModePair, WeakReference>();

        #endregion Private Data
    }

    internal struct DateTimeCalendarModePair
    {
        internal DateTimeCalendarModePair(DateTime date, CalendarMode mode)
        {
            _buttonMode = mode;
            _date = date;
        }

        private CalendarMode _buttonMode;
        private DateTime _date;
    }
}