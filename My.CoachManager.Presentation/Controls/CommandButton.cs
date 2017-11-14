using System;
using System.ComponentModel;
using System.Security;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Input;

namespace My.CoachManager.Presentation.Controls
{
    [DefaultEvent("Click")]
    internal class CommandButton : Elysium.Controls.CommandButton
    {
        #region "Private Variables"

        // Cache valid bits
        private ControlBoolFlags _controlBoolField;

        #endregion "Private Variables"

        #region "Constructors"

        static CommandButton()
        {
            EventManager.RegisterClassHandler(
                typeof(CommandButton),
                AccessKeyManager.AccessKeyPressedEvent,
                new AccessKeyPressedEventHandler(
                    OnAccessKeyPressed));

            KeyboardNavigation.AcceptsReturnProperty.OverrideMetadata(
                typeof(CommandButton),
                new FrameworkPropertyMetadata(
                    true));

            // Disable IME on button.
            //  - key typing should not be eaten by IME.
            //  - when the button has a focus, IME's disabled status should
            //    be indicated as
            //    grayed buttons on the language bar.
            InputMethod.IsInputMethodEnabledProperty.OverrideMetadata(
                typeof(CommandButton),
                new FrameworkPropertyMetadata(
                    false,
                    FrameworkPropertyMetadataOptions.Inherits));
        }

        #endregion "Constructors"

        #region "AccessKey"

        private static void OnAccessKeyPressed(object sender,
            AccessKeyPressedEventArgs e)
        {
            if (!e.Handled && e.Scope == null && e.Target == null)
            {
                e.Target = sender as CommandButton;
            }
        }

        /// <summary>
        /// The Access key for this control was invoked.
        /// </summary>
        protected override void OnAccessKey(AccessKeyEventArgs e)
        {
            if (e.IsMultiple)
            {
                base.OnAccessKey(e);
            }
            else
            {
                // Don't call the base b/c we don't want to take focus
                OnClick();
            }
        }

        #endregion "AccessKey"

        #region "Click"

        /// <summary>
        /// This virtual method is called when button is clicked and
        /// it raises the Click event
        /// </summary>
        private void BaseOnClick()
        {
            RoutedEventArgs locRoutedEventArgs = new RoutedEventArgs(
                ClickEvent,
                this);
            RaiseEvent(locRoutedEventArgs);
            ExecuteCommandSource(this);
        }

        /// <summary>
        /// This method is called when button is clicked.
        /// </summary>
        protected override void OnClick()
        {
            if (AutomationPeer.ListenerExists(AutomationEvents.InvokePatternOnInvoked))
            {
                AutomationPeer locPeer =
                    UIElementAutomationPeer.CreatePeerForElement(this);
                if (locPeer != null)
                {
                    locPeer.RaiseAutomationEvent(AutomationEvents.InvokePatternOnInvoked);
                }
            }

            // base.OnClick should be called first. Our default command
            // for Cancel Button to close dialog should happen after
            // Button's click event handler has been called.
            // If there Is excption And it Then 's a Cancel button and
            // RoutedCommand is null,
            // we will raise Window.DialogCancelCommand.
            try
            {
                BaseOnClick();
            }
            finally
            {
                // When the Button RoutedCommand is null, if it's a
                // Cancel Button,
                // Window.DialogCancelCommand will be the default command.
                // Do not assign Window.DialogCancelCommand to
                // Button.Command.
                // If in Button click handler user nulls the Command,
                // we still want to provide the default behavior.
                if (Command == null && IsCancel)
                {
                    // Can't invoke Window.DialogCancelCommand directly.
                    // Have to raise event.
                    // directly invoke a command.
                    ExecuteCommand(DialogCancelCommand, null, this);
                }
            }
        }

        #endregion "Click"

        #region "KeyDown"

        /// <summary>
        /// This is the method that responds to the KeyDown event.
        /// </summary>
        /// <param name="e">Event arguments</param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (ClickMode == ClickMode.Hover)
            {
                // Ignore when in hover-click mode.
                return;
            }

