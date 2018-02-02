﻿using System;
using System.Collections.ObjectModel;
using My.CoachManager.Application.Dtos.Persons;
using My.CoachManager.Presentation.Prism.Core.ViewModels.Screens;
using My.CoachManager.Presentation.Prism.Modules.Administration.Resources.Strings;
using My.CoachManager.Presentation.Prism.Modules.Administration.Views;
using My.CoachManager.Presentation.Prism.ViewModels;
using My.CoachManager.Presentation.Prism.ViewModels.Mapping;
using My.CoachManager.Presentation.ServiceAgent.AdminServiceReference;

namespace My.CoachManager.Presentation.Prism.Modules.Administration.ViewModels
{
    public class PlayersListViewModel : ListViewModel<PlayerViewModel, PlayerEditViewModel>, IPlayersListViewModel
    {
        #region Fields

        private readonly IAdminService _adminService;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="CategoriesListViewModel"/>.
        /// </summary>
        public PlayersListViewModel(IAdminService adminService)
        {
            _adminService = adminService;

            Title = AdministrationResources.PlayersTitle;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Get Item View Type.
        /// </summary>
        /// <returns></returns>
        protected override Type GetEditViewType()
        {
            return typeof(PlayerEditView);
        }

        /// <summary>
        /// Remove the item from data source.
        /// </summary>
        /// <param name="item"></param>
        protected override void RemoveItemCore(PlayerViewModel item)
        {
            _adminService.RemovePlayer(item.ToDto<PlayerDto>());
        }

        /// <summary>
        /// Load Data.
        /// </summary>
        /// <returns></returns>
        protected override void LoadDataCore()
        {
            var result = _adminService.GetPlayersList();

            Items = new ObservableCollection<PlayerViewModel>(result.ToViewModels<PlayerViewModel>());
        }

        #endregion Methods
    }
}