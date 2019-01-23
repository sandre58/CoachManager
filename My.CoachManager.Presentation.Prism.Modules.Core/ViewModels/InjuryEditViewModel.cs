using System;
using System.ComponentModel;
using My.CoachManager.Application.Dtos;
using My.CoachManager.CrossCutting.Core.Constants;
using My.CoachManager.Presentation.Prism.Core.ViewModels;
using My.CoachManager.Presentation.Prism.Models;
using My.CoachManager.Presentation.Prism.Models.Aggregates;
using My.CoachManager.Presentation.Prism.Modules.Core.Resources;
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
        public InjuryEditViewModel(IPersonService personService)
        {
            _personService = personService;
            NewItemMessage = InjuryResources.NewInjury;
            EditItemMessage = InjuryResources.EditInjury;
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

        protected override void OnLoadDataCompleted()
        {
            if (Item != null)
            {
                Item.PropertyChanged += OnItemPropertyChanged;

                var date = Parameters is InjuryEditParameters p ? p.Date : DateTime.Today.Date;
                Item.Date = date;
            }

            base.OnLoadDataCompleted();

        }

        /// <inheritdoc />
        /// <summary>
        /// Save.
        /// </summary>
        protected override int SaveItemCore()
        {
            var id = Parameters is InjuryEditParameters p ? p.PlayerId : 0;
            return _personService.SaveInjury(id, InjuryFactory.Get(Item, Mode == ScreenMode.Creation ? CrudStatus.Created : CrudStatus.Updated));
        }

        #endregion Data


        #region Properties Changed

        /// <summary>
        /// On property changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Type":
                    Item.Condition = InjuryConstants.GetDefaultCondition(Item.Type);
                    break;
            }
        }

        #endregion
        #endregion Methods
    }
}