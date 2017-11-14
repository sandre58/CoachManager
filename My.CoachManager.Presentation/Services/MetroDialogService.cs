using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using My.CoachManager.Presentation.Controls.Dialogs;
using My.CoachManager.Presentation.Core.Services.Enums;
using My.CoachManager.Presentation.Core.Services.Interfaces;
using My.CoachManager.Presentation.Core.Services.Settings;
using My.CoachManager.Presentation.Core.ViewModels.Screens.Enums;
using My.CoachManager.Presentation.Core.ViewModels.Screens.Interfaces;
using My.CoachManager.Presentation.Resources.Strings.Screens;
using My.CoachManager.Presentation.ViewModels.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interactivity;
using System.Windows.Interop;
using My.CoachManager.Presentation.ViewModels.Dialogs;
using Action = System.Action;
using CustomDialog = My.CoachManager.Presentation.Controls.Dialogs.CustomDialog;
using FadeBehavior = My.CoachManager.Presentation.Core.Behaviors.FadeBehavior;
using MessageDialog = My.CoachManager.Presentation.Controls.Dialogs.MessageDialog;
using MessageDialogStyle = My.CoachManager.Presentation.Core.ViewModels.Screens.Enums.MessageDialogStyle;
using MessageDialogType = My.CoachManager.Presentation.Core.ViewModels.Screens.Enums.MessageDialogType;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using SlideBehavior = My.CoachManager.Presentation.Core.Behaviors.SlideBehavior;
using Window = System.Windows.Window;

namespace My.CoachManager.Presentation.Services
{
    /// <summary>
    /// Class abstracting the interaction between view models and views when it comes to
    /// opening dialogs using the MVVM pattern in WPF.
    /// </summary>
    public class MetroDialogService : IDialogService<CustomDialogSettings>
    {
        #region Fields

        private readonly MetroWindow _metroWindow;
        private readonly ProgressMessageDialog _progressMessageDialog;

        /// <summary>
        /// Max number of notifications window
        /// </summary>
        private const int MaxNotifications = 1;

        /// <summary>
        /// Number of notification windows
        /// </summary>
        private int _notificationWindowsCount;

        /// <summary>
        /// list of notifications window.
        /// </summary>
        private readonly List<WindowInfo> _notificationWindows;

        /// <summary>
        /// buffer list of notifications window.
        /// </summary>
        private readonly List<WindowInfo> _notificationsBuffer;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MetroDialogService"/> class.
        /// </summary>
        public MetroDialogService()
        {
            _metroWindow = System.Windows.Application.Current.MainWindow as MetroWindow;
            _progressMessageDialog = new ProgressMessageDialog(_metroWindow, new MetroDialogSettings()
            {
                ColorScheme = MetroDialogColorScheme.Accented
            });

            _notificationWindows = new List<WindowInfo>();
            _notificationsBuffer = new List<WindowInfo>();
            _notificationWindowsCount = 0;
        }

        #endregion Constructors

        #region IDialogService Members

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
        public Task<MessageDialogResponse> ShowMessage(string header, string message, MessageDialogType type, CustomDialogSettings settings = null)
        {
            return _metroWindow.Invoke(() =>
            {
                var options = (MessageDialogSettings)settings ?? MessageDialogSettings.Default;
                options.Header = header;
                options.Type = type;
                var dialog = GetMessageDialog(message, options);
                return ShowMessageDialogAsync(dialog);
            });
        }

        /// <summary>
        /// Displays a modal dialog of a type that is determined by the dialog type locator.
        /// </summary>
        /// <param name="message">Information</param>
        ///  <param name="type">Type</param>
        /// <returns>
        /// A nullable value of type <see cref="bool"/> that signifies how a window was closed by
        /// the user.
        /// </returns>
        public Task<MessageDialogResponse> ShowMessage(string message, MessageDialogStyle type)
        {
            return _metroWindow.Invoke(() =>
            {
                var dialog = GetMessageDialog(message, type);
                return ShowMessageDialogAsync(dialog);
            });
        }

