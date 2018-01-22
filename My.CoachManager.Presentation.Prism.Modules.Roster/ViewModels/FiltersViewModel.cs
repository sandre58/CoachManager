using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using My.CoachManager.CrossCutting.Core.Extensions;
using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.Presentation.Prism.Core.Filters;
using My.CoachManager.Presentation.Prism.Core.Services;
using My.CoachManager.Presentation.Prism.Core.ViewModels.Screens;
using My.CoachManager.Presentation.Prism.ViewModels;
using Prism.Commands;
using Prism.Events;

namespace My.CoachManager.Presentation.Prism.Modules.Roster.ViewModels
{
    internal class FiltersViewModel : WorkspaceDialogViewModel, IFiltersViewModel
    {
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

            _allFilters.Add("FullName", typeof(StringFilter));
            _allFilters.Add("Number", typeof(IntegerCompareFilter));
            _allFilters.Add("Category", typeof(StringFilter));

            Filters.Add(GetFilter("FullName"));
            Filters.Add(GetFilter("Number"));
            Filters.Add(GetFilter("Category"));

            AddFilterCommand = new DelegateCommand<string>(AddFilter, CanAddFilter);
            RemoveFilterCommand = new DelegateCommand<string>(RemoveFilter, CanRemoveFilter);
        }

        #endregion Constructor

        public DelegateCommand<string> AddFilterCommand { get; set; }
        public DelegateCommand<string> RemoveFilterCommand { get; set; }

        private ObservableCollection<FilterViewModel> _filters;

        public ObservableCollection<FilterViewModel> Filters
        {
            get { return _filters; }
            set { SetProperty(ref _filters, value); }
        }

        public Dictionary<string, Type> AllFilters
        {
            get { return _allFilters; }
        }

        private void AddFilter(string propertyName)
        {
            Filters.Add(GetFilter(propertyName));
        }

        private bool CanAddFilter(string propertyName)
        {
            return Filters.All(x => x.Filter.PropertyInfo.Name != propertyName);
        }

        private void RemoveFilter(string propertyName)
        {
            Filters.Remove(Filters.FirstOrDefault(x => x.Filter.PropertyInfo.Name == propertyName));
        }

        private bool CanRemoveFilter(string propertyName)
        {
            return Filters.Any(x => x.Filter.PropertyInfo.Name == propertyName);
        }

        private readonly Dictionary<string, Type> _allFilters = new Dictionary<string, Type>();

        private FilterViewModel GetFilter(string propertyName)
        {
            var typeFilter = _allFilters[propertyName];
            var propertyInfo = typeof(PlayerDetailViewModel).GetProperty(propertyName);
            if (propertyInfo != null && typeFilter != null)
            {
                var filter = (IFilter)Activator.CreateInstance(typeFilter, propertyInfo);
                var name = propertyInfo.GetDisplayName();
                var title = !string.IsNullOrEmpty(name) ? name : propertyName;

                return new FilterViewModel(title, filter);
            }

            return null;
        }
    }
}