using System.Collections.Generic;
using System.Linq;
using My.CoachManager.CrossCutting.Core.Configurations;
using My.CoachManager.CrossCutting.Core.Resources;

namespace My.CoachManager.Presentation.Wpf.Core
{
    /// <inheritdoc />
    /// <summary>
    /// Database Configuration Keys.
    /// </summary>
    public class ConfigurationManager : ConfigurationManagerBase
    {

        #region Members

        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <value>
        /// The name of the WCF user.
        /// </value>
        public static string Server => GetAppSettingKey<string>(nameof(Server));

        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <value>
        /// The name of the WCF user.
        /// </value>
        public static bool WindowsAuthentication => GetAppSettingKey<bool>(nameof(WindowsAuthentication));

        /// <summary>
        /// Gets items per page.
        /// </summary>
        public static IDictionary<string, int> ItemsPerPageList {
            get
            {
                var list = new Dictionary<string, int>();
                foreach (var item in GetAppSettingKey<string>(nameof(ItemsPerPageList)).Split(',').Select(int.Parse))
                {
                    list.Add(item.ToString(), item);
                }

                list.Add(GlobalResources.AllMales, 0);

                return list;
            }
    }

        #endregion

    }
}