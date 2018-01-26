﻿using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.Presentation.Prism.Core.Services;
using My.CoachManager.Presentation.Prism.Core.ViewModels;
using My.CoachManager.Presentation.Prism.Core.ViewModels.Screens;
using My.CoachManager.Presentation.Prism.Modules.Home.Resources.Strings;
using Prism.Events;

namespace My.CoachManager.Presentation.Prism.Modules.Home.ViewModels
{
    public class HomeViewModel : NavigatableWorkspaceViewModel, IHomeViewModel
    {
        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="HomeViewModel"/>.
        /// </summary>
        public HomeViewModel(IDialogService dialogService, IEventAggregator eventAggregator, ILogger logger)
            : base(dialogService, eventAggregator, logger)
        {
            Title = HomeResources.HomeTitle;
        }

        #endregion Constructors
    }
}