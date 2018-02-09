using System;
using My.CoachManager.Presentation.Prism.Core.ViewModels.Screens;
using My.CoachManager.Presentation.Prism.Modules.Roster.Views;
using My.CoachManager.Presentation.Prism.ViewModels;
using My.CoachManager.Presentation.Prism.ViewModels.Mapping;
using My.CoachManager.Presentation.ServiceAgent.RosterServiceReference;

namespace My.CoachManager.Presentation.Prism.Modules.Roster.ViewModels
{
    public class PlayerItemViewModel : ItemViewModel<PlayerDetailViewModel>, IPlayerItemViewModel
    {
        #region Fields

        private readonly IRosterService _rosterService;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="PlayerItemViewModel"/>.
        /// </summary>
        public PlayerItemViewModel(IRosterService rosterService) : this()
        {
            _rosterService = rosterService;
        }

        /// <summary>
        /// Initialise a new instance of <see cref="PlayerItemViewModel"/>.
        /// </summary>
        public PlayerItemViewModel()
        {
        }

        #endregion Constructors

        #region Methods

        #region Abstract Method

        protected override Type GetEditViewType()
        {
            return typeof(PlayerEditView);
        }

        #endregion Abstract Method

        #region Data

        /// <summary>
        /// Load an item from data source.
        /// </summary>
        /// <param name="id"></param>
        protected override PlayerDetailViewModel LoadItemCore(int id)
        {
            return _rosterService.GetPlayer(id).ToViewModel<PlayerDetailViewModel>();
        }

        /// <summary>
        /// Call after load data.
        /// </summary>
        protected override void OnLoadDataCompleted()
        {
            base.OnLoadDataCompleted();
            Title = Item.FullName;
        }

        #endregion Data

        #endregion Methods
    }
}