        /// <summary>
        /// Displays a modal dialog of a type that is determined by the dialog type locator.
        /// </summary>
        /// <param name="message">Information</param>
        /// <returns>
        /// A nullable value of type <see cref="bool"/> that signifies how a window was closed by
        /// the user.
        /// </returns>
        public Task<MessageDialogResponse> ShowInformation(string message)
        {
            return ShowMessage(message, MessageDialogStyle.Information);
        }

        /// <summary>
        /// Displays a modal dialog of a type that is determined by the dialog type locator.
        /// </summary>
        /// <param name="message">Information</param>
        /// <returns>
        /// A nullable value of type <see cref="bool"/> that signifies how a window was closed by
        /// the user.
        /// </returns>
        public Task<MessageDialogResponse> ShowQuestion(string message)
        {
            return ShowMessage(message, MessageDialogStyle.Question);
        }

        /// <summary>
        /// Displays a modal dialog of a type that is determined by the dialog type locator.
        /// </summary>
        /// <param name="message">Information</param>
        /// <returns>
        /// A nullable value of type <see cref="bool"/> that signifies how a window was closed by
        /// the user.
        /// </returns>
        public Task<MessageDialogResponse> ShowQuestionWithCancel(string message)
        {
            return ShowMessage(message, MessageDialogStyle.QuestionWithCancel);
        }

        /// <summary>
        /// Displays a modal dialog of a type that is determined by the dialog type locator.
        /// </summary>
        /// <param name="message">Information</param>
        /// <returns>
        /// A nullable value of type <see cref="bool"/> that signifies how a window was closed by
        /// the user.
        /// </returns>
        public Task<MessageDialogResponse> ShowError(string message)
        {
            return ShowMessage(message, MessageDialogStyle.Error);
        }

        /// <summary>
        /// Shows a pogress message to the user.
        /// </summary>
        /// <param name="header">The header text for the dialog.</param>
        /// <param name="message">The message to be displayed in the dialog.</param>
        /// <param name="action"></param>
        public async void ShowProgressMessageAsync(string header, string message, Action action = null)
        {
            _progressMessageDialog.Message = message;
            _progressMessageDialog.Title = header;
            _progressMessageDialog.SetIndeterminate();

            await ShowCustomDialogAsync(_progressMessageDialog);

            if (action != null)
            {
                await Task.Run(action);
                HideProgressMessageAsync();
            }
        }

        /// <summary>
        /// Shows a pogress message to the user.
        /// </summary>
        public async void HideProgressMessageAsync()
        {
            if (_progressMessageDialog != null)
            {
                await DialogCoordinator.Instance.HideMetroDialogAsync(_metroWindow.DataContext, _progressMessageDialog);
            }
        }

        /// <summary>
        /// Displays a modal dialog of a type that is determined by the dialog type locator.
        /// </summary>
        /// <param name="viewModel">The view model of the new dialog.</param>
        /// <param name="settings"></param>
        /// <returns>
        /// A nullable value of type <see cref="bool"/> that signifies how a window was closed by
        /// the user.
        /// </returns>
        public Task<TViewModel> ShowDialog<TViewModel>(TViewModel viewModel, CustomDialogSettings settings = null) where TViewModel : IDialogViewModel
        {
            if (settings == null)
            {
                settings = CustomDialogSettings.Default;
                settings.Theme = MetroDialogColorScheme.Theme;
                settings.FullWidth = false;
                settings.Header = viewModel.DisplayName;
            }

            return ShowViewModelDialogAsync(viewModel, settings);
        }

        /// <summary>
        /// Displays a modal dialog of a type that is determined by the dialog type locator.
        /// </summary>
        /// <returns>
        /// A nullable value of type <see cref="bool"/> that signifies how a window was closed by
        /// the user.
        /// </returns>
        public Task<TViewModel> ShowDialog<TViewModel>(CustomDialogSettings settings = null) where TViewModel : IDialogViewModel
        {
            return ShowDialog(Activator.CreateInstance<TViewModel>(), settings);
        }

