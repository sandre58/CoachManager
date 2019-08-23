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
    public class CategoriesListViewModel : OrderableListViewModel<CategoryModel, CategoryEditViewModel, CategoryEditViewModel>
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

            Title = CommonResources.Categories;
        }

        #endregion Initialization

        #region Data

        /// <inheritdoc />
        /// <summary>
        /// Remove the item from data source.
        /// </summary>
        /// <param name="item"></param>
        protected override void RemoveItemCore(CategoryModel item)
        {
            ApiHelper.DeleteData(ApiConstants.ApiCategories, item.Id);
        }

        /// <inheritdoc />
        /// <summary>
        /// Validates order in the data source.
        /// </summary>
        protected override void ValidateOrderCore()
        {
            ApiHelper.PostData<IDictionary<int, int>, bool>(ApiConstants.ApiCategoriesOrders, Items.ToDictionary(c => c.Id, c => c.Order));
        }

        /// <inheritdoc />
        /// <summary>
        /// Load Data.
        /// </summary>
        /// <returns></returns>
        protected override void LoadDataCore()
        {
            var result = ApiHelper.GetData<IEnumerable<CategoryDto>>(ApiConstants.ApiCategories);
            SetItemsByDispatcher(result.Select(CategoryFactory.Get));
        }

        #endregion Data

        #endregion Methods
    }
}