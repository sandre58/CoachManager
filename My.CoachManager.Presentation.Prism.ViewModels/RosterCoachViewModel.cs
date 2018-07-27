using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Metadatas;
using My.CoachManager.Presentation.Prism.Core.ViewModels;

namespace My.CoachManager.Presentation.Prism.ViewModels
{
    [MetadataType(typeof(RosterCoachMetadata))]
    public class RosterCoachViewModel : EntityViewModelBase
    {
        private int _rosterId;

        /// <summary>
        /// Gets or sets the coach's roster id.
        /// </summary>
        public int RosterId { get { return _rosterId; } set { SetProperty(ref _rosterId, value); } }

        private RosterViewModel _roster;

        /// <summary>
        /// Gets or sets the coach's roster.
        /// </summary>
        public RosterViewModel Roster { get { return _roster; } set { SetProperty(ref _roster, value); } }

        private int _coachId;

        /// <summary>
        /// Gets or sets the coach id.
        /// </summary>
        public int CoachId { get { return _coachId; } set { SetProperty(ref _coachId, value); } }

        private CoachViewModel _coach;

        /// <summary>
        /// Gets or sets the coach.
        /// </summary>
        public CoachViewModel Coach { get { return _coach; } set { SetProperty(ref _coach, value); } }

        private int _functionId;

        /// <summary>
        /// Gets or sets the function id.
        /// </summary>
        public int FunctionId { get { return _functionId; } set { SetProperty(ref _functionId, value); } }

        private FunctionViewModel _function;

        /// <summary>
        /// Gets or sets the function.
        /// </summary>
        public FunctionViewModel Function { get { return _function; } set { SetProperty(ref _function, value); } }
    }
}