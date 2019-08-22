using My.CoachManager.Presentation.Models;
using My.CoachManager.Presentation.Wpf.Core.ViewModels;

namespace My.CoachManager.Presentation.Wpf.Modules.Training.ViewModels
{
    public class TrainingsListFiltersViewModel : ListFiltersViewModel<TrainingModel>
    {

        #region Initialisation
        public TrainingsListFiltersViewModel() : base(true)
        {
        }

        #endregion Initialisation
        }
}
