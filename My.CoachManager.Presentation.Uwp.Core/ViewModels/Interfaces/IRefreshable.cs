namespace My.CoachManager.Presentation.Uwp.Core.ViewModels.Interfaces
{
    public interface IRefreshable
    {

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
