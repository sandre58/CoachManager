namespace My.CoachManager.Presentation.Prism.Core.ViewModels
{
    public interface ICollectionnable
    {
        bool CanAdd { get; set; }

        bool CanRemove { get; set; }
    }
}