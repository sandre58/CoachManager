using Prism.Commands;

namespace My.CoachManager.Presentation.Prism.Core.Global
{
    public static class GlobalCommands
    {
        /// <summary>
        /// Command to navigate in the application.
        /// </summary>
        public static CompositeCommand NavigateCommand = new CompositeCommand();
    }
}