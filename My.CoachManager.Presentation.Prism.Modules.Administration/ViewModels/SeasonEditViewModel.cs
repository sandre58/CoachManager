using My.CoachManager.Application.Dtos.Seasons;
using My.CoachManager.Presentation.Prism.Core.ViewModels.Screens;
using My.CoachManager.Presentation.Prism.Models;
using My.CoachManager.Presentation.Prism.Modules.Administration.Resources.Strings;
using My.CoachManager.Presentation.ServiceAgent.SeasonServiceReference;

namespace My.CoachManager.Presentation.Prism.Modules.Administration.ViewModels
{
    public class SeasonEditViewModel : EditViewModel<SeasonModel>, ISeasonEditViewModel
    {
        #region Fields

        private readonly ISeasonService _seasonService;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="SeasonEditViewModel"/>.
        /// </summary>
        public SeasonEditViewModel(ISeasonService seasonService)
        {
            _seasonService = seasonService;
            Title = AdministrationResources.SeasonTitle;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Save.
        /// </summary>
        protected override bool SaveItemCore()
        {
            //Item = _seasonService.CreateOrUpdate(Item.ToDto<SeasonDto>()).ToViewModel<SeasonViewModel>();
            return true;
        }

        /// <summary>
        /// Load an item from data source.
        /// </summary>
        /// <param name="id"></param>
        protected override SeasonModel LoadItemCore(int id)
        {
            return null;
            //return _seasonService.GetById(id).ToViewModel<SeasonViewModel>();
        }

        #endregion Methods
    }
}