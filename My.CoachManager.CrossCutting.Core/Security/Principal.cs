using System.Security.Principal;

namespace My.CoachManager.CrossCutting.Core.Security
{
    public class Principal : IPrincipal
    {
        #region Fields

        private Identity _identity;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Get or set the identity.
        /// </summary>
        public Identity Identity
        {
            get { return _identity ?? new AnonymousIdentity(); }
            set { _identity = value; }
        }

        #endregion Properties

        #region IPrincipal Members

        IIdentity IPrincipal.Identity
        {
            get { return Identity; }
        }

        /// <summary>
        /// Has authorisation ?
        /// </summary>
        /// <param name="permission">The permission.</param>
        /// <returns></returns>
        public bool IsInRole(string permission)
        {
            return Identity.IsAuthenticated;
        }

        #endregion IPrincipal Members
    }
}