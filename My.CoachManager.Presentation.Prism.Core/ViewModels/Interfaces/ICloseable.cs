using System;

namespace My.CoachManager.Presentation.Prism.Core.ViewModels.Interfaces
{
    public interface ICloseable
    {
        /// <summary>
        /// Gets the close event.
        /// </summary>
        event EventHandler CloseRequest;
    }
}