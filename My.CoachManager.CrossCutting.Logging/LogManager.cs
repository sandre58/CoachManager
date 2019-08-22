using System;
using System.Runtime.CompilerServices;
using CommonServiceLocator;

namespace My.CoachManager.CrossCutting.Logging
{
    /// <summary>
    /// Class representing a Logger.
    /// </summary>
    public static class LogManager
    {
        public static ILogger Logger => ServiceLocator.Current.GetInstance<ILogger>();

        /// <summary>
        /// Information the specified message.
        /// </summary>
        /// <param name="message">The resource.</param>
        /// <param name="memberName">The member name.</param>
        /// <param name="sourceFilePath">The source file path.</param>
        public static void Info(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "")
        {
            Logger?.Info(message);
        }

        /// <summary>
        /// Traces the specified message.
        /// </summary>
        /// <param name="message">The resource.</param>
        /// <param name="memberName">The member name.</param>
        /// <param name="sourceFilePath">The source file path.</param>
        public static void Trace(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "")
        {
            Logger?.Trace(message);
        }

        /// <summary>
        /// Debugs the specified message.
        /// </summary>
        /// <param name="message">The resource.</param>
        /// <param name="memberName">The member name.</param>
        /// <param name="sourceFilePath">The source file path.</param>
        public static void Debug(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "")
        {
            Logger?.Debug(message);
        }

        /// <summary>
        /// Warnings the specified message.
        /// </summary>
        /// <param name="message">The resource.</param>
        /// <param name="memberName">The member name.</param>
        /// <param name="sourceFilePath">The source file path.</param>
        public static void Warning(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "")
        {
            Logger?.Warning(message);
        }

        /// <summary>
        /// Log Application Error.
        /// </summary>
        /// <param name="ex">Non managed exception.</param>
        /// <param name="memberName">The member name.</param>
        /// <param name="sourceFilePath">The source file path.</param>
        public static void Error(Exception ex, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "")
        {
            Logger?.Error(ex, string.Empty);
        }

        /// <summary>
        /// Log Critical Error that can crash application.
        /// </summary>
        /// <param name="ex">Critical non managed exception.</param>
        /// <param name="memberName">The member name.</param>
        /// <param name="sourceFilePath">The source file path.</param>
        public static void Fatal(Exception ex, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "")
        {
            Logger?.Fatal(ex, string.Empty);
        }

        public static IDisposable TraceGroup(string title)
        {
            return new TraceGroup(title);
        }
    }
}