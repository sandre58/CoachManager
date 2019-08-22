using System;
using Microsoft.Extensions.Logging;

namespace My.CoachManager.CrossCutting.Logging.Supervision
{
    public class LoggerProvider : ILoggerProvider
    {
        public Microsoft.Extensions.Logging.ILogger CreateLogger(string categoryName)
        {
            return new Logger(categoryName);
        }

        public void Dispose()
        {
        }
    }
}
