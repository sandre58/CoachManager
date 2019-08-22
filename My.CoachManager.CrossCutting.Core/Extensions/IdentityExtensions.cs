using System.Diagnostics.Contracts;
using System.Security.Claims;
using System.Security.Principal;

namespace My.CoachManager.CrossCutting.Core.Extensions
{
    public static class IdentityExtensions
    {
        #region Public Methods and Operators

        [Pure]
        public static int GetRosterId(this IPrincipal principal)
        {
            return int.Parse(GetClaim(principal, "RosterId"));
        }

        public static string GetClaim(this IPrincipal principal, string type)
        {
            if (principal is ClaimsPrincipal p)
            {
                return p.FindFirst(type).Value;
            }
            return string.Empty;
        }

        #endregion Public Methods and Operators
    }
}
