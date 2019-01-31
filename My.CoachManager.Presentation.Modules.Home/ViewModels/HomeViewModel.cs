using My.CoachManager.Presentation.Core.ViewModels;
using My.CoachManager.Presentation.Modules.Home.Resources;

namespace My.CoachManager.Presentation.Modules.Home.ViewModels
{
    public class HomeViewModel : NavigatableWorkspaceViewModel
    {
        #region Initialization

        /// <inheritdoc />
        /// <summary>
        /// Initializes Data.
        /// </summary>
        protected override void InitializeData()
        {
            base.InitializeData();

            Title = HomeResources.HomeTitle;
        }

        #endregion Initialization
    }
}