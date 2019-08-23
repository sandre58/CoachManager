using My.CoachManager.Presentation.Wpf.Core.ViewModels.Base;

namespace My.CoachManager.Presentation.Wpf.ViewModels.Home
{
    public class HomeViewModel : NavigableWorkspaceViewModel
    {
        #region Initialisation

        protected override void Initialize()
        {
            base.Initialize();

            ShowHeader = false;
        }

        #endregion

        #region Data

        /// <inheritdoc />
        /// <summary>
        /// Load data.
        /// </summary>
        protected override void LoadDataCore()
        {
        }

        #endregion Data

    }
}