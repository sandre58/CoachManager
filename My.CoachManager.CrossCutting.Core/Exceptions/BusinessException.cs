using System;
using System.Resources;
using System.Runtime.Serialization;

namespace My.CoachManager.CrossCutting.Core.Exceptions
{
    /// <summary>
    /// Manage Exception for Business Layers.
    /// </summary>
    [Serializable]
    public class BusinessException : MyException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessException"/> class.
        /// </summary>
        public BusinessException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public BusinessException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessException"/> class.
        /// Default Constructor.
        /// </summary>
        /// <param name="resourceType">
        /// The resource Type.
        /// </param>
        /// <param name="resourceKey">
        /// The resource Key.
        /// </param>
        public BusinessException(Type resourceType, string resourceKey)
            : base((new ResourceManager(resourceType)).GetString(resourceKey))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        public BusinessException(string message, Exception exception)
            : base(message, exception)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        protected BusinessException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
