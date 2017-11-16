using System;
using System.Windows;
using My.CoachManager.Presentation.Prism.Core.Interactivity;
using My.CoachManager.Presentation.Prism.Core.Interactivity.InteractionRequest;

namespace My.CoachManager.Presentation.Prism.Core.Services
{
    /// <summary>
    /// Interface abstracting the interaction between view models and views when it comes to
    /// opening dialogs using the MVVM pattern in WPF.
    /// </summary>
    public interface IDialogService
    {
        /// <summary>
        /// Displays a modal dialog of a type that is determined by the dialog type locator.
        /// </summary>
        /// <returns>
        /// A nullable value of type <see cref="bool"/> that signifies how a window was closed by
        /// the user.
        /// </returns>
        void ShowWorkspaceDialog<TView>(Action<IDialog> callbackBefore = null, Action<IDialog> callbackAfter = null) where TView : FrameworkElement;

        /// <summary>
        /// Displays a modal dialog of a type that is determined by the dialog type locator.
        /// </summary>
        /// <returns>
        /// A nullable value of type <see cref="bool"/> that signifies how a window was closed by
        /// the user.
        /// </returns>
        void ShowWorkspaceDialog(Type typeView, Action<IDialog> callbackBefore = null, Action<IDialog> callbackAfter = null);

        /// <summary>
        /// Displays a modal dialog of a type that is determined by the dialog type locator.
        /// </summary>
        /// <returns>
        /// A nullable value of type <see cref="bool"/> that signifies how a window was closed by
        /// the user.
        /// </returns>
        void ShowInformationDialog(string message, Action<IDialog> callback = null, MessageDialogType type = MessageDialogType.Ok);

        /// <summary>
        /// Displays a modal dialog of a type that is determined by the dialog type locator.
        /// </summary>
        /// <returns>
        /// A nullable value of type <see cref="bool"/> that signifies how a window was closed by
        /// the user.
        /// </returns>
        void ShowErrorDialog(string message, Action<IDialog> callback = null, MessageDialogType type = MessageDialogType.Ok);

        /// <summary>
        /// Displays a modal dialog of a type that is determined by the dialog type locator.
        /// </summary>
        /// <returns>
        /// A nullable value of type <see cref="bool"/> that signifies how a window was closed by
        /// the user.
        /// </returns>
        void ShowWarningDialog(string message, Action<IDialog> callback = null, MessageDialogType type = MessageDialogType.Ok);

        /// <summary>
        /// Displays a modal dialog of a type that is determined by the dialog type locator.
        /// </summary>
        /// <returns>
        /// A nullable value of type <see cref="bool"/> that signifies how a window was closed by
        /// the user.
        /// </returns>
        void ShowSuccessDialog(string message, Action<IDialog> callback = null, MessageDialogType type = MessageDialogType.Ok);

        /// <summary>
        /// Displays a modal dialog of a type that is determined by the dialog type locator.
        /// </summary>
        /// <returns>
        /// A nullable value of type <see cref="bool"/> that signifies how a window was closed by
        /// the user.
        /// </returns>
        void ShowQuestionDialog(string message, Action<IDialog> callback = null, MessageDialogType type = MessageDialogType.YesNo);

        /// <summary>
        /// Displays a modal dialog of a type that is determined by the dialog type locator.
        /// </summary>
        /// <returns>
        /// A nullable value of type <see cref="bool"/> that signifies how a window was closed by
        /// the user.
        /// </returns>
        void ShowMessageDialog(string title, string message, Action<IDialog> callback = null,
            MessageDialogType type = MessageDialogType.Okcancel,
            MessageDialogStyle style = MessageDialogStyle.Information);

        /// <summary>
        /// Displays a modal dialog of a type that is determined by the dialog type locator.
        /// </summary>
        /// <returns>
        /// A nullable value of type <see cref="bool"/> that signifies how a window was closed by
        /// the user.
        /// </returns>
        void ShowInformationPopup(string message);

        /// <summary>
        /// Displays a modal dialog of a type that is determined by the dialog type locator.
        /// </summary>
        /// <returns>
        /// A nullable value of type <see cref="bool"/> that signifies how a window was closed by
        /// the user.
        /// </returns>
        void ShowErrorPopup(string message);

        /// <summary>
        /// Displays a modal dialog of a type that is determined by the dialog type locator.
        /// </summary>
        /// <returns>
        /// A nullable value of type <see cref="bool"/> that signifies how a window was closed by
        /// the user.
        /// </returns>
        void ShowWarningPopup(string message);

        /// <summary>
        /// Displays a modal dialog of a type that is determined by the dialog type locator.
        /// </summary>
        /// <returns>
        /// A nullable value of type <see cref="bool"/> that signifies how a window was closed by
        /// the user.
        /// </returns>
        void ShowSuccessPopup(string message);

        /// <summary>
        /// Displays a modal dialog of a type that is determined by the dialog type locator.
        /// </summary>
        /// <returns>
        /// A nullable value of type <see cref="bool"/> that signifies how a window was closed by
        /// the user.
        /// </returns>
        void ShowNotificationPopup(string title, string message,
            MessageDialogStyle style = MessageDialogStyle.Information);

        /// <summary>
        /// Show the dialog for open a file.
        /// </summary>
        string ShowOpenFileDialog(string filter = "", bool multiselect = false, string initialDirectory = "",
            bool restoreDirectory = false);
    }
}