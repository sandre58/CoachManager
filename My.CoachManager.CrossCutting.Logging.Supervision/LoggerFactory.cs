namespace My.CoachManager.CrossCutting.Logging.Supervision
{
    public static class LoggerFactory
    {
        public static ILogger CreateLogger(string categoryName)
        {
            return new Logger(categoryName);
        }

        public static ILogger CreateLogger()
        {
            return new Logger();
        }
    }
}
