using My.CoachManager.Application.Dtos;
using My.CoachManager.Presentation.Core.Helpers;
using My.CoachManager.Presentation.Models;
using My.CoachManager.Presentation.Models.Aggregates;
using My.CoachManager.Presentation.Wpf.Core.ViewModels;
using My.CoachManager.Presentation.Wpf.Modules.Shared;
using My.CoachManager.Presentation.Wpf.Modules.Shared.ViewModels;

namespace My.CoachManager.Presentation.Wpf.Modules.Admin.ViewModels
{
    public class PlayerEditViewModel : PlayerEditViewModelBase<PlayerModel>
    {

        #region Methods

        /// <inheritdoc />
        /// <summary>
        /// Save.
        /// </summary>
        protected override int SaveItemCore()
        {
            return ApiHelper.PostData<PlayerDto, int>(ApiConstants.ApiPlayers, PlayerFactory.Get(Item, Mode == ScreenMode.Creation ? CrudStatus.Created : CrudStatus.Updated));
        }

        /// <inheritdoc />
        /// <summary>
        /// Load an item from data source.
        /// </summary>
        /// <param name="id"></param>
        protected override PlayerModel LoadItemCore(int id)
        {
            var result = ApiHelper.GetData<PlayerDto>(ApiConstants.ApiPlayers, id);
            return PlayerFactory.Get(result);
        }

        #endregion Methods
    }
}