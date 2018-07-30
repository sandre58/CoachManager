﻿using System.Collections.ObjectModel;
using System.Linq;
using My.CoachManager.Presentation.Prism.Core.ViewModels.Screens;
using My.CoachManager.Presentation.Prism.Models;
using My.CoachManager.Presentation.Prism.Modules.Administration.Resources.Strings;
using My.CoachManager.Presentation.ServiceAgent.CategoryServiceReference;

namespace My.CoachManager.Presentation.Prism.Modules.Administration.ViewModels
{
    public class CategoriesListViewModel : OrderedListViewModel<CategoryModel>
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

            Title = AdministrationResources.CategoriesTitle;
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
            //_categoryService.Remove(item.ToDto<CategoryDto>());
        }

        /// <inheritdoc />
        /// <summary>
        /// Validates order in the data source.
        /// </summary>
        protected override void ValidateOrderCore()
        {
            _categoryService.UpdateOrders(Items.ToDictionary(c => c.Id, c => c.Order));
        }

        /// <inheritdoc />
        /// <summary>
        /// Load Data.
        /// </summary>
        /// <returns></returns>
        protected override void LoadDataCore()
        {
            var result = _categoryService.GetCategories();

            //Items = new ObservableCollection<CategoryModel>(result.Select(x => new CategoryModel
            //{
            //    Id = x.Id,
            //    Code = x.Code,
            //    Label = x.Label,
            //    Description = x.Description,
            //    Order = x.Order,
            //    Year = x.Year,
            //}));
        }

        #endregion Data

        #endregion Methods
    }
}