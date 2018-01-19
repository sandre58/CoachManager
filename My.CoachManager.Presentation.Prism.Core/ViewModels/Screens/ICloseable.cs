using System;

namespace My.CoachManager.Presentation.Prism.Core.ViewModels.Screens
{
    public interface ICloseable
    {
        /// <summary>
        /// Gets the close event.
        /// </summary>
        event EventHandler CloseRequest;
    }
}