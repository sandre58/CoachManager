using System;
using System.Runtime.CompilerServices;

namespace My.CoachManager.CrossCutting.Logging
{
    /// <summary>
    /// Interface for logging Library.
    /// </summary>
    public interface ILogger

    {
        /// <summary>
        /// Log Trace.
        /// </summary>
        /// <param name="message">The resource.</param>
        /// <param name="loggingContext">Logging Context information (used ToString method).</param>
        /// <param name="memberName">The member Name.</param>
        /// <param name="sourceFilePath">The source File Path.</param>
        void Trace(string message, LoggingContext loggingContext = null, [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "");

        /// <summary>
        /// Log Debug.
        /// </summary>
        /// <param name="message">The resource.</param>
        /// <param name="loggingContext">Logging Context information (used ToString method).</param>
        /// <param name="memberName">The member Name.</param>
        /// <param name="sourceFilePath">The source File Path.</param>
        void Debug(string message, LoggingContext loggingContext = null, [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "");

        /// <summary>
        /// Log information.
        /// </summary>
        /// <param name="message">The resource.</param>
        /// <param name="loggingContext">Logging Context information (used ToString method).</param>
        /// <param name="memberName">The member Name.</param>
        /// <param name="sourceFilePath">The source File Path.</param>
        void Info(string message, LoggingContext loggingContext = null, [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "");

        /// <summary>
        /// Log Warning.
        /// </summary>
        /// <param name="message">The resource.</param>
        /// <param name="loggingContext">Logging Context information (used ToString method).</param>
        /// <param name="memberName">The member Name.</param>
        /// <param name="sourceFilePath">The source File Path.</param>
        void Warning(string message, LoggingContext loggingContext = null, [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "");

        /// <summary>
        /// Log Application Error.
        /// </summary>
        /// <param name="ex">Non managed exception.</param>
        /// <param name="loggingContext">Logging Context information (used ToString method).</param>
        /// <param name="memberName">The member Name.</param>
        /// <param name="sourceFilePath">The source File Path.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords",
            MessageId = "Error", Justification = "Error Method for logger")]
        void Error(Exception ex, LoggingContext loggingContext = null, [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "");

        /// <summary>
        /// Log Critical Error that can crash application.
        /// </summary>
        /// <param name="ex">Critical non managed exception.</param>
        /// <param name="loggingContext">Logging Context information (used ToString method).</param>
        /// <param name="memberName">The member Name.</param>
        /// <param name="sourceFilePath">The source File Path.</param>
        void Fatal(Exception ex, LoggingContext loggingContext = null, [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "");
    }
}