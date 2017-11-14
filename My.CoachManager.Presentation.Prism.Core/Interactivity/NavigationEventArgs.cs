using System;
using Prism.Regions;

namespace My.CoachManager.Presentation.Prism.Core.Interactivity
{
    public class NavigationEventArgs : EventArgs
    {
        #region Members

        /// <summary>
        /// Gets or set the dialog.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the callback action.
        /// </summary>
        public Action<NavigationResult> Callback { get; set; }

        #endregion Members

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="NavigationEventArgs"/>.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="callback"></param>
        public NavigationEventArgs(string path, Action<NavigationResult> callback = null)
        {
            Path = path;
            Callback = callback;
        }

        #endregion Constructors
    }
}