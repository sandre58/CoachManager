using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Microsoft.Practices.ServiceLocation;
using My.CoachManager.CrossCutting.Logging;

namespace My.CoachManager.Presentation.Prism.Core.Manager
{
    /// <summary>
    /// The keyboard manager.
    /// </summary>
    public static class KeyboardManager
    {
        /// <summary>
        /// The current bindings.
        /// </summary>
        private static readonly IList<KeyBinding> GlobalBindings;

        /// <summary>
        /// The current bindings.
        /// </summary>
        private static readonly IList<KeyBinding> WorkspaceBindings;

        /// <summary>
        /// The current bindings.
        /// </summary>
        private static readonly IList<KeyBinding> WorkspaceDialogBindings;

        public static bool GlobalBindingsIsSuspended;

        public static bool WorkspaceBindingsIsSuspended;

        public static bool WorkspaceDialogBindingsIsSuspended;

        /// <summary>
        /// Set default binding collection.
        /// </summary>
        static KeyboardManager()
        {
            GlobalBindings = new List<KeyBinding>();
            WorkspaceBindings = new List<KeyBinding>();
            WorkspaceDialogBindings = new List<KeyBinding>();

            EventManager.RegisterClassHandler(typeof(Window), Keyboard.PreviewKeyDownEvent, new KeyEventHandler(OnPreviewKeyDown), true);
        }

        /// <summary>
        /// Registers the current shortcuts.
        /// </summary>
        /// <param name="shortcuts">The shortcuts.</param>
        public static void RegisterGlobalShortcuts(IEnumerable<KeyBinding> shortcuts)
        {
            // Register them in the binding collection
            foreach (var shortcut in shortcuts)
            {
                RegisterGlobalShortcut(shortcut);
            }
        }

        /// <summary>
        /// Registers the current shortcuts.
        /// </summary>
        /// <param name="shortcut">The shortcut.</param>
        public static void RegisterGlobalShortcut(KeyBinding shortcut)
        {
            // Register them in the binding collection
            GlobalBindings.Add(shortcut);
        }

        /// <summary>
        /// Removes the current shortcuts.
        /// </summary>
        /// <param name="shortcuts">The shortcuts.</param>
        public static void RemoveGlobalShortcuts(IEnumerable<KeyBinding> shortcuts)
        {
            foreach (var shortcut in shortcuts)
            {
                if (!GlobalBindings.Contains(shortcut))
                {
                    continue;
                }

                var index = GlobalBindings.IndexOf(shortcut);
                GlobalBindings.RemoveAt(index);
            }
        }

        /// <summary>
        /// Registers the current shortcuts.
        /// </summary>
        /// <param name="shortcuts">The shortcuts.</param>
        public static void RegisterWorkspaceShortcuts(IEnumerable<KeyBinding> shortcuts)
        {
            // Register them in the binding collection
            foreach (var shortcut in shortcuts)
            {
                RegisterWorkspaceShortcut(shortcut);
            }
        }

        /// <summary>
        /// Registers the current shortcuts.
        /// </summary>
        /// <param name="shortcut">The shortcut.</param>
        public static void RegisterWorkspaceShortcut(KeyBinding shortcut)
        {
            // Register them in the binding collection
            WorkspaceBindings.Add(shortcut);
        }

        /// <summary>
        /// Removes the current shortcuts.
        /// </summary>
        /// <param name="shortcuts">The shortcuts.</param>
        public static void RemoveWorkspaceShortcuts(IEnumerable<KeyBinding> shortcuts)
        {
            foreach (var shortcut in shortcuts)
            {
                if (!WorkspaceBindings.Contains(shortcut))
                {
                    continue;
                }

                var index = WorkspaceBindings.IndexOf(shortcut);
                WorkspaceBindings.RemoveAt(index);
            }
        }

        /// <summary>
        /// Registers the current shortcuts.
        /// </summary>
        /// <param name="shortcuts">The shortcuts.</param>
        public static void RegisterWorkspaceDialogShortcuts(IEnumerable<KeyBinding> shortcuts)
        {
            // Register them in the binding collection
            foreach (var shortcut in shortcuts)
            {
                RegisterWorkspaceDialogShortcut(shortcut);
            }
        }

        /// <summary>
        /// Registers the current shortcuts.
        /// </summary>
        /// <param name="shortcut">The shortcut.</param>
        public static void RegisterWorkspaceDialogShortcut(KeyBinding shortcut)
        {
            // Register them in the binding collection
            WorkspaceDialogBindings.Add(shortcut);
        }

        /// <summary>
        /// Removes the current shortcuts.
        /// </summary>
        /// <param name="shortcuts">The shortcuts.</param>
        public static void RemoveWorkspaceDialogShortcuts(IEnumerable<KeyBinding> shortcuts)
        {
            foreach (var shortcut in shortcuts)
            {
                if (!WorkspaceDialogBindings.Contains(shortcut))
                {
                    continue;
                }

                var index = WorkspaceDialogBindings.IndexOf(shortcut);
                WorkspaceDialogBindings.RemoveAt(index);
            }
        }

        /// <summary>
        /// Sends the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        public static void Send(Key key)
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
        private static void OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                // WorkspaceDialog Bindings
                if (!WorkspaceDialogBindingsIsSuspended)
                {
                    var keyBinding = WorkspaceDialogBindings.FirstOrDefault(w => w.Key == e.Key && w.Modifiers == Keyboard.Modifiers);
                    if (keyBinding != null)
                    {
                        keyBinding.Command.Execute(keyBinding.CommandParameter);
                        return;
                    }
                }

                // Workspace Bindings
                if (!WorkspaceBindingsIsSuspended)
                {
                    var keyBinding = WorkspaceBindings.FirstOrDefault(w => w.Key == e.Key && w.Modifiers == Keyboard.Modifiers);
                    if (keyBinding != null)
                    {
                        keyBinding.Command.Execute(keyBinding.CommandParameter);
                        return;
                    }
                }

                // Global Bindings
                if (GlobalBindingsIsSuspended) return;

                var keyBinding1 = GlobalBindings.FirstOrDefault(w => w.Key == e.Key && w.Modifiers == Keyboard.Modifiers);
                keyBinding1?.Command.Execute(keyBinding1.CommandParameter);
            }
            catch (Exception exception)
            {
                ServiceLocator.Current.TryResolve<ILogger>().Error(exception);
            }
        }
    }
}