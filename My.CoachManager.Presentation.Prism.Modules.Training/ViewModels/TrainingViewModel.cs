using My.CoachManager.Presentation.Prism.Core.ViewModels;
using My.CoachManager.Presentation.Prism.Models;
using My.CoachManager.Presentation.Prism.Models.Aggregates;
using My.CoachManager.Presentation.Prism.Modules.Training.Views;
using My.CoachManager.Presentation.ServiceAgent.TrainingServiceReference;

namespace My.CoachManager.Presentation.Prism.Modules.Training.ViewModels
{
    public class TrainingViewModel : ItemViewModel<TrainingModel, TrainingEditView>
    {
        #region Fields

        private readonly ITrainingService _trainingService;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="TrainingViewModel"/>.
        /// </summary>
        public TrainingViewModel(ITrainingService trainingService)
        {
            _trainingService = trainingService;
        }

        #endregion Constructors

        #region Data

        protected override TrainingModel LoadItemCore(int id)
        {
            return TrainingFactory.Get(_trainingService.GetTrainingById(id));
        }

        #endregion Data
    }
}