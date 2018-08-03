using My.CoachManager.CrossCutting.Core.Configurations;

namespace My.CoachManager.Presentation.Prism.Core.Manager
{
    public class ConfigurationManager : ConfigurationManagerBase
    {
        #region Constants

        public const string WindowsAuthenticationKey = "WindowsAuthentication";
        public const string ClubIdKey = "ClubId";

        #endregion Constants

        /// <summary>
        /// Gets a value indicating whether [Windows authentication is enabled].
        /// </summary>
        /// <value><c>true</c> if [Windows authentication is enabled]; otherwise, <c>false</c>.</value>
        public static bool WindowsAuthentication => GetAppSettingKey<bool>(WindowsAuthenticationKey);

        /// <summary>
        /// Gets a value indicating whether [Windows authentication is enabled].
        /// </summary>
        /// <value><c>true</c> if [Windows authentication is enabled]; otherwise, <c>false</c>.</value>
        public static int ClubId => GetAppSettingKey<int>(ClubIdKey);
    }
}