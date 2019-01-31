using System.Collections.Generic;
using My.CoachManager.CrossCutting.Core.Enums;
using My.CoachManager.CrossCutting.Core.Resources.Entities;
using My.CoachManager.Presentation.Core.Models.Filters;
using My.CoachManager.Presentation.Core.ViewModels;
using My.CoachManager.Presentation.Models;

namespace My.CoachManager.Presentation.Modules.Shared.ViewModels
{
    public class SelectPlayersFiltersViewModel : ListFiltersViewModel
    {

        #region Initialisation

        /// <summary>
        /// Initialise a new instance of <see cref="SelectPlayersFiltersViewModel"/>.
        /// </summary>
        public SelectPlayersFiltersViewModel(IEnumerable<CategoryModel> categories, IEnumerable<CountryModel> countries)
        {
            SpeedFilter = new FilterViewModel(new StringFilter("FullName"), PersonResources.FullName, LogicalOperator.Or);

            AddAllowedFilter(PersonResources.FullName, () => new StringFilter("FullName"));
            AddAllowedFilter(PlayerResources.Category, () => new SelectedLabelablesFilter("CategoryId", categories));
            AddAllowedFilter(PersonResources.Age, () => new IntegerFilter("Age", ComplexComparableOperator.IsBetween, 16, 35)
            {
                Minimum = 0,
                Maximum = 120
            });
            AddAllowedFilter(PersonResources.Gender, () => new EnumValuesFilter("Gender", typeof(GenderType)));
            AddAllowedFilter(PersonResources.Country, () => new SelectedLabelablesFilter("CountryId", countries));
            AddAllowedFilter(PersonResources.Address, () => new StringFilter("FullAddress"));
            AddAllowedFilter(PlayerResources.Laterality, () => new EnumValuesFilter("Laterality", typeof(Laterality)));
            AddAllowedFilter(PlayerResources.Height, () => new IntegerFilter("Height", ComplexComparableOperator.IsBetween, 120, 195)
            {
                Minimum = 100,
                Maximum = 230
            });
            AddAllowedFilter(PlayerResources.Weight, () => new IntegerFilter("Weight", ComplexComparableOperator.IsBetween, 50, 100)
            {
                Minimum = 40,
                Maximum = 120
            });
            AddAllowedFilter(PersonResources.Size, () => new StringFilter("Size"));
            AddAllowedFilter(PlayerResources.ShoesSize, () => new IntegerFilter("ShoesSize", ComplexComparableOperator.IsBetween, 33, 45)
            {
                Minimum = 30,
                Maximum = 48
            });
        }

        #endregion Initialisation
    }
}