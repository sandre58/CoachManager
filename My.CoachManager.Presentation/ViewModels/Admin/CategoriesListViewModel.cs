using System.Linq;
using My.CoachManager.Application.Dtos.Data;
using My.CoachManager.Presentation.ServiceAgent;
using My.CoachManager.Presentation.ServiceAgent.AdminServiceReference;
using My.CoachManager.Presentation.ViewModels.Core;
using My.CoachManager.Presentation.ViewModels.Mapping;

namespace My.CoachManager.Presentation.ViewModels.Admin
{
    /// <summary>
    /// ViewModel for the settings window.
    /// </summary>
    public class CategoriesListViewModel : DataListViewModel<CategoryViewModel, EditCategoryViewModel>
    {
        #region Properties

        /// <summary>
        /// Get the display Name.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return Resources.Strings.Screens.AdminResources.CategoriesList;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Load Data.
        /// </summary>
        /// <returns></returns>
        protected override void LoadDataCore()
        {
            using (var client = ServiceClientFactory.Create<AdminServiceClient, IAdminService>())
            {
                var result = client.GetCategories();

                //SetEntities(result);
            }
        }

        /// <summary>
        /// Save order.
        /// </summary>
        /// <returns></returns>
        protected override void SaveOrders()
        {
            using (var client = ServiceClientFactory.Create<AdminServiceClient, IAdminService>())
            {
                client.UpdateCategoriesOrders(Entities.ToDictionary(d => d.Id, d => d.Order));
            }
        }

        /// <summary>
        /// Remove an item.
        /// </summary>
        protected override void RemoveItemCore(CategoryViewModel item)
        {
            using (var client = ServiceClientFactory.Create<AdminServiceClient, IAdminService>())
            {
                client.RemoveCategory(item.ToDto<CategoryDto>());
            }
        }

        #endregion Methods
    }
}