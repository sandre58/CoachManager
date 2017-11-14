using System.Diagnostics.Contracts;
using System.Security.Principal;
using My.CoachManager.CrossCutting.Core.Security;

namespace My.CoachManager.CrossCutting.Core.Extensions
{
    public static class IdentityExtensions
    {
        #region Public Methods and Operators

        [Pure]
        public static string GetLogin(this IIdentity identity)
        {
            var value = identity.Name;
            if (identity.GetType() == typeof(Identity))
            {
                var ident = (Identity)identity;
                value = ident.Login;
            }
            return value.Contains("\\") ? value.Split('\\')[1] : value;
        }

        #endregion Public Methods and Operators
    }
}