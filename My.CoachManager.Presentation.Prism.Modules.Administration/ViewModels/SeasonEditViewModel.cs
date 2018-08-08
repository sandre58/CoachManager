using My.CoachManager.Application.Dtos;
using My.CoachManager.Presentation.Prism.Core.ViewModels;
using My.CoachManager.Presentation.Prism.Models;
using My.CoachManager.Presentation.Prism.Models.Aggregates;
using My.CoachManager.Presentation.ServiceAgent.SeasonServiceReference;

namespace My.CoachManager.Presentation.Prism.Modules.Administration.ViewModels
{
    public class SeasonEditViewModel : EditViewModel<SeasonModel>
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
        }

        #endregion Constructors

        #region Methods

        /// <inheritdoc />
        /// <summary>
        /// Save.
        /// </summary>
        protected override bool SaveItemCore()
        {
            var dto = _seasonService.SaveSeason(SeasonFactory.Get(Item, Mode == ScreenMode.Creation ? CrudStatus.Created : CrudStatus.Updated));
            Item = SeasonFactory.Get(dto);
            return true;
        }

        /// <inheritdoc />
        /// <summary>
        /// Load an item from data source.
        /// </summary>
        /// <param name="id"></param>
        protected override SeasonModel LoadItemCore(int id)
        {
            return SeasonFactory.Get(_seasonService.GetSeasonById(id));
        }

        #endregion Methods
    }
}