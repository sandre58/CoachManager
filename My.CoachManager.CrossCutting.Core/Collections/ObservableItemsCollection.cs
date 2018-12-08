using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;

namespace My.CoachManager.CrossCutting.Core.Collections
{
    /// <summary>
    /// An <see cref="ObservableCollection{T}"/> which is also capable of tracking when an item has changed.
    /// </summary>
    /// <typeparam name="T">The type of the items within this instance.</typeparam>
    [DebuggerDisplay("Count = {" + nameof(Count) + "}")]
    public class ObservableItemsCollection<T> : ObservableCollection<T>
    {

        #region Constructors

        public ObservableItemsCollection()
        {
            Initialize();
        }

        public ObservableItemsCollection(IEnumerable<T> value) : base(value)
        {
            Initialize();
        }


        private void Initialize()
        {
            CollectionChanged += ObservableCollectionCollectionChanged;
        }
        #endregion

        #region Methods

        private void ObservableCollectionCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
                foreach (var item in e.NewItems)
                    ((INotifyPropertyChanged)item).PropertyChanged += ItemPropertyChanged;
            if (e.OldItems == null) return;
            {
                foreach (var item in e.OldItems)
                    ((INotifyPropertyChanged)item).PropertyChanged -= ItemPropertyChanged;
            }
        }

        private void ItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (Contains((T)sender))
            {
                var args = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, sender, sender,
                    IndexOf((T) sender));
                OnCollectionChanged(args);
            }
        }

        #endregion

    }
}