using System;
using System.Collections.ObjectModel;

namespace My.CoachManager.Presentation.ViewModels
{
    public class ContactsCollection<T> : ObservableCollection<T>
        where T : ContactViewModel
    {
        /// <summary>
        /// Add a default new instance.
        /// </summary>
        public void Add()
        {
            Add(Activator.CreateInstance<T>());
        }

        protected override void InsertItem(int index, T addedItem)
        {
            base.InsertItem(index, addedItem);

            UpdateCanAdd();
        }

        protected override void RemoveItem(int index)
        {
            base.RemoveItem(index);

            UpdateCanAdd();
        }

        protected override void MoveItem(int oldIndex, int newIndex)
        {
            base.MoveItem(oldIndex, newIndex);

            UpdateCanAdd();
        }

        private void UpdateCanAdd()
        {
            foreach (var item in this)
            {
                item.CanAdd = IndexOf(item) == Count - 1;
            }
        }
    }
}