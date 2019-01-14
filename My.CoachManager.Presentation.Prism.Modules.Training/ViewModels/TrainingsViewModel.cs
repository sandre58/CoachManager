using System.Linq;
using My.CoachManager.Application.Dtos;
using My.CoachManager.CrossCutting.Core.Extensions;
using My.CoachManager.Presentation.Prism.Core.ViewModels;
using My.CoachManager.Presentation.Prism.Models;
using My.CoachManager.Presentation.Prism.Models.Aggregates;
using My.CoachManager.Presentation.Prism.Modules.Training.Views;
using My.CoachManager.Presentation.ServiceAgent.TrainingServiceReference;

namespace My.CoachManager.Presentation.Prism.Modules.Training.ViewModels
{
    public class TrainingsViewModel : ListViewModel<TrainingModel, TrainingEditView, TrainingView>
    {
        #region Fields

        private readonly ITrainingService _trainingService;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="TrainingsViewModel"/>.
        /// </summary>
        public TrainingsViewModel(ITrainingService trainingService)
        {
            _trainingService = trainingService;
        }

        #endregion Constructors

        #region Methods

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
                var result = _trainingService.GetTrainings();

                Items = result.Select(TrainingFactory.Get).ToItemsObservableCollection();
        }

        #endregion Data

        #endregion Methods
    }
}