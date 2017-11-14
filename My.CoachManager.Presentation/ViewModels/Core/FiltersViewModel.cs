using My.CoachManager.Presentation.Core.ViewModels.Screens;
using My.CoachManager.Presentation.Core.ViewModels.Screens.Enums;
using My.CoachManager.Presentation.Resources.Strings;

namespace My.CoachManager.Presentation.ViewModels.Core
{
    public abstract class FiltersViewModel : FlyoutViewModel
    {
        #region Constructor

        /// <summary>
        /// Initializes an instance of <see cref="FiltersViewModel"/>.
        /// </summary>
        public FiltersViewModel()
        {
            Position = FlyoutVisibilityPosition.Right;
            Theme = FlyoutTheme.AccentedTheme;
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        /// Get the display Name.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return ControlResources.Filter;
            }
        }

        #endregion Properties
    }
}