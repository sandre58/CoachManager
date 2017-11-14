using System.Security.Principal;

namespace My.CoachManager.CrossCutting.Core.Security
{
    public class SubstitutePrincipal : Principal
    {
        #region Fields

        private IIdentity _realIdentity;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Get or set the identity.
        /// </summary>
        public IIdentity RealIdentity
        {
            get { return _realIdentity ?? new AnonymousIdentity(); }
            set { _realIdentity = value; }
        }

        #endregion Properties
    }
}