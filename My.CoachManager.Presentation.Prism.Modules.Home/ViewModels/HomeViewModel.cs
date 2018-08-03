using My.CoachManager.Presentation.Prism.Core.ViewModels;
using My.CoachManager.Presentation.Prism.Modules.Home.Resources.Strings;

namespace My.CoachManager.Presentation.Prism.Modules.Home.ViewModels
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