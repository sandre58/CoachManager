using My.CoachManager.CrossCutting.Logging.Configuration;

namespace My.CoachManager.CrossCutting.Logging
{
    /// <summary>
    /// Standard Logging Context.
    /// </summary>
    public class LoggingContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoggingContext"/> class.
        /// </summary>
        public LoggingContext()
        {
            Action = string.Empty;
            MessageIdentity = string.Empty;
        }

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

        #region ----- Overrides Methods -----

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// Return Key1|Key2|MessageIdentity|.
        /// </returns>
        public override string ToString()
        {
            return string.Concat(Action, LoggingConfigurationManager.Separator, MessageIdentity, LoggingConfigurationManager.Separator);
        }

        #endregion ----- Overrides Methods -----
    }
}