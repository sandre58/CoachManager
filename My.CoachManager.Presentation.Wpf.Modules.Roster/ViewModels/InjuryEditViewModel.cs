using System;
using System.ComponentModel;
using My.CoachManager.Application.Dtos;
using My.CoachManager.CrossCutting.Core.Constants;
using My.CoachManager.Presentation.Core.Helpers;
using My.CoachManager.Presentation.Models;
using My.CoachManager.Presentation.Models.Aggregates;
using My.CoachManager.Presentation.Wpf.Core.Constants;
using My.CoachManager.Presentation.Wpf.Core.ViewModels;
using My.CoachManager.Presentation.Wpf.Modules.Roster.Resources;
using My.CoachManager.Presentation.Wpf.Modules.Shared;

namespace My.CoachManager.Presentation.Wpf.Modules.Roster.ViewModels
{

    public class InjuryEditViewModel : EditViewModel<InjuryModel>
    {

        #region Methods

        protected override void Initialize()
        {
            base.Initialize();

            
            NewItemMessage = InjuryResources.NewInjury;
            EditItemMessage = InjuryResources.EditInjury;
        }

        /// <inheritdoc />
        /// <summary>
        /// Save.
        /// </summary>
        protected override int SaveItemCore()
        {
            var id = GetParameter<int>(ParametersConstants.PlayerId);
            return ApiHelper.PostData<InjuryDto, int>(ApiConstants.ApiPlayersInjury, InjuryFactory.Get(Item, id, Mode == ScreenMode.Creation ? CrudStatus.Created : CrudStatus.Updated));
        }

        /// <inheritdoc />
        /// <summary>
        /// Load an item from data source.
        /// </summary>
        /// <param name="id"></param>
        protected override InjuryModel LoadItemCore(int id)
        {
            var result = ApiHelper.GetData<InjuryDto>(ApiConstants.ApiPlayersInjury, id);
            return InjuryFactory.Get(result);
        }

        protected override void OnLoadDataCompleted()
        {
            if (Item != null)
            {
                Item.PropertyChanged += OnItemPropertyChanged;

                var date = GetParameter<DateTime>(ParametersConstants.Date);
                if (date != default(DateTime)) Item.Date = date;
               
            }

            base.OnLoadDataCompleted();

        }

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