using System.ServiceModel;

namespace My.CoachManager.Presentation.ServiceAgent
{
    /// <summary>
    /// Service factory.
    /// </summary>
    public static class ServiceClientFactory
    {
        /// <summary>
        /// Creates the specified service client type.
        /// </summary>
        /// <typeparam name="TClient">The type of the service client.</typeparam>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <returns>The instance of the specified service client type.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "No parameters needed.")]
        public static TClient Create<TClient, TService>()
            where TService : class
            where TClient : ClientBase<TService>, new()
        {
            var client = new TClient();

            // Specify credentials
            if (client.ClientCredentials == null)
            {
                return client;
            }

            client.ClientCredentials.Windows.ClientCredential.UserName = ServiceAgentConfigurationManager.WcfUserName;
            client.ClientCredentials.Windows.ClientCredential.Password = ServiceAgentConfigurationManager.WcfUserPassword;

            return client;
        }
    }
}