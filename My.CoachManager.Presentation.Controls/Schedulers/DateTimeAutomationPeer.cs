using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using My.CoachManager.CrossCutting.Core.Helpers;

namespace My.CoachManager.Presentation.Controls.Schedulers
{
    /// <summary>
    /// AutomationPeer for SchedulerDay and SchedulerButton
    /// </summary>
    public sealed class DateTimeAutomationPeer : AutomationPeer, ISelectionItemProvider, ITableItemProvider, IInvokeProvider, IVirtualizedItemProvider
    {
        /// <summary>
        /// Initializes a new instance of the DateTimeAutomationPeer class.
        /// </summary>
        /// <param name="date"></param>
        /// <param name="owningScheduler"></param>
        /// <param name="buttonMode"></param>
        internal DateTimeAutomationPeer(DateTime date, Scheduler owningScheduler, CalendarMode buttonMode)
        {
            if (date == null)
            {
                throw new ArgumentNullException(nameof(date));
            }

            Date = date;
            ButtonMode = buttonMode;
            OwningScheduler = owningScheduler ?? throw new ArgumentNullException(nameof(owningScheduler));
        }

        #region Private Properties

        private Scheduler OwningScheduler
        {
            get;
            set;
        }

        internal DateTime Date
        {
            get;
            private set;
        }

        internal CalendarMode ButtonMode
        {
            get;
            private set;
        }

        internal bool IsDayButton
        {
            get
            {
                return ButtonMode == CalendarMode.Month;
            }
        }

        private IRawElementProviderSimple OwningSchedulerProvider
        {
            get
            {
                if (OwningScheduler != null)
                {
                    AutomationPeer peer = UIElementAutomationPeer.CreatePeerForElement(OwningScheduler);

                    if (peer != null)
                    {
                        return ProviderFromPeer(peer);
                    }
                }

                return null;
            }
        }

        internal ItemsControl OwningButton
        {
            get
            {
                if (OwningScheduler.DisplayMode != ButtonMode)
                {
                    return null;
                }

                if (IsDayButton)
                {
                    return OwningScheduler.MonthControl?.GetSchedulerDay(Date);
                }
                else
                {
                    return OwningScheduler.MonthControl?.GetSchedulerButton(Date, ButtonMode);
                }
            }
        }

        internal FrameworkElementAutomationPeer WrapperPeer
        {
            get
            {
                ItemsControl owningButton = OwningButton;
                if (owningButton != null)
                {
                    return UIElementAutomationPeer.CreatePeerForElement(owningButton) as FrameworkElementAutomationPeer;
                }
                return null;
            }
        }

        #endregion Private Properties

        #region AutomationPeer override Methods

        protected override string GetAcceleratorKeyCore()
        {
            AutomationPeer wrapperPeer = WrapperPeer;
            if (wrapperPeer != null)
            {
                return wrapperPeer.GetAcceleratorKey();
            }
            else
            {
                ThrowElementNotAvailableException();
            }

            return String.Empty;
        }

        protected override string GetAccessKeyCore()
        {
            AutomationPeer wrapperPeer = WrapperPeer;
            if (wrapperPeer != null)
            {
                return wrapperPeer.GetAccessKey();
            }
            else
            {
                ThrowElementNotAvailableException();
            }

            return String.Empty;
        }

        protected override AutomationControlType GetAutomationControlTypeCore()
        {
            return AutomationControlType.Button;
        }

        protected override string GetAutomationIdCore()
        {
            AutomationPeer wrapperPeer = WrapperPeer;
            if (wrapperPeer != null)
            {
                return wrapperPeer.GetAutomationId();
            }
            else
            {
                ThrowElementNotAvailableException();
            }

            return String.Empty;
        }

        protected override Rect GetBoundingRectangleCore()
        {
            AutomationPeer wrapperPeer = WrapperPeer;
            if (wrapperPeer != null)
            {
                return wrapperPeer.GetBoundingRectangle();
            }
            else
            {
                ThrowElementNotAvailableException();
            }

            return new Rect();
        }

        protected override List<AutomationPeer> GetChildrenCore()
        {
            AutomationPeer wrapperPeer = WrapperPeer;
            if (wrapperPeer != null)
            {
                return wrapperPeer.GetChildren();
            }
            else
            {
                ThrowElementNotAvailableException();
            }

            return null;
        }

        protected override string GetClassNameCore()
        {
            AutomationPeer wrapperPeer = WrapperPeer;
            return (wrapperPeer != null) ? wrapperPeer.GetClassName() : (IsDayButton) ? "SchedulerDay" : "SchedulerButton";
        }

