using System;
using System.Windows;
using System.Windows.Interactivity;
using My.CoachManager.Presentation.Prism.Controls;
using My.CoachManager.Presentation.Prism.Core.Dialog;
using Prism.Interactivity.InteractionRequest;

namespace My.CoachManager.Presentation.Prism.Wpf.Interactivity
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
            get { return (WorkspaceDialog)GetValue(WorkspaceDialogProperty); }
            set { SetValue(WorkspaceDialogProperty, value); }
        }

        /// <summary>
        /// Displays the child window and collects results for <see cref="IInteractionRequest"/>.
        /// </summary>
        /// <param name="parameter">The parameter to the action. If the action does not require a parameter, the parameter may be set to a null reference.</param>
        protected override void Invoke(object parameter)
        {
            var args = parameter as InteractionRequestedEventArgs;
            if (args == null)
            {
                return;
            }

            var wrapper = GetDialog((IDialog)args.Context);

            // We invoke the callback when the interaction's window is closed.
            var callback = args.Callback;

            DependencyPropertyChangedEventHandler handler = null;
            handler =
                (o, e) =>
                {
                    if ((bool)e.NewValue) return;
                    wrapper.IsVisibleChanged -= handler;
                    wrapper.Content = null;
                    if (callback != null) callback();
                };
            wrapper.IsVisibleChanged += handler;

            wrapper.ShowHandlerDialog();
        }

        /// <summary>
        /// Returns the window to display as part of the trigger action.
        /// </summary>
        /// <param name="dialog">The dialog to be set as a DataContext in the window.</param>
        /// <returns></returns>
        protected virtual WorkspaceDialog GetDialog(IDialog dialog)
        {
            var wrapper = WorkspaceDialog;

            if (wrapper == null)
                throw new NullReferenceException("WorkspaceDialog cannot return null");

            // If the WindowContent does not have its own DataContext, it will inherit this one.
            wrapper.DataContext = dialog;
            wrapper.Content = dialog.Content;

            EventHandler handler = (o, e) => { wrapper.HideHandlerDialog(); };
            if (dialog.Context != null) dialog.Context.CloseRequest += handler;

            return wrapper;
        }
    }
}