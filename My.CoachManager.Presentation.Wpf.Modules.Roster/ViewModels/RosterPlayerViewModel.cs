using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using My.CoachManager.Application.Dtos;
using My.CoachManager.CrossCutting.Core.Extensions;
using My.CoachManager.CrossCutting.Core.Resources;
using My.CoachManager.Presentation.Core.Helpers;
using My.CoachManager.Presentation.Models;
using My.CoachManager.Presentation.Models.Aggregates;
using My.CoachManager.Presentation.Wpf.Core.Constants;
using My.CoachManager.Presentation.Wpf.Core.Dialog;
using My.CoachManager.Presentation.Wpf.Core.Manager;
using My.CoachManager.Presentation.Wpf.Core.ViewModels;
using My.CoachManager.Presentation.Wpf.Modules.Shared;
using My.CoachManager.Presentation.Wpf.Modules.Shared.Events;
using Prism.Commands;

namespace My.CoachManager.Presentation.Wpf.Modules.Roster.ViewModels
{

    public class RosterPlayerViewModel : ItemViewModel<RosterPlayerModel, RosterPlayerEditViewModel>
    {
        #region Members

        /// <summary>
        /// Gets or sets positions.
        /// </summary>
        public IEnumerable<PlayerPositionModel> Positions { get; set; }

        /// <summary>
        /// Gets or sets all positions.
        /// </summary>
        public IEnumerable<PositionModel> AllPositions { get; private set; }

        /// <summary>
        /// Gets or sets Add injury command.
        /// </summary>
        public ICommand AddInjuryCommand { get; set; }

        /// <summary>
        /// Gets or sets Edit injury command.
        /// </summary>
        public DelegateCommand<InjuryModel> EditInjuryCommand { get; set; }

        /// <summary>
        /// Gets or sets remove injury command.
        /// </summary>
        public DelegateCommand<InjuryModel> RemoveInjuryCommand { get; set; }

        /// <summary>
        /// Gets or sets selected injury.
        /// </summary>
        public InjuryModel SelectedInjury { get; set; }

        #endregion

        #region Initialization

        /// <inheritdoc />
        /// <summary>
        /// Initializes commands.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
            
            AddInjuryCommand = new DelegateCommand(AddInjury, CanAddInjury);
            EditInjuryCommand = new DelegateCommand<InjuryModel>(EditInjury, CanEditInjury);
            RemoveInjuryCommand = new DelegateCommand<InjuryModel>(RemoveInjury, CanRemoveInjury);
        }

        #endregion Initialization

        #region Data

        protected override void InitializeDataCore()
        {
            base.InitializeDataCore();

            var result = ApiHelper.GetData<IEnumerable<PositionDto>>(ApiConstants.ApiPositions);
            AllPositions = result.Select(PositionFactory.Get).ToObservableCollection();
        }

        protected override RosterPlayerModel LoadItemCore(int id)
        {
            return RosterFactory.Get(ApiHelper.GetData<RosterPlayerDto>(ApiConstants.ApiRostersPlayer, id));
        }

        /// <summary>
        /// Called after load data.
        /// </summary>
        protected override void OnLoadDataCompleted()
        {
            if (Item != null) Title = Item.FirstName + " " + Item.LastName.ToUpper();
            if (AllPositions != null)
            {
                Positions = AllPositions.Select(x =>
                {
                    var pos = Item.Positions.FirstOrDefault(p => p.PositionId == x.Id);
                    if (pos == null)
                    {
                        return new PlayerPositionModel()
                        {
                            Position = x,
                            IsSelectable = false
                        };
                    }

                    return pos;
                }).ToList();
            }

            if (Item != null) SelectedInjury = Item.Injury;
        }

        #endregion Data

        #region AddInjury

        /// <summary>
        /// Edit Item.
        /// </summary>
        protected virtual void AddInjury()
        {
            EventAggregator.GetEvent<EditInjuryRequestEvent>().Publish(new EditItemRequestEventArgs(0, 
                dialog =>
                {
                    if (dialog.Result == DialogResult.Ok) Refresh();
                }
                , new List<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>(ParametersConstants.PlayerId, Item.PlayerId),
                new KeyValuePair<string, object>(ParametersConstants.Date, DateTime.Today)
            }));
        }

        /// <summary>
        /// Can Edit item.
        /// </summary>
        protected virtual bool CanAddInjury()
        {
            return Mode == ScreenMode.Read;
        }

        #endregion

        #region EditInjury

        /// <summary>
        /// Edit Item.
        /// </summary>
        protected virtual void EditInjury(InjuryModel injury)
        {
            EventAggregator.GetEvent<EditInjuryRequestEvent>().Publish(new EditItemRequestEventArgs(injury.Id, 
                dialog =>
                {
                    if (dialog.Result == DialogResult.Ok) Refresh();
                }
                , new List<KeyValuePair<string, object>>
                {
                    new KeyValuePair<string, object>(ParametersConstants.PlayerId, Item.PlayerId)
                }));

        }

        /// <summary>
        /// Can Edit item.
        /// </summary>
        protected virtual bool CanEditInjury(InjuryModel injury)
        {
            return Mode == ScreenMode.Read && injury != null;
        }

        #endregion

        #region RemoveInjury

        /// <summary>
        /// Called when [background worker on do work].
        /// </summary>
        private void RemoveInjuryCore(InjuryModel injury)
        {
            ApiHelper.DeleteData(ApiConstants.ApiPlayersInjury, injury.Id);
        }

        /// <summary>
        /// Edit Item.
        /// </summary>
        protected virtual void RemoveInjury(InjuryModel injury)
        {
            if(!OnRemoveInjuryRequested(injury)) return;
            CallWebService(() => RemoveInjuryCore(injury), () => OnRemoveInjurySucceeded(injury), null, null, true);
        }

        /// <summary>
        /// Can Edit item.
        /// </summary>
        protected virtual bool CanRemoveInjury(InjuryModel injury)
        {
            return Mode == ScreenMode.Read && injury != null;
        }

        /// <summary>
        /// Called before the edit action;
        /// </summary>
        protected virtual bool OnRemoveInjuryRequested(InjuryModel injury)
        {
            return DialogManager.ShowWarningDialog(MessageResources.ConfirmationRemovingItem,
                       MessageDialogButtons.YesNo) == DialogResult.Yes;
        }

        /// <summary>
        /// Called after the edit action;
        /// </summary>
        protected virtual void OnRemoveInjurySucceeded(InjuryModel injury)
        {
            NotificationManager.ShowSuccess(MessageResources.ItemRemoved);
        }

        #endregion
    }
}