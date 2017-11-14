using System;
using System.Collections.ObjectModel;

namespace My.CoachManager.Presentation.Prism.ViewModels
{
    public class ContactsCollection<T> : ObservableCollection<T>
        where T : ContactViewModel
    {
        public void Add()
        {
            Add(Activator.CreateInstance<T>());
        }

        protected override void InsertItem(int index, T addedItem)
        {
            base.InsertItem(index, addedItem);
            addedItem.PropertyChanged += OnItemPropertyChanged;

            UpdateCanAdd();
            UpdateCanRemove();
        }

        protected override void RemoveItem(int index)
        {
            base.RemoveItem(index);

            UpdateCanAdd();
            UpdateCanRemove();
        }

        protected override void MoveItem(int oldIndex, int newIndex)
        {
            base.MoveItem(oldIndex, newIndex);

            UpdateCanAdd();
            UpdateCanRemove();
        }

        private void UpdateCanAdd()
        {
            foreach (var item in this)
            {
                item.CanAdd = IndexOf(item) == Count - 1 && !string.IsNullOrEmpty(item.Value);
            }
        }

        private void UpdateCanRemove()
        {
            foreach (var item in this)
            {
                item.CanRemove = Count > 1 || !string.IsNullOrEmpty(item.Value);
            }
        }

        private void OnItemPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "CanAdd":
                case "CanRemove":
                    break;

                default:
                    UpdateCanAdd();
                    UpdateCanRemove();
                    break;
            }
        }
    }
}