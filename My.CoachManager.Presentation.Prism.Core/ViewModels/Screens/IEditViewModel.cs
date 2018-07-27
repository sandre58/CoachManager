using My.CoachManager.Presentation.Prism.Core.Models;

namespace My.CoachManager.Presentation.Prism.Core.ViewModels.Screens
{
    public interface IEditViewModel<TModel> : IItemViewModel
        where TModel : class, IEntityModel, IValidatable, IModifiable, new()
    {

        /// <summary>
        /// Get or set Item.
        /// </summary>
        TModel Item { get; set; }
    }
}