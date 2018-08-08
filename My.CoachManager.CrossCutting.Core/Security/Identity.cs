using System.Security.Principal;

namespace My.CoachManager.CrossCutting.Core.Security
{
    public class Identity : IIdentity
    {
        #region Constants

        public const string AuthenticationTypeName = "COACH";

        #endregion Constants

        #region Constructors and Destructors

        /// <summary>
        /// Initialise a new instance of <see cref="Identity"/>.
        /// </summary>
        /// <param name="login"></param>
        /// <param name="name"></param>
        /// <param name="email"></param>
        public Identity(string login, string name, string email)
        {
            Login = login;
            Name = name;
            Email = email;
        }

        #endregion Constructors and Destructors

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Login { get; private set; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the email.
        /// </summary>
        public string Email { get; private set; }

        #region IIdentity Members

        /// <summary>
        /// Get the authentification type.
        /// </summary>
        public string AuthenticationType { get { return AuthenticationTypeName; } }

        /// <summary>
        /// Gets a value indicates if he is identified.
        /// </summary>
        public bool IsAuthenticated { get { return !string.IsNullOrEmpty(Name); } }

        #endregion IIdentity Members
    }
}