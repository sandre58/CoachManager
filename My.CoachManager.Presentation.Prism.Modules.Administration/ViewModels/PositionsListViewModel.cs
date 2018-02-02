﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using My.CoachManager.Application.Dtos.Administration;
using My.CoachManager.Presentation.Prism.Core.ViewModels.Screens;
using My.CoachManager.Presentation.Prism.Modules.Administration.Resources.Strings;
using My.CoachManager.Presentation.Prism.Modules.Administration.Views;
using My.CoachManager.Presentation.Prism.ViewModels;
using My.CoachManager.Presentation.Prism.ViewModels.Mapping;
using My.CoachManager.Presentation.ServiceAgent.AdminServiceReference;

namespace My.CoachManager.Presentation.Prism.Modules.Administration.ViewModels
{
    public class PositionsListViewModel : OrderedListViewModel<PositionViewModel, PositionEditViewModel>, IPositionsListViewModel
    {
        #region Fields

        private readonly IAdminService _adminService;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="PositionsListViewModel"/>.
        /// </summary>
        public PositionsListViewModel(IAdminService adminService)
        {
            _adminService = adminService;

            Title = AdministrationResources.PositionsTitle;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Get Item View Type.
        /// </summary>
        /// <returns></returns>
        protected override Type GetEditViewType()
        {
            return typeof(PositionEditView);
        }

        /// <summary>
        /// Remove the item from data source.
        /// </summary>
        /// <param name="item"></param>
        protected override void RemoveItemCore(PositionViewModel item)
        {
            _adminService.RemovePosition(item.ToDto<PositionDto>());
        }

        /// <summary>
        /// Validates order in the data source.
        /// </summary>
        protected override void ValidateOrderCore()
        {
            _adminService.UpdatePositionsOrders(Items.ToDictionary(c => c.Id, c => c.Order));
        }

        /// <summary>
        /// Load Data.
        /// </summary>
        /// <returns></returns>
        protected override void LoadDataCore()
        {
            var result = _adminService.GetPositionsList();

            Items = new ObservableCollection<PositionViewModel>(result.OrderBy(x => x.Order).ToViewModels<PositionViewModel>());
        }

        #endregion Methods
    }
}