namespace My.CoachManager.CrossCutting.Logging
{
    /// <summary>
    /// Logger Caller.
    /// </summary>
    public class LoggingCaller
    {
        /// <summary>
        /// Gets or sets the source file path.
        /// </summary>
        public string SourceFilePath { get; set; }

        /// <summary>
        /// Gets or sets the member path.
        /// </summary>
        public string MemberPath { get; set; }

        /// <summary>
        /// Gets or sets the line number.
        /// </summary>
        public string LineNumber { get; set; }
    }
}