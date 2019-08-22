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
    public class SelectSquadsViewModel : SelectItemsViewModel<SquadModel>
    {
        #region Members

        /// <summary>
        /// Gets or sets roster id.
        /// </summary>
        public int RosterId { get; set; }

#endregion

        #region Methods

        #region Data

        /// <summary>
        /// Load Data.
        /// </summary>
        /// <returns></returns>
        protected override ObservableItemsCollection<SquadModel> LoadItems()
        {
            if (RosterId > 0)
            {
                var result = ApiHelper.GetData<IEnumerable<SquadDto>>(ApiConstants.ApiSquadsRoster, RosterId);

                return result.Select(SquadFactory.Get).ToItemsObservableCollection();
            }

            return new ObservableItemsCollection<SquadModel>();
        }

    #endregion Data

    #region PropertyChanged

    protected virtual void OnRosterIdChanged()
    {
        Refresh();
    }

    #endregion

    #endregion Methods
}
}