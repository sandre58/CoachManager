﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using My.CoachManager.Application.Dtos;
using My.CoachManager.CrossCutting.Core.Exceptions;
using My.CoachManager.CrossCutting.Core.Resources;
using My.CoachManager.Presentation.Core.Constants;
using My.CoachManager.Presentation.Core.Dialog;
using My.CoachManager.Presentation.Core.Manager;
using My.CoachManager.Presentation.Core.ViewModels;
using My.CoachManager.Presentation.Models;
using My.CoachManager.Presentation.Models.Aggregates;
using My.CoachManager.Presentation.Modules.Shared.Interfaces;
using My.CoachManager.Presentation.ServiceAgent.PersonServiceReference;
using My.CoachManager.Presentation.ServiceAgent.PositionServiceReference;
using My.CoachManager.Presentation.ServiceAgent.RosterServiceReference;
using Prism.Commands;
using InjuryEditView = My.CoachManager.Presentation.Modules.Roster.Views.InjuryEditView;

namespace My.CoachManager.Presentation.Modules.Roster.ViewModels
{
    public class RosterPlayerViewModel : ItemViewModel<RosterPlayerModel, IRosterPlayerEditView>
    {
        #region Fields

        private readonly IRosterService _rosterService;
        private readonly IPersonService _personService;
        private readonly IPositionService _positionService;

        #endregion Fields

        #region Members

        /// <summary>
        /// Gets or sets all positions.
        /// </summary>
        public IEnumerable<PlayerPositionModel> Positions { get; set; }

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

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="SquadViewModel"/>.
        /// </summary>
        public RosterPlayerViewModel(IRosterService rosterService, IPositionService positionService, IPersonService personService)
        {
            _rosterService = rosterService;
            _positionService = positionService;
            _personService = personService;
        }

        #endregion Constructors

        #region Initialization

        /// <inheritdoc />
        /// <summary>
        /// Initializes commands.
        /// </summary>
        protected override void InitializeCommand()
        {
            base.InitializeCommand();

            AddInjuryCommand = new DelegateCommand(AddInjury, CanAddInjury);
            EditInjuryCommand = new DelegateCommand<InjuryModel>(EditInjury, CanEditInjury);
            RemoveInjuryCommand = new DelegateCommand<InjuryModel>(RemoveInjury, CanRemoveInjury);
        }

        #endregion Initialization

        #region Data

        protected override RosterPlayerModel LoadItemCore(int id)
        {
            return RosterFactory.Get(_rosterService.GetRosterPlayerById(id));
        }

        /// <summary>
        /// Called after load data.
        /// </summary>
        protected override void OnLoadDataCompleted()
        {
            var positions = _positionService.GetPositions().Select(PositionFactory.Get).ToList();
            Positions = positions.Select(x =>
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

            SelectedInjury = Item.Injury;
        }

        #endregion Data

        #region AddInjury

        /// <summary>
        /// Edit Item.
        /// </summary>
        protected virtual void AddInjury()
        {
            DialogManager.ShowEditDialog<InjuryEditView>(0, dialog =>
            {
                if(dialog.Result == DialogResult.Ok)
                Refresh();
            },new List<KeyValuePair<string, object>>()
            {
                new KeyValuePair<string, object>(ParametersConstants.PlayerId, Item.Id),
                new KeyValuePair<string, object>(ParametersConstants.Date, DateTime.Today)
            } );
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
            DialogManager.ShowEditDialog<InjuryEditView>(injury.Id, dialog =>
            {
                OnEditCompleted(dialog.Result);
            }, new List<KeyValuePair<string, object>>
                {
                    new KeyValuePair<string, object>(ParametersConstants.PlayerId, Item.Id)
                });
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
        /// Edit Item.
        /// </summary>
        protected virtual void RemoveInjury(InjuryModel injury)
        {
            SelectedInjury = Item.Injury;
            if (DialogManager.ShowWarningDialog(MessageResources.ConfirmationRemovingItem, MessageDialogButtons.YesNo) != DialogResult.Yes) return;

            try
            {
                _personService.RemoveInjury(InjuryFactory.Get(injury, CrudStatus.Deleted));
                Refresh();
            }
            catch (BusinessException e)
            {
                OnBusinessExceptionOccured(e);
            }
            catch (Exception e)
            {
                OnExceptionOccured(e);
            }
        }

        /// <summary>
        /// Can Edit item.
        /// </summary>
        protected virtual bool CanRemoveInjury(InjuryModel injury)
        {
            return Mode == ScreenMode.Read && injury != null;
        }

        #endregion

        #region PropertyChanged

        /// <summary>
        /// Calls when Item changes.
        /// </summary>
        protected override void OnItemChanged()
        {
            base.OnItemChanged();

            if (Item != null)
                Title = Item.FullName;
        }

        #endregion PropertyChanged
    }
}