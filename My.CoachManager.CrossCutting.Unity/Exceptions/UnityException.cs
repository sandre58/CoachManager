using System;
using System.Runtime.Serialization;

namespace My.CoachManager.CrossCutting.Unity.Exceptions
{
    /// <summary>
    /// Unity Exception.
    /// </summary>
    [Serializable]
    public class UnityException : Exception
    {
        #region ----- Constructors -----

        /// <summary>
        /// Initializes a new instance of the <see cref="UnityException"/> class.
        /// </summary>
        public UnityException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnityException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public UnityException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnityException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public UnityException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnityException"/> class.
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