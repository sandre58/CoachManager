namespace My.CoachManager.Presentation.Prism.Core.ViewModels.Interfaces
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
        /// Gets or sets parameters.
        /// </summary>
        IScreenParameters Parameters { get; set; }
    }
}