        /// <summary>
        /// Displays a modal dialog of a type that is determined by the dialog type locator.
        /// </summary>
        /// <param name="viewModel">The view model of the new dialog.</param>
        /// <param name="settings"></param>
        /// <returns>
        /// A nullable value of type <see cref="bool"/> that signifies how a window was closed by
        /// the user.
        /// </returns>
        public async void ShowDialogAsync<TViewModel>(TViewModel viewModel, CustomDialogSettings settings = null) where TViewModel : IDialogViewModel
        {
            await ShowDialog(viewModel, settings);
        }

        /// <summary>
        /// Displays a modal dialog of a type that is determined by the dialog type locator.
        /// </summary>
        /// <returns>
        /// A nullable value of type <see cref="bool"/> that signifies how a window was closed by
        /// the user.
        /// </returns>
        public async void ShowDialogAsync<TViewModel>(CustomDialogSettings settings = null) where TViewModel : IDialogViewModel
        {
            await ShowDialog(Activator.CreateInstance<TViewModel>(), settings);
        }

        /// <summary>
        /// Displays a modal dialog of a type that is determined by the dialog type locator.
        /// </summary>
        /// <returns>
        /// A nullable value of type <see cref="bool"/> that signifies how a window was closed by
        /// the user.
        /// </returns>
        public Task<ILoginViewModel> ShowLoginDialog(string username = "", string password = "", CustomDialogSettings settings = null)
        {
            var viewModel = LoginViewModel.Instance;
            viewModel.Username = username;
            viewModel.Password = password;

            if (settings == null)
            {
                settings = CustomDialogSettings.Default;
                settings.Theme = MetroDialogColorScheme.Theme;
                settings.FullWidth = true;
                settings.Header = viewModel.DisplayName;
            }

            return ShowViewModelDialogAsync(viewModel, settings).ContinueWith(y => (ILoginViewModel)viewModel);
        }

        /// <summary>
        /// Show the dialog for open a file.
        /// </summary>
        /// <returns></returns>
        public string ShowOpenFileDialog(OpenFileDialogSettings settings = null)
        {
            if (settings == null) settings = OpenFileDialogSettings.Default;

            var dialog = new OpenFileDialog()
            {
                Filter = settings.Filter,
                Multiselect = settings.MultiSelect,
                InitialDirectory = settings.InitialDirectory,
                RestoreDirectory = settings.RestoreDirectory
            };

            var result = dialog.ShowDialog(_metroWindow);

            if (result.HasValue && result.Value)
                return dialog.FileName;

            return string.Empty;
        }

        /// <summary>
        /// Displays a modal dialog of a type that is determined by the dialog type locator.
        /// </summary>
        /// <param name="message">Information</param>
        /// <returns>
        /// A nullable value of type <see cref="bool"/> that signifies how a window was closed by
        /// the user.
        /// </returns>
        public void ShowMessagePopup(string message)
        {
            ShowPopup(message, MessagePopupStyle.Information);
        }

        /// <summary>
        /// Displays a modal dialog of a type that is determined by the dialog type locator.
        /// </summary>
        /// <param name="message">Information</param>
        /// <returns>
        /// A nullable value of type <see cref="bool"/> that signifies how a window was closed by
        /// the user.
        /// </returns>
        public void ShowErrorPopup(string message)
        {
            ShowPopup(message, MessagePopupStyle.Error);
        }

        /// <summary>
        /// Displays a modal dialog of a type that is determined by the dialog type locator.
        /// </summary>
        /// <param name="message">Information</param>
        /// <returns>
        /// A nullable value of type <see cref="bool"/> that signifies how a window was closed by
        /// the user.
        /// </returns>
        public void ShowWarningPopup(string message)
        {
            ShowPopup(message, MessagePopupStyle.Warning);
        }