            if (e.Key == Key.Space)
            {
                // Alt+Space should bring up system menu, we shouldn't
                // handle it.
                if ((Keyboard.Modifiers &
                     (ModifierKeys.Control | ModifierKeys.Alt)) !=
                    ModifierKeys.Alt)
                {
                    if ((!IsMouseCaptured) &&
                        (ReferenceEquals(e.OriginalSource, this)))
                    {
                        IsSpaceKeyDown = true;
                        CaptureMouse();
                        if (ClickMode == ClickMode.Press)
                        {
                            OnClick();
                        }
                        e.Handled = true;
                    }
                }
            }
            else if (e.Key == Key.Enter
                     && Convert.ToBoolean(GetValue(KeyboardNavigation.AcceptsReturnProperty)))
            {
                if (ReferenceEquals(e.OriginalSource, this))
                {
                    IsSpaceKeyDown = false;
                    if (IsMouseCaptured)
                    {
                        ReleaseMouseCapture();
                    }
                    OnClick();
                    e.Handled = true;
                }
            }
            else
            {
                // On any other key we set IsPressed to false only if
                // Space key is pressed
                if (IsSpaceKeyDown)
                {
                    IsSpaceKeyDown = false;
                    if (IsMouseCaptured)
                    {
                        ReleaseMouseCapture();
                    }
                }
            }
        }

        private bool IsSpaceKeyDown
        {
            get { return ReadControlFlag(ControlBoolFlags.IsSpaceKeyDown); }
            set { WriteControlFlag(ControlBoolFlags.IsSpaceKeyDown, value); }
        }

        #endregion "KeyDown"

        #region "IsDefault"

        /// <summary>
        ///     The DependencyProperty for the IsDefault property.
        ///     Flags:              None
        ///     Default Value:      false
        /// </summary>
        public static readonly DependencyProperty IsDefaultProperty =
            DependencyProperty.RegisterAttached(
                "IsDefault",
                typeof(bool),
                typeof(CommandButton),
                new UIPropertyMetadata(
                    false,
                    OnIsDefaultChanged));

        /// <summary>
        /// Specifies whether or not this button is the default button.
        /// </summary>
        /// <value></value>
        public bool IsDefault
        {
            get { return (bool)GetValue(IsDefaultProperty); }
            set { SetValue(IsDefaultProperty, value); }
        }

        private static void OnIsDefaultChanged(
            DependencyObject valTarget,
            DependencyPropertyChangedEventArgs e)
        {
            CommandButton locCommandButton = valTarget as CommandButton;
            if (locCommandButton != null)
            {
                Window locWindow = Window.GetWindow(locCommandButton);
                if (locWindow == null)
                {
                    locWindow = System.Windows.Application.Current.MainWindow;
                }
                if (_focusChangedEventHandler == null)
                {
                    _focusChangedEventHandler =
                        locCommandButton.OnFocusChanged;
                }

                if (locWindow != null)
                {
                    if ((bool)e.NewValue)
                    {
                        AccessKeyManager.Register("\x000D", locCommandButton);
                        KeyboardNavigation.SetAcceptsReturn(
                            locCommandButton, true);
                        locCommandButton.UpdateIsDefaulted(
                            Keyboard.FocusedElement);
                    }
                    else
                    {
                        AccessKeyManager.Unregister("\x000D", locCommandButton);
                        KeyboardNavigation.SetAcceptsReturn(
                            locCommandButton, false);
                        locCommandButton.UpdateIsDefaulted(null);
                    }
                }
            }
        }

        private static KeyboardFocusChangedEventHandler _focusChangedEventHandler;

        private void OnFocusChanged(object valTarget, KeyboardFocusChangedEventArgs e)
        {
            UpdateIsDefaulted(Keyboard.FocusedElement);
        }

        #endregion "IsDefault"

        #region "IsDefaulted"

