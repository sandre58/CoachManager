using My.CoachManager.Presentation.Prism.Core.Models;

namespace My.CoachManager.Presentation.Prism.Core.ViewModels
{

    public interface IEditViewModel : IItemViewModel, IDialogViewModel
    {

    }

    public interface IEditViewModel<TModel> : IItemViewModel<TModel>, IEditViewModel
        where TModel : class, IEntityModel, IValidatable, IModifiable, new()
    {

    }
}