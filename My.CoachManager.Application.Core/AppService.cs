using My.CoachManager.CrossCutting.Logging;

namespace My.CoachManager.Application.Core
{
    public abstract class AppService
    {
        #region Fields

        protected readonly ILogger Logger;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialize a new instance of <see cref="AppService"/>.
        /// </summary>
        /// <param name="logger"></param>
        public AppService(ILogger logger)
        {
            Logger = logger;
        }

        #endregion Constructors
    }
}