using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using My.CoachManager.Presentation.Prism.Core.Dialog;
using My.CoachManager.Presentation.Prism.Core.Filters;
using Prism.Commands;

namespace My.CoachManager.Presentation.Prism.Core.ViewModels.Screens
{
    public abstract class FiltersViewModel : WorkspaceDialogViewModel, IFiltersViewModel
    {
        #region Fields

        private ObservableCollection<FilterViewModel> _filters;
        private bool _updateOnLive;

        #endregion Fields

        #region Members

        /// <summary>
        /// Gets or sets a methods that create a filter.
        /// </summary>
        public Func<string, IFilterViewModel> CreateFilter { get; set; }

        /// <summary>
        /// Gets the allowed filters.
        /// </summary>
        public Dictionary<string, string> AllowedFilters { get; }

        /// <summary>
        /// Gets or sets the filters.
        /// </summary>
        public ObservableCollection<FilterViewModel> Filters
        {
            get { return _filters; }
            set
            {
                SetProperty(ref _filters, value, () =>
                {
                    if (_filters != null)
                    {
                        value.CollectionChanged -= FiltersOnCollectionChanged;
                        value.CollectionChanged += FiltersOnCollectionChanged;
                        foreach (var item in _filters)
                        {
                            item.PropertyChanged += Filter_PropertyChanged;
                        }
                    }
                });
            }
        }

        /// <summary>
        /// Gets or sets the update on live.
        /// </summary>
        public bool UpdateOnLive
        {
            get { return _updateOnLive; }
            set
            {
                SetProperty(ref _updateOnLive, value, () =>
                {
                    OnFiltersChanged(EventArgs.Empty);
                });
            }
        }

        /// <summary>
        /// Gets or sets the Add command.
        /// </summary>
        public DelegateCommand<string> AddFilterCommand { get; set; }

        /// <summary>
        /// Gets or sets the Remove command.
        /// </summary>
        public DelegateCommand<IFilter> RemoveFilterCommand { get; set; }

        /// <summary>
        /// Gets or sets the Reset command.
        /// </summary>
        public DelegateCommand ResetCommand { get; set; }

        /// <summary>
        /// Gets or sets the Validate command.
        /// </summary>
        public DelegateCommand ValidateCommand { get; set; }

        /// <summary>
        /// Gets or sets the Validate command.
        /// </summary>
        public DelegateCommand CancelCommand { get; set; }

        #endregion Members

        #region Constructor

        /// <summary>
        /// Initialise a new instance of <see cref="FiltersViewModel"/>
        /// </summary>
        public FiltersViewModel()
        {
            Filters = new ObservableCollection<FilterViewModel>();

            AllowedFilters = new Dictionary<string, string>();

            AddFilterCommand = new DelegateCommand<string>(AddFilter, CanAddFilter);
            RemoveFilterCommand = new DelegateCommand<IFilter>(RemoveFilter, CanRemoveFilter);
            ResetCommand = new DelegateCommand(Reset, CanReset);
            ValidateCommand = new DelegateCommand(() => Close(DialogResult.Ok));
            CancelCommand = new DelegateCommand(() => Close(DialogResult.Cancel));
        }

        #endregion Constructor

        #region Methods

        /// <summary>
        /// Add a filter.
        /// </summary>
        /// <param name="propertyName"></param>
        private void AddFilter(string propertyName)
        {
            if (CreateFilter != null)
            {
                var filter = CreateFilter(propertyName);

                if (filter != null)
                {
                    Filters.Add((FilterViewModel)filter);
                    AddFilterCommand.RaiseCanExecuteChanged();
                    ResetCommand.RaiseCanExecuteChanged();
                    RemoveFilterCommand.RaiseCanExecuteChanged();
                }
            }
        }

        /// <summary>
        /// Can Add a filter ?
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        private bool CanAddFilter(string propertyName)
        {
            return true;
        }

        /// <summary>
        /// Remove a filter.
        /// </summary>
        /// <param name="filter"></param>
        private void RemoveFilter(IFilter filter)
        {
            Filters.Remove(Filters.FirstOrDefault(x => ReferenceEquals(x.Filter, filter)));
            AddFilterCommand.RaiseCanExecuteChanged();
            ResetCommand.RaiseCanExecuteChanged();
            RemoveFilterCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Can remove a filter ?
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        private bool CanRemoveFilter(IFilter filter)
        {
            return true;
        }

        /// <summary>
        /// Reset filters.
        /// </summary>
        private void Reset()
        {
            Filters.Clear();
            AddFilterCommand.RaiseCanExecuteChanged();
            ResetCommand.RaiseCanExecuteChanged();
            RemoveFilterCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Can reset filters ?
        /// </summary>
        /// <returns></returns>
        private bool CanReset()
        {
            return Filters.Any();
        }

        #endregion Methods

        #region Events

        private void FiltersOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            switch (notifyCollectionChangedEventArgs.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (var item in notifyCollectionChangedEventArgs.NewItems)
                    {
                        var i = (IFilterViewModel)item;
                        i.PropertyChanged += Filter_PropertyChanged;
                    }
                    break;

                case NotifyCollectionChangedAction.Remove:
                    foreach (var item in notifyCollectionChangedEventArgs.OldItems)
                    {
                        var i = (IFilterViewModel)item;
                        i.PropertyChanged -= Filter_PropertyChanged;
                    }
                    break;
            }

            OnFiltersChanged(EventArgs.Empty);
        }

        /// <summary>
        /// When Filters changed.
        /// </summary>
        public event EventHandler FiltersChanged;

        protected void OnFiltersChanged(EventArgs e)
        {
            if (FiltersChanged != null)
            {
                FiltersChanged(this, e);
            }
        }

        protected void Filter_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnFiltersChanged(EventArgs.Empty);
        }

        #endregion Events
    }
}