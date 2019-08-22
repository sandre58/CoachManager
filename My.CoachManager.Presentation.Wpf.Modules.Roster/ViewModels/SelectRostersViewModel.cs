using System.Collections.Generic;
using System.Linq;
using My.CoachManager.Application.Dtos;
using My.CoachManager.CrossCutting.Core.Collections;
using My.CoachManager.CrossCutting.Core.Extensions;
using My.CoachManager.Presentation.Core.Helpers;
using My.CoachManager.Presentation.Models;
using My.CoachManager.Presentation.Models.Aggregates;
using My.CoachManager.Presentation.Wpf.Core.ViewModels;
using My.CoachManager.Presentation.Wpf.Modules.Shared;

namespace My.CoachManager.Presentation.Wpf.Modules.Roster.ViewModels
{
    public class SelectRostersViewModel : SelectItemsViewModel<RosterModel>
    {
        #region Methods

        #region Data

        /// <summary>
        /// Load Data.
        /// </summary>
        /// <returns></returns>
        protected override ObservableItemsCollection<RosterModel> LoadItems()
        {
            var result = ApiHelper.GetData<IEnumerable<RosterDto>>(ApiConstants.ApiRosters);

                return result.Select(RosterFactory.Get).ToItemsObservableCollection();
        }

        #endregion Data

        #endregion Methods
    }
}