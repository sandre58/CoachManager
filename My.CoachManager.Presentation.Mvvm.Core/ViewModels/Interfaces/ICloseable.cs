using System;

namespace My.CoachManager.Presentation.Mvvm.Core.ViewModels.Interfaces
{
    public interface ICloseable
    {
        /// <summary>
        /// Gets the close event.
        /// </summary>
        event EventHandler CloseRequest;
    }
}