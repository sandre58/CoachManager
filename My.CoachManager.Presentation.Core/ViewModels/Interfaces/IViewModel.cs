using System.ComponentModel;

namespace My.CoachManager.Presentation.Core.ViewModels.Interfaces
{
    public interface IViewModel : INotifyPropertyChanged
    {
        bool IsModified { get; }
    }
}