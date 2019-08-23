using My.CoachManager.Presentation.Wpf.Core.Manager;
using My.CoachManager.Presentation.Wpf.Core.ViewModels.Base;
using My.CoachManager.Presentation.Wpf.Core.ViewModels.Interfaces;

namespace My.CoachManager.Presentation.Wpf.ViewModels.Shell
{
    /// <summary>
    /// Shell view model.
    /// </summary>
    public class MainViewModel : ScreenViewModel
    {

        //        #region Members

        /// <summary>
        /// Gets or sets the menu is Expanded.
        /// </summary>
        public bool IsMenuExpanded { get; set; }

        /// <summary>
        /// Gets or sets the active workspace.
        /// </summary>
        public INavigableWorkspaceViewModel CurrentWorkspace => NavigationManager.CurrentWorkspace;

        //        /// <summary>
        //        /// Gets or sets the active workspace dialog.
        //        /// </summary>
        //        public IWorkspaceDialogViewModel ActiveWorkspaceDialog { get; private set; }

        //        /// <summary>
        //        /// Gets or Set the dialog interaction.
        //        /// </summary>
        //        public InteractionRequest<IWorkspaceDialog> WorkspaceDialogInteractionRequest { get; set; }

        //        #endregion Members

        #region Initialisation

        /// <inheritdoc />
        /// <summary>
        /// Launch on constructor for initialize all command property.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();

            IsMenuExpanded = true;

            //Messenger.GetEvent<ShowWorkspaceDialogRequestEvent>().Subscribe(OnShowWorkspaceDialogRequested, ThreadOption.UIThread, true);

            //WorkspaceDialogInteractionRequest = new InteractionRequest<IWorkspaceDialog>();
            //WorkspaceDialogInteractionRequest.Raised += OnWorkspaceDialogInteractionRequestRaised;

        }

        #endregion Initialisation

        //        #region Methods

        //        private void OnWorkspaceDialogInteractionRequestRaised(object sender, InteractionRequestedEventArgs e)
        //        {
        //            ActiveWorkspaceDialog = (e.Context.Content as FrameworkElement)?.DataContext as IWorkspaceDialogViewModel;
        //            if (ActiveWorkspaceDialog != null)
        //                ActiveWorkspaceDialog.CloseRequest += (o, args) =>
        //                {
        //                    ActiveWorkspaceDialog = null;
        //                };
        //        }
        
        //        /// <summary>
        //        /// Call when the window dialog is requested.
        //        /// </summary>
        //        /// <param name="e"></param>
        //        private void OnShowWorkspaceDialogRequested(DialogEventArgs e)
        //        {
        //            WorkspaceDialogInteractionRequest.Raise(e.Dialog, e.Callback);
        //        }

        //        #endregion Methods
    }
}