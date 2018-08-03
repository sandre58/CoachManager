namespace My.CoachManager.Presentation.Prism.Core.ViewModels
{
    public interface IItemViewModel
    {
        /// <summary>
        /// Load an item by id.
        /// </summary>
        void LoadItemById(int id);
    }
}