        /// <summary>
        /// Displays a modal dialog of a type that is determined by the dialog type locator.
        /// </summary>
        /// <param name="message">Information</param>
        /// <returns>
        /// A nullable value of type <see cref="bool"/> that signifies how a window was closed by
        /// the user.
        /// </returns>
        public void ShowSuccessPopup(string message)
        {
            ShowPopup(message, MessagePopupStyle.Success);
        }

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
        public void ShowPopup(string message, MessagePopupStyle style, PopupSettings settings = null)
        {
            string title;

            switch (style)
            {
                case MessagePopupStyle.Information:
                    title = DialogResources.Information;
                    break;

                case MessagePopupStyle.Error:
                    title = DialogResources.Error;
                    break;

                case MessagePopupStyle.Warning:
                    title = DialogResources.Warning;
                    break;

                case MessagePopupStyle.Success:
                    title = DialogResources.Success;
                    break;

                default:
                    throw new ArgumentOutOfRangeException("style", style, null);
            }

            _metroWindow.Dispatcher.Invoke(() =>
            {
                var vm = new MessagePopupViewModel()
                {
                    Message = message,
                    Style = style,
                    DisplayName = title
                };
                ShowPopupWindow(GetPopupWindow(vm), settings);
            });
        }

        /// <summary>
        /// Displays a modal dialog of a type that is determined by the dialog type locator.
        /// </summary>
        /// <param name="settings"></param>
        /// <returns>
        /// A nullable value of type <see cref="bool"/> that signifies how a window was closed by
        /// the user.
        /// </returns>
        public void ShowPopup<TScreen>(PopupSettings settings = null) where TScreen : IScreenViewModel
        {
            ShowPopupWindow(GetPopupWindow(Activator.CreateInstance<TScreen>()), settings);
        }

        #endregion IDialogService Members

        #region Methods

        /// <summary>
        /// Displays a modal dialog of a type that is determined by the dialog type locator.
        /// </summary>
        /// <returns>
        /// A nullable value of type <see cref="bool"/> that signifies how a window was closed by
        /// the user.
        /// </returns>
        protected Task<TViewModel> ShowViewModelDialogAsync<TViewModel>(TViewModel viewModel, CustomDialogSettings settings = null) where TViewModel : IDialogViewModel
        {
            if (settings == null) settings = CustomDialogSettings.Default;

            var dialog = GetCustomDialog(viewModel, settings);

            return ShowCustomDialogAsync(dialog).ContinueWith(z =>
            {
                return viewModel.WaitForClose().ContinueWith(y =>
                {
                    _metroWindow.Invoke(() => { DialogCoordinator.Instance.HideMetroDialogAsync(_metroWindow.DataContext, dialog); });
                    return viewModel;
                });
            }).Unwrap();
        }

        /// <summary>
        /// Displays a modal dialog of a type that is determined by the dialog type locator.
        /// </summary>²
        /// <param name="dialog">The message dialog.</param>
        /// <returns></returns>
        protected Task<TDialog> ShowCustomDialogAsync<TDialog>(TDialog dialog) where TDialog : BaseMetroDialog
        {
            return DialogCoordinator.Instance.ShowMetroDialogAsync(_metroWindow.DataContext, dialog).ContinueWith(task => dialog);
        }

        /// <summary>
        /// Displays a modal dialog of a type that is determined by the dialog type locator.
        /// </summary>
        /// <param name="dialog">The message dialog.</param>
        /// <returns></returns>
        protected Task<MessageDialogResponse> ShowMessageDialogAsync(MessageDialog dialog)
        {
            return ShowCustomDialogAsync(dialog).ContinueWith(z =>
            {
                return dialog.WaitForButtonPressAsync().ContinueWith(y =>
                {
                    Task result = null;
                    _metroWindow.Invoke(() => { result = DialogCoordinator.Instance.HideMetroDialogAsync(_metroWindow.DataContext, dialog); });
                    return result.ContinueWith(y2 => y).Unwrap();
                }).Unwrap();
            }).Unwrap();
        }

