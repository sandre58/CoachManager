using System.Collections.Generic;
using Microsoft.Practices.Unity.Configuration;
using My.CoachManager.CrossCutting.Core.Configurations;

namespace My.CoachManager.CrossCutting.Unity.Configurations
{
    /// <summary>
    /// Unity Configuration Parameters.
    /// </summary>
    public class UnityConfigurationManager : ConfigurationManagerBase
    {
        /// <summary>
        /// Gets the loaded unity sections list.
        /// </summary>
        public static IDictionary<string, UnityConfigurationSection> UnitySections
        {
            get
            {
                return GetAllConfigSections<UnityConfigurationSection>("unity");
            }
        }
    }
}