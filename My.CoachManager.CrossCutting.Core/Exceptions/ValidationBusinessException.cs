using System;
using System.Collections;
using System.Resources;
using System.Runtime.Serialization;
using System.Text;

namespace My.CoachManager.CrossCutting.Core.Exceptions
{
    /// <summary>
    /// Manage Exception for Business Layers.
    /// </summary>
    [Serializable]
    public class ValidationBusinessException : BusinessException
    {
        #region Fields

        public IEnumerable Errors { get; private set; }

        #endregion Fields

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="errors"></param>
        public ValidationBusinessException(string message, IEnumerable errors)
            : base(message)
        {
            Errors = errors;
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
        public ValidationBusinessException(Type resourceType, string resourceKey)
            : base(new ResourceManager(resourceType).GetString(resourceKey))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        public ValidationBusinessException(string message, Exception exception)
            : base(message, exception)
        {
            Errors = (exception as ValidationBusinessException)?.Errors;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        protected ValidationBusinessException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Errors = (IEnumerable)info.GetValue(nameof(Errors), typeof(IEnumerable));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue(nameof(Errors), Errors);
        }

        public override string ToString()
        {
            var result = new StringBuilder();

            result.AppendLine(Message);

            foreach (var error in Errors)
            {
                result.AppendLine(error.ToString());
            }

            return result.ToString();
        }
    }
}