using System.Collections.Generic;
using System.Linq;
using My.CoachManager.Application.Dtos;
using My.CoachManager.Presentation.Core.Helpers;
using My.CoachManager.Presentation.Models;
using My.CoachManager.Presentation.Models.Aggregates;
using My.CoachManager.Presentation.Wpf.Constants;
using My.CoachManager.Presentation.Wpf.Core.ViewModels;

namespace My.CoachManager.Presentation.Wpf.ViewModels.Administration
{
    public class RosterEditViewModel : EditViewModel<RosterModel>
    {

        #region Members

        /// <summary>
        /// Gets or sets categories.
        /// </summary>
        public IEnumerable<CategoryModel> Categories { get; private set; }

        /// <summary>
        /// Gets or sets seasons.
        /// </summary>
        public IEnumerable<SeasonModel> Seasons { get; private set; }

        #endregion Members

        #region Methods

        /// <inheritdoc />
        /// <summary>
        /// Initialize data asynchronous.
        /// </summary>
        protected override void InitializeDataCore()
        {
            Categories = ApiHelper.GetData<IEnumerable<CategoryDto>>(ApiConstants.ApiCategories).Select(CategoryFactory.Get);
            Seasons = ApiHelper.GetData<IEnumerable<SeasonDto>>(ApiConstants.ApiSeasons).Select(SeasonFactory.Get);
        }

        /// <inheritdoc />
        /// <summary>
        /// Save.
        /// </summary>
        protected override int SaveItemCore()
        {
            return ApiHelper.PostData<RosterDto, int>(ApiConstants.ApiRosters, RosterFactory.Get(Item, Mode == ScreenMode.Creation ? CrudStatus.Created : CrudStatus.Updated));
        }

        /// <inheritdoc />
        /// <summary>
        /// Load an item from data source.
        /// </summary>
        /// <param name="id"></param>
        protected override RosterModel LoadItemCore(int id)
        {
            var result = ApiHelper.GetData<RosterDto>(ApiConstants.ApiRosters, id);
            return RosterFactory.Get(result);
        }

        #endregion Methods
    }
}