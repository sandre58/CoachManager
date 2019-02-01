using System.Linq;
using My.CoachManager.Application.Dtos;
using My.CoachManager.CrossCutting.Core.Extensions;
using My.CoachManager.Presentation.Core.ViewModels;
using My.CoachManager.Presentation.Models;
using My.CoachManager.Presentation.Models.Aggregates;
using My.CoachManager.Presentation.Modules.Administration.Resources;
using My.CoachManager.Presentation.Modules.Shared.Interfaces;
using My.CoachManager.Presentation.ServiceAgent.CategoryServiceReference;

namespace My.CoachManager.Presentation.Modules.Administration.ViewModels
{
    public class CategoriesListViewModel : OrderedListViewModel<CategoryModel, ICategoryEditView, ICategoryEditView>
    {
        #region Fields

        private readonly ICategoryService _categoryService;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="CategoriesListViewModel"/>.
        /// </summary>
        public CategoriesListViewModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        #endregion Constructors

        #region Methods

        #region Initialization

        /// <inheritdoc />
        /// <summary>
        /// Initializes Data.
        /// </summary>
        protected override void InitializeData()
        {
            base.InitializeData();

            Title = CategoryResources.CategoriesTitle;
        }

        #endregion Initialization

        #region Data

        /// <inheritdoc />
        /// <summary>
        /// Remove the item from data source.
        /// </summary>
        /// <param name="item"></param>
        protected override void RemoveItemCore(CategoryModel item) => _categoryService.RemoveCategory(CategoryFactory.Get(item, CrudStatus.Deleted));

        /// <inheritdoc />
        /// <summary>
        /// Validates order in the data source.
        /// </summary>
        protected override void ValidateOrderCore() => _categoryService.UpdateOrders(Items.ToDictionary(c => c.Id, c => c.Order));

        /// <inheritdoc />
        /// <summary>
        /// Load Data.
        /// </summary>
        /// <returns></returns>
        protected override void LoadDataCore()
        {
            var result = _categoryService.GetCategories();
            
            Items = result.Select(CategoryFactory.Get).ToItemsObservableCollection();
        }

        #endregion Data

        #endregion Methods
    }
}