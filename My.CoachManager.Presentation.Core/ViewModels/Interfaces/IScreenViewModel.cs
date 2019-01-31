namespace My.CoachManager.Presentation.Core.ViewModels.Interfaces
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
    }
}