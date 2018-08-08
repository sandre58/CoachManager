using System.Collections.Generic;
using System.Linq;
using My.CoachManager.Application.Dtos;
using My.CoachManager.Presentation.Prism.Core.ViewModels;
using My.CoachManager.Presentation.Prism.Models;
using My.CoachManager.Presentation.Prism.Models.Aggregates;
using My.CoachManager.Presentation.ServiceAgent.CategoryServiceReference;
using My.CoachManager.Presentation.ServiceAgent.RosterServiceReference;
using My.CoachManager.Presentation.ServiceAgent.SeasonServiceReference;

namespace My.CoachManager.Presentation.Prism.Modules.Administration.ViewModels
{
    public class RosterEditViewModel : EditViewModel<RosterModel>
    {
        #region Fields

        private readonly IRosterService _rosterService;
        private readonly ICategoryService _categoryService;
        private readonly ISeasonService _seasonService;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="RosterEditViewModel"/>.
        /// </summary>
        public RosterEditViewModel(IRosterService rosterService, ICategoryService categoryService, ISeasonService seasonService)
        {
            _rosterService = rosterService;
            _categoryService = categoryService;
            _seasonService = seasonService;
        }

        #endregion Constructors

        #region Members

        /// <summary>
        /// Gets or sets categories.
        /// </summary>
        public IEnumerable<CategoryModel> Categories { get; private set; }

        /// <summary>
        /// Gets or sets seasons.
        /// </summary>
        public IEnumerable<SeasonModel> Seasons { get; private set; }

        #endregion Members

        #region Methods

        /// <inheritdoc />
        /// <summary>
        /// Save.
        /// </summary>
        protected override bool SaveItemCore()
        {
            var dto = _rosterService.SaveRoster(RosterFactory.Get(Item, Mode == ScreenMode.Creation ? CrudStatus.Created : CrudStatus.Updated));
            Item = RosterFactory.Get(dto);
            return true;
        }

        /// <inheritdoc />
        /// <summary>
        /// Load an item from data source.
        /// </summary>
        /// <param name="id"></param>
        protected override RosterModel LoadItemCore(int id)
        {
            return RosterFactory.Get(_rosterService.GetRosterById(id));
        }

        /// <inheritdoc />
        /// <summary>
        /// Initialize data asynchronous.
        /// </summary>
        protected override void InitializeDataCore()
        {
            Categories = _categoryService.GetCategories().Select(CategoryFactory.Get);
            Seasons = _seasonService.GetSeasons().Select(SeasonFactory.Get);
        }

        #endregion Methods
    }
}