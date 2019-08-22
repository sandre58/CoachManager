using System;
using System.Runtime.Serialization;

namespace My.CoachManager.Services.Unity.Exceptions
{
    /// <inheritdoc />
    /// <summary>
    /// Unity Exception.
    /// </summary>
    [Serializable]
    public class UnityException : Exception
    {
        #region ----- Constructors -----

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:My.CoachManager.Services.Unity.Exceptions.UnityException" /> class.
        /// </summary>
        public UnityException()
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:My.CoachManager.Services.Unity.Exceptions.UnityException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public UnityException(string message)
            : base(message)
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:My.CoachManager.Services.Unity.Exceptions.UnityException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public UnityException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:My.CoachManager.Services.Unity.Exceptions.UnityException" /> class.
        /// </summary>
        /// <param name="info">The info.</param>
        /// <param name="context">The context.</param>
        protected UnityException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion ----- Constructors -----
    }
}
