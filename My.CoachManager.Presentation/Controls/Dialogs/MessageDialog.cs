using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ControlzEx;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using My.CoachManager.Presentation.Core.ViewModels.Screens.Enums;

namespace My.CoachManager.Presentation.Controls.Dialogs
{
    public partial class MessageDialog
    {
        #region Fields

        public static readonly DependencyProperty MessageProperty = DependencyProperty.Register("Message", typeof(string), typeof(MessageDialog));
        public static readonly DependencyProperty TypeProperty = DependencyProperty.Register("Type", typeof(MessageDialogType), typeof(MessageDialog), new PropertyMetadata(MessageDialogType.Ok));
        public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(Style), typeof(MessageDialog));

        #endregion Fields

        #region Constructors

        public MessageDialog()
            : this(null)
        {
        }

        public MessageDialog(MetroDialogSettings settings)
            : this(null, settings)
        {
        }

        public MessageDialog(MetroWindow parentWindow, MetroDialogSettings settings = null)
            : base(parentWindow, settings)
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Methods

        internal Task<MessageDialogResponse> WaitForButtonPressAsync()
        {
            Dispatcher.BeginInvoke(new Action(() =>
                                                       {
                                                           Focus();

                                                           var defaultButtonFocus = MessageDialogResponse.Ok;

                                                           if (Type == MessageDialogType.YesNo || Type == MessageDialogType.YesNoCancel)
                                                               defaultButtonFocus = MessageDialogResponse.Yes;

                                                           //kind of acts like a selective 'IsDefault' mechanism.
                                                           switch (defaultButtonFocus)
                                                           {
                                                               case MessageDialogResponse.Ok:
                                                                   KeyboardNavigationEx.Focus(BtnOk);
                                                                   break;

                                                               case MessageDialogResponse.Cancel:
                                                                   KeyboardNavigationEx.Focus(BtnCancel);
                                                                   break;

                                                               case MessageDialogResponse.Yes:
                                                                   KeyboardNavigationEx.Focus(BtnYes);
                                                                   break;

                                                               case MessageDialogResponse.No:
                                                                   KeyboardNavigationEx.Focus(BtnNo);
                                                                   break;
                                                           }
                                                       }));

            var tcs = new TaskCompletionSource<MessageDialogResponse>();

            RoutedEventHandler[] handlers = { null, null, null, null };
            KeyEventHandler[] keyHandlers = { null, null, null, null };

            KeyEventHandler[] escapeKeyHandler = { null };

            Action cleanUpHandlers = () =>
                {
                    BtnNo.Click -= handlers[0];
                    BtnYes.Click -= handlers[1];
                    BtnCancel.Click -= handlers[2];
                    BtnOk.Click -= handlers[3];

                    BtnNo.KeyDown -= keyHandlers[0];
                    BtnYes.KeyDown -= keyHandlers[1];
                    BtnCancel.KeyDown -= keyHandlers[2];
                    BtnOk.KeyDown -= keyHandlers[3];

                    KeyDown -= escapeKeyHandler[0];
                };

            keyHandlers[0] = (sender, e) =>
            {
                if (e.Key != Key.Enter) return;
                cleanUpHandlers();

                tcs.TrySetResult(MessageDialogResponse.No);
            };

            keyHandlers[1] = (sender, e) =>
            {
                if (e.Key != Key.Enter) return;
                cleanUpHandlers();

                tcs.TrySetResult(MessageDialogResponse.Yes);
            };

            keyHandlers[2] = (sender, e) =>
            {
                if (e.Key != Key.Enter) return;
                cleanUpHandlers();

                tcs.TrySetResult(MessageDialogResponse.Cancel);
            };

            keyHandlers[3] = (sender, e) =>
            {
                if (e.Key != Key.Enter) return;
                cleanUpHandlers();

                tcs.TrySetResult(MessageDialogResponse.Ok);
            };

            handlers[0] = (sender, e) =>
                {
                    cleanUpHandlers();

                    tcs.TrySetResult(MessageDialogResponse.No);

                    e.Handled = true;
                };

            handlers[1] = (sender, e) =>
                {
                    cleanUpHandlers();

                    tcs.TrySetResult(MessageDialogResponse.Yes);

                    e.Handled = true;
                };

            handlers[2] = (sender, e) =>
                {
                    cleanUpHandlers();

                    tcs.TrySetResult(MessageDialogResponse.Cancel);

                    e.Handled = true;
                };

            handlers[3] = (sender, e) =>
                {
                    cleanUpHandlers();

                    tcs.TrySetResult(MessageDialogResponse.Ok);

                    e.Handled = true;
                };

            escapeKeyHandler[0] = (sender, e) =>
                {
                    if (e.Key == Key.Escape)
                    {
                        cleanUpHandlers();

                        tcs.TrySetResult(Type == MessageDialogType.YesNo ? MessageDialogResponse.Yes : MessageDialogResponse.No);

                        if (Type == MessageDialogType.YesNoCancel || Type == MessageDialogType.Okcancel)
                            tcs.TrySetResult(MessageDialogResponse.Cancel);
                        else if (Type == MessageDialogType.YesNo)
                            tcs.TrySetResult(MessageDialogResponse.No);
                    }
                    else if (e.Key == Key.Enter)
                    {
                        cleanUpHandlers();

                        if (Type == MessageDialogType.YesNo || Type == MessageDialogType.YesNoCancel)
                            tcs.TrySetResult(MessageDialogResponse.Yes);
                        else if (Type == MessageDialogType.Ok || Type == MessageDialogType.Okcancel)
                            tcs.TrySetResult(MessageDialogResponse.Ok);
                    }
                };

            BtnNo.KeyDown += keyHandlers[0];
            BtnYes.KeyDown += keyHandlers[1];
            BtnCancel.KeyDown += keyHandlers[2];
            BtnOk.KeyDown += keyHandlers[3];

            BtnNo.Click += handlers[0];
            BtnYes.Click += handlers[1];
            BtnCancel.Click += handlers[2];
            BtnOk.Click += handlers[3];

            KeyDown += escapeKeyHandler[0];

            return tcs.Task;
        }

        #endregion Methods

        #region Properties

        /// <summary>
        /// Get or Set the message.
        /// </summary>
        public string Message
        {
            get
            {
                return (string)GetValue(MessageProperty);
            }

            set
            {
                SetValue(MessageProperty, value);
            }
        }

        /// <summary>
        /// Get or Set the type.
        /// </summary>
        public MessageDialogType Type
        {
            get
            {
                return (MessageDialogType)GetValue(TypeProperty);
            }

            set
            {
                SetValue(TypeProperty, value);
            }
        }

        /// <summary>
        /// Get or Set the icon.
        /// </summary>
        public Style Icon
        {
            get
            {
                return (Style)GetValue(IconProperty);
            }

            set
            {
                SetValue(IconProperty, value);
            }
        }

        #endregion Properties
    }
}