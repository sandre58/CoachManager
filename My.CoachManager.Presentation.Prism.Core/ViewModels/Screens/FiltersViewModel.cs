using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using My.CoachManager.CrossCutting.Core.Extensions;
using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.Presentation.Prism.Core.Filters;
using My.CoachManager.Presentation.Prism.Core.Services;
using My.CoachManager.Presentation.Prism.Core.ViewModels.Entities;
using Prism.Commands;
using Prism.Events;

namespace My.CoachManager.Presentation.Prism.Core.ViewModels.Screens
{
    public abstract class FiltersViewModel<T> : WorkspaceDialogViewModel, IFiltersViewModel
        where T : IEntityViewModel
    {
        #region Fields

        public ObservableCollection<FilterViewModel> AllowedFilters { get; }
        private ObservableCollection<FilterViewModel> _filters;

        #endregion Fields

        #region Members

        /// <summary>
        /// Gets or sets the filters.
        /// </summary>
        public ObservableCollection<FilterViewModel> Filters
        {
            get { return _filters; }
            set { SetProperty(ref _filters, value); }
        }

        /// <summary>
        /// Gets or sets the Add command.
        /// </summary>
        public DelegateCommand<FilterViewModel> AddFilterCommand { get; set; }

        /// <summary>
        /// Gets or sets the Remove command.
        /// </summary>
        public DelegateCommand<FilterViewModel> RemoveFilterCommand { get; set; }

        /// <summary>
        /// Gets or sets the Reset command.
        /// </summary>
        public DelegateCommand ResetCommand { get; set; }

        public int Count
        {
            get { return Filters.Count(x => x.IsEnabled); }
        }

        /// <summary>
        /// Gets or sets filters.
        /// </summary>
        public IEnumerable<IFilter> AvailableFilters
        {
            get { return Filters.Where(x => x.IsEnabled).Select(x => x.Filter); }
        }

        #endregion Members

        #region Constructor

        /// <summary>
        /// Initialise a new instance of <see cref="FiltersViewModel{T}"/>
        /// </summary>
        /// <param name="dialogService"></param>
        /// <param name="eventAggregator"></param>
        /// <param name="logger"></param>
        public FiltersViewModel(IDialogService dialogService, IEventAggregator eventAggregator, ILogger logger) : base(dialogService, eventAggregator, logger)
        {
            Filters = new ObservableCollection<FilterViewModel>();
            Filters.CollectionChanged += FiltersOnCollectionChanged;

            AllowedFilters = new ObservableCollection<FilterViewModel>
            {
                GetFilter("FullName", typeof(StringFilter)),
                GetFilter("Number", typeof(IntegerCompareFilter)),
                GetFilter("Category", typeof(StringFilter))
            };

            AddFilterCommand = new DelegateCommand<FilterViewModel>(AddFilter, CanAddFilter);
            RemoveFilterCommand = new DelegateCommand<FilterViewModel>(RemoveFilter, CanRemoveFilter);
            ResetCommand = new DelegateCommand(Reset, CanReset);
        }

        #endregion Constructor

        #region Methods

        /// <summary>
        /// Add a filter.
        /// </summary>
        /// <param name="filter"></param>
        private void AddFilter(FilterViewModel filter)
        {
            Filters.Add(filter);
            AddFilterCommand.RaiseCanExecuteChanged();
            ResetCommand.RaiseCanExecuteChanged();
            RemoveFilterCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Can Add a filter ?
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        private bool CanAddFilter(FilterViewModel filter)
        {
            return Filters.All(x => x != filter);
        }

        /// <summary>
        /// Remove a filter.
        /// </summary>
        /// <param name="filter"></param>
        private void RemoveFilter(FilterViewModel filter)
        {
            Filters.Remove(filter);
            AddFilterCommand.RaiseCanExecuteChanged();
            ResetCommand.RaiseCanExecuteChanged();
            RemoveFilterCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Can remove a filter ?
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        private bool CanRemoveFilter(FilterViewModel filter)
        {
            return Filters.Any(x => x == filter);
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

        private FilterViewModel GetFilter(string propertyName, Type typeFilter)
        {
            var propertyInfo = typeof(T).GetProperty(propertyName);
            if (propertyInfo != null && typeFilter != null)
            {
                var filter = (IFilter)Activator.CreateInstance(typeFilter, propertyInfo);
                var name = propertyInfo.GetDisplayName();
                var title = !string.IsNullOrEmpty(name) ? name : propertyName;

                return new FilterViewModel(title, filter);
            }

            return null;
        }

        #region Events

        /// <summary>
        /// Closes the dialog.
        /// </summary>
        public event EventHandler FiltersChanged;

        /// <summary>
        /// Closes the dialog.
        /// </summary>
        /// <param name="e"></param>
        protected void OnFiltersChanged(EventArgs e)
        {
            RaisePropertyChanged(() => Count);
            if (FiltersChanged != null)
            {
                FiltersChanged(this, e);
            }
        }

        private void FiltersOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            switch (notifyCollectionChangedEventArgs.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (var item in notifyCollectionChangedEventArgs.NewItems)
                    {
                        var i = item as FilterViewModel;
                        //if (i != null) i.PropertyChanged += (o, args) => OnFiltersChanged(EventArgs.Empty);
                    }
                    break;
            }

            OnFiltersChanged(EventArgs.Empty);
        }

        #endregion Events
    }
}