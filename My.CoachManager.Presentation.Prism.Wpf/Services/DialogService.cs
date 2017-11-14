using System;
using System.Windows;
using Microsoft.Practices.ServiceLocation;
using My.CoachManager.Presentation.Prism.Core.EventAggregator;
using My.CoachManager.Presentation.Prism.Core.Interactivity;
using My.CoachManager.Presentation.Prism.Core.Interactivity.InteractionRequest;
using My.CoachManager.Presentation.Prism.Core.Services;
using My.CoachManager.Presentation.Prism.Resources.Strings;
using Prism.Events;

namespace My.CoachManager.Presentation.Prism.Wpf.Services
{
    /// <summary>
    /// Class abstracting the interaction between view models and views when it comes to
    /// opening dialogs using the MVVM pattern in WPF.
    /// </summary>
    public class DialogService : IDialogService
    {
        #region Fields

        private readonly IEventAggregator _eventAggregator;
        private readonly IServiceLocator _serviceLocator;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DialogService"/> class.
        /// </summary>
        public DialogService(IEventAggregator eventAggregator, IServiceLocator serviceLocator)
        {
            _eventAggregator = eventAggregator;
            _serviceLocator = serviceLocator;
        }

        #endregion Constructors

        #region IDialogService Members

        /// <summary>
        /// Displays a modal dialog of a type that is determined by the dialog type locator.
        /// </summary>
        /// <returns>
        /// A nullable value of type <see cref="bool"/> that signifies how a window was closed by
        /// the user.
        /// </returns>
        public void ShowWorkspaceDialog<TView>(Action<IDialog> callbackBefore = null, Action<IDialog> callbackAfter = null) where TView : FrameworkElement
        {
            ShowWorkspaceDialog(typeof(TView), callbackBefore, callbackAfter);
        }

        /// <summary>
        /// Displays a modal dialog of a type that is determined by the dialog type locator.
        /// </summary>
        /// <returns>
        /// A nullable value of type <see cref="bool"/> that signifies how a window was closed by
        /// the user.
        /// </returns>
        public void ShowWorkspaceDialog(Type typeView, Action<IDialog> callbackBefore = null, Action<IDialog> callbackAfter = null)
        {
            var view = _serviceLocator.GetInstance(typeView) as FrameworkElement;
            if (view != null)
            {
                var dialog = new Dialog()
                {
                    Content = view
                };

                if (callbackBefore != null)
                {
                    callbackBefore(dialog);
                }

                _eventAggregator.GetEvent<ShowWorkspaceDialogRequestEvent>().Publish(new DialogEventArgs(dialog, callbackAfter));
            }
        }

        /// <summary>
        /// Displays a modal dialog of a type that is determined by the dialog type locator.
        /// </summary>
        /// <returns>
        /// A nullable value of type <see cref="bool"/> that signifies how a window was closed by
        /// the user.
        /// </returns>
        public void ShowInformationDialog(string message, Action<IDialog> callback = null, MessageDialogType type = MessageDialogType.Ok)
        {
            ShowMessageDialog(DialogResources.Information, message, callback, type);
        }

        /// <summary>
        /// Displays a modal dialog of a type that is determined by the dialog type locator.
        /// </summary>
        /// <returns>
        /// A nullable value of type <see cref="bool"/> that signifies how a window was closed by
        /// the user.
        /// </returns>
        public void ShowErrorDialog(string message, Action<IDialog> callback = null, MessageDialogType type = MessageDialogType.Ok)
        {
            ShowMessageDialog(DialogResources.Error, message, callback, type, MessageDialogStyle.Error);
        }

        /// <summary>
        /// Displays a modal dialog of a type that is determined by the dialog type locator.
        /// </summary>
        /// <returns>
        /// A nullable value of type <see cref="bool"/> that signifies how a window was closed by
        /// the user.
        /// </returns>
        public void ShowWarningDialog(string message, Action<IDialog> callback = null, MessageDialogType type = MessageDialogType.Ok)
        {
            ShowMessageDialog(DialogResources.Warning, message, callback, type, MessageDialogStyle.Warning);
        }