        protected override Point GetClickablePointCore()
        {
            AutomationPeer wrapperPeer = WrapperPeer;
            if (wrapperPeer != null)
            {
                return wrapperPeer.GetClickablePoint();
            }
            else
            {
                ThrowElementNotAvailableException();
            }

            return new Point(double.NaN, double.NaN);
        }

        protected override string GetHelpTextCore()
        {
            string dateString = DateTimeHelper.ToLongDateString(Date, DateTimeHelper.GetCulture());
            if (IsDayButton && OwningScheduler.BlackoutDates.Contains(Date))
            {
                return string.Format(DateTimeHelper.GetCurrentDateFormat(), string.Empty, dateString);
            }

            return dateString;
        }

        protected override string GetItemStatusCore()
        {
            AutomationPeer wrapperPeer = WrapperPeer;
            if (wrapperPeer != null)
            {
                return wrapperPeer.GetItemStatus();
            }
            else
            {
                ThrowElementNotAvailableException();
            }

            return String.Empty;
        }

        protected override string GetItemTypeCore()
        {
            AutomationPeer wrapperPeer = WrapperPeer;
            if (wrapperPeer != null)
            {
                return wrapperPeer.GetItemType();
            }
            else
            {
                ThrowElementNotAvailableException();
            }

            return String.Empty;
        }

        protected override AutomationPeer GetLabeledByCore()
        {
            AutomationPeer wrapperPeer = WrapperPeer;
            if (wrapperPeer != null)
            {
                return wrapperPeer.GetLabeledBy();
            }
            else
            {
                ThrowElementNotAvailableException();
            }

            return null;
        }

        protected override AutomationLiveSetting GetLiveSettingCore()
        {
            AutomationPeer wrapperPeer = WrapperPeer;
            if (wrapperPeer != null)
            {
                return wrapperPeer.GetLiveSetting();
            }
            else
            {
                ThrowElementNotAvailableException();
            }

            return AutomationLiveSetting.Off;
        }

        protected override string GetLocalizedControlTypeCore()
        {
            return string.Empty;
        }

        protected override string GetNameCore()
        {
            string dateString = "";

            switch (ButtonMode)
            {
                case CalendarMode.Month:
                    dateString = DateTimeHelper.ToLongDateString(Date, DateTimeHelper.GetCulture());
                    break;
                case CalendarMode.Year:
                    dateString = DateTimeHelper.ToYearMonthPatternString(Date, DateTimeHelper.GetCulture());
                    break;
                case CalendarMode.Decade:
                    dateString = DateTimeHelper.ToYearString(Date, DateTimeHelper.GetCulture());
                    break;
            }

            return dateString;

        }

        protected override AutomationOrientation GetOrientationCore()
        {
            AutomationPeer wrapperPeer = WrapperPeer;
            if (wrapperPeer != null)
            {
                return wrapperPeer.GetOrientation();
            }
            ThrowElementNotAvailableException();

            return AutomationOrientation.None;
        }

        /// <summary>
        /// Gets the control pattern that is associated with the specified System.Windows.Automation.Peers.PatternInterface.
        /// </summary>
        /// <param name="patternInterface">A value from the System.Windows.Automation.Peers.PatternInterface enumeration.</param>
        /// <returns>The object that supports the specified pattern, or null if unsupported.</returns>
        public override object GetPattern(PatternInterface patternInterface)
        {
            object result = null;
            ItemsControl owningButton = OwningButton;

            switch (patternInterface)
            {
                case PatternInterface.Invoke:
                case PatternInterface.GridItem:
                    {
                        if (owningButton != null)
                        {
                            result = this;
                        }
                        break;
                    }
                case PatternInterface.TableItem:
                    {
                        if (IsDayButton && owningButton != null)
                        {
                            result = this;
                        }
                        break;
                    }
                case PatternInterface.SelectionItem:
                    {
                        result = this;
                        break;
                    }
                case PatternInterface.VirtualizedItem:
                    if (VirtualizedItemPatternIdentifiers.Pattern != null)
                    {
                        if (owningButton == null)
                        {
                            result = this;
                        }
                        else
                        {

                                return this;
                        }
                    }
                    break;
            }

            return result;
        }

        protected override bool HasKeyboardFocusCore()
        {
            AutomationPeer wrapperPeer = WrapperPeer;
            if (wrapperPeer != null)
            {
                return wrapperPeer.HasKeyboardFocus();
            }
            ThrowElementNotAvailableException();

            return false;
        }

