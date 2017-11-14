using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.Domain.Person.Aggregate;

namespace My.CoachManager.Domain.Person.Service
{
    public class CoachDomainService : DomainService, ICoachDomainService
    {
        #region Fields

        private readonly ICoachRepository _coachRepository;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="CoachDomainService"/>.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="coachRepository"></param>
        public CoachDomainService(ILogger logger, ICoachRepository coachRepository)
            : base(logger)
        {
            _coachRepository = coachRepository;
        }

        #endregion Constructors
    }
}