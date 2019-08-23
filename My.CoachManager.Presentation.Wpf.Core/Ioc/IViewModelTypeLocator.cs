using System;

namespace My.CoachManager.Presentation.Wpf.Core.Ioc
{
    /// <summary>
    /// Interface responsible for finding a dialog type matching a view model.
    /// </summary>
    public interface IViewModelTypeLocator
    {
        /// <summary>
        /// Locates a dialog type based on the specified view model.
        /// </summary>
        Type LocateView(Type viewModelType);

        /// <summary>
        /// Locates a dialog type based on the specified view model.
        /// </summary>
        Type LocateViewModel(Type viewType);

    }
}
