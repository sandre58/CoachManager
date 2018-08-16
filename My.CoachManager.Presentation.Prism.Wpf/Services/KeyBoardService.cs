using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Microsoft.Practices.ServiceLocation;
using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.Presentation.Prism.Core.Services;
using My.CoachManager.Presentation.Prism.Wpf.Views;
using SplashScreen = My.CoachManager.Presentation.Prism.Wpf.Views.SplashScreen;

namespace My.CoachManager.Presentation.Prism.Wpf.Services
{
    /// <summary>
    /// The keyboard manager.
    /// </summary>
    public class KeyboardService : IKeyBoardService
    {
        #region Fields

        /// <summary>
        /// The not intercept control.
        /// </summary>
        private static readonly List<Type> NotInterceptControl = new List<Type>
        {
            typeof(CustomDialog),
            typeof(LoginDialog),
            typeof(MessageDialog),
            typeof(SplashScreen)
        };

        /// <summary>
        /// The current bindings.
        /// </summary>
        private readonly IList<KeyBinding> _globalBindings;

        /// <summary>
        /// The current bindings.
        /// </summary>
        private readonly IList<KeyBinding> _workspaceBindings;

        /// <summary>
        /// The current bindings.
        /// </summary>
        private readonly IList<KeyBinding> _workspaceDialogBindings;

        #endregion Fields

        #region Members

        public bool GlobalBindingsIsSuspended { get; set; }

        public bool WorkspaceBindingsIsSuspended { get; set; }

        public bool WorkspaceDialogBindingsIsSuspended { get; set; }

        #endregion Members

        #region Constructors

        /// <summary>
        /// Initialise a new instance.
        /// </summary>
        public KeyboardService()
        {
            _globalBindings = new List<KeyBinding>();
            _workspaceBindings = new List<KeyBinding>();
            _workspaceDialogBindings = new List<KeyBinding>();

            EventManager.RegisterClassHandler(typeof(Window), Keyboard.PreviewKeyDownEvent, new KeyEventHandler(OnPreviewKeyDown), true);
        }

        #endregion Constructors

        /// <summary>
        /// Registers the current shortcuts.
        /// </summary>
        /// <param name="shortcut">The shortcut.</param>
        public void RegisterGlobalShortcut(KeyBinding shortcut)
        {
            // Register them in the binding collection
            _globalBindings.Add(shortcut);
        }

        /// <summary>
        /// Removes the current shortcuts.
        /// </summary>
        /// <param name="shortcuts">The shortcuts.</param>
        public void RemoveGlobalShortcuts(IEnumerable<KeyBinding> shortcuts)
        {
            foreach (var shortcut in shortcuts)
            {
                if (!_globalBindings.Contains(shortcut))
                {
                    continue;
                }

                var index = _globalBindings.IndexOf(shortcut);
                _globalBindings.RemoveAt(index);
            }
        }

        /// <summary>
        /// Registers the current shortcuts.
        /// </summary>
        /// <param name="shortcut">The shortcut.</param>
        public void RegisterWorkspaceShortcut(KeyBinding shortcut)
        {
            // Register them in the binding collection
            _workspaceBindings.Add(shortcut);
        }

        /// <summary>
        /// Removes the current shortcuts.
        /// </summary>
        /// <param name="shortcuts">The shortcuts.</param>
        public void RemoveWorkspaceShortcuts(IEnumerable<KeyBinding> shortcuts)
        {
            foreach (var shortcut in shortcuts)
            {
                if (!_workspaceBindings.Contains(shortcut))
                {
                    continue;
                }

                var index = _workspaceBindings.IndexOf(shortcut);
                _workspaceBindings.RemoveAt(index);
            }
        }

        /// <summary>
        /// Registers the current shortcuts.
        /// </summary>
        /// <param name="shortcut">The shortcut.</param>
        public void RegisterWorkspaceDialogShortcut(KeyBinding shortcut)
        {
            // Register them in the binding collection
            _workspaceDialogBindings.Add(shortcut);
        }

        /// <summary>
        /// Removes the current shortcuts.
        /// </summary>
        /// <param name="shortcuts">The shortcuts.</param>
        public void RemoveWorkspaceDialogShortcuts(IEnumerable<KeyBinding> shortcuts)
        {
            foreach (var shortcut in shortcuts)
            {
                if (!_workspaceDialogBindings.Contains(shortcut))
                {
                    continue;
                }

                var index = _workspaceDialogBindings.IndexOf(shortcut);
                _workspaceDialogBindings.RemoveAt(index);
            }
        }

        /// <summary>
        /// Sends the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        public void Send(Key key)
        {
            if (Keyboard.PrimaryDevice == null)
            {
                return;
            }

            if (Keyboard.PrimaryDevice.ActiveSource == null)
            {
                return;
            }

            // Send the Preview Key Down Event
            InputManager.Current.ProcessInput(new KeyEventArgs(Keyboard.PrimaryDevice, Keyboard.PrimaryDevice.ActiveSource, 0, key)
            {
                RoutedEvent = Keyboard.PreviewKeyDownEvent
            });

            // Send the Key Down Event
            InputManager.Current.ProcessInput(new KeyEventArgs(Keyboard.PrimaryDevice, Keyboard.PrimaryDevice.ActiveSource, 0, key)
            {
                RoutedEvent = Keyboard.KeyDownEvent
            });
        }

        /// <summary>
        /// Called when [preview key down].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="KeyEventArgs" /> instance containing the event data.</param>
        private void OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            // Not intercept the Esc key for toogle  Menu
            if (NotInterceptControl.Any(x => sender.GetType() == x))
            {
                return;
            }

            try
            {
                // WorkspaceDialog Bindings
                if (!WorkspaceDialogBindingsIsSuspended)
                {
                    var keyBinding = _workspaceDialogBindings.FirstOrDefault(w =>
                        w.Key == e.Key && w.Modifiers == Keyboard.Modifiers);
                    if (keyBinding != null)
                    {
                        keyBinding.Command.Execute(keyBinding.CommandParameter);
                        return;
                    }
                }

                // Workspace Bindings
                if (!WorkspaceBindingsIsSuspended)
                {
                    var keyBinding =
                        _workspaceBindings.FirstOrDefault(w => w.Key == e.Key && w.Modifiers == Keyboard.Modifiers);
                    if (keyBinding != null)
                    {
                        keyBinding.Command.Execute(keyBinding.CommandParameter);
                        return;
                    }
                }

                // Global Bindings
                if (GlobalBindingsIsSuspended) return;

                var keyBinding1 =
                    _globalBindings.FirstOrDefault(w => w.Key == e.Key && w.Modifiers == Keyboard.Modifiers);
                keyBinding1?.Command.Execute(keyBinding1.CommandParameter);
            }
            catch (Exception exception)
            {
                ServiceLocator.Current.TryResolve<ILogger>().Error(exception);
            }
        }
    }
}