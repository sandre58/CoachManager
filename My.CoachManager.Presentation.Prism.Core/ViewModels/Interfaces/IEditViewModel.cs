using My.CoachManager.Presentation.Prism.Core.Models;

namespace My.CoachManager.Presentation.Prism.Core.ViewModels.Interfaces
{

    public interface IEditViewModel : IItemViewModel, IDialogViewModel
    {
        /// <summary>
        /// Load an item by id.
        /// </summary>
        void LoadId(int id);
    }

    public interface IEditViewModel<TModel> : IItemViewModel<TModel>, IEditViewModel
        where TModel : class, IEntityModel, IValidatable, IModifiable, new()
    {

    }
}