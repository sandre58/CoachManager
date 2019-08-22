using My.CoachManager.CrossCutting.Core.Enums;
using My.CoachManager.CrossCutting.Core.Resources.Entities;
using My.CoachManager.Presentation.Core.Models.Filters;
using My.CoachManager.Presentation.Models;
using My.CoachManager.Presentation.Wpf.Core.ViewModels;
using My.CoachManager.Presentation.Wpf.Core.ViewModels.Interfaces;
using My.CoachManager.Presentation.Wpf.Modules.Shared;
using System;
using System.Collections.Generic;

namespace My.CoachManager.Presentation.Wpf.Modules.Admin.ViewModels
{
    public class PlayersListFiltersViewModel : ApiPagingFiltersViewModel<PlayerModel>
    {
        #region Initialisation

        /// <summary>
        /// Initialise a new instance of <see cref="PlayersListFiltersViewModel"/>.
        /// </summary>
        public PlayersListFiltersViewModel(IListViewModel<PlayerModel> list, IEnumerable<CountryModel> countries) : base(list, AppManager.ItemsPerPage, false)
        {
            SpeedFilter = new FilterViewModel(new StringFilter("FullName"), PersonResources.FullName, LogicalOperator.Or);

            AddAllowedFilter(PersonResources.FullName, () => new StringFilter("FullName", StringOperator.Contains, false, true));
            AddAllowedFilter(PersonResources.YearOfBirth, () => new IntegerFilter("YearOfBirth", ComplexComparableOperator.EqualsTo, 2000, DateTime.Now.Year, true)
            {
                Minimum = 1900,
                Maximum = DateTime.Now.Year
            });
            AddAllowedFilter(PersonResources.Gender, () => new EnumValueFilter("Gender", typeof(GenderType), true));
            AddAllowedFilter(PersonResources.Country, () => new SelectedLabelableFilter("CountryId", countries, true));
            AddAllowedFilter(PersonResources.LicenseNumber, () => new StringFilter("LicenseNumber", StringOperator.Contains, false, true));
        }

        #endregion Initialisation
    }
}