using Prism.Commands;

namespace My.CoachManager.Presentation.Prism.Core.Commands
{
    public static class GlobalCommands
    {
        /// <summary>
        /// Command to navigate in the application.
        /// </summary>
        public static readonly CompositeCommand NavigateCommand = new CompositeCommand();

        /// <summary>
        /// Command to show about view.
        /// </summary>
        public static readonly CompositeCommand ShowAboutViewCommand = new CompositeCommand();

        /// <summary>
        /// Command to toggle settings.
        /// </summary>
        public static readonly CompositeCommand ToggleSettingsCommand = new CompositeCommand();
    }
}