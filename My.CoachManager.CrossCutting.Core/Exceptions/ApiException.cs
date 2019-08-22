using System;
using System.Resources;
using System.Runtime.Serialization;

namespace My.CoachManager.CrossCutting.Core.Exceptions
{
    /// <summary>
    /// Manage Exception for Business Layers.
    /// </summary>
    [Serializable]
    public class ApiException : MyException
    {
        /// <summary>
        /// Returns the inner exception contained in this exception
        /// </summary>
        public Exception CastInnerException { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiException"/> class.
        /// </summary>
        public ApiException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public ApiException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiException"/> class.
        /// Default Constructor.
        /// </summary>
        /// <param name="resourceType">
        /// The resource Type.
        /// </param>
        /// <param name="resourceKey">
        /// The resource Key.
        /// </param>
        public ApiException(Type resourceType, string resourceKey)
            : base((new ResourceManager(resourceType)).GetString(resourceKey))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        public ApiException(string message, Exception exception)
            : base(message, exception)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        protected ApiException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            var typeStr = info.GetString("InnerExceptionType");
            var type = Type.GetType(typeStr);
            CastInnerException = (Exception)info.GetValue("CastInnerException", type ?? throw new InvalidOperationException());
        }

        /// <summary>When overridden in a derived class, sets the <see cref="T:System.Runtime.Serialization.SerializationInfo"></see> with information about the exception.</summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"></see> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"></see> that contains contextual information about the source or destination.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="info">info</paramref> parameter is a null reference (Nothing in Visual Basic).</exception>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            // Use the AddValue method to specify serialized values.
            if (InnerException != null) info.AddValue("InnerExceptionType", InnerException.GetType().FullName);
            if (InnerException != null) info.AddValue("CastInnerException", InnerException, InnerException.GetType());
        }
    }
}