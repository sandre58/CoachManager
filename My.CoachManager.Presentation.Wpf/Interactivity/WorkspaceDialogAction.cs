using System;
using System.Windows;
using System.Windows.Interactivity;
using My.CoachManager.Presentation.Controls;
using My.CoachManager.Presentation.Core.Dialog;
using My.CoachManager.Presentation.Core.ViewModels.Interfaces;
using Prism.Interactivity.InteractionRequest;

namespace My.CoachManager.Presentation.Wpf.Interactivity
{
    /// <summary>
    /// Shows a popup window in response to an <see cref="InteractionRequest{T}"/> being raised.
    /// </summary>
    public class WorkspaceDialogAction : TriggerAction<FrameworkElement>
    {
        /// <summary>
        /// The content of the child window to display as part of the popup.
        /// </summary>
        public static readonly DependencyProperty WorkspaceDialogProperty =
            DependencyProperty.Register(
                "WorkspaceDialog",
                typeof(WorkspaceDialog),
                typeof(WorkspaceDialogAction),
                new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets the content of the window.
        /// </summary>
        public WorkspaceDialog WorkspaceDialog
        {
            get => (WorkspaceDialog)GetValue(WorkspaceDialogProperty);
            set => SetValue(WorkspaceDialogProperty, value);
        }

        /// <summary>
        /// Displays the child window and collects results for <see cref="IInteractionRequest"/>.
        /// </summary>
        /// <param name="parameter">The parameter to the action. If the action does not require a parameter, the parameter may be set to a null reference.</param>
        protected override void Invoke(object parameter)
        {
            if (!(parameter is InteractionRequestedEventArgs args))
            {
                return;
            }

            var wrapper = GetDialog((IWorkspaceDialog)args.Context);

            // We invoke the callback when the interaction's window is closed.
            var callback = args.Callback;

            void Handler(object o, DependencyPropertyChangedEventArgs e)
            {
                if ((bool)e.NewValue) return;
                wrapper.IsVisibleChanged -= Handler;
                wrapper.Content = null;
                callback?.Invoke();
            }

            wrapper.IsVisibleChanged += Handler;

            wrapper.ShowHandlerDialog();
        }

        /// <summary>
        /// Returns the window to display as part of the trigger action.
        /// </summary>
        /// <param name="dialog">The dialog to be set as a DataContext in the window.</param>
        /// <returns></returns>
        protected virtual WorkspaceDialog GetDialog(IWorkspaceDialog dialog)
        {
            var wrapper = WorkspaceDialog;

            if (wrapper == null)
                throw new NullReferenceException("WorkspaceDialog cannot return null");

            wrapper.Content = dialog.Content;

            void Handler(object o, EventArgs e)
            {
                System.Windows.Application.Current.Dispatcher.Invoke(() =>
                {
                    wrapper.HideHandlerDialog();
                });
            }

            ((IDialogViewModel)dialog.Content.DataContext).CloseRequest += Handler;

            return wrapper;
        }
    }
}