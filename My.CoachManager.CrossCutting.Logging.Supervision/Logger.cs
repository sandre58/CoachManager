using Microsoft.Extensions.Logging;
using System;
using System.Runtime.CompilerServices;

namespace My.CoachManager.CrossCutting.Logging.Supervision
{
    /// <summary>
    /// Class representing a Logger.
    /// </summary>
    public sealed class Logger : LoggerBase, Microsoft.Extensions.Logging.ILogger
    {
        private readonly NLog.Logger _logger;

        #region ----- Constructors -----

        /// <summary>
        /// Initializes a new instance of the <see cref="Logger"/> class.
        /// </summary>
        public Logger()
        {
            _logger = NLog.LogManager.GetCurrentClassLogger();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Logger"/> class.
        /// </summary>
        public Logger(string name)
        {
            _logger = NLog.LogManager.GetLogger(name);
        }

        public static void LoadConfiguration(string configFile)
        {
            NLog.LogManager.LoadConfiguration(configFile);
        }

        #endregion ----- Constructors -----

        /// <summary>
        /// Information the specified message.
        /// </summary>
        /// <param name="message">The resource.</param>
        /// <param name="memberName">The member name.</param>
        /// <param name="sourceFilePath">The source file path.</param>
        public override void Info(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "")
        {
            _logger.Info(message);
        }

        /// <summary>
        /// Traces the specified message.
        /// </summary>
        /// <param name="message">The resource.</param>
        /// <param name="memberName">The member name.</param>
        /// <param name="sourceFilePath">The source file path.</param>
        public override void Trace(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "")
        {
            _logger.Trace(message);
        }

        /// <summary>
        /// Debugs the specified message.
        /// </summary>
        /// <param name="message">The resource.</param>
        /// <param name="memberName">The member name.</param>
        /// <param name="sourceFilePath">The source file path.</param>
        public override void Debug(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "")
        {
            _logger.Debug(message);
        }

        /// <summary>
        /// Warnings the specified message.
        /// </summary>
        /// <param name="message">The resource.</param>
        /// <param name="memberName">The member name.</param>
        /// <param name="sourceFilePath">The source file path.</param>
        public override void Warning(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "")
        {
            _logger.Warn(message);
        }

        /// <summary>
        /// Log Application Error.
        /// </summary>
        /// <param name="ex">Non managed exception.</param>
        /// <param name="memberName">The member name.</param>
        /// <param name="sourceFilePath">The source file path.</param>
        public override void Error(Exception ex, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "")
        {
            var message = "";
            _logger.Error(ex, message);
        }

        /// <summary>
        /// Log Critical Error that can crash application.
        /// </summary>
        /// <param name="ex">Critical non managed exception.</param>
        /// <param name="memberName">The member name.</param>
        /// <param name="sourceFilePath">The source file path.</param>
        public override void Fatal(Exception ex, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "")
        {
            var message = "";
            _logger.Fatal(ex, message);
        }
        
        #region ILogger

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            var message = formatter(state, exception);

            switch (logLevel)
            {
                case LogLevel.Critical:
                    Fatal(exception);
                    break;

                case LogLevel.Trace:
                    Trace(message);
                    break;

                case LogLevel.Debug:
                    Debug(message);
                    break;

                case LogLevel.Information:
                    Info(message);
                    break;

                case LogLevel.Warning:
                    Warning(message);
                    break;

                case LogLevel.Error:
                    Error(exception);
                    break;

                case LogLevel.None:
                    Info(message);
                    break;
            }
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        #endregion ILogger
    }
}