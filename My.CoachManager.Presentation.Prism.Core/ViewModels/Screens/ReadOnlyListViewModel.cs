using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Input;
using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.Presentation.Prism.Core.Filters;
using My.CoachManager.Presentation.Prism.Core.Interactivity;
using My.CoachManager.Presentation.Prism.Core.Services;
using My.CoachManager.Presentation.Prism.Core.ViewModels.Entities;
using Prism.Commands;
using Prism.Events;

namespace My.CoachManager.Presentation.Prism.Core.ViewModels.Screens
{
    public abstract class ReadOnlyListViewModel<TEntityViewModel> : NavigatableWorkspaceViewModel
        where TEntityViewModel : class, IEntityViewModel, INotifyPropertyChanged
    {
        #region Fields

        private ObservableCollection<TEntityViewModel> _items;
        private TEntityViewModel _selectedItem;

        #endregion Fields

        #region Constructor

        /// <summary>
        /// Initialise a new instance of <see cref="ReadOnlyListViewModel{TEntityViewModel}"/>.
        /// </summary>
        /// <param name="dialogService">The dialog service.</param>
        /// <param name="eventAggregator"></param>
        /// <param name="logger">The logger.</param>
        protected ReadOnlyListViewModel(IDialogService dialogService, IEventAggregator eventAggregator, ILogger logger)
            : base(dialogService, eventAggregator, logger)
        {
            RefreshCommand = new DelegateCommand(Refresh, CanRefresh);
            KeyboardActionCommand = new DelegateCommand<KeyDownItemEventArgs>(KeyboardAction, CanKeyboardAction);
            _items = new ObservableCollection<TEntityViewModel>();
            FilteredItems = new FilteredCollectionView(Items);
        }

        #endregion Constructor

        #region Members

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        public ObservableCollection<TEntityViewModel> Items
        {
            get { return _items; }
            set
            {
                SetProperty(ref _items, value);
            }
        }

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        public FilteredCollectionView FilteredItems { get; private set; }

        /// <summary>
        /// Gets or sets the selected item.
        /// </summary>
        public virtual TEntityViewModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                SetProperty(ref _selectedItem, value, OnSelectedItemChanged);
            }
        }

        /// <summary>
        /// Gets or sets the keyboard action command.
        /// </summary>
        public DelegateCommand<KeyDownItemEventArgs> KeyboardActionCommand { get; set; }

        /// <summary>
        /// Gets or sets the refresh command.
        /// </summary>
        public DelegateCommand RefreshCommand { get; set; }

        #endregion Members

        #region Methods

        /// <summary>
        /// Set Items Collection.
        /// </summary>
        /// <param name="collection"></param>
        protected void SetCollection(IEnumerable<TEntityViewModel> collection)
        {
            Items.Clear();
            Items.AddRange(collection);
        }

        #region Refresh

        /// <summary>
        /// Refresh Items.
        /// </summary>
        public virtual void Refresh()
        {
            RefreshData();
        }

        /// <summary>
        /// Can refresh item.
        /// </summary>
        public virtual bool CanRefresh()
        {
            return Mode == ScreenMode.Read;
        }

        #endregion Refresh

        #region Keyboard

        /// <summary>
        /// Do action by keyboard trigger.
        /// </summary>
        public virtual void KeyboardAction(KeyDownItemEventArgs e)
        {
            switch (e.EventArgs.Key)
            {
                case Key.F5:
                    Refresh();
                    break;
            }
        }

        /// <summary>
        /// Can Remove item.
        /// </summary>
        public virtual bool CanKeyboardAction(KeyDownItemEventArgs e)
        {
            return Mode == ScreenMode.Read;
        }

        #endregion Keyboard

        /// <summary>
        /// Calls when selected item change.
        /// </summary>
        protected virtual void OnSelectedItemChanged()
        {
        }

        /// <summary>
        /// Gets specified property info.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        protected PropertyInfo GetPropertyInfo(string propertyName)
        {
            return typeof(TEntityViewModel).GetProperty(propertyName);
        }

        #endregion Methods
    }
}