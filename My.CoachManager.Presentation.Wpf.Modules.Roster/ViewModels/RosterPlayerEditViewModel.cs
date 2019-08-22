using System.Collections.Generic;
using System.Linq;
using My.CoachManager.Application.Dtos;
using My.CoachManager.Presentation.Core.Helpers;
using My.CoachManager.Presentation.Models;
using My.CoachManager.Presentation.Models.Aggregates;
using My.CoachManager.Presentation.Wpf.Core.ViewModels;
using My.CoachManager.Presentation.Wpf.Modules.Shared;
using My.CoachManager.Presentation.Wpf.Modules.Shared.ViewModels;

namespace My.CoachManager.Presentation.Wpf.Modules.Roster.ViewModels
{
    public class RosterPlayerEditViewModel : PlayerEditViewModelBase<RosterPlayerModel>
    {
        #region Memebers

        /// <summary>
        /// Gets or sets categories.
        /// </summary>
        public IEnumerable<CategoryModel> AllCategories { get; private set; }

        #endregion Memebers

        #region Methods

        /// <summary>
        /// Initialize data asynchronous.
        /// </summary>
        protected override void InitializeDataCore()
        {
            base.InitializeDataCore();
            
            AllCategories = ApiHelper.GetData<IEnumerable<CategoryDto>>(ApiConstants.ApiCategories).Select(CategoryFactory.Get);
        }

        /// <inheritdoc />
        /// <summary>
        /// Save.
        /// </summary>
        protected override int SaveItemCore()
        {
            return ApiHelper.PostData<RosterPlayerDto, int>(ApiConstants.ApiRostersPlayer, RosterFactory.Get(Item, Mode == ScreenMode.Creation ? CrudStatus.Created : CrudStatus.Updated));
        }

        /// <inheritdoc />
        /// <summary>
        /// Load an item from data source.
        /// </summary>
        /// <param name="id"></param>
        protected override RosterPlayerModel LoadItemCore(int id)
        {
            var result = ApiHelper.GetData<RosterPlayerDto>(ApiConstants.ApiRostersPlayer, id);
            return RosterFactory.Get(result);
        }

        /// <summary>
        /// Called after load data.
        /// </summary>
        protected override void OnLoadDataCompleted()
        {
            base.OnLoadDataCompleted();
            var start = Mode == ScreenMode.Edition ? EditItemMessage : NewItemMessage;
            Title = start + " : " + Item.FullName;
        }

        #endregion Methods
    }
}