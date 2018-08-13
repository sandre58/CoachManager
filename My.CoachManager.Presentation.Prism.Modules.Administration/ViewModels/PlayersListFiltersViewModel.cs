using My.CoachManager.CrossCutting.Core.Resources.Entities;
using My.CoachManager.Presentation.Prism.Core.Filters;
using My.CoachManager.Presentation.Prism.Core.ViewModels;

namespace My.CoachManager.Presentation.Prism.Modules.Administration.ViewModels
{
    public class PlayersListFiltersViewModel : ListFiltersViewModel
    {
        #region Initialisation

        /// <summary>
        /// Initialise a new instance of <see cref="PlayersListFiltersViewModel"/>.
        /// </summary>
        public PlayersListFiltersViewModel()
        {
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

        #endregion Initialisation
    }
}