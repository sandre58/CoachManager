using System.Collections.Generic;
using System.Collections.ObjectModel;
using My.CoachManager.Presentation.Prism.Core.ViewModels.Screens;
using My.CoachManager.Presentation.Prism.ViewModels;

namespace My.CoachManager.Presentation.Prism.Modules.Roster.ViewModels
{
    public class SquadsNavigationViewModel : ScreenViewModel
    {
        #region Fields

        private ObservableCollection<SquadViewModel> _squads;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="SquadsNavigationViewModel"/>.
        /// </summary>
        public SquadsNavigationViewModel(IEnumerable<SquadViewModel> squads)
        {
            Squads = new ObservableCollection<SquadViewModel>(squads);
        }

        /// <summary>
        /// Constructor used by Design Mode.
        /// </summary>
        public SquadsNavigationViewModel()
        {
        }

        #endregion Constructors

        #region Members

        /// <summary>
        /// Gets or sets all squads.
        /// </summary>
        public ObservableCollection<SquadViewModel> Squads
        {
            get { return _squads; }
            private set { SetProperty(ref _squads, value); }
        }

        #endregion Members
    }
}