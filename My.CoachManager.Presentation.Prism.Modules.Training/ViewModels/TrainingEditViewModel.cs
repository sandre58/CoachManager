using System.Threading;
using My.CoachManager.Application.Dtos;
using My.CoachManager.CrossCutting.Core.Extensions;
using My.CoachManager.Presentation.Prism.Core.ViewModels;
using My.CoachManager.Presentation.Prism.Models;
using My.CoachManager.Presentation.Prism.Models.Aggregates;
using My.CoachManager.Presentation.Prism.Modules.Training.Resources;
using My.CoachManager.Presentation.ServiceAgent.TrainingServiceReference;

namespace My.CoachManager.Presentation.Prism.Modules.Training.ViewModels
{
    public class TrainingEditViewModel : EditViewModel<TrainingModel>
    {
        #region Fields

        private readonly ITrainingService _trainingService;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="TrainingEditViewModel"/>.
        /// </summary>
        public TrainingEditViewModel(ITrainingService trainingService)
        {
            _trainingService = trainingService;
            NewItemMessage = TrainingResources.NewTraining;
            EditItemMessage = TrainingResources.EditTraining;
        }

        #endregion Constructors

        #region Methods

        #region Data

        protected override int SaveItemCore()
        {
            return _trainingService.SaveTraining(Thread.CurrentPrincipal.Identity.GetRosterId(), TrainingFactory.Get(Item, Mode == ScreenMode.Creation ? CrudStatus.Created : CrudStatus.Updated));
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

            if (Mode == ScreenMode.Creation)
            {
                if (Parameters is TrainingEditParameters p)
                {
                    if (p.Date.HasValue)
                    {
                        Item.Date = p.Date.Value;
                    }

                    Item.StartTime = p.StartTime;
                    Item.EndTime = p.StartTime
                        .Add(p.Duration);
                }

                Item.ResetModified();
            }
        }

        #endregion Data

        #endregion Methods
    }
}