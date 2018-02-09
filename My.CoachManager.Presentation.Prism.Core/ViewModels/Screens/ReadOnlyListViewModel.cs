﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using My.CoachManager.Presentation.Prism.Core.Filters;
using My.CoachManager.Presentation.Prism.Core.Services;
using My.CoachManager.Presentation.Prism.Core.ViewModels.Entities;
using Prism.Commands;

namespace My.CoachManager.Presentation.Prism.Core.ViewModels.Screens
{
    public abstract class ReadOnlyListViewModel<TEntityViewModel> : NavigatableWorkspaceViewModel, IReadOnlyListViewModel
        where TEntityViewModel : class, IEntityViewModel, INotifyPropertyChanged
    {
        #region Fields

        private ObservableCollection<TEntityViewModel> _items;
        private TEntityViewModel _selectedItem;
        private ObservableCollection<string> _displayedColumns;
        private Dictionary<object, string[]> _presetColumns;

        #endregion Fields

        #region Members

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        public ObservableCollection<TEntityViewModel> Items
        {
            get { return _items; }
            set
            {
                SetItems(value);
                RaisePropertyChanged(() => Items);
            }
        }

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        public FilteredCollectionView<TEntityViewModel> FilteredItems { get; private set; }

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        IFilteredCollection IReadOnlyListViewModel.FilteredItems
        {
            get { return FilteredItems; }
        }

        /// <summary>
        /// Gets or sets the selected item.
        /// </summary>
        public virtual TEntityViewModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                SetProperty(ref _selectedItem, value);
            }
        }

        /// <summary>
        /// Gets or sets the columns to displayed.
        /// </summary>
        public ObservableCollection<string> DisplayedColumns
        {
            get { return _displayedColumns; }
            set { SetProperty(ref _displayedColumns, value); }
        }

        /// <summary>
        /// Gets or sets the preset columns to displayed.
        /// </summary>
        public Dictionary<object, string[]> PresetColumns
        {
            get { return _presetColumns; }
            private set { SetProperty(ref _presetColumns, value); }
        }

        /// <summary>
        /// Command to change displayed columns.
        /// </summary>
        public DelegateCommand<object> ChangeDisplayedColumnsCommand { get; set; }

        /// <summary>
        /// Gets or sets the refresh command.
        /// </summary>
        public DelegateCommand RefreshCommand { get; set; }

        #endregion Members

        #region Methods

        #region Abstracts Methods

        /// <summary>
        /// Get Item View Type.
        /// </summary>
        /// <returns></returns>
        protected abstract Type GetItemViewType();

        #endregion Abstracts Methods

        #region Initialization

        /// <summary>
        /// Initializes commands.
        /// </summary>
        protected override void InitializeCommands()
        {
            base.InitializeCommands();

            RefreshCommand = new DelegateCommand(Refresh, CanRefresh);
            ChangeDisplayedColumnsCommand = new DelegateCommand<object>(ChangeDisplayedColumns);
        }

        /// <summary>
        /// Initializes Data.
        /// </summary>
        protected override void InitializeData()
        {
            base.InitializeData();

            _items = new ObservableCollection<TEntityViewModel>();
            FilteredItems = new FilteredCollectionView<TEntityViewModel>(Items);

            PresetColumns = new Dictionary<object, string[]>();
        }

        /// <summary>
        /// Set Items Collection.
        /// </summary>
        /// <param name="collection"></param>
        protected void SetItems(IEnumerable<TEntityViewModel> collection)
        {
            if (_items == null)
            {
                _items = new ObservableCollection<TEntityViewModel>();
            }

            Items.Clear();
            Items.AddRange(collection);
        }

        #endregion Initialization

        #region Open

        /// <summary>
        /// Edit Item.
        /// </summary>
        public virtual void Open(TEntityViewModel item)
        {
            if (CanOpen(item))
            {
                Locator.GetInstance<INavigationService>().NavigateTo(GetItemViewType(), item.Id);
            }
        }

        /// <summary>
        /// Can Edit item.
        /// </summary>
        public virtual bool CanOpen(TEntityViewModel item)
        {
            return Mode == ScreenMode.Read && item != null && GetItemViewType() != null;
        }

        #endregion Open

        #region Refresh

        /// <summary>
        /// Refresh Items.
        /// </summary>
        public virtual void Refresh()
        {
            RefreshDataAsync();
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
        /// Action when "Enter" key is down.
        /// </summary>
        protected override void Enter()
        {
            Open(SelectedItem);
        }

        /// <summary>
        /// Do action by keyboard trigger.
        /// </summary>
        protected override void KeyboardAction(KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.F5:
                    Refresh();
                    break;
            }

            base.KeyboardAction(e);
        }

        #endregion Keyboard

        #region Columns Management

        /// <summary>
        /// Changes displayed columns.
        /// </summary>
        protected void ChangeDisplayedColumns(object type)
        {
            if (type != null)
                DisplayedColumns = new ObservableCollection<string>(PresetColumns[type]);
        }

        /// <summary>
        /// Add a preset columns.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="columns"></param>
        protected void AddPresetColumns(object key, string[] columns)
        {
            if (PresetColumns != null)
            {
                PresetColumns.Add(key, columns);
            }
        }

        #endregion Columns Management

        #endregion Methods
    }
}