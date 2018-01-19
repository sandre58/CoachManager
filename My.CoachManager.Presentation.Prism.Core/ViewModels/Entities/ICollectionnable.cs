namespace My.CoachManager.Presentation.Prism.Core.ViewModels.Entities
{
    public interface ICollectionnable
    {
        bool CanAdd { get; set; }

        bool CanRemove { get; set; }
    }
}