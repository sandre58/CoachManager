using My.CoachManager.Application.Dtos.Admin;
using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.Presentation.Prism.Administration.Resources.Strings;
using My.CoachManager.Presentation.Prism.Core.Services;
using My.CoachManager.Presentation.Prism.Core.ViewModels;
using My.CoachManager.Presentation.Prism.ViewModels;
using My.CoachManager.Presentation.Prism.ViewModels.Mapping;
using My.CoachManager.Presentation.ServiceAgent.AdminServiceReference;

namespace My.CoachManager.Presentation.Prism.Administration.ViewModels
{
    public class PositionEditViewModel : EditViewModel<PositionViewModel>, IPositionEditViewModel
    {
        #region Fields

        private readonly IAdminService _adminService;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="PositionEditViewModel"/>.
        /// </summary>
        public PositionEditViewModel(IAdminService adminService, IDialogService dialogService, ILogger logger)
            : base(dialogService, logger)
        {
            _adminService = adminService;
            Title = AdministrationResources.PositionTitle;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Save.
        /// </summary>
        protected override void SaveItemCore()
        {
            Item = _adminService.CreateOrUpdatePosition(Item.ToDto<PositionDto>()).ToViewModel<PositionViewModel>();
        }

        /// <summary>
        /// Load an item from data source.
        /// </summary>
        /// <param name="id"></param>
        protected override PositionViewModel LoadItemCore(int id)
        {
            return _adminService.GetPositionById(id).ToViewModel<PositionViewModel>();
        }

        #endregion Methods
    }
}