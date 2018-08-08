using System;
using Microsoft.Extensions.Logging;

namespace My.CoachManager.Infrastructure.Data
{
    internal class DbLoggerFactory : LoggerFactory
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public ILogger CreateLogger(string categoryName)
        {
            throw new NotImplementedException();
        }

        public void AddProvider(ILoggerProvider provider)
        {
            throw new NotImplementedException();
        }
    }
}
