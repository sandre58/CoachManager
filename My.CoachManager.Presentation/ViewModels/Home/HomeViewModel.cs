using My.CoachManager.Presentation.ViewModels.Core;

namespace My.CoachManager.Presentation.ViewModels.Home
{
    /// <summary>
    /// ViewModel for the settings window.
    /// </summary>
    public class HomeViewModel : DetailViewModel
    {
        #region Properties

        /// <summary>
        /// Get the display Name.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return Resources.Strings.Screens.HomeResources.Home;
            }
        }

        /// <summary>
        ///  Can go back.
        /// </summary>
        public override bool CanGoBack
        {
            get
            {
                return false;
            }
        }

        #endregion Properties
    }
}