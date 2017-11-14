using System;
using Caliburn.Micro;
using My.CoachManager.Presentation.Core.Arguments;
using My.CoachManager.Presentation.Core.ViewModels.Interfaces;

namespace My.CoachManager.Presentation.Core.ViewModels.Screens.Interfaces
{
    public interface IScreenViewModel : IViewModel, IScreen
    {
        event EventHandler<CloseViewEventArgs> CloseView;
    }
}