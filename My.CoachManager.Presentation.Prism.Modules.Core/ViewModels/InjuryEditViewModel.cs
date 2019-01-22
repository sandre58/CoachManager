using My.CoachManager.Application.Dtos;
using My.CoachManager.Presentation.Prism.Core.ViewModels;
using My.CoachManager.Presentation.Prism.Models;
using My.CoachManager.Presentation.Prism.Models.Aggregates;
using My.CoachManager.Presentation.ServiceAgent.PersonServiceReference;

namespace My.CoachManager.Presentation.Prism.Modules.Core.ViewModels
{

    public class InjuryEditViewModel : EditViewModel<InjuryModel>
    {

        #region Fields

        private readonly IPersonService _personService;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="InjuryEditViewModel"/>.
        /// </summary>
        protected InjuryEditViewModel(IPersonService personService)
        {
            _personService = personService;
        }

        #endregion Constructors

 #region Methods

        #region Data

        /// <inheritdoc />
        /// <summary>
        /// Load an item from data source.
        /// </summary>
        /// <param name="id"></param>
        protected override InjuryModel LoadItemCore(int id)
        {
            return InjuryFactory.Get(_personService.GetInjuryById(id));
        }

        /// <inheritdoc />
        /// <summary>
        /// Save.
        /// </summary>
        protected override int SaveItemCore()
        {
            return _personService.SaveInjury(0, InjuryFactory.Get(Item, Mode == ScreenMode.Creation ? CrudStatus.Created : CrudStatus.Updated));
        }

        #endregion Data
        
        #endregion Methods
    }
}