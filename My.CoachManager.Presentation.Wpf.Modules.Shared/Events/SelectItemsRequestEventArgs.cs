using My.CoachManager.Presentation.Wpf.Core.Dialog;
using My.CoachManager.Presentation.Wpf.Core.Enums;
using System;
using System.Collections;
using System.Collections.Generic;

namespace My.CoachManager.Presentation.Wpf.Modules.Shared.Events
{
    public class SelectItemsRequestEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets id.
        /// </summary>
        public SelectionMode SelectionMode { get; }

        /// <summary>
        /// Gets or sets id.
        /// </summary>
        public IList NotSelectetableItems { get; }

        /// <summary>
        /// Gets or sets action.
        /// </summary>
        public Action<IWorkspaceDialog> Callback { get; }

        /// <summary>
        /// Gets or sets parameters.
        /// </summary>
        public IEnumerable<KeyValuePair<string, object>> Parameters { get; }

        public SelectItemsRequestEventArgs(Action<IWorkspaceDialog> callback = null, SelectionMode selectionMode = SelectionMode.Single, IList notSelectableItems = null, IEnumerable<KeyValuePair<string, object>> parameters = null)
        {
            SelectionMode = selectionMode;
            NotSelectetableItems = notSelectableItems;
            Callback = callback;
            Parameters = parameters;
        }
    }
}