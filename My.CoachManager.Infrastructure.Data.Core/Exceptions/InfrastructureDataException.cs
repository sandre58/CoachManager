using System;

namespace My.CoachManager.Infrastructure.Data.Core.Exceptions
{
    /// <summary>
    /// Exception throw in Infrastructure Data Layer
    /// </summary>
    public class InfrastructureDataException : Exception
    {
        #region ----- Constructors -----

        /// <summary>
        /// Initializes a new instance of the <see cref="InfrastructureDataException"/> class.
        /// </summary>
        /// <param name="message">custom Message.</param>
        public InfrastructureDataException(string message)
            : base(message)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="InfrastructureDataException"/> class.
        /// </summary>
        /// <param name="message">custom Message.</param>
        /// <param name="ex">original Exception.</param>
        public InfrastructureDataException(string message, Exception ex)
            : base(message, ex)
        { }

        #endregion ----- Constructors -----
    }
}
