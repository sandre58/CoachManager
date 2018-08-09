using Microsoft.Extensions.Logging;

namespace My.CoachManager.Infrastructure.Data
{
    public class LoggerProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName)
        {
            return CrossCutting.Logging.Supervision.LoggerFactory.GetLogger() as ILogger;
        }

        public void Dispose()
        { }
    }
}
