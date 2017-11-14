using My.CoachManager.CrossCutting.Core.Configurations;

namespace My.CoachManager.Presentation.ServiceAgent
{
    /// <summary>
    /// Service agent Configuration Keys.
    /// </summary>
    public class ServiceAgentConfigurationManager : ConfigurationManagerBase
    {
        /// <summary>
        /// Gets the name of the WCF user.
        /// </summary>
        /// <value>
        /// The name of the WCF user.
        /// </value>
        public static string WcfUserName
        {
            get { return GetAppSettingKey<string>("WCFUserName"); }
        }

        /// <summary>
        /// Gets the WCF user password.
        /// </summary>
        /// <value>
        /// The WCF user password.
        /// </value>
        public static string WcfUserPassword
        {
            get { return GetAppSettingKey<string>("WCFUserPassword"); }
        }
    }
}