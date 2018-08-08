using System.Collections.Generic;
using System.Windows.Input;
using Microsoft.Practices.ServiceLocation;
using My.CoachManager.Presentation.Prism.Core.Services;

namespace My.CoachManager.Presentation.Prism.Core.Manager
{
    /// <summary>
    /// The keyboard manager.
    /// </summary>
    public static class KeyboardManager
    {
        #region Fields

        private static IKeyBoardService _keyBoardService;

        #endregion Fields

        #region Members

        /// <summary>
        /// Gets Navigation Service.
        /// </summary>
        private static IKeyBoardService KeyBoardService => _keyBoardService ??
                                                               (_keyBoardService = ServiceLocator.Current.GetInstance<IKeyBoardService>());

        public static bool GlobalBindingsIsSuspended
        {
            get => KeyBoardService.GlobalBindingsIsSuspended;
            set => KeyBoardService.GlobalBindingsIsSuspended = value;
        }

        public static bool WorkspaceBindingsIsSuspended
        {
            get => KeyBoardService.WorkspaceBindingsIsSuspended;
            set => KeyBoardService.WorkspaceBindingsIsSuspended = value;
        }

        public static bool WorkspaceDialogBindingsIsSuspended
        {
            get => KeyBoardService.WorkspaceDialogBindingsIsSuspended;
            set => KeyBoardService.WorkspaceDialogBindingsIsSuspended = value;
        }

        #endregion Members

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
            KeyBoardService.RegisterGlobalShortcut(shortcut);
        }

        /// <summary>
        /// Removes the current shortcuts.
        /// </summary>
        /// <param name="shortcuts">The shortcuts.</param>
        public static void RemoveGlobalShortcuts(IEnumerable<KeyBinding> shortcuts)
        {
            KeyBoardService.RemoveGlobalShortcuts(shortcuts);
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
            KeyBoardService.RegisterWorkspaceShortcut(shortcut);
        }

        /// <summary>
        /// Removes the current shortcuts.
        /// </summary>
        /// <param name="shortcuts">The shortcuts.</param>
        public static void RemoveWorkspaceShortcuts(IEnumerable<KeyBinding> shortcuts)
        {
            KeyBoardService.RemoveWorkspaceShortcuts(shortcuts);
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
            KeyBoardService.RegisterWorkspaceDialogShortcut(shortcut);
        }

        /// <summary>
        /// Removes the current shortcuts.
        /// </summary>
        /// <param name="shortcuts">The shortcuts.</param>
        public static void RemoveWorkspaceDialogShortcuts(IEnumerable<KeyBinding> shortcuts)
        {
            KeyBoardService.RemoveWorkspaceDialogShortcuts(shortcuts);
        }

        /// <summary>
        /// Sends the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        public static void Send(Key key)
        {
            KeyBoardService.Send(key);
        }
    }
}