        /// <summary>
        ///     The key needed set a read-only property.
        /// </summary>
        private static readonly DependencyPropertyKey IsDefaultedPropertyKey =
            DependencyProperty.RegisterReadOnly(
                "IsDefaulted",
                typeof(bool),
                typeof(CommandButton),
                new FrameworkPropertyMetadata(
                    false));

        /// <summary>
        ///     The DependencyProperty for the IsDefaulted property.
        ///     Flags:              None
        ///     Default Value:      false
        /// </summary>
        public static readonly DependencyProperty IsDefaultedProperty =
            IsDefaultedPropertyKey.DependencyProperty;

        /// <summary>
        /// Specifies whether or not this button is the button that
        /// would be invoked when Enter is pressed.
        /// </summary>
        /// <value></value>
        public bool IsDefaulted
        {
            get { return (bool)GetValue(IsDefaultedProperty); }
        }

        private void UpdateIsDefaulted(IInputElement valFocusElement)
        {
            // If it's not a default button, or nothing is focused,
            // or it's disabled
            // then it's not defaulted.
            if (!IsDefault || valFocusElement == null || !IsEnabled)
            {
                SetValue(IsDefaultedPropertyKey, false);
                return;
            }
            var locFocusDependencyObj =
                valFocusElement as DependencyObject;

            // If the focused thing is not in this scope then
            // IsDefaulted = false

            var locIsDefaulted = false;
            try
            {
                // Step 1: Determine the AccessKey scope from currently
                // focused element
                var locEventArgs = new AccessKeyPressedEventArgs();
                valFocusElement.RaiseEvent(locEventArgs);
                var locFocusScope = locEventArgs.Scope;

                // Step 2: Determine the AccessKey scope from this button
                locEventArgs = new AccessKeyPressedEventArgs();
                RaiseEvent(locEventArgs);
                var locThisScope = locEventArgs.Scope;

                // Step 3: Compare scopes
                if (ReferenceEquals(locThisScope, locFocusScope)
                    && (locFocusDependencyObj == null
                        || !(bool)locFocusDependencyObj.GetValue(KeyboardNavigation.AcceptsReturnProperty)))
                {
                    locIsDefaulted = true;
                }
            }
            finally
            {
                SetValue(IsDefaultedPropertyKey, locIsDefaulted);
            }
        }

        #endregion "IsDefaulted"

        #region "IsCancel"

        /// <summary>
        ///     The DependencyProperty for the IsCancel property.
        ///     Flags:              None
        ///     Default Value:      false
        /// </summary>
        public static readonly DependencyProperty IsCancelProperty =
            DependencyProperty.Register(
                "IsCancel",
                typeof(bool),
                typeof(CommandButton),
                new FrameworkPropertyMetadata(
                    false,
                    OnIsCancelChanged));

        /// <summary>
        /// Specifies whether or not this button is the cancel button.
        /// </summary>
        /// <value></value>
        public bool IsCancel
        {
            get { return (bool)GetValue(IsCancelProperty); }
            set { SetValue(IsCancelProperty, value); }
        }

        private static void OnIsCancelChanged(
            DependencyObject valTarget,
            DependencyPropertyChangedEventArgs e)
        {
            CommandButton locCommandButton = valTarget as CommandButton;
            if (locCommandButton != null)
            {
                if ((bool)e.NewValue)
                {
                    AccessKeyManager.Register("\x001B", locCommandButton);
                }
                else
                {
                    AccessKeyManager.Unregister("\x001B", locCommandButton);
                }
            }
        }

        #endregion "IsCancel"

        #region "Helper Functions"

        /// <summary>
        /// This allows a caller to override its ICommandSource values
        //// (used by Button and ScrollBar)
        /// </summary>
        static internal void ExecuteCommand(
            ICommand command,
            object parameter,
            IInputElement target)
        {
            RoutedCommand routed = command as RoutedCommand;
            if (routed != null)
            {
                if (routed.CanExecute(parameter, target))
                {
                    routed.Execute(parameter, target);
                }
            }
            else if (command.CanExecute(parameter))
            {
                command.Execute(parameter);
            }
        }

