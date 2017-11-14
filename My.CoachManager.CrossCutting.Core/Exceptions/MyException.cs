namespace My.CoachManager.CrossCutting.Core.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Abstract class used for Exceptions.
    /// </summary>
    [Serializable]
    public abstract class MyException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MyException"/> class.
        /// </summary>
        protected MyException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MyException"/> class
        /// with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        protected MyException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MyException"/> class with serialized data.
        /// </summary>
        /// <param name="info">The <see cref="System.Runtime.Serialization.SerializationInfo"/> that holds the serialized
        /// object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="System.Runtime.Serialization.StreamingContext"/> that contains contextual
        /// information about the source or destination.</param>
        /// <exception cref="System.ArgumentNullException">The info parameter is null.</exception>
        /// <exception cref="System.Runtime.Serialization.SerializationException">The class name is null or <see cref="System.Exception.HResult"/> is zero (0).</exception>
        protected MyException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MyException"/> class with a specified error message and a reference to the inner exception that is the cause of
        /// this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference
        /// (Nothing in Visual Basic) if no inner exception is specified.</param>
        protected MyException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}