namespace My.CoachManager.Presentation.Uwp.Core.ViewModels.Interfaces
{
    public interface IScreenViewModel
    {

        /// <summary>
        /// Get or set the state.
        /// </summary>
        /// <remarks></remarks>
        ScreenState State { get; set; }

        /// <summary>
        /// Get or set the mode.
        /// </summary>
        /// <remarks></remarks>
        ScreenMode Mode { get; set; }

        /// <summary>
        /// Initialize all events, data, and commands.
        /// </summary>
        void Initialize();
    }
}
