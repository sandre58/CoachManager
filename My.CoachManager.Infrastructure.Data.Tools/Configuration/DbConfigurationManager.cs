using My.CoachManager.CrossCutting.Core.Configurations;

namespace My.CoachManager.Infrastructure.Data.Tools.Configuration
{
    /// <summary>
    /// Database Configuration Keys.
    /// </summary>
    public class DbConfigurationManager : ConfigurationManagerBase
    {
        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <value>
        /// The name of the WCF user.
        /// </value>
        public static string ConnectionString => GetConnectionString("CoachManager");
    }
}