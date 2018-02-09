using My.CoachManager.Application.Dtos.Positions;
using My.CoachManager.Presentation.Prism.Core.ViewModels.Screens;
using My.CoachManager.Presentation.Prism.Modules.Administration.Resources.Strings;
using My.CoachManager.Presentation.Prism.ViewModels;
using My.CoachManager.Presentation.Prism.ViewModels.Mapping;
using My.CoachManager.Presentation.ServiceAgent.PositionServiceReference;

namespace My.CoachManager.Presentation.Prism.Modules.Administration.ViewModels
{
    public class PositionEditViewModel : EditViewModel<PositionViewModel>, IPositionEditViewModel
    {
        #region Fields

        private readonly IPositionService _positionService;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="PositionEditViewModel"/>.
        /// </summary>
        public PositionEditViewModel(IPositionService positionService)
        {
            _positionService = positionService;
            Title = AdministrationResources.PositionTitle;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Save.
        /// </summary>
        protected override bool SaveItemCore()
        {
            Item = _positionService.CreateOrUpdate(Item.ToDto<PositionDto>()).ToViewModel<PositionViewModel>();
            return true;
        }

        /// <summary>
        /// Load an item from data source.
        /// </summary>
        /// <param name="id"></param>
        protected override PositionViewModel LoadItemCore(int id)
        {
            return _positionService.GetById(id).ToViewModel<PositionViewModel>();
        }

        #endregion Methods
    }
}