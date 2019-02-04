﻿using System.Threading;
using My.CoachManager.Application.Dtos;
using My.CoachManager.CrossCutting.Core.Extensions;
using My.CoachManager.Presentation.Core.ViewModels;
using My.CoachManager.Presentation.Models;
using My.CoachManager.Presentation.Models.Aggregates;
using My.CoachManager.Presentation.Modules.Roster.Resources;
using My.CoachManager.Presentation.ServiceAgent.RosterServiceReference;

namespace My.CoachManager.Presentation.Modules.Roster.ViewModels
{
    public class SquadEditViewModel : EditViewModel<SquadModel>
    {
        #region Fields

        private readonly IRosterService _rosterService;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="SquadEditViewModel"/>.
        /// </summary>
        public SquadEditViewModel(IRosterService rosterService)
        {
            _rosterService = rosterService;
        }

        #endregion Constructors

        #region Methods

        /// <inheritdoc />
        /// <summary>
        /// Save.
        /// </summary>
        protected override int SaveItemCore()
        {
            return _rosterService.SaveSquad(Thread.CurrentPrincipal.Identity.GetRosterId(), SquadFactory.Get(Item, Mode == ScreenMode.Creation ? CrudStatus.Created : CrudStatus.Updated));
        }

        /// <inheritdoc />
        /// <summary>
        /// Load an item from data source.
        /// </summary>
        /// <param name="id"></param>
        protected override SquadModel LoadItemCore(int id)
        {
            return SquadFactory.Get(_rosterService.GetSquadById(id));
        }

        #region Propertiers Changed

        /// <summary>
        /// Called when mode changes.
        /// </summary>
        protected override void OnModeChanged()
        {
            base.OnModeChanged();
            Title = Mode == ScreenMode.Edition ? SquadResources.EditSquad : SquadResources.NewSquad;
        }

        #endregion

        #endregion Methods
    }
}