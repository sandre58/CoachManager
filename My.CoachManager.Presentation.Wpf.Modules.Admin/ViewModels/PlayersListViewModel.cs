using My.CoachManager.Application.Dtos;
using My.CoachManager.Application.Dtos.Parameters;
using My.CoachManager.Application.Dtos.Results;
using My.CoachManager.CrossCutting.Core.Collections;
using My.CoachManager.CrossCutting.Core.Enums;
using My.CoachManager.CrossCutting.Core.Extensions;
using My.CoachManager.Presentation.Core.Helpers;
using My.CoachManager.Presentation.Core.Models.Filters;
using My.CoachManager.Presentation.Models;
using My.CoachManager.Presentation.Models.Aggregates;
using My.CoachManager.Presentation.Wpf.Core.ViewModels;
using My.CoachManager.Presentation.Wpf.Modules.Admin.Resources;
using My.CoachManager.Presentation.Wpf.Modules.Shared;
using System.Collections.Generic;
using System.Linq;

namespace My.CoachManager.Presentation.Wpf.Modules.Admin.ViewModels
{
    public class PlayersListViewModel : ListViewModel<PlayerModel, PlayerEditViewModel, PlayerEditViewModel>
    {
        #region Methods

        #region Initialization

        /// <inheritdoc />
        /// <summary>
        /// Initializes Data.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();

            Title = AdminResources.PlayersTitle;
        }

        #endregion Initialization

        #region Data

        /// <inheritdoc />
        /// <summary>
        /// Remove the item from data source.
        /// </summary>
        /// <param name="item"></param>
        protected override void RemoveItemCore(PlayerModel item)
        {
            ApiHelper.DeleteData(ApiConstants.ApiPlayers, item.Id);
        }

        /// <inheritdoc />
        /// <summary>
        /// Load Data.
        /// </summary>
        /// <returns></returns>
        protected override void LoadDataCore()
        {
            if (Filters is PlayersListFiltersViewModel filters)
            {
                var parameters = new PlayersListParametersDto
                {
                    Count = filters.ItemsPerPage,
                    Page = filters.CurrentPage
                };

                var parameter = filters.GetFilter("FullName");
                if (parameter != null && parameter.IsEnabled)
                {
                    var filter = parameter.Filter as StringFilter;
                    parameters.Name = filter?.Value;
                }

                parameter = filters.GetFilter("YearOfBirth");
                if (parameter != null && parameter.IsEnabled)
                {
                    var filter = parameter.Filter as IntegerFilter;
                    parameters.YearOfBirth = filter?.From;
                }

                parameter = filters.GetFilter("Gender");
                if (parameter != null && parameter.IsEnabled)
                {
                    var filter = parameter.Filter as EnumValueFilter;
                    parameters.Gender = (GenderType?)filter?.Value;
                }

                parameter = filters.GetFilter("CountryId");
                if (parameter != null && parameter.IsEnabled)
                {
                    var filter = parameter.Filter as SelectedLabelableFilter;
                    parameters.CountryId = filter?.Value;
                }

                parameter = filters.GetFilter("LicenseNumber");
                if (parameter != null && parameter.IsEnabled)
                {
                    var filter = parameter.Filter as StringFilter;
                    parameters.LicenseNumber = filter?.Value;
                }

                parameters.SortProperty = filters.SortDescription.PropertyName;
                parameters.SortDirection = filters.SortDescription.Direction;

                var result = ApiHelper.PostData<PlayersListParametersDto, ListResultDto<PlayerDto>>(ApiConstants.ApiPlayersGet, parameters);

                Items = result.Items.Select(PlayerFactory.Get).ToItemsObservableCollection();
                AllItemsCount = result.AllItemsCount;
                Count = result.Count;
            }
            else
            {
                Items = new ObservableItemsCollection<PlayerModel>();
            }
        }

        /// <summary>
        /// Initialize data asynchronous.
        /// </summary>
        protected override void InitializeDataCore()
        {
            base.InitializeDataCore();

            var countries = ApiHelper.GetData<IEnumerable<CountryDto>>(ApiConstants.ApiCountries).Select(CountryFactory.Get).ToList();

            Filters = new PlayersListFiltersViewModel(this, countries);

            ListParameters = new PlayersListParametersViewModel();
        }

        #endregion Data

        #endregion Methods
    }
}