        /// <summary>
        /// Shows the specified window as a notification.
        /// </summary>
        /// <param name="window">The window.</param>
        /// <param name="settings"></param>
        public void ShowPopupWindow(Window window, PopupSettings settings = null)
        {
            if (settings == null)
                settings = PopupSettings.Default;

            _metroWindow.Dispatcher.Invoke(() =>
            {
                var behaviors = Interaction.GetBehaviors(window);
                behaviors.Add(new FadeBehavior());
                behaviors.Add(new SlideBehavior());
                SetPopupDirection(window, settings.Position);
                _notificationWindowsCount += 1;
                var windowInfo = new WindowInfo()
                {
                    Id = _notificationWindowsCount,
                    DisplayDuration = settings.Delay,
                    Window = window
                };
                windowInfo.Window.Closed += OnPopupClosed;
                if (_notificationWindows.Count() + 1 > MaxNotifications)
                {
                    _notificationsBuffer.Add(windowInfo);
                }
                else
                {
                    Observable.Timer(settings.Delay).Subscribe(x => OnTimerElapsed(windowInfo));
                    _notificationWindows.Add(windowInfo);
                    window.Show();
                }
            });
        }

        /// <summary>
        /// Called when the timer has elapsed. Removes any stale notifications.
        /// </summary>
        /// <param name="windowInfo"></param>
        private void OnTimerElapsed(WindowInfo windowInfo)
        {
            _metroWindow.Dispatcher.Invoke(() =>
            {
                if (_notificationWindows.Count > 0 && _notificationWindows.All(i => i.Id != windowInfo.Id))
                {
                    return;
                }

                if (windowInfo.Window.IsMouseOver)
                {
                    Observable.Timer(windowInfo.DisplayDuration).Subscribe(x => OnTimerElapsed(windowInfo));
                }
                else
                {
                    var behaviors = Interaction.GetBehaviors(windowInfo.Window);
                    var fadeBehavior = behaviors.OfType<FadeBehavior>().First();
                    var slideBehavior = behaviors.OfType<SlideBehavior>().First();

                    fadeBehavior.FadeOut();
                    slideBehavior.SlideOut();

                    EventHandler eventHandler = null;
                    eventHandler = (sender2, e2) =>
                    {
                        fadeBehavior.FadeOutCompleted -= eventHandler;
                        _notificationWindows.Remove(windowInfo);
                        windowInfo.Window.Close();

                        if (_notificationsBuffer != null && _notificationsBuffer.Count > 0)
                        {
                            var bufferWindowInfo = _notificationsBuffer.First();
                            Observable.Timer(bufferWindowInfo.DisplayDuration).Subscribe(x => OnTimerElapsed(bufferWindowInfo));
                            _notificationWindows.Add(bufferWindowInfo);
                            bufferWindowInfo.Window.Show();
                            _notificationsBuffer.Remove(bufferWindowInfo);
                        }
                    };
                    fadeBehavior.FadeOutCompleted += eventHandler;
                }
            });
        }

        /// <summary>
        /// Called when the window is about to close.
        /// Remove the notification window from notification windows list and add one from the buffer list.
        /// </summary>
        private void OnPopupClosed(object sender, EventArgs e)
        {
            _metroWindow.Dispatcher.Invoke(() =>
            {
                var window = (Window)sender;
                if (_notificationWindows.Count > 0 && _notificationWindows.First().Window.Equals(window))
                {
                    var windowInfo = _notificationWindows.First();
                    _notificationWindows.Remove(windowInfo);
                    if (_notificationsBuffer != null && _notificationsBuffer.Count > 0)
                    {
                        var bufferWindowInfo = _notificationsBuffer.First();
                        Observable.Timer(bufferWindowInfo.DisplayDuration).Subscribe(x => OnTimerElapsed(bufferWindowInfo));
                        _notificationWindows.Add(bufferWindowInfo);
                        bufferWindowInfo.Window.Show();
                        _notificationsBuffer.Remove(bufferWindowInfo);
                    }
                }
            });
        }

