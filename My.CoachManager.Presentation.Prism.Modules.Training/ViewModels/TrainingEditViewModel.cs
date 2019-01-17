using System;
using My.CoachManager.Application.Dtos;
using My.CoachManager.Presentation.Prism.Core.Manager;
using My.CoachManager.Presentation.Prism.Core.ViewModels;
using My.CoachManager.Presentation.Prism.Models;
using My.CoachManager.Presentation.Prism.Models.Aggregates;
using My.CoachManager.Presentation.ServiceAgent.TrainingServiceReference;

namespace My.CoachManager.Presentation.Prism.Modules.Training.ViewModels
{
    public class TrainingEditViewModel : EditViewModel<TrainingModel>
    {
        #region Fields

        private readonly ITrainingService _trainingService;

        #endregion Fields

        #region Members

        /// <summary>
        /// Gets or sets defaultdate.
        /// </summary>
        public DateTime? DefaultDate { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="TrainingEditViewModel"/>.
        /// </summary>
        public TrainingEditViewModel(ITrainingService trainingService)
        {
            _trainingService = trainingService;
        }

        #endregion Constructors

        #region Methods

        #region Data

        protected override int SaveItemCore()
        {
            return _trainingService.SaveTraining(TrainingFactory.Get(Item, Mode == ScreenMode.Creation ? CrudStatus.Created : CrudStatus.Updated));
        }

        protected override TrainingModel LoadItemCore(int id)
        {
            return TrainingFactory.Get(_trainingService.GetTrainingById(id));
        }

        /// <summary>
        /// Calls when load data is completed.
        /// </summary>
        protected override void OnLoadDataCompleted()
        {
            base.OnLoadDataCompleted();

            Item.RosterId = SettingsManager.GetRosterId();

            if (Mode == ScreenMode.Creation)
            {
                if (DefaultDate.HasValue)
                {
                    Item.Date = DefaultDate.Value;
                }

                Item.StartTime = SettingsManager.GetDefaultTrainingStartTime();
                Item.EndTime = SettingsManager.GetDefaultTrainingStartTime()
                    .Add(SettingsManager.GetDefaultTrainingDuration());
            }
        }

        #endregion Data

        #endregion Methods
    }
}