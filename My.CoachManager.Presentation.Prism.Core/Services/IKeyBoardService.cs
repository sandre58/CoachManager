using System.Collections.Generic;
using System.Windows.Input;

namespace My.CoachManager.Presentation.Prism.Core.Services
{
    /// <summary>
    /// The keyboard manager.
    /// </summary>
    public interface IKeyBoardService
    {
        #region Members

        bool GlobalBindingsIsSuspended { get; set; }

        bool WorkspaceBindingsIsSuspended { get; set; }

        bool WorkspaceDialogBindingsIsSuspended { get; set; }

        #endregion

        #region Methods

        

        #endregion

        /// <summary>
        /// Registers the current shortcuts.
        /// </summary>
        /// <param name="shortcut">The shortcut.</param>
        void RegisterGlobalShortcut(KeyBinding shortcut);

        /// <summary>
        /// Removes the current shortcuts.
        /// </summary>
        /// <param name="shortcuts">The shortcuts.</param>
        void RemoveGlobalShortcuts(IEnumerable<KeyBinding> shortcuts);

        /// <summary>
        /// Registers the current shortcuts.
        /// </summary>
        /// <param name="shortcut">The shortcut.</param>
        void RegisterWorkspaceShortcut(KeyBinding shortcut);

        /// <summary>
        /// Removes the current shortcuts.
        /// </summary>
        /// <param name="shortcuts">The shortcuts.</param>
        void RemoveWorkspaceShortcuts(IEnumerable<KeyBinding> shortcuts);

        /// <summary>
        /// Registers the current shortcuts.
        /// </summary>
        /// <param name="shortcut">The shortcut.</param>
        void RegisterWorkspaceDialogShortcut(KeyBinding shortcut);

        /// <summary>
        /// Removes the current shortcuts.
        /// </summary>
        /// <param name="shortcuts">The shortcuts.</param>
        void RemoveWorkspaceDialogShortcuts(IEnumerable<KeyBinding> shortcuts);

        /// <summary>
        /// Sends the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        void Send(Key key);
    }
}