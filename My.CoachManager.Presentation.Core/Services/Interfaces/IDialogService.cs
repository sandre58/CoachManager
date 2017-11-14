using System;
using System.Threading.Tasks;
using My.CoachManager.Presentation.Core.Services.Settings;
using My.CoachManager.Presentation.Core.ViewModels.Screens.Enums;
using My.CoachManager.Presentation.Core.ViewModels.Screens.Interfaces;

namespace My.CoachManager.Presentation.Core.Services.Interfaces
{
    /// <summary>
    /// Interface abstracting the interaction between view models and views when it comes to
    /// opening dialogs using the MVVM pattern in WPF.
    /// </summary>
    public interface IDialogService<in TSettings> where TSettings : class
    {
        /// <summary>
        /// Displays a modal dialog of a type that is determined by the dialog type locator.
        /// </summary>
        /// <param name="message">Information</param>
        /// <param name="style"></param>
        /// <param name="settings"></param>
        /// <returns>
        /// A nullable value of type <see cref="bool"/> that signifies how a window was closed by
        /// the user.
        /// </returns>
        void ShowPopup(string message, MessagePopupStyle style, PopupSettings settings = null);

        /// <summary>
        /// Displays a modal dialog of a type that is determined by the dialog type locator.
        /// </summary>
        /// <param name="message">Information</param>
        /// <returns>
        /// A nullable value of type <see cref="bool"/> that signifies how a window was closed by
        /// the user.
        /// </returns>
        void ShowMessagePopup(string message);

        /// <summary>
        /// Displays a modal dialog of a type that is determined by the dialog type locator.
        /// </summary>
        /// <param name="message">Information</param>
        /// <returns>
        /// A nullable value of type <see cref="bool"/> that signifies how a window was closed by
        /// the user.
        /// </returns>
        void ShowErrorPopup(string message);

        /// <summary>
        /// Displays a modal dialog of a type that is determined by the dialog type locator.
        /// </summary>
        /// <param name="message">Information</param>
        /// <returns>
        /// A nullable value of type <see cref="bool"/> that signifies how a window was closed by
        /// the user.
        /// </returns>
        void ShowWarningPopup(string message);

        /// <summary>
        /// Displays a modal dialog of a type that is determined by the dialog type locator.
        /// </summary>
        /// <param name="message">Information</param>
        /// <returns>
        /// A nullable value of type <see cref="bool"/> that signifies how a window was closed by
        /// the user.
        /// </returns>
        void ShowSuccessPopup(string message);

        /// <summary>
        /// Displays a modal dialog of a type that is determined by the dialog type locator.
        /// </summary>
        /// <param name="settings"></param>
        /// <returns>
        /// A nullable value of type <see cref="bool"/> that signifies how a window was closed by
        /// the user.
        /// </returns>
        void ShowPopup<TScreen>(PopupSettings settings = null) where TScreen : IScreenViewModel;

        /// <summary>
        /// Displays a modal dialog of a type that is determined by the dialog type locator.
        /// </summary>
        /// <param name="header">Header</param>
        /// <param name="message">Information</param>
        /// <param name="type">Type.</param>
        /// <param name="settings"></param>
        /// <returns>
        /// A nullable value of type <see cref="bool"/> that signifies how a window was closed by
        /// the user.
        /// </returns>
        Task<MessageDialogResponse> ShowMessage(string header, string message, MessageDialogType type, TSettings settings = null);

        /// <summary>
        /// Displays a modal dialog of a type that is determined by the dialog type locator.
        /// </summary>
        /// <param name="message">Information</param>
        ///  <param name="type">Type</param>
        /// <returns>
        /// A nullable value of type <see cref="bool"/> that signifies how a window was closed by
        /// the user.
        /// </returns>
        Task<MessageDialogResponse> ShowMessage(string message, MessageDialogStyle type);

        /// <summary>
        /// Displays a modal dialog of a type that is determined by the dialog type locator.
        /// </summary>
        /// <param name="message">Information</param>
        /// <returns>
        /// A nullable value of type <see cref="bool"/> that signifies how a window was closed by
        /// the user.
        /// </returns>
        Task<MessageDialogResponse> ShowInformation(string message);

