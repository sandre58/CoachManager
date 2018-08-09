using System;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;
using NLog;
using Prism.Logging;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace My.CoachManager.CrossCutting.Logging.Supervision
{
    /// <summary>
    /// Class representing a Logger.
    /// </summary>
    public sealed class Logger : LoggerBase, ILoggerFacade,Microsoft.Extensions.Logging.ILogger
    {

        /// <summary>
        /// The instance.
        /// </summary>
        private static Logger _instance;

        private static NLog.Logger _logger;

        #region ----- Constructors -----

        /// <summary>
        /// Initializes a new instance of the <see cref="Logger"/> class.
        /// </summary>
        public Logger()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

        /// <summary>
        /// Create the logger.
        /// </summary>
        /// <returns>The <see cref="Logger"/>.</returns>
        public static Logger CreateLogger()
        {
            return _instance ?? (_instance = new Logger());
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

        #region ILoggerFacade

        /// <summary>
        /// The logging method.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="category">The category.</param>
        /// <param name="priority">The priority.</param>
        [Obsolete]
        public void Log(string message, Category category, Priority priority)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return;
            }

            switch (category)
            {
                case Category.Debug:
                    Debug(message);
                    break;

                case Category.Info:
                    Info(message);
                    break;

                case Category.Warn:
                    Warning(message);
                    break;

                case Category.Exception:
                    Error(new Exception(message));
                    break;
            }
        }

        #endregion ILoggerFacade

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

        #endregion
    }
}