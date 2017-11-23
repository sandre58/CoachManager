using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.Presentation.Prism.Core.Services;
using My.CoachManager.Presentation.Prism.Core.ViewModels;
using My.CoachManager.Presentation.Prism.Home.Resources.Strings;

namespace My.CoachManager.Presentation.Prism.Home.ViewModels
{
    public class HomeViewModel : WorkspaceViewModel, IHomeViewModel
    {
        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="HomeViewModel"/>.
        /// </summary>
        public HomeViewModel(IDialogService dialogService, ILogger logger)
            : base(dialogService, logger)
        {
            Title = HomeResources.HomeTitle;
        }

        #endregion Constructors
    }
}