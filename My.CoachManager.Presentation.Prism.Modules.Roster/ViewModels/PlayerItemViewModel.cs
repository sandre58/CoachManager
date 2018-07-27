using System;
using My.CoachManager.Presentation.Prism.Core.ViewModels.Screens;
using My.CoachManager.Presentation.Prism.Modules.Roster.Views;
using My.CoachManager.Presentation.Prism.ViewModels;
using My.CoachManager.Presentation.ServiceAgent.PersonServiceReference;

namespace My.CoachManager.Presentation.Prism.Modules.Roster.ViewModels
{
    public class PlayerItemViewModel : ItemViewModel<PlayerDetailViewModel>, IPlayerItemViewModel
    {
        #region Fields

        private readonly IPersonService _personService;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="PlayerItemViewModel"/>.
        /// </summary>
        public PlayerItemViewModel(IPersonService personService) : this()
        {
            _personService = personService;
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
            return null;
            //return _personService.GetPlayerDetails(id).ToViewModel<PlayerDetailViewModel>();
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