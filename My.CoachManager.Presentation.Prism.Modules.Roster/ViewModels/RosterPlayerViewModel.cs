using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using My.CoachManager.Presentation.Prism.Core.Dialog;
using My.CoachManager.Presentation.Prism.Core.Manager;
using My.CoachManager.Presentation.Prism.Core.ViewModels;
using My.CoachManager.Presentation.Prism.Models;
using My.CoachManager.Presentation.Prism.Models.Aggregates;
using My.CoachManager.Presentation.Prism.Modules.Core.ViewModels;
using My.CoachManager.Presentation.Prism.Modules.Core.Views;
using My.CoachManager.Presentation.Prism.Modules.Roster.Views;
using My.CoachManager.Presentation.ServiceAgent.PositionServiceReference;
using My.CoachManager.Presentation.ServiceAgent.RosterServiceReference;
using Prism.Commands;

namespace My.CoachManager.Presentation.Prism.Modules.Roster.ViewModels
{
    public class RosterPlayerViewModel : ItemViewModel<RosterPlayerModel, RosterPlayerEditView>
    {
        #region Fields

        private readonly IRosterService _rosterService;
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

        #endregion

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="SquadViewModel"/>.
        /// </summary>
        public RosterPlayerViewModel(IRosterService rosterService, IPositionService positionService)
        {
            _rosterService = rosterService;
            _positionService = positionService;
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
        }

        #endregion Data

        #region AddInjury

        /// <summary>
        /// Edit Item.
        /// </summary>
        protected virtual void AddInjury()
        {
            DialogManager.ShowEditDialog<InjuryEditView>(new InjuryEditParameters(0)
            {
                PlayerId = Item.Id,
                Date = DateTime.Today
            }, dialog =>
            {
                if(dialog.Result == DialogResult.Ok)
                Refresh();
            });
        }

        /// <summary>
        /// Can Edit item.
        /// </summary>
        protected virtual bool CanAddInjury()
        {
            return Mode == ScreenMode.Read;
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