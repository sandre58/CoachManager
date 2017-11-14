using System.Configuration;
using My.CoachManager.CrossCutting.Core.Configurations;

namespace My.CoachManager.CrossCutting.Logging.Supervision.Configuration
{
    /// <summary>
    /// The Log4Net configuration manager.
    /// </summary>
    public class NLogConfigurationManager : ConfigurationManagerBase
    {
        /// <summary>
        /// Gets the log4net file.
        /// </summary>
        public static string GetNLogFile
        {
            get
            {
                var nLogConfiguration = GetConfigSection<DefaultSection>("nlog");
                return nLogConfiguration.CurrentConfiguration.HasFile ? nLogConfiguration.CurrentConfiguration.FilePath : string.Empty;
            }
        }
    }
}