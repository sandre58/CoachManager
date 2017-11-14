using My.CoachManager.Presentation.ViewModels.Core;
using My.CoachManager.Presentation.ViewModels.Players;

namespace My.CoachManager.Presentation.ViewModels.Staff
{
    /// <summary>
    /// ViewModel for the settings window.
    /// </summary>
    public class StaffListViewModel : ListViewModel<PlayerViewModel, EditPlayerViewModel, PlayerFiltersViewModel>
    {
        #region Properties

        /// <summary>
        /// Get the display Name.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return Resources.Strings.Screens.StaffResources.StaffList;
            }
        }

        #endregion Properties

        protected override void RemoveItemCore(PlayerViewModel item)
        {
            throw new System.NotImplementedException();
        }

        protected override void OpenItemCore(PlayerViewModel item)
        {
            throw new System.NotImplementedException();
        }
    }
}