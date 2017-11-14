using System.Threading.Tasks;
using System.Windows.Input;

namespace My.CoachManager.Presentation.Core.ViewModels.Screens.Interfaces
{
    public interface IDialogViewModel : IWorkspaceViewModel
    {
        /// <summary>
        /// Get or set dialog Result.
        /// </summary>
        bool DialogResult { get; }

        /// <summary>
        /// Close command.
        /// </summary>
        ICommand CloseCommand { get; }

        /// <summary>
        /// Wait for the view close.
        /// </summary>
        /// <returns></returns>
        Task<bool> WaitForClose();
    }
}