        /// <summary>
        /// Displays a modal dialog of a type that is determined by the dialog type locator.
        /// </summary>
        /// <returns>
        /// A nullable value of type <see cref="bool"/> that signifies how a window was closed by
        /// the user.
        /// </returns>
        public void ShowSuccessDialog(string message, Action<IDialog> callback = null, MessageDialogType type = MessageDialogType.Ok)
        {
            ShowMessageDialog(DialogResources.Success, message, callback, type, MessageDialogStyle.Success);
        }

        /// <summary>
        /// Displays a modal dialog of a type that is determined by the dialog type locator.
        /// </summary>
        /// <returns>
        /// A nullable value of type <see cref="bool"/> that signifies how a window was closed by
        /// the user.
        /// </returns>
        public void ShowQuestionDialog(string message, Action<IDialog> callback = null, MessageDialogType type = MessageDialogType.YesNo)
        {
            ShowMessageDialog(DialogResources.Question, message, callback, type, MessageDialogStyle.Question);
        }

        /// <summary>
        /// Displays a modal dialog of a type that is determined by the dialog type locator.
        /// </summary>
        /// <returns>
        /// A nullable value of type <see cref="bool"/> that signifies how a window was closed by
        /// the user.
        /// </returns>
        public void ShowMessageDialog(string title, string message, Action<IDialog> callback = null, MessageDialogType type = MessageDialogType.Okcancel, MessageDialogStyle style = MessageDialogStyle.Information)
        {
            var dialog = new MessageDialog()
            {
                Content = message,
                Title = title,
                Type = type,
                Style = style
            };

            _eventAggregator.GetEvent<ShowMessageDialogRequestEvent>().Publish(new MessageDialogEventArgs(dialog, callback));
        }

        /// <summary>
        /// Displays a modal dialog of a type that is determined by the dialog type locator.
        /// </summary>
        /// <returns>
        /// A nullable value of type <see cref="bool"/> that signifies how a window was closed by
        /// the user.
        /// </returns>
        public void ShowInformationPopup(string message)
        {
            ShowNotificationPopup(DialogResources.Information, message);
        }

        /// <summary>
        /// Displays a modal dialog of a type that is determined by the dialog type locator.
        /// </summary>
        /// <returns>
        /// A nullable value of type <see cref="bool"/> that signifies how a window was closed by
        /// the user.
        /// </returns>
        public void ShowErrorPopup(string message)
        {
            ShowNotificationPopup(DialogResources.Error, message, MessageDialogStyle.Error);
        }

        /// <summary>
        /// Displays a modal dialog of a type that is determined by the dialog type locator.
        /// </summary>
        /// <returns>
        /// A nullable value of type <see cref="bool"/> that signifies how a window was closed by
        /// the user.
        /// </returns>
        public void ShowWarningPopup(string message)
        {
            ShowNotificationPopup(DialogResources.Warning, message, MessageDialogStyle.Warning);
        }

        /// <summary>
        /// Displays a modal dialog of a type that is determined by the dialog type locator.
        /// </summary>
        /// <returns>
        /// A nullable value of type <see cref="bool"/> that signifies how a window was closed by
        /// the user.
        /// </returns>
        public void ShowSuccessPopup(string message)
        {
            ShowNotificationPopup(DialogResources.Success, message, MessageDialogStyle.Success);
        }

        /// <summary>
        /// Displays a modal dialog of a type that is determined by the dialog type locator.
        /// </summary>
        /// <returns>
        /// A nullable value of type <see cref="bool"/> that signifies how a window was closed by
        /// the user.
        /// </returns>
        public void ShowNotificationPopup(string title, string message, MessageDialogStyle style = MessageDialogStyle.Information)
        {
            var dialog = new NotificationPopup()
            {
                Content = message,
                Title = title,
                Style = style
            };

            _eventAggregator.GetEvent<ShowNotificationPopupRequestEvent>().Publish(new NotificationEventArgs(dialog));
        }

        #endregion IDialogService Members
    }
}