using System.Collections.Generic;
using System.Linq;
using My.CoachManager.Presentation.Prism.Core.ViewModels;
using My.CoachManager.Presentation.Prism.Models;
using My.CoachManager.Presentation.Prism.Models.Aggregates;
using My.CoachManager.Presentation.ServiceAgent.PositionServiceReference;
using My.CoachManager.Presentation.ServiceAgent.RosterServiceReference;

namespace My.CoachManager.Presentation.Prism.Modules.Roster.ViewModels
{
    public class RosterPlayerViewModel : ItemViewModel<RosterPlayerModel>
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

        #endregion

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="RosterViewModel"/>.
        /// </summary>
        public RosterPlayerViewModel(IRosterService rosterService, IPositionService positionService)
        {
            _rosterService = rosterService;
            _positionService = positionService;
        }

        #endregion Constructors

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