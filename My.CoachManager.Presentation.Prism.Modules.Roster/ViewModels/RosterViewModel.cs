using System.Linq;
using My.CoachManager.CrossCutting.Core.Extensions;
using My.CoachManager.Presentation.Prism.Core.Dialog;
using My.CoachManager.Presentation.Prism.Core.Enums;
using My.CoachManager.Presentation.Prism.Core.Manager;
using My.CoachManager.Presentation.Prism.Core.Resources;
using My.CoachManager.Presentation.Prism.Core.ViewModels;
using My.CoachManager.Presentation.Prism.Core.ViewModels.Interfaces;
using My.CoachManager.Presentation.Prism.Models;
using My.CoachManager.Presentation.Prism.Models.Aggregates;
using My.CoachManager.Presentation.Prism.Modules.Core.Views;
using My.CoachManager.Presentation.Prism.Modules.Roster.Resources;
using My.CoachManager.Presentation.Prism.Modules.Roster.Views;
using My.CoachManager.Presentation.ServiceAgent.CategoryServiceReference;
using My.CoachManager.Presentation.ServiceAgent.PersonServiceReference;
using My.CoachManager.Presentation.ServiceAgent.PositionServiceReference;
using My.CoachManager.Presentation.ServiceAgent.RosterServiceReference;

namespace My.CoachManager.Presentation.Prism.Modules.Roster.ViewModels
{
    public class RosterViewModel : ListViewModel<RosterPlayerModel, RosterPlayerEditView, RosterPlayerView>
    {
        #region Fields

        private readonly IRosterService _rosterService;
        private readonly IPersonService _personService;
        private readonly ICategoryService _categoryService;
        private readonly IPositionService _positionService;

        #endregion Fields

        #region Members

        /// <summary>
        /// Gets or sets model.
        /// </summary>
        public RosterModel Roster { get; set; }

#endregion

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="RosterViewModel"/>.
        /// </summary>
        public RosterViewModel(IRosterService rosterService, IPersonService personService, ICategoryService categoryService, IPositionService positionService)
        {
            _rosterService = rosterService;
            _personService = personService;
            _categoryService = categoryService;
            _positionService = positionService;
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

            Title = RosterResources.RosterTitle;
        }

        #endregion Initialization

        #region Data

        /// <inheritdoc />
        /// <summary>
        /// Remove the item from data source.
        /// </summary>
        /// <param name="item"></param>
        protected override void RemoveItemCore(RosterPlayerModel item)
        {
            _rosterService.RemovePlayers(Roster.Id, new[] { item.PlayerId });
        }

        /// <inheritdoc />
        /// <summary>
        /// Load Data.
        /// </summary>
        /// <returns></returns>
        protected override void LoadDataCore()
        {
            Roster = RosterFactory.Get(_rosterService.GetRosterById(SettingsManager.GetRosterId()));
            var result = _rosterService.GetPlayers(Roster.Id);

            Items = result.Select(RosterFactory.Get).ToItemsObservableCollection();
            Title = Roster.Name;
        }

        protected override void InitializeDataCore()
        {
            base.InitializeDataCore();

            var categories = _categoryService.GetCategories().Select(CategoryFactory.Get);
            var countries = _personService.GetCountries().Select(CountryFactory.Get);
            var positions  = _positionService.GetPositions().Select(PositionFactory.Get);
            Filters = new RosterFiltersViewModel(categories, positions, countries);
            Parameters = new RosterParametersViewModel();
        }

        #endregion Data

        #region Add

        protected override void Add()
        {

            DialogManager.ShowSelectItemsDialog<SelectPlayersView>(dialog =>
            {
                var model = dialog.Content.DataContext as ISelectItemsViewModel<RosterPlayerModel>;
                if (dialog.Result == DialogResult.Ok)
                {
                    if (model != null)
                    {
                        _rosterService.AddPlayers(Roster.Id, model.SelectedItems.Select(x => x.Id).ToArray());

                        NotificationManager.ShowSuccess(string.Format(MessageResources.ItemsAdded,
                            model.SelectedItems.Count()));
                    }
                }

                OnAddCompleted(dialog.Result);
            },
            SelectionMode.Multiple,
            Items.Select(x => new PlayerModel()
            {
                Id = x.PlayerId
            }).ToList());
        }

        #endregion Add

        #endregion Methods
    }
}