using System.ComponentModel;
using System.Windows;
using My.CoachManager.Presentation.Wpf.Core.Commands;
using My.CoachManager.Presentation.Wpf.Core.Dialog;
using My.CoachManager.Presentation.Wpf.Core.Events;
using My.CoachManager.Presentation.Wpf.Core.Manager;
using My.CoachManager.Presentation.Wpf.Core.ViewModels.Base;
using My.CoachManager.Presentation.Wpf.Core.ViewModels.Interfaces;
using My.CoachManager.Presentation.Wpf.Modules.Shared.Events;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using Prism.Regions;

namespace My.CoachManager.Presentation.Wpf.ViewModels
{
    /// <summary>
    /// Shell view model.
    /// </summary>
    public class ShellViewModel : ScreenViewModel
    {

        #region Members

        /// <summary>
        /// Gets or sets the menu is expended.
        /// </summary>
        public bool IsMenuExpended { get; set; }
        
        /// <summary>
        /// Gets or sets the active workspace.
        /// </summary>
        public INavigableWorkspaceViewModel ActiveWorkspace => NavigationManager.ActiveView;

        /// <summary>
        /// Gets active title.
        /// </summary>
        public string ActiveTitle => ActiveWorkspace?.Title;

        /// <summary>
        /// Gets or sets the active workspace dialog.
        /// </summary>
        public IWorkspaceDialogViewModel ActiveWorkspaceDialog { get; private set; }

        /// <summary>
        /// Gets or Set the dialog interaction.
        /// </summary>
        public InteractionRequest<IWorkspaceDialog> WorkspaceDialogInteractionRequest { get; set; }
        
        #endregion Members

        #region Initialisation

        /// <inheritdoc />
        /// <summary>
        /// Launch on constructor for initialize all command property.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();

            IsMenuExpended = true;

            EventAggregator.GetEvent<ShowWorkspaceDialogRequestEvent>().Subscribe(OnShowWorkspaceDialogRequested, ThreadOption.UIThread, true);

            WorkspaceDialogInteractionRequest = new InteractionRequest<IWorkspaceDialog>();
            WorkspaceDialogInteractionRequest.Raised += OnWorkspaceDialogInteractionRequestRaised;

            NavigationManager.Navigated += OnNavigateCompleted;
        }

        #endregion Initialisation

        #region Methods

        private void OnWorkspaceDialogInteractionRequestRaised(object sender, InteractionRequestedEventArgs e)
        {
            ActiveWorkspaceDialog = (e.Context.Content as FrameworkElement)?.DataContext as IWorkspaceDialogViewModel;
            if (ActiveWorkspaceDialog != null)
                ActiveWorkspaceDialog.CloseRequest += (o, args) =>
                {
                    ActiveWorkspaceDialog = null;
                };
        }

        /// <summary>
        /// Call when the navigation is completed.
        /// </summary>
        private void OnNavigateCompleted(object sender, RegionNavigationEventArgs e)
        {
            RaisePropertyChanged(nameof(ActiveWorkspace));
            RaisePropertyChanged(nameof(ActiveTitle));
            NavigationCommandsManager.GoForwardCommand?.RaiseCanExecuteChanged();
            NavigationCommandsManager.GoBackCommand?.RaiseCanExecuteChanged();

            if (ActiveWorkspace is INotifyPropertyChanged workspace)
            {
                workspace.PropertyChanged += delegate(object o, PropertyChangedEventArgs args)
                {
                    if (args.PropertyName == "Title") RaisePropertyChanged(nameof(ActiveTitle));
                };
            }
        }

        /// <summary>
        /// Call when the window dialog is requested.
        /// </summary>
        /// <param name="e"></param>
        private void OnShowWorkspaceDialogRequested(DialogEventArgs e)
        {
            WorkspaceDialogInteractionRequest.Raise(e.Dialog, e.Callback);
        }

        #endregion Methods

        #region PropertyChanged 

        /// <summary>
        /// Occurs when expended menu changed.
        /// </summary>
        protected void OnIsMenuExpendedChanged()
        {
            EventAggregator.GetEvent<MenuExpendedChangedEvent>().Publish(IsMenuExpended);
        }

#endregion
    }
}