        internal static bool CanExecuteCommandSource(
            ICommandSource commandSource)
        {
            ICommand command = commandSource.Command;
            if (command != null)
            {
                object parameter = commandSource.CommandParameter;
                IInputElement target = commandSource.CommandTarget;
                RoutedCommand routed = command as RoutedCommand;
                if (routed != null)
                {
                    if (target == null)
                    {
                        target = commandSource as IInputElement;
                    }
                    return routed.CanExecute(parameter, target);
                }
                else
                {
                    return command.CanExecute(parameter);
                }
            }
            return false;
        }

        /// <summary>
        ///     Executes the command on the given command source.
        /// </summary>
        /// <SecurityNote>
        ///     Critical - calls critical function (ExecuteCommandSource).
        ///     TreatAsSafe - always passes in false for userInitiated,
        ////                  which is safe
        /// </SecurityNote>
        [SecurityCritical(), SecuritySafeCritical()]
        static internal void ExecuteCommandSource(
            ICommandSource commandSource)
        {
            CriticalExecuteCommandSource(commandSource, false);
        }

        /// <summary>
        ///     Executes the command on the given command source.
        /// </summary>
        /// <SecurityNote>
        /// Critical - sets the user initiated bit on a command,
        ///            which is used for security purposes later.
        ///            It is important to validate the callers of this,
        ///            and the implementation to make sure
        ///            that we only call MarkAsUserInitiated in the
        ///            correct cases.
        /// </SecurityNote>
        [SecurityCritical()]
        static internal void CriticalExecuteCommandSource(
            ICommandSource commandSource,
            bool userInitiated)
        {
            ICommand command = commandSource.Command;
            if (command != null)
            {
                object parameter = commandSource.CommandParameter;
                IInputElement target = commandSource.CommandTarget;
                RoutedCommand routed = command as RoutedCommand;
                if (routed != null)
                {
                    if (target == null)
                    {
                        target = commandSource as IInputElement;
                    }
                    if (routed.CanExecute(parameter, target))
                    {
                        routed.Execute(parameter, target);
                    }
                }
                else if (command.CanExecute(parameter))
                {
                    command.Execute(parameter);
                }
            }
        }

        /// <summary>
        /// DialogCancel Command. It closes window if it's dialog and return
        /// false as the dialog value.
        /// </summary>
        /// Right now this is only used by Cancel Button to close the dialog.
        private static readonly RoutedCommand DialogCancelCommand =
            new RoutedCommand(
                "DialogCancel",
                typeof(Window));

        #endregion "Helper Functions"

        #region "ControlFlags"

        internal bool ReadControlFlag(ControlBoolFlags reqFlag)
        {
            return (_controlBoolField & reqFlag) != 0;
        }

        internal void WriteControlFlag(ControlBoolFlags reqFlag, bool @set)
        {
            if (@set)
            {
                _controlBoolField = _controlBoolField | reqFlag;
            }
            else
            {
                _controlBoolField = _controlBoolField & (~reqFlag);
            }
        }

        [Flags]
        internal enum ControlBoolFlags : ushort
        {
            ContentIsNotLogical = 0x1,

            // used in contentcontrol.cs
            IsSpaceKeyDown = 0x2,

            // used in ButtonBase.cs
            HeaderIsNotLogical = 0x4,

            // used in HeaderedContentControl.cs, HeaderedItemsControl.cs
            CommandDisabled = 0x8,

            // used in ButtonBase.cs, MenuItem.cs
            ContentIsItem = 0x10,

            // used in contentcontrol.cs
            HeaderIsItem = 0x20,

            // used in HeaderedContentControl.cs, HeaderedItemsControl.cs
            ScrollHostValid = 0x40,

            // used in ItemsControl.cs
            ContainsSelection = 0x80,

            // used in TreeViewItem.cs
            VisualStateChangeSuspended = 0x100

            // used in Control.cs
        }

        #endregion "ControlFlags"
    }
}