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
        private static readonly IList<KeyBinding> CurrentBindings;
        
        /// <summary>
        /// Set default binding collection.
        /// </summary>
        static KeyboardManager()
        {
            CurrentBindings = new List<KeyBinding>();

            EventManager.RegisterClassHandler(typeof(Window), Keyboard.PreviewKeyDownEvent, new KeyEventHandler(OnPreviewKeyDown), true);
        }

        /// <summary>
        /// Registers the current shortcuts.
        /// </summary>
        /// <param name="shortcuts">The shortcuts.</param>
        public static void RegisterCurrentShortcuts(IEnumerable<KeyBinding> shortcuts)
        {
            // Register them in the binding collection
            foreach (var shortcut in shortcuts)
            {
                RegisterCurrentShortcuts(shortcut);
            }
        }

        /// <summary>
        /// Registers the current shortcuts.
        /// </summary>
        /// <param name="shortcut">The shortcut.</param>
        public static void RegisterCurrentShortcuts(KeyBinding shortcut)
        {
            // Register them in the binding collection
            CurrentBindings.Add(shortcut);
        }

        /// <summary>
        /// Removes the current shortcuts.
        /// </summary>
        /// <param name="shortcuts">The shortcuts.</param>
        public static void RemoveCurrentShortcuts(IEnumerable<KeyBinding> shortcuts)
        {
            foreach (var shortcut in shortcuts)
            {
                if (!CurrentBindings.Contains(shortcut))
                {
                    continue;
                }

                var index = CurrentBindings.IndexOf(shortcut);
                CurrentBindings.RemoveAt(index);
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
                // Get the key binding
                var keyBinding = CurrentBindings.FirstOrDefault(w => w.Key == e.Key && w.Modifiers == Keyboard.Modifiers);
                keyBinding?.Command.Execute(keyBinding.CommandParameter);
            }
            catch (Exception exception)
            {
                ServiceLocator.Current.TryResolve<ILogger>().Error(exception);
            }
        }

    }
}