        protected override bool IsContentElementCore()
        {
            AutomationPeer wrapperPeer = WrapperPeer;
            if (wrapperPeer != null)
            {
                return wrapperPeer.IsContentElement();
            }
            ThrowElementNotAvailableException();

            return true;
        }

        protected override bool IsControlElementCore()
        {
            AutomationPeer wrapperPeer = WrapperPeer;
            if (wrapperPeer != null)
            {
                return wrapperPeer.IsControlElement();
            }
            ThrowElementNotAvailableException();

            return true;
        }

        protected override bool IsEnabledCore()
        {
            AutomationPeer wrapperPeer = WrapperPeer;
            if (wrapperPeer != null)
            {
                return wrapperPeer.IsEnabled();
            }
            else
            {
                ThrowElementNotAvailableException();
            }

            return false;
        }

        protected override bool IsKeyboardFocusableCore()
        {
            AutomationPeer wrapperPeer = WrapperPeer;
            if (wrapperPeer != null)
            {
                return wrapperPeer.IsKeyboardFocusable();
            }
            else
            {
                ThrowElementNotAvailableException();
            }

            return false;
        }

        protected override bool IsOffscreenCore()
        {
            AutomationPeer wrapperPeer = WrapperPeer;
            if (wrapperPeer != null)
            {
                return wrapperPeer.IsOffscreen();
            }
            else
            {
                ThrowElementNotAvailableException();
            }

            return true;
        }

        protected override bool IsPasswordCore()
        {
            AutomationPeer wrapperPeer = WrapperPeer;
            if (wrapperPeer != null)
            {
                return wrapperPeer.IsPassword();
            }
            else
            {
                ThrowElementNotAvailableException();
            }

            return false;
        }

        protected override bool IsRequiredForFormCore()
        {
            AutomationPeer wrapperPeer = WrapperPeer;
            if (wrapperPeer != null)
            {
                return wrapperPeer.IsRequiredForForm();
            }
            else
            {
                ThrowElementNotAvailableException();
            }

            return false;
        }

        protected override void SetFocusCore()
        {
            UIElementAutomationPeer wrapperPeer = WrapperPeer;
            if (wrapperPeer != null)
            {
                wrapperPeer.SetFocus();
            }
            else
            {
                ThrowElementNotAvailableException();
            }
        }

        #endregion AutomationPeer override Methods

        #region IGridItemProvider

        /// <summary>
        /// Grid item column.
        /// </summary>
        int IGridItemProvider.Column
        {
            get
            {
                ItemsControl owningButton = OwningButton;
                if (owningButton != null)
                {
                    return (int)owningButton.GetValue(Grid.ColumnProperty);
                }
                else
                {
                    throw new ElementNotAvailableException();
                }
            }
        }

        /// <summary>
        /// Grid item column span.
        /// </summary>
        int IGridItemProvider.ColumnSpan
        {
            get
            {
                ItemsControl owningButton = OwningButton;
                if (owningButton != null)
                {
                    return (int)owningButton.GetValue(Grid.ColumnSpanProperty);
                }
                else
                {
                    throw new ElementNotAvailableException();
                }
            }
        }

        /// <summary>
        /// Grid item's containing grid.
        /// </summary>
        IRawElementProviderSimple IGridItemProvider.ContainingGrid => OwningSchedulerProvider;

        /// <summary>
        /// Grid item row.
        /// </summary>
        int IGridItemProvider.Row
        {
            get
            {
                ItemsControl owningButton = OwningButton;
                if (owningButton != null)
                {
                    if (IsDayButton)
                    {
                        Debug.Assert((int)owningButton.GetValue(Grid.RowProperty) > 0);

                        // we decrement the Row value by one since the first row is composed of DayTitles
                        return (int)owningButton.GetValue(Grid.RowProperty) - 1;
                    }
                    else
                    {
                        return (int)owningButton.GetValue(Grid.RowProperty);
                    }
                }
                else
                {
                    throw new ElementNotAvailableException();
                }
            }
        }

        /// <summary>
        /// Grid item row span.
        /// </summary>
        int IGridItemProvider.RowSpan
        {
            get
            {
                ItemsControl owningButton = OwningButton;
                if (owningButton != null)
                {
                    if (IsDayButton)
                    {
                        return (int)owningButton.GetValue(Grid.RowSpanProperty);
                    }
                    else
                    {
                        return 1;
                    }
                }
                else
                {
                    throw new ElementNotAvailableException();
                }
            }
        }

        #endregion IGridItemProvider

        #region ISelectionItemProvider

