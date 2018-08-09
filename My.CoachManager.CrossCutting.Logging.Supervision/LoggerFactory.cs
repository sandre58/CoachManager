namespace My.CoachManager.CrossCutting.Logging.Supervision
{
    public static class LoggerFactory
    {
        private static ILogger _logger;

        public static ILogger GetLogger()
        {
            return _logger ?? (_logger = Logger.CreateLogger());
        }
    }
}
