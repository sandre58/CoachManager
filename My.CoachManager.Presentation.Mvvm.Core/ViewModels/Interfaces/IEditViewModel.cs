namespace My.CoachManager.Presentation.Mvvm.Core.ViewModels.Interfaces
{

    public interface IEditViewModel : IItemViewModel, IWorkspaceDialogViewModel
    {

    }

    public interface IEditViewModel<TModel> : IItemViewModel<TModel>, IEditViewModel
        where TModel : class, IEntityModel, IValidatable, IModifiable, new()
    {

    }
}