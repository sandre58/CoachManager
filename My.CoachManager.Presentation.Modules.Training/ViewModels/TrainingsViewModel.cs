using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using My.CoachManager.Application.Dtos;
using My.CoachManager.CrossCutting.Core.Extensions;
using My.CoachManager.Presentation.Core.Constants;
using My.CoachManager.Presentation.Core.Manager;
using My.CoachManager.Presentation.Core.ViewModels;
using My.CoachManager.Presentation.Models;
using My.CoachManager.Presentation.Models.Aggregates;
using My.CoachManager.Presentation.Modules.Training.Resources;
using My.CoachManager.Presentation.Modules.Training.Views;
using My.CoachManager.Presentation.ServiceAgent.TrainingServiceReference;
using Prism.Commands;

namespace My.CoachManager.Presentation.Modules.Training.ViewModels
{
    public class TrainingsViewModel : ListViewModel<TrainingModel, TrainingEditView, TrainingView>
    {
        #region Fields

        private readonly ITrainingService _trainingService;

        #endregion Fields

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

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="TrainingsViewModel"/>.
        /// </summary>
        public TrainingsViewModel(ITrainingService trainingService)
        {
            _trainingService = trainingService;
            Title = TrainingResources.TrainingsTitle;
        }

        #endregion Constructors

        #region Methods

        #region Initialization

        /// <inheritdoc />
        /// <summary>
        /// Initializes commands.
        /// </summary>
        protected override void InitializeCommand()
        {
            base.InitializeCommand();

            AddToDateCommand = new DelegateCommand<DateTime?>(Add, CanAdd);
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
            _trainingService.RemoveTraining(TrainingFactory.Get(item, CrudStatus.Deleted));
        }

        /// <inheritdoc />
        /// <summary>
        /// Load Data.
        /// </summary>
        /// <returns></returns>
        protected override void LoadDataCore()
        {
                var result = _trainingService.GetTrainings(Thread.CurrentPrincipal.Identity.GetRosterId());

                Items = result.Select(TrainingFactory.Get).ToItemsObservableCollection();
        }

        #endregion Data

        #region Add

        /// <summary>
        /// Add a new item.
        /// </summary>
        protected virtual void Add(DateTime? date)
        {
            DialogManager.ShowEditDialog<TrainingEditView>(0, dialog =>
            {
                OnAddCompleted(dialog.Result);
            }, new List<KeyValuePair<string, object>>
        {
            new KeyValuePair<string, object>(ParametersConstants.Date, date),
            new KeyValuePair<string, object>(ParametersConstants.Duration, SettingsManager.GetDefaultTrainingDuration()),
            new KeyValuePair<string, object>(ParametersConstants.StartTime, SettingsManager.GetDefaultTrainingStartTime()),
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
                DialogManager.ShowEditDialog<TrainingsAddView>(1, dialog =>
                {
                    OnAddCompleted(dialog.Result);
                }, new List<KeyValuePair<string, object>>
                {
                    new KeyValuePair<string, object>(ParametersConstants.StartDate, SelectedDates.OrderBy(x => x.Date).First()),
                    new KeyValuePair<string, object>(ParametersConstants.EndDate, SelectedDates.OrderBy(x => x.Date).Last()),
                    new KeyValuePair<string, object>(ParametersConstants.Duration, SettingsManager.GetDefaultTrainingDuration()),
                    new KeyValuePair<string, object>(ParametersConstants.StartTime, SettingsManager.GetDefaultTrainingStartTime()),
                });
            } else if (SelectedDates != null && SelectedDates.Count == 1)
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