using System;
using My.CoachManager.CrossCutting.Logging.Enums;

namespace My.CoachManager.CrossCutting.Logging.Attributes
{
    /// <summary>
    /// Attribute Used for Logger event.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Method)]
    public sealed class LoggingContextAttribute : Attribute
    {
        #region ----- Constructors -----

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggingContextAttribute"/> class.
        /// </summary>
        public LoggingContextAttribute()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggingContextAttribute"/> class.
        /// </summary>
        /// <param name="loggingAction">The logging action.</param>
        public LoggingContextAttribute(LoggingAction loggingAction)
        {
            Action = loggingAction.ToString();
        }

        #endregion ----- Constructors -----

        #region ----- Properties -----

        /// <summary>
        /// Gets the logging action.
        /// </summary>
        /// <value>
        /// The logging action.
        /// </value>
        public LoggingAction LoggingAction
        {
            get
            {
                LoggingAction result;

                if (Enum.TryParse(Action, out result))
                {
                    return result;
                }
                else
                {
                    return LoggingAction.None;
                }
            }
        }

        /// <summary>
        /// Gets or sets the domain.
        /// </summary>
        /// <value>
        /// The domain.
        /// </value>
        public string Domain { get; set; }

        /// <summary>
        /// Gets or sets the action.
        /// </summary>
        /// <value>
        /// The action.
        /// </value>
        public string Action { get; set; }

        /// <summary>
        /// Gets or sets the message identity.
        /// </summary>
        /// <value>
        /// The message identity.
        /// </value>
        public string MessageIdentity { get; set; }

        #endregion ----- Properties -----
    }
}