using My.CoachManager.Application.Dtos;
using My.CoachManager.Presentation.Core.Helpers;
using My.CoachManager.Presentation.Models;
using My.CoachManager.Presentation.Models.Aggregates;
using My.CoachManager.Presentation.Wpf.Constants;
using My.CoachManager.Presentation.Wpf.Core.ViewModels;

namespace My.CoachManager.Presentation.Wpf.ViewModels.Administration
{
    public class SeasonEditViewModel : EditViewModel<SeasonModel>
    {

        #region Methods

        /// <inheritdoc />
        /// <summary>
        /// Save.
        /// </summary>
        protected override int SaveItemCore()
        {
            return ApiHelper.PostData<SeasonDto, int>(ApiConstants.ApiSeasons, SeasonFactory.Get(Item, Mode == ScreenMode.Creation ? CrudStatus.Created : CrudStatus.Updated));
        }

        /// <inheritdoc />
        /// <summary>
        /// Load an item from data source.
        /// </summary>
        /// <param name="id"></param>
        protected override SeasonModel LoadItemCore(int id)
        {
            var result = ApiHelper.GetData<SeasonDto>(ApiConstants.ApiSeasons, id);
            return SeasonFactory.Get(result);
        }

        #endregion Methods
    }
}