        /// <summary>
        /// Display the notification window in specified direction of the screen
        /// </summary>
        /// <param name="window"> The window object</param>
        /// <param name="notificationFlowDirection"> Direction in which new notifications will appear.</param>
        private void SetPopupDirection(Window window, PopupDirection notificationFlowDirection)
        {
            _metroWindow.Dispatcher.Invoke(() =>
            {
                var workingArea = Screen.FromHandle(new WindowInteropHelper(_metroWindow).Handle).WorkingArea;
                var presentationSource = PresentationSource.FromVisual(_metroWindow);
                if (presentationSource != null)
                {
                    if (presentationSource.CompositionTarget != null)
                    {
                        var transform = presentationSource.CompositionTarget.TransformFromDevice;
                        var corner = transform.Transform(new Point(workingArea.Left, workingArea.Top));

                        switch (notificationFlowDirection)
                        {
                            case PopupDirection.TopCenter:
                                window.Left = corner.X + (_metroWindow.Width - window.Width) / 2 + window.Margin.Left;
                                window.Top = corner.Y + window.Margin.Top;
                                break;

                            case PopupDirection.BottomCenter:
                                window.Left = corner.X + (_metroWindow.Width - window.Width) / 2 + window.Margin.Left;
                                window.Top = corner.Y + _metroWindow.Height - window.Height - window.Margin.Bottom;
                                break;

                            case PopupDirection.BottomRight:
                                window.Left = corner.X + _metroWindow.Width - window.Width - window.Margin.Right;
                                window.Top = corner.Y + _metroWindow.Height - window.Height - window.Margin.Bottom;
                                break;

                            case PopupDirection.BottomLeft:
                                window.Left = corner.X + window.Margin.Left;
                                window.Top = corner.Y + _metroWindow.Height - window.Height - window.Margin.Bottom;
                                break;

                            case PopupDirection.TopLeft:
                                window.Left = corner.X + (_metroWindow.Width - window.Width) / 2;
                                window.Top = corner.Y + window.Margin.Left;
                                break;

                            case PopupDirection.TopRight:
                                window.Left = corner.X + _metroWindow.Width - window.Width - window.Margin.Right;
                                window.Top = corner.Y + window.Margin.Top;
                                break;
                        }
                    }
                }
            });
        }

        /// <summary>
        /// Create he view model used in window.
        /// </summary>
        /// <returns></returns>
        protected virtual CustomDialog GetCustomDialog(IDialogViewModel viewModel, CustomDialogSettings settings = null)
        {
            if (settings == null)
            {
                settings = CustomDialogSettings.Default;
            }

            var s = new MetroDialogSettings()
            {
                SuppressDefaultResources = true,
                ColorScheme = settings.Theme,
                CustomResourceDictionary = new ResourceDictionary()
                    {
                        Source = new Uri("../../Resources/Styles/Generic.xaml", UriKind.Relative)
                    }
            };
            var dialog = new CustomDialog(_metroWindow, s)
            {
                Content = viewModel,
                Title = settings.Header == "" ? null : (settings.Header ?? viewModel.DisplayName),
                CloseCommand = viewModel.CloseCommand
            };

            if (settings.FullWidth)
            {
                dialog.Style = (Style)_metroWindow.FindResource("FullWidthDialogStyle");
            }

            return dialog;
        }

        /// <summary>
        /// Create he view model used in window.
        /// </summary>
        /// <returns></returns>
        protected virtual MessageDialog GetMessageDialog(string message, MessageDialogSettings settings)
        {
            var s = new MetroDialogSettings()
            {
                SuppressDefaultResources = true,
                ColorScheme = settings.Theme,
                CustomResourceDictionary = new ResourceDictionary()
                    {
                        Source = new Uri("../../Resources/Styles/Generic.xaml", UriKind.Relative)
                    }
            };

            return new MessageDialog(s)
            {
                Message = message,
                Title = settings.Header,
                Icon = (settings.IconStyle != "" && _metroWindow != null) ? (Style)_metroWindow.FindResource(settings.IconStyle) : null,
                Type = settings.Type
            };
        }

