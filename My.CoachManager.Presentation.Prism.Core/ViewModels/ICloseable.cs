using System;

namespace My.CoachManager.Presentation.Prism.Core.ViewModels
{
    public interface ICloseable
    {
        event EventHandler CloseRequest;
    }
}