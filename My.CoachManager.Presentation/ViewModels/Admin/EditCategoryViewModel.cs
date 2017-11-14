using My.CoachManager.Application.Dtos.Data;
using My.CoachManager.Presentation.Core.ViewModels.Screens.Enums;
using My.CoachManager.Presentation.Resources.Strings.Screens;
using My.CoachManager.Presentation.ServiceAgent;
using My.CoachManager.Presentation.ServiceAgent.AdminServiceReference;
using My.CoachManager.Presentation.ViewModels.Core;
using My.CoachManager.Presentation.ViewModels.Mapping;

namespace My.CoachManager.Presentation.ViewModels.Admin
{
    public class EditCategoryViewModel : EditViewModel<CategoryViewModel>
    {
        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="EditCategoryViewModel"/>.
        /// </summary>
        public EditCategoryViewModel()
        {
        }

        /// <summary>
        /// Initialise a new instance of <see cref="EditCategoryViewModel"/>.
        /// </summary>
        /// <param name="id"></param>
        public EditCategoryViewModel(int id)
            : base(id)
        {
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Get the display Name.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return Mode == ModeView.Modification ? AdminResources.EditCategory : AdminResources.CreateCategory;
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
                if (Mode != ModeView.Creation)
                    Item = client.GetCategoryById(ItemId).ToViewModel<CategoryViewModel>();
            }
        }

        /// <summary>
        /// Save item.
        /// </summary>
        protected override void SaveDataCore()
        {
            using (var client = ServiceClientFactory.Create<AdminServiceClient, IAdminService>())
            {
                Item = client.CreateOrUpdateCategory(Item.ToDto<CategoryDto>()).ToViewModel<CategoryViewModel>();
            }
        }

        #endregion Methods
    }
}