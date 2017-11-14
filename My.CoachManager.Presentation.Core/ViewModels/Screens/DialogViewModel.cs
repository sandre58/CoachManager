using System.Threading.Tasks;
using System.Windows.Input;
using My.CoachManager.Presentation.Core.Commands;
using My.CoachManager.Presentation.Core.ViewModels.Screens.Interfaces;

namespace My.CoachManager.Presentation.Core.ViewModels.Screens
{
    /// <summary>
    /// A base class for all viewmodels that cater to workspace views.
    /// </summary>
    public abstract class DialogViewModel : WorkspaceViewModel, IDialogViewModel
    {
        #region Property

        /// <summary>
        /// Get or set dialog Result.
        /// </summary>
        public bool DialogResult { get; private set; }

        /// <summary>
        /// Get or set close command.
        /// </summary>
        public ICommand CloseCommand { get; private set; }

        #endregion Property

        protected override void Initialize()
        {
            base.Initialize();

            CloseCommand = new DelegateCommand(Close, CanClose);
        }

        public virtual Task<bool> WaitForClose()
        {
            var tcs = new TaskCompletionSource<bool>();

            CloseView += (sender, e) =>
            {
                DialogResult = e.DialogResult;
                tcs.TrySetResult(e.DialogResult);
            };

            return tcs.Task;
        }

        protected virtual void Close()
        {
            TryClose(false);
        }

        protected virtual bool CanClose()
        {
            return true;
        }
    }
}