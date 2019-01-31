using My.CoachManager.Presentation.Core.Models;

namespace My.CoachManager.Presentation.Core.ViewModels.Interfaces
{

    public interface IEditViewModel : IItemViewModel, IDialogViewModel
    {

    }

    public interface IEditViewModel<TModel> : IItemViewModel<TModel>, IEditViewModel
        where TModel : class, IEntityModel, IValidatable, IModifiable, new()
    {

    }
}