using System;
using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.Presentation.Prism.Core.Services;
using My.CoachManager.Presentation.Prism.Core.ViewModels.Screens;
using Prism.Events;

namespace My.CoachManager.Presentation.Prism.Modules.Roster.ViewModels
{
    #region Constructors

    public class PlayerFiltersViewModel : FiltersViewModel, IPlayerFiltersViewModel
    {
        /// <summary>
        /// Initialise a new instance of <see cref="PlayerFiltersViewModel"/>.
        /// </summary>
        /// <param name="dialogService"></param>
        /// <param name="eventAggregator"></param>
        /// <param name="logger"></param>
        public PlayerFiltersViewModel(IDialogService dialogService, IEventAggregator eventAggregator, ILogger logger) : base(dialogService, eventAggregator, logger)
        {
        }
    }

    #endregion Constructors
}