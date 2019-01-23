﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using My.CoachManager.Application.Dtos;
using My.CoachManager.CrossCutting.Core.Extensions;
using My.CoachManager.Presentation.Prism.Core.Manager;
using My.CoachManager.Presentation.Prism.Core.ViewModels;
using My.CoachManager.Presentation.Prism.Models;
using My.CoachManager.Presentation.Prism.Models.Aggregates;
using My.CoachManager.Presentation.Prism.Modules.Training.Resources;
using My.CoachManager.Presentation.Prism.Modules.Training.Views;
using My.CoachManager.Presentation.ServiceAgent.TrainingServiceReference;
using Prism.Commands;

namespace My.CoachManager.Presentation.Prism.Modules.Training.ViewModels
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
            DialogManager.ShowEditDialog<TrainingEditView>(new TrainingEditParameters(0)
            {
                Date = date,
                Duration = SettingsManager.GetDefaultTrainingDuration(),
                StartTime = SettingsManager.GetDefaultTrainingStartTime()
            }, dialog =>
            {
                OnAddCompleted(dialog.Result);
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
                DialogManager.ShowEditDialog<TrainingsAddView>(new TrainingsAddParameters(1)
                {
                    Date = SelectedDates.OrderBy(x => x.Date).First(),
                    EndDate = SelectedDates.OrderBy(x => x.Date).Last(),
                    Duration = SettingsManager.GetDefaultTrainingDuration(),
                    StartTime = SettingsManager.GetDefaultTrainingStartTime()
                }, dialog =>
                {
                    OnAddCompleted(dialog.Result);
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