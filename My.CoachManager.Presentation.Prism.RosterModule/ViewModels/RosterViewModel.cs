using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.Presentation.Prism.Core.Services;
using My.CoachManager.Presentation.Prism.Core.ViewModels;
using My.CoachManager.Presentation.Prism.RosterModule.Resources.Strings;
using Prism.Events;

namespace My.CoachManager.Presentation.Prism.RosterModule.ViewModels
{
    public class RosterViewModel : WorkspaceViewModel, IRosterViewModel
    {
        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="RosterViewModel"/>.
        /// </summary>
        public RosterViewModel(IDialogService dialogService, IEventAggregator eventAggregator, ILogger logger)
            : base(dialogService, eventAggregator, logger)
        {
            Title = RosterResources.RosterTitle;
        }

        #endregion Constructors
    }
}