        /// <summary>
        /// True if the owning SchedulerDay is selected.
        /// </summary>
        bool ISelectionItemProvider.IsSelected
        {
            get
            {
                if (IsDayButton)
                {
                    return OwningScheduler.SelectedDatesInternal.Contains(Date);
                }

                return false;
            }
        }

        /// <summary>
        /// Selection items selection container.
        /// </summary>
        IRawElementProviderSimple ISelectionItemProvider.SelectionContainer => OwningSchedulerProvider;

        /// <summary>
        /// Adds selection item to selection.
        /// </summary>
        void ISelectionItemProvider.AddToSelection()
        {
            // Return if the day is already selected or if it is a BlackoutDate
            if (((ISelectionItemProvider)this).IsSelected)
            {
                return;
            }

            if (IsDayButton && EnsureSelection())
            {
                if (OwningScheduler.SelectionMode == CalendarSelectionMode.SingleDate)
                {
                    OwningScheduler.SelectedDateInternal = Date;
                }
                else
                {
                    OwningScheduler.SelectedDatesInternal.Add(Date);
                }
            }
            
        }

        /// <summary>
        /// Removes selection item from selection.
        /// </summary>
        void ISelectionItemProvider.RemoveFromSelection()
        {
            // Return if the item is not already selected.
            if (!((ISelectionItemProvider)this).IsSelected)
            {
                return;
            }

            if (IsDayButton)
            {
                OwningScheduler.SelectedDatesInternal.Remove(Date);
            }
            
        }

        /// <summary>
        /// Selects this item.
        /// </summary>
        void ISelectionItemProvider.Select()
        {
            ItemsControl owningButton = OwningButton;
            if (IsDayButton)
            {
                if (EnsureSelection() && OwningScheduler.SelectionMode == CalendarSelectionMode.SingleDate)
                {
                    OwningScheduler.SelectedDateInternal = Date;
                }
            }
            else if (owningButton != null && owningButton.IsEnabled)
            {
                owningButton.Focus();
            }
        }

        #endregion ISelectionItemProvider

        #region ITableItemProvider

        /// <summary>
        /// Gets the table item's column headers.
        /// </summary>
        /// <returns>The table item's column headers</returns>
        IRawElementProviderSimple[] ITableItemProvider.GetColumnHeaderItems()
        {
            if (IsDayButton && OwningButton != null)
            {
                if (OwningScheduler != null && OwningSchedulerProvider != null)
                {
                    IRawElementProviderSimple[] headers = ((ITableProvider)UIElementAutomationPeer.CreatePeerForElement(OwningScheduler)).GetColumnHeaders();

                    if (headers != null)
                    {
                        int column = ((IGridItemProvider)this).Column;
                        return new[] { headers[column] };
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Get's the table item's row headers.
        /// </summary>
        /// <returns>The table item's row headers</returns>
        IRawElementProviderSimple[] ITableItemProvider.GetRowHeaderItems()
        {
            return null;
        }

        #endregion ITableItemProvider

        #region IInvokeProvider

        void IInvokeProvider.Invoke()
        {
            ItemsControl owningButton = OwningButton;
            if (owningButton == null || !IsEnabled())
                throw new ElementNotEnabledException();

            // Async call of click event
            // In ClickHandler opens a dialog and suspend the execution we don't want to block this thread
            //Dispatcher.BeginInvoke(DispatcherPriority.Input, new DispatcherOperationCallback(delegate (object param)
            //{
            //    owningButton.AutomationButtonBaseClick();

            //    return null;
            //}), null);
        }

        #endregion IInvokeProvider

        #region IVirtualizedItemProvider

        void IVirtualizedItemProvider.Realize()
        {
            // Change Display mode
            if (OwningScheduler.DisplayMode != ButtonMode)
            {
                OwningScheduler.DisplayMode = ButtonMode;
            }

            // Bring into view
            OwningScheduler.DisplayDate = Date;
        }

        #endregion IVirtualizedItemProvider

        #region Private Methods

        private bool EnsureSelection()
        {
            // If the day is a blackout day or the SelectionMode is None, selection is not allowed
            if (OwningScheduler.BlackoutDates.Contains(Date) ||
                OwningScheduler.SelectionMode == CalendarSelectionMode.None)
            {
                return false;
            }

            return true;
        }

        private void ThrowElementNotAvailableException()
        {
            // To avoid the situation on legacy systems which may not have new unmanaged core. this check with old unmanaged core
            // avoids throwing exception and provide older behavior returning default values for items which are virtualized rather than throwing exception.
            if (VirtualizedItemPatternIdentifiers.Pattern != null)
                throw new ElementNotAvailableException();
        }

        #endregion Private Methods
    }
}
