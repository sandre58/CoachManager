using System;
using System.Runtime.Serialization;

using My.CoachManager.CrossCutting.Core.Resources;

namespace My.CoachManager.CrossCutting.Core.Exceptions
{
    /// <summary>
    /// Manage Exception for Business Layers.
    /// </summary>
    [Serializable]
    public class ConstraintCheckException : BusinessException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessException"/> class.
        /// </summary>
        public ConstraintCheckException()
            : base(ValidationMessageResources.ConstraintCheckMessage)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        public ConstraintCheckException(string message, Exception exception)
            : base(message, exception)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        protected ConstraintCheckException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
