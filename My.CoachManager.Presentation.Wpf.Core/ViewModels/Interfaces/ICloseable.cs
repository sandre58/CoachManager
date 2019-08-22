using System;

namespace My.CoachManager.Presentation.Wpf.Core.ViewModels.Interfaces
{
    public interface ICloseable
    {
        /// <summary>
        /// Gets the close event.
        /// </summary>
        event EventHandler CloseRequest;
    }
}