namespace My.CoachManager.CrossCutting.Core.Security
{
    public class AnonymousIdentity : Identity
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initialise a new instance of <see cref="Identity"/>.
        /// </summary>
        public AnonymousIdentity()
            : base(string.Empty, string.Empty, string.Empty)
        {
        }

        #endregion Constructors and Destructors
    }
}