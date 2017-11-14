using Caliburn.Micro;
using My.CoachManager.Presentation.Core.ViewModels.Screens.Enums;

namespace My.CoachManager.Presentation.Core.ViewModels.Screens.Interfaces
{
    /// <summary>
    /// A view model representing a workspace.
    /// </summary>
    public interface IWorkspaceViewModel : IScreenViewModel
    {
        /// <summary>
        /// Etat du ViewModel
        /// </summary>
        /// <remarks>La modification de l'état du ViewModel peut entrainer l'affichage des écrans de chargement / sauvegarde</remarks>
        ViewModelState State { get; set; }
    }
}