using System;

namespace My.CoachManager.Presentation.Wpf.Core.Ioc
{
    /// <summary>
    /// Interface responsible for finding a dialog type matching a view model.
    /// </summary>
    public interface IViewModelProvider
    {
        /// <summary>
        /// Locates a dialog type based on the specified view model.
        /// </summary>
        TView GetView<TView>();

        /// <summary>
        /// Locates a dialog type based on the specified view model.
        /// </summary>
        object GetView(Type viewType);

        /// <summary>
        /// Locates a dialog type based on the specified view model.
        /// </summary>
        TViewModel GetViewModel<TViewModel>();

        /// <summary>
        /// Locates a dialog type based on the specified view model.
        /// </summary>
        object GetViewModel(Type viewModelType);

    }
}
