using Prism.Commands;

namespace My.CoachManager.Presentation.Core.Commands
{
    public static class PlayerCommands
    {
        /// <summary>
        /// Command to navigate in the application.
        /// </summary>
        public static readonly CompositeCommand NavigateToPlayerCommand = new CompositeCommand();
    }
}