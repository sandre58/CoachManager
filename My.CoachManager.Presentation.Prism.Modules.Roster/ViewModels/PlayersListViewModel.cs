﻿using System;
using System.Collections.Generic;
using My.CoachManager.CrossCutting.Core.Enums;
using My.CoachManager.CrossCutting.Core.Extensions;
using My.CoachManager.CrossCutting.Core.Resources.Entities;
using My.CoachManager.Presentation.Prism.Core.Filters;
using My.CoachManager.Presentation.Prism.Core.ViewModels.Screens;
using My.CoachManager.Presentation.Prism.Modules.Roster.Core.Enums;
using My.CoachManager.Presentation.Prism.Modules.Roster.Views;
using My.CoachManager.Presentation.Prism.ViewModels;
using My.CoachManager.Presentation.Prism.ViewModels.Mapping;
using My.CoachManager.Presentation.ServiceAgent.AdminServiceReference;
using My.CoachManager.Presentation.ServiceAgent.RosterServiceReference;
using RosterResources = My.CoachManager.Presentation.Prism.Modules.Roster.Core.Resources.Strings.RosterResources;

namespace My.CoachManager.Presentation.Prism.Modules.Roster.ViewModels
{
    public class PlayersListViewModel : FilteredListViewModel<PlayerDetailViewModel, PlayerFiltersView>, IPlayersListViewModel
    {
        #region Constants

        private static readonly string[] GeneralInformationsColumns =
            {"Age", "Birthdate", "Category", "Gender", "Country", "Address", "Phone", "Email"};

        private static readonly string[] ClubInformationsColumns = { "Number", "Category", "License", "LicenseState" };

        private static readonly string[] BodyInformationsColumns =
            {"Laterality", "Height", "Weight", "Size", "ShoesSize"};

        #endregion Constants

        #region Fields

        private readonly IAdminService _adminService;
        private readonly IRosterService _rosterService;
        private IEnumerable<CategoryViewModel> _categories;
        private IEnumerable<CountryViewModel> _countries;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="PlayersListViewModel"/>.
        /// </summary>
        public PlayersListViewModel(IRosterService rosterService, IAdminService adminService) : this()
        {
            _rosterService = rosterService;
            _adminService = adminService;
        }

        /// <summary>
        /// Initialise a new instance of <see cref="PlayersListViewModel"/>.
        /// </summary>
        public PlayersListViewModel()
        {
            Title = RosterResources.PlayersTitle;

            AddPresetColumns(PresetColumnsType.GeneralInformations, GeneralInformationsColumns);
            AddPresetColumns(PresetColumnsType.ClubInformations, ClubInformationsColumns);
            AddPresetColumns(PresetColumnsType.BodyInformations, BodyInformationsColumns);

            SpeedFilter = new FilterViewModel(new StringFilter("FullName"), PersonResources.FullName, LogicalOperator.Or);

            AddAllowedFilter("FullName", PersonResources.FullName);
            AddAllowedFilter("CategoryId", PlayerResources.Category);
            AddAllowedFilter("Number", PlayerResources.Number);
            AddAllowedFilter("Age", PersonResources.Age);
            AddAllowedFilter("Gender", PersonResources.Gender);
            AddAllowedFilter("CountryId", PersonResources.Country);
            AddAllowedFilter("FullAddress", PersonResources.Address);
            AddAllowedFilter("Laterality", PlayerResources.Laterality);
            AddAllowedFilter("Height", PlayerResources.Height);
            AddAllowedFilter("Weight", PlayerResources.Weight);
            AddAllowedFilter("Size", PersonResources.Size);
            AddAllowedFilter("ShoesSize", PlayerResources.ShoesSize);
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Get Item View Type.
        /// </summary>
        /// <returns></returns>
        protected override Type GetItemViewType()
        {
            return typeof(PlayerView);
        }

        /// <summary>
        /// Initialize data.
        /// </summary>
        protected override void InitializeDataCore()
        {
            base.InitializeDataCore();

            if (_adminService != null)
            {
                _categories = _adminService.GetCategoriesList().ToViewModels<CategoryViewModel>();
                _countries = _adminService.GetCountriesForPlayer().ToViewModels<CountryViewModel>();
            }
        }

        /// <summary>
        /// Load Data.
        /// </summary>
        /// <returns></returns>
        protected override void LoadDataCore()
        {
            if (_rosterService != null)
            {
                var result = _rosterService.GetPlayers(1);

                System.Windows.Application.Current.Dispatcher.Invoke(() =>
                {
                    SetCollection(result.ToViewModels<PlayerDetailViewModel>());
                });
            }
        }

        /// <summary>
        /// Call after load data.
        /// </summary>
        protected override void OnLoadDataCompleted()
        {
            ChangeDisplayedColumns(PresetColumnsType.GeneralInformations);
            base.OnLoadDataCompleted();
        }

        /// <summary>
        /// Create a filter.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        protected override IFilterViewModel CreateFilter(string propertyName)
        {
            IFilter filter = null;
            string title = string.Empty;

            var property = typeof(PlayerDetailViewModel).GetProperty(propertyName);
            if (property != null)
            {
                title = property.GetDisplayName();
            }

            switch (propertyName)
            {
                case "FullName":
                    filter = new StringFilter(propertyName);
                    break;

                case "CategoryId":
                    filter = new SelectedLabelablesFilter(propertyName, _categories);
                    break;

                case "Number":
                    filter = new IntegerFilter(propertyName, ComparableOperator.EqualsTo, 1, 14)
                    {
                        Minimum = 1,
                        Maximum = 99
                    };
                    break;

                case "Age":
                    filter = new IntegerFilter(propertyName, ComparableOperator.IsBetween, 16, 35)
                    {
                        Minimum = 0,
                        Maximum = 120
                    };
                    break;

                case "Gender":
                    filter = new EnumValuesFilter(propertyName, typeof(GenderType));
                    break;

                case "CountryId":
                    filter = new SelectedLabelablesFilter(propertyName, _countries);
                    break;

                case "FullAddress":
                    filter = new StringFilter(propertyName);
                    break;

                case "Laterality":
                    filter = new EnumValuesFilter(propertyName, typeof(Laterality));
                    break;

                case "Height":
                    filter = new IntegerFilter(propertyName, ComparableOperator.IsBetween, 120, 195)
                    {
                        Minimum = 100,
                        Maximum = 230
                    };
                    break;

                case "Weight":
                    filter = new IntegerFilter(propertyName, ComparableOperator.IsBetween, 50, 100)
                    {
                        Minimum = 40,
                        Maximum = 120
                    };
                    break;

                case "Size":
                    filter = new StringFilter(propertyName);
                    break;

                case "ShoesSize":
                    filter = new IntegerFilter(propertyName, ComparableOperator.IsBetween, 33, 45)
                    {
                        Minimum = 30,
                        Maximum = 48
                    };
                    break;
            }

            return new FilterViewModel(filter, title);
        }

        #endregion Methods
    }
}