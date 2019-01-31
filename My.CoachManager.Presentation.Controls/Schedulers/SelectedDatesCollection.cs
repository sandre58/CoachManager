using My.CoachManager.CrossCutting.Core.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Controls;

namespace My.CoachManager.Presentation.Controls.Schedulers
{
    /// <summary>
    /// Represents the collection of SelectedDatesInternal for the Scheduler Control.
    /// </summary>
    public sealed class SelectedDatesCollection : ObservableCollection<DateTime>
    {
        #region Data

        private readonly Collection<DateTime> _addedItems;
        private readonly Collection<DateTime> _removedItems;
        private readonly Thread _dispatcherThread;
        private bool _isAddingRange;
        private readonly Scheduler _owner;

        #endregion Data

        /// <summary>
        /// Initializes a new instance of the SchedulerSelectedDatesCollection class.
        /// </summary>
        /// <param name="owner"></param>
        public SelectedDatesCollection(Scheduler owner)
        {
            _dispatcherThread = Thread.CurrentThread;
            _owner = owner;
            _addedItems = new Collection<DateTime>();
            _removedItems = new Collection<DateTime>();
        }

        #region Properties

        public DateTime? MaximumDate => Count > 0 ? this.Max() : (DateTime?) null;

        public DateTime? MinimumDate => Count > 0 ? this.Min() : (DateTime?)null;

        #endregion Properties

        #region Public methods

        /// <summary>
        /// Adds a range of dates to the Scheduler SelectedDatesInternal.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public void AddRange(DateTime start, DateTime end)
        {
            BeginAddRange();

            // If SchedulerSelectionMode.SingleRange and a user programmatically tries to add multiple ranges, we will throw away the old range and replace it with the new one.
            if (_owner.SelectionMode == CalendarSelectionMode.SingleRange && Count > 0)
            {
                ClearInternal();
            }

            foreach (DateTime current in GetDaysInRange(start, end))
            {
                if (Scheduler.IsValidDateSelection(_owner, current))
                {
                    Add(current);
                }
            }

            EndAddRange();
        }

        #endregion Public methods

        #region Protected methods

        /// <summary>
        /// Clears all the items of the SelectedDatesInternal.
        /// </summary>
        protected override void ClearItems()
        {
            ClearInternal();

            RaiseSelectionChanged(_removedItems, new List<object>());
            _removedItems.Clear();
        }

        /// <summary>
        /// Clears all the items of the SelectedDatesInternal.
        /// </summary>
        internal void ClearInternal()
        {
            if (Count > 0)
            {
                foreach (DateTime item in this)
                {
                    _removedItems.Add(item);
                }

                base.ClearItems();
            }

        }

        /// <summary>
        /// Inserts the item in the specified position of the SelectedDatesInternal collection.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        protected override void InsertItem(int index, DateTime item)
        {
            if (!IsValidThread())
            {
                throw new NotSupportedException();
            }

            if (!Contains(item))
            {
                Collection<DateTime> addedItems = new Collection<DateTime>();

                bool isCleared = CheckSelectionMode();

                if (Scheduler.IsValidDateSelection(_owner, item))
                {
                    // If the Collection is cleared since it is SingleRange and it had another range
                    // set the index to 0
                    if (isCleared)
                    {
                        index = 0;
                    }

                    base.InsertItem(index, item);

                    if (!_isAddingRange)
                    {
                        addedItems.Add(item);

                        RaiseSelectionChanged(_removedItems, addedItems);
                        _removedItems.Clear();
                    }
                    else
                    {
                        _addedItems.Add(item);
                    }
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }

        /// <summary>
        /// Removes the item at the specified position.
        /// </summary>
        /// <param name="index"></param>
        protected override void RemoveItem(int index)
        {
            if (!IsValidThread())
            {
                throw new NotSupportedException();
            }

            if (index >= Count)
            {
                base.RemoveItem(index);
            }
            else
            {
                Collection<DateTime> addedItems = new Collection<DateTime>();
                Collection<DateTime> removedItems = new Collection<DateTime>();

                removedItems.Add(this[index]);
                base.RemoveItem(index);

                RaiseSelectionChanged(removedItems, addedItems);
            }
        }

        /// <summary>
        /// The object in the specified index is replaced with the provided item.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        protected override void SetItem(int index, DateTime item)
        {
            if (!IsValidThread())
            {
                throw new NotSupportedException();
            }

            if (!Contains(item))
            {
                Collection<DateTime> addedItems = new Collection<DateTime>();
                Collection<DateTime> removedItems = new Collection<DateTime>();

                if (index >= Count)
                {
                    base.SetItem(index, item);
                }
                else
                {
                    if (DateTime.Compare(this[index], item) != 0 && Scheduler.IsValidDateSelection(_owner, item))
                    {
                        removedItems.Add(this[index]);
                        base.SetItem(index, item);

                        addedItems.Add(item);

                        RaiseSelectionChanged(removedItems, addedItems);
                    }
                }
            }
        }

        #endregion Protected methods

        #region Private Methods

        private void RaiseSelectionChanged(IList removedItems, IList addedItems)
        {
            _owner.OnSelectedDatesCollectionChanged(new SchedulerSelectionChangedEventArgs(Scheduler.SelectedDatesChangedEvent, removedItems, addedItems));
        }

        private void BeginAddRange()
        {
            Debug.Assert(!_isAddingRange);
            _isAddingRange = true;
        }

        private void EndAddRange()
        {
            Debug.Assert(_isAddingRange);

            _isAddingRange = false;
            RaiseSelectionChanged(_removedItems, _addedItems);
            _removedItems.Clear();
            _addedItems.Clear();
            //_owner.UpdateCellItems();
        }

        private bool CheckSelectionMode()
        {
            if (_owner.SelectionMode == CalendarSelectionMode.None)
            {
                throw new InvalidOperationException();
            }

            if (_owner.SelectionMode == CalendarSelectionMode.SingleDate && Count > 0)
            {
                throw new InvalidOperationException();
            }

            // if user tries to add an item into the SelectedDatesInternal in SingleRange mode, we throw away the old range and replace it with the new one
            // in order to provide the removed items without an additional event, we are calling ClearInternal
            if (_owner.SelectionMode == CalendarSelectionMode.SingleRange && !_isAddingRange && Count > 0)
            {
                ClearInternal();
                return true;
            }

            return false;
        }

        private bool IsValidThread()
        {
            return Thread.CurrentThread == _dispatcherThread;
        }

        private static IEnumerable<DateTime> GetDaysInRange(DateTime start, DateTime end)
        {
            // increment parameter specifies if the Days were selected in Descending order or Ascending order
            // based on this value, we add the days in the range either in Ascending order or in Descending order
            int increment = GetDirection(start, end);

            DateTime? rangeStart = start;

            do
            {
                yield return rangeStart.Value;
                rangeStart = DateTimeHelper.AddDays(rangeStart.Value, increment);
            }
            while (rangeStart.HasValue && DateTime.Compare(end, rangeStart.Value) != -increment);
        }

        private static int GetDirection(DateTime start, DateTime end)
        {
            return (DateTime.Compare(end, start) >= 0) ? 1 : -1;
        }

        #endregion Private Methods
    }
}