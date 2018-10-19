namespace My.CoachManager.Presentation.Prism.Core.ViewModels.Interfaces
{
    public interface IRefreshable
    {
        /// <summary>
        /// Gets if we can refresh after initialisation.
        /// </summary>
        bool RefreshOnInit { get; }

        /// <summary>
        /// Can Refresh.
        /// </summary>
        /// <returns></returns>
        bool CanRefresh();

        /// <summary>
        /// Refreshes data.
        /// </summary>
        void Refresh();
    }
}