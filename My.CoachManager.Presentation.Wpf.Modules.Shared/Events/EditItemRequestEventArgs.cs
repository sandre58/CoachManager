using My.CoachManager.Presentation.Wpf.Core.Dialog;
using System;
using System.Collections.Generic;

namespace My.CoachManager.Presentation.Wpf.Modules.Shared.Events
{
    public class EditItemRequestEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets id.
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Gets or sets action.
        /// </summary>
        public Action<IWorkspaceDialog> Callback { get; }

        /// <summary>
        /// Gets or sets parameters.
        /// </summary>
        public IEnumerable<KeyValuePair<string, object>> Parameters { get; }

        public EditItemRequestEventArgs(int id, Action<IWorkspaceDialog> callback = null, IEnumerable<KeyValuePair<string, object>> parameters = null)
        {
            Id = id;
            Callback = callback;
            Parameters = parameters;
        }
    }
}