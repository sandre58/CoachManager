using System;
using System.Collections.ObjectModel;
using System.Linq;
using My.CoachManager.Application.Dtos.Categories;
using My.CoachManager.Presentation.Prism.Core.ViewModels.Screens;
using My.CoachManager.Presentation.Prism.Modules.Administration.Resources.Strings;
using My.CoachManager.Presentation.Prism.Modules.Administration.Views;
using My.CoachManager.Presentation.Prism.ViewModels;
using My.CoachManager.Presentation.Prism.ViewModels.Mapping;
using My.CoachManager.Presentation.ServiceAgent.CategoryServiceReference;

namespace My.CoachManager.Presentation.Prism.Modules.Administration.ViewModels
{
    public class CategoriesListViewModel : OrderedListViewModel<CategoryViewModel>, ICategoriesListViewModel
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

        #region Abstract Methods

        /// <summary>
        /// Get Item View Type.
        /// </summary>
        /// <returns></returns>
        protected override Type GetEditViewType()
        {
            return typeof(CategoryEditView);
        }

        #endregion Abstract Methods

        #region Initialization

        /// <summary>
        /// Initializes Data.
        /// </summary>
        protected override void InitializeData()
        {
            base.InitializeData();

            Title = AdministrationResources.CategoriesTitle;
        }

        #endregion Initialization

        #region Data

        /// <summary>
        /// Remove the item from data source.
        /// </summary>
        /// <param name="item"></param>
        protected override void RemoveItemCore(CategoryViewModel item)
        {
            _categoryService.Remove(item.ToDto<CategoryDto>());
        }

        /// <summary>
        /// Validates order in the data source.
        /// </summary>
        protected override void ValidateOrderCore()
        {
            _categoryService.UpdateOrders(Items.ToDictionary(c => c.Id, c => c.Order));
        }

        /// <summary>
        /// Load Data.
        /// </summary>
        /// <returns></returns>
        protected override void LoadDataCore()
        {
            var result = _categoryService.GetList();

            Items = new ObservableCollection<CategoryViewModel>(result.OrderBy(x => x.Order).ToViewModels<CategoryViewModel>());
        }

        #endregion Data

        #endregion Methods
    }
}