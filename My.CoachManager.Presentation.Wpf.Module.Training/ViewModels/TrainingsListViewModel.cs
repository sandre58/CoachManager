using System;
using System.Collections.Generic;
using System.Linq;
using My.CoachManager.Application.Dtos;
using My.CoachManager.CrossCutting.Core.Extensions;
using My.CoachManager.Presentation.Core.Helpers;
using My.CoachManager.Presentation.Models;
using My.CoachManager.Presentation.Models.Aggregates;
using My.CoachManager.Presentation.Wpf.Core.Constants;
using My.CoachManager.Presentation.Wpf.Core.Manager;
using My.CoachManager.Presentation.Wpf.Core.ViewModels;
using My.CoachManager.Presentation.Wpf.Modules.Shared;
using My.CoachManager.Presentation.Wpf.Modules.Training.Resources;
using Prism.Commands;

namespace My.CoachManager.Presentation.Wpf.Modules.Training.ViewModels
{
    public class TrainingsListViewModel : ListViewModel<TrainingModel, TrainingEditViewModel, TrainingViewModel>
    {
        #region  Members

        /// <summary>
        /// Gets or sets selected Date.
        /// </summary>
        public IList<DateTime> SelectedDates { get; set; }

        /// <summary>
        /// Gets or sets the add command.
        /// </summary>
        public DelegateCommand<DateTime?> AddToDateCommand { get; set; }

        #endregion

        #region Methods

        #region Initialization

        /// <inheritdoc />
        /// <summary>
        /// Initializes Data.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();

            Filters = new TrainingsListFiltersViewModel();
            AddToDateCommand = new DelegateCommand<DateTime?>(Add, CanAdd);
            Title = TrainingResources.TrainingsTitle;
        }

        #endregion Initialization

        #region Data

        /// <inheritdoc />
        /// <summary>
        /// Remove the item from data source.
        /// </summary>
        /// <param name="item"></param>
        protected override void RemoveItemCore(TrainingModel item)
        {
            ApiHelper.DeleteData(ApiConstants.ApiTrainings, item.Id);
        }

        /// <inheritdoc />
        /// <summary>
        /// Load Data.
        /// </summary>
        /// <returns></returns>
        protected override void LoadDataCore()
        {
            var result = ApiHelper.GetData<IEnumerable<TrainingDto>>(ApiConstants.ApiTrainings, AppManager.Roster.Id);

            Items = result.Select(TrainingFactory.Get).ToItemsObservableCollection();
        }

        #endregion Data

        #region Add

        /// <summary>
        /// Add a new item.
        /// </summary>
        protected virtual void Add(DateTime? date)
        {
            DialogManager.ShowEditDialog<TrainingEditViewModel>(0, dialog =>
            {
                OnAddCompleted(dialog.Result);
            }, new List<KeyValuePair<string, object>>
        {
            new KeyValuePair<string, object>(ParametersConstants.Date, date),
            new KeyValuePair<string, object>(ParametersConstants.Duration, AppManager.DefaultTrainingDuration),
            new KeyValuePair<string, object>(ParametersConstants.StartTime, AppManager.DefaultTrainingStartTime),
        });
        }

        /// <summary>
        /// Can add a new item.
        /// </summary>
        /// <returns></returns>
        protected virtual bool CanAdd(DateTime? date)
        {
            return Mode == ScreenMode.Read && !IsReadOnly && date.HasValue;
        }

        /// <summary>
        /// Add a new item.
        /// </summary>
        protected override void Add()
        {
            if (SelectedDates != null && SelectedDates.Count > 1)
            {
                DialogManager.ShowEditDialog<TrainingsAddViewModel>(1, dialog =>
                {
                    OnAddCompleted(dialog.Result);
                }, new List<KeyValuePair<string, object>>
                {
                    new KeyValuePair<string, object>(ParametersConstants.StartDate, SelectedDates.OrderBy(x => x.Date).First()),
                    new KeyValuePair<string, object>(ParametersConstants.EndDate, SelectedDates.OrderBy(x => x.Date).Last()),
                    new KeyValuePair<string, object>(ParametersConstants.Duration, AppManager.DefaultTrainingDuration),
                    new KeyValuePair<string, object>(ParametersConstants.StartTime, AppManager.DefaultTrainingStartTime),
                });
            }
            else if (SelectedDates != null && SelectedDates.Count == 1)
            {
                Add(SelectedDates.First());
            }
        }

        /// <summary>
        /// Can add a new item.
        /// </summary>
        /// <returns></returns>
        protected override bool CanAdd()
        {
            return Mode == ScreenMode.Read && !IsReadOnly && SelectedDates != null && SelectedDates.Any();
        }

        #endregion Add

        #region Open

        /// <summary>
        /// Open Item.
        /// </summary>
        protected override void Open(TrainingModel item)
        {
            if (SelectedItems.Any())
            {
                item.IsSelected = !item.IsSelected;
            }
            else
            {
                base.Open(item);
            }
        }

        #endregion Open

        #region PropertuChanged

        protected void OnSelectedDatesChanged()
        {
            AddCommand.RaiseCanExecuteChanged();
        }

        #endregion

        #endregion Methods
    }
}