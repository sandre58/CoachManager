namespace My.CoachManager.Presentation.Core.ViewModels.Interfaces
{
    public interface ICollectionnable
    {
        bool CanAdd { get; set; }

        bool CanRemove { get; set; }
    }
}