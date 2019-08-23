using System.Collections.Generic;
using System.Linq;
using My.CoachManager.Application.Dtos;
using My.CoachManager.Presentation.Core.Helpers;
using My.CoachManager.Presentation.Models;
using My.CoachManager.Presentation.Models.Aggregates;
using My.CoachManager.Presentation.Wpf.Constants;
using My.CoachManager.Presentation.Wpf.Core.ViewModels;
using My.CoachManager.Presentation.Wpf.Resources.Strings;

namespace My.CoachManager.Presentation.Wpf.ViewModels.Administration
{
    public class RostersListViewModel : ListViewModel<RosterModel, RosterEditViewModel, RosterEditViewModel>
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

            Title = CommonResources.Rosters;
        }

        #endregion Initialization

        #region Data

        /// <inheritdoc />
        /// <summary>
        /// Remove the item from data source.
        /// </summary>
        /// <param name="item"></param>
        protected override void RemoveItemCore(RosterModel item)
        {
            ApiHelper.DeleteData(ApiConstants.ApiRosters, item.Id);
        }

        /// <inheritdoc />
        /// <summary>
        /// Load Data.
        /// </summary>
        /// <returns></returns>
        protected override void LoadDataCore()
        {
            var result = ApiHelper.GetData<IEnumerable<RosterDto>>(ApiConstants.ApiRosters);
            SetItemsByDispatcher(result.Select(RosterFactory.Get));
        }

        #endregion Data

        #endregion Methods
    }
}