        /// <summary>
        /// Create he view model used in window.
        /// </summary>
        /// <returns></returns>
        protected virtual MessageDialog GetMessageDialog(string message, MessageDialogStyle type)
        {
            var settings = GetSettings(type);
            return GetMessageDialog(message, settings);
        }

        /// <summary>
        /// Create he view model used in window.
        /// </summary>
        /// <returns></returns>
        protected virtual Window GetPopupWindow(IScreenViewModel viewModel)
        {
            var window = new PopupWindow()
            {
                Content = viewModel
            };

            return window;
        }

        /// <summary>
        /// Get default settings.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private MessageDialogSettings GetSettings(MessageDialogStyle type)
        {
            switch (type)
            {
                case MessageDialogStyle.Information:
                    return new MessageDialogSettings()
                    {
                        Header = DialogResources.Information,
                        Theme = MetroDialogColorScheme.Inverted,
                        IconStyle = "InfoMessageIconStyle",
                        Type = MessageDialogType.Ok,
                        FullWidth = true
                    };

                case MessageDialogStyle.Error:
                    return new MessageDialogSettings()
                    {
                        Header = DialogResources.Error,
                        Theme = MetroDialogColorScheme.Theme,
                        IconStyle = "ErroMessageIconStyle",
                        Type = MessageDialogType.Ok,
                        FullWidth = true
                    };

                case MessageDialogStyle.Warning:
                    return new MessageDialogSettings()
                    {
                        Header = DialogResources.Warning,
                        Theme = MetroDialogColorScheme.Theme,
                        IconStyle = "WarningMessage1IconStyle",
                        Type = MessageDialogType.Ok,
                        FullWidth = true
                    };

                case MessageDialogStyle.Question:
                    return new MessageDialogSettings()
                    {
                        Header = DialogResources.Question,
                        Theme = MetroDialogColorScheme.Inverted,
                        IconStyle = "QuestionMessageIconStyle",
                        Type = MessageDialogType.YesNo,
                        FullWidth = true
                    };

                case MessageDialogStyle.QuestionWithCancel:
                    return new MessageDialogSettings()
                    {
                        Header = DialogResources.QuestionWithCancel,
                        Theme = MetroDialogColorScheme.Inverted,
                        IconStyle = "QuestionMessageIconStyle",
                        Type = MessageDialogType.YesNoCancel,
                        FullWidth = true
                    };

                default:
                    throw new ArgumentOutOfRangeException("type", type, null);
            }
        }

        #endregion Methods

        #region Classes

        /// <summary>
        /// Window metadata.
        /// </summary>
        private sealed class WindowInfo
        {
            public int Id { get; set; }

            /// <summary>
            /// Gets or sets the display duration.
            /// </summary>
            /// <value>
            /// The display duration.
            /// </value>
            public TimeSpan DisplayDuration { get; set; }

            /// <summary>
            /// Gets or sets the window.
            /// </summary>
            /// <value>
            /// The window.
            /// </value>
            public Window Window { get; set; }
        }

        #endregion Classes
    }

    public class CustomDialogSettings
    {
        public bool FullWidth { get; set; }
        public MetroDialogColorScheme Theme { get; set; }
        public string Header { get; set; }

        public static CustomDialogSettings Default
        {
            get
            {
                return new CustomDialogSettings()
                {
                    FullWidth = true,
                    Theme = MetroDialogColorScheme.Accented,
                    Header = ""
                };
            }
        }
    }

    public class MessageDialogSettings : CustomDialogSettings
    {
        public string IconStyle { get; set; }
        public MessageDialogType Type { get; set; }

        public new static MessageDialogSettings Default
        {
            get
            {
                return new MessageDialogSettings()
                {
                    FullWidth = true,
                    Theme = MetroDialogColorScheme.Accented,
                    Header = "",
                    IconStyle = "",
                    Type = MessageDialogType.Ok
                };
            }
        }
    }
}