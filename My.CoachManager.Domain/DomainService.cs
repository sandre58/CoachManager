using My.CoachManager.CrossCutting.Logging;

namespace My.CoachManager.Domain
{
    public abstract class DomainService
    {
        #region Fields

        protected readonly ILogger Logger;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="DomainService"/>.
        /// </summary>
        /// <param name="logger"></param>
        public DomainService(ILogger logger)
        {
            Logger = logger;
        }

        #endregion Constructors
    }
}