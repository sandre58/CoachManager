using My.CoachManager.Application.Dtos;
using My.CoachManager.Presentation.Core.Helpers;
using My.CoachManager.Presentation.Models;
using My.CoachManager.Presentation.Models.Aggregates;
using My.CoachManager.Presentation.Wpf.Core.ViewModels;
using My.CoachManager.Presentation.Wpf.Modules.Shared;

namespace My.CoachManager.Presentation.Wpf.Modules.Roster.ViewModels
{

    public class SquadEditViewModel : EditViewModel<SquadModel>
    {

        #region Methods

        /// <inheritdoc />
        /// <summary>
        /// Save.
        /// </summary>
        protected override int SaveItemCore()
        {
            return ApiHelper.PostData<SquadDto, int>(ApiConstants.ApiSquads, SquadFactory.Get(Item, AppManager.Roster.Id, Mode == ScreenMode.Creation ? CrudStatus.Created : CrudStatus.Updated));
        }

        /// <inheritdoc />
        /// <summary>
        /// Load an item from data source.
        /// </summary>
        /// <param name="id"></param>
        protected override SquadModel LoadItemCore(int id)
        {
            var result = ApiHelper.GetData<SquadDto>(ApiConstants.ApiSquads, id);
            return SquadFactory.Get(result);
        }

        #endregion Methods
    }
}