        /// <summary>
        /// Displays a modal dialog of a type that is determined by the dialog type locator.
        /// </summary>
        /// <param name="message">Information</param>
        /// <returns>
        /// A nullable value of type <see cref="bool"/> that signifies how a window was closed by
        /// the user.
        /// </returns>
        Task<MessageDialogResponse> ShowQuestion(string message);

        /// <summary>
        /// Displays a modal dialog of a type that is determined by the dialog type locator.
        /// </summary>
        /// <param name="message">Information</param>
        /// <returns>
        /// A nullable value of type <see cref="bool"/> that signifies how a window was closed by
        /// the user.
        /// </returns>
        Task<MessageDialogResponse> ShowQuestionWithCancel(string message);

        /// <summary>
        /// Displays a modal dialog of a type that is determined by the dialog type locator.
        /// </summary>
        /// <param name="message">Information</param>
        /// <returns>
        /// A nullable value of type <see cref="bool"/> that signifies how a window was closed by
        /// the user.
        /// </returns>
        Task<MessageDialogResponse> ShowError(string message);

        /// <summary>
        /// Shows a pogress message to the user.
        /// </summary>
        /// <param name="header">The header text for the dialog.</param>
        /// <param name="message">The message to be displayed in the dialog.</param>
        /// <param name="action"></param>
        void ShowProgressMessageAsync(string header, string message, Action action = null);

        /// <summary>
        /// Hide a pogress message to the user.
        /// </summary>
        void HideProgressMessageAsync();

        /// <summary>
        /// Displays a modal dialog of a type that is determined by the dialog type locator.
        /// </summary>
        /// <param name="viewModel">The view model of the new dialog.</param>
        /// <param name="settings"></param>
        /// <returns>
        /// A nullable value of type <see cref="bool"/> that signifies how a window was closed by
        /// the user.
        /// </returns>
        Task<TViewModel> ShowDialog<TViewModel>(TViewModel viewModel, TSettings settings = null) where TViewModel : IDialogViewModel;

        /// <summary>
        /// Displays a modal dialog of a type that is determined by the dialog type locator.
        /// </summary>
        /// <returns>
        /// A nullable value of type <see cref="bool"/> that signifies how a window was closed by
        /// the user.
        /// </returns>
        Task<TViewModel> ShowDialog<TViewModel>(TSettings settings = null) where TViewModel : IDialogViewModel;

        /// <summary>
        /// Displays a modal dialog of a type that is determined by the dialog type locator.
        /// </summary>
        /// <returns>
        /// A nullable value of type <see cref="bool"/> that signifies how a window was closed by
        /// the user.
        /// </returns>
        void ShowDialogAsync<TViewModel>(TSettings settings = null) where TViewModel : IDialogViewModel;

        /// <summary>
        /// Displays a modal dialog of a type that is determined by the dialog type locator.
        /// </summary>
        /// <param name="viewModel">The view model of the new dialog.</param>
        /// <param name="settings"></param>
        /// <returns>
        /// A nullable value of type <see cref="bool"/> that signifies how a window was closed by
        /// the user.
        /// </returns>
        void ShowDialogAsync<TViewModel>(TViewModel viewModel, TSettings settings = null) where TViewModel : IDialogViewModel;

        /// <summary>
        /// Displays a modal dialog of a type that is determined by the dialog type locator.
        /// </summary>
        /// <returns>
        /// A nullable value of type <see cref="bool"/> that signifies how a window was closed by
        /// the user.
        /// </returns>
        Task<ILoginViewModel> ShowLoginDialog(string username = "", string password = "", TSettings settings = null);

        /// <summary>
        /// Displays a modal dialog of a type that is determined by the dialog type locator.
        /// </summary>
        /// <returns>
        /// A nullable value of type <see cref="bool"/> that signifies how a window was closed by
        /// the user.
        /// </returns>
        string ShowOpenFileDialog(OpenFileDialogSettings settings = null);
    }
}