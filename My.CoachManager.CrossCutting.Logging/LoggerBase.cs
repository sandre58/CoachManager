using System;
using System.Runtime.CompilerServices;

namespace My.CoachManager.CrossCutting.Logging
{
    /// <summary>
    /// Logger Base.
    /// </summary>
    public abstract class LoggerBase : ILogger
    {
        /// <summary>
        /// Log Trace.
        /// </summary>
        /// <param name="message">The resource.</param>
        /// <param name="loggingContext">Logging Context information (used ToString method).</param>
        /// <param name="memberName">The member Name.</param>
        /// <param name="sourceFilePath">The source File Path.</param>
        public abstract void Trace(string message, LoggingContext loggingContext = null,
            [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "");

        /// <summary>
        /// Log Debug.
        /// </summary>
        /// <param name="message">The resource.</param>
        /// <param name="loggingContext">Logging Context information (used ToString method).</param>
        /// <param name="memberName">The member Name.</param>
        /// <param name="sourceFilePath">The source File Path.</param>
        public abstract void Debug(string message, LoggingContext loggingContext = null,
            [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "");

        /// <summary>
        /// Log information.
        /// </summary>
        /// <param name="message">The resource.</param>
        /// <param name="loggingContext">Logging Context information (used ToString method).</param>
        /// <param name="memberName">The member Name.</param>
        /// <param name="sourceFilePath">The source File Path.</param>
        public abstract void Info(string message, LoggingContext loggingContext = null,
            [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "");

        /// <summary>
        /// Log Warning.
        /// </summary>
        /// <param name="message">The resource.</param>
        /// <param name="loggingContext">Logging Context information (used ToString method).</param>
        /// <param name="memberName">The member Name.</param>
        /// <param name="sourceFilePath">The source File Path.</param>
        public abstract void Warning(string message,
            LoggingContext loggingContext = null, [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "");

        /// <summary>
        /// Log Application Error.
        /// </summary>
        /// <param name="ex">Non managed exception.</param>
        /// <param name="loggingContext">Logging Context information (used ToString method).</param>
        /// <param name="memberName">The member Name.</param>
        /// <param name="sourceFilePath">The source File Path.</param>
        public abstract void Error(Exception ex,
            LoggingContext loggingContext = null, [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "");

        /// <summary>
        /// Log Critical Error that can crash application.
        /// </summary>
        /// <param name="ex">Critical non managed exception.</param>
        /// <param name="loggingContext">Logging Context information (used ToString method).</param>
        /// <param name="memberName">The member Name.</param>
        /// <param name="sourceFilePath">The source File Path.</param>
        public abstract void Fatal(Exception ex,
            LoggingContext loggingContext = null, [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "");

        /// <summary>
        /// Get Category Keys on Attributes.
        /// </summary>
        /// <param name="loggingContext">The logging context.</param>
        /// <returns>
        /// Logging Context.
        /// </returns>
        protected static LoggingContext GetLoggingContext(LoggingContext loggingContext)
        {
            // Used ILoggingContext interface directly
            if (loggingContext != null)
            {
                return loggingContext;
            }

            return new LoggingContext { Action = "Action", MessageIdentity = "Identity" };
        }
    }
}