using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.Presentation.Prism.Core.Dialog;
using My.CoachManager.Presentation.Prism.Core.Filters;
using My.CoachManager.Presentation.Prism.Core.Services;
using Prism.Commands;
using Prism.Events;

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
        public Func<string, IFilter> CreateFilter { get; set; }

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
                SetProperty(ref _filters, value);
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
        /// <param name="dialogService"></param>
        /// <param name="eventAggregator"></param>
        /// <param name="logger"></param>
        public FiltersViewModel(IDialogService dialogService, IEventAggregator eventAggregator, ILogger logger) : base(dialogService, eventAggregator, logger)
        {
            Filters = new ObservableCollection<FilterViewModel>();
            Filters.CollectionChanged += FiltersOnCollectionChanged;

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
                    Filters.Add(new FilterViewModel(filter));
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
            Filters.Remove(Filters.FirstOrDefault(x => x.Filter.Equals(filter)));
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
                        var i = (FilterViewModel)item;
                        i.PropertyChanged += Filter_PropertyChanged;
                    }
                    break;

                case NotifyCollectionChangedAction.Remove:
                    foreach (var item in notifyCollectionChangedEventArgs.OldItems)
                    {
                        var i = (FilterViewModel)item;
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