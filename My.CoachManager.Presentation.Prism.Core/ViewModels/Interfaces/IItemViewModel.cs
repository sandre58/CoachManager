using My.CoachManager.Presentation.Prism.Core.Models;

namespace My.CoachManager.Presentation.Prism.Core.ViewModels.Interfaces
{
    public interface IItemViewModel
    {
        /// <summary>
        /// Gets or sets item.
        /// </summary>
        IEntityModel Item { get; set; }
    }

    public interface IItemViewModel<TModel> : IItemViewModel
        where TModel : class, IEntityModel, IValidatable, IModifiable, new()
    {
        /// <summary>
        /// Gets or sets item.
        /// </summary>
        new TModel Item { get; set; }
    }
}