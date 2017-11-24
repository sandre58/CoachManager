using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.Presentation.Prism.Core.Services;
using My.CoachManager.Presentation.Prism.Core.ViewModels;
using Prism.Events;

namespace My.CoachManager.Presentation.Prism.RosterModule.ViewModels
{
    public class SquadViewModel : WorkspaceViewModel, ISquadViewModel
    {
        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="RosterViewModel"/>.
        /// </summary>
        public SquadViewModel(IDialogService dialogService, IEventAggregator eventAggregator, ILogger logger)
            : base(dialogService, eventAggregator, logger)
        {
        }

        #endregion Constructors
    }
}