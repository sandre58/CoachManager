using System;
using System.Runtime.CompilerServices;
using NLog;

namespace My.CoachManager.CrossCutting.Logging.Supervision
{
    /// <summary>
    /// Class representing a Logger.
    /// </summary>
    public sealed class Logger : LoggerBase
    {
        private static NLog.Logger _logger;

        #region ----- Constructors -----

        /// <summary>
        /// Initializes a new instance of the <see cref="Logger"/> class.
        /// </summary>
        public Logger()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

        #endregion ----- Constructors -----

        /// <summary>
        /// Information the specified message.
        /// </summary>
        /// <param name="message">The resource.</param>
        /// <param name="loggingContext">Logging Context information (used ToString method).</param>
        /// <param name="memberName">The member name.</param>
        /// <param name="sourceFilePath">The source file path.</param>
        public override void Info(string message, LoggingContext loggingContext = null, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "")
        {
            _logger.Info(message, GetLoggingContext(loggingContext));
        }

        /// <summary>
        /// Traces the specified message.
        /// </summary>
        /// <param name="message">The resource.</param>
        /// <param name="loggingContext">Logging Context information (used ToString method).</param>
        /// <param name="memberName">The member name.</param>
        /// <param name="sourceFilePath">The source file path.</param>
        public override void Trace(string message, LoggingContext loggingContext = null, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "")
        {
            _logger.Trace(message, GetLoggingContext(loggingContext));
        }

        /// <summary>
        /// Debugs the specified message.
        /// </summary>
        /// <param name="message">The resource.</param>
        /// <param name="loggingContext">Logging Context information (used ToString method).</param>
        /// <param name="memberName">The member name.</param>
        /// <param name="sourceFilePath">The source file path.</param>
        public override void Debug(string message, LoggingContext loggingContext = null, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "")
        {
            _logger.Debug(message, GetLoggingContext(loggingContext));
        }

        /// <summary>
        /// Warnings the specified message.
        /// </summary>
        /// <param name="message">The resource.</param>
        /// <param name="loggingContext">Logging Context information (used ToString method).</param>
        /// <param name="memberName">The member name.</param>
        /// <param name="sourceFilePath">The source file path.</param>
        public override void Warning(string message, LoggingContext loggingContext = null, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "")
        {
            _logger.Warn(message, GetLoggingContext(loggingContext));
        }

        /// <summary>
        /// Log Application Error.
        /// </summary>
        /// <param name="ex">Non managed exception.</param>
        /// <param name="loggingContext">The logging context.</param>
        /// <param name="memberName">The member name.</param>
        /// <param name="sourceFilePath">The source file path.</param>
        public override void Error(Exception ex, LoggingContext loggingContext = null, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "")
        {
            var message = "";
            _logger.Error(ex, message, GetLoggingContext(loggingContext));
        }

        /// <summary>
        /// Log Critical Error that can crash application.
        /// </summary>
        /// <param name="ex">Critical non managed exception.</param>
        /// <param name="loggingContext">The logging context.</param>
        /// <param name="memberName">The member name.</param>
        /// <param name="sourceFilePath">The source file path.</param>
        public override void Fatal(Exception ex, LoggingContext loggingContext = null, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "")
        {
            var message = "";
            _logger.Fatal(ex, message, GetLoggingContext(loggingContext));
        }
    }
}