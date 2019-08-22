using System;

namespace My.CoachManager.Presentation.Mvvm.Core.ViewModels.Interfaces
{
    public interface IRefreshable
    {
        /// <summary>
        /// Get or set the state.
        /// </summary>
        /// <remarks></remarks>
        ScreenState State { get; }

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

            /// <summary>
            /// Calls when refresh.
            /// </summary>
        event EventHandler Refreshed;
    }
}