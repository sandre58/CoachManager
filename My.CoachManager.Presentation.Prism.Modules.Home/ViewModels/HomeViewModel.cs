using My.CoachManager.Presentation.Prism.Core.ViewModels.Screens;
using My.CoachManager.Presentation.Prism.Modules.Home.Resources.Strings;

namespace My.CoachManager.Presentation.Prism.Modules.Home.ViewModels
{
    public class HomeViewModel : NavigatableWorkspaceViewModel, IHomeViewModel
    {
        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="HomeViewModel"/>.
        /// </summary>
        public HomeViewModel()
        {
            Title = HomeResources.HomeTitle;
        }

        #endregion Constructors
    }
}