using My.CoachManager.Presentation.Wpf.Core.Dialog;
using My.CoachManager.Presentation.Wpf.Core.Enums;
using System;
using System.Collections;
using System.Collections.Generic;

namespace My.CoachManager.Presentation.Wpf.Modules.Shared.Events
{
    public class SelectSquadsRequestEventArgs : SelectItemsRequestEventArgs
    {
        /// <summary>
        /// Gets or sets id.
        /// </summary>
        public int RosterId { get; }
        
        public SelectSquadsRequestEventArgs(int rosterId, Action<IWorkspaceDialog> callback = null, SelectionMode selectionMode = SelectionMode.Single, IList notSelectableItems = null, IEnumerable<KeyValuePair<string, object>> parameters = null) : base(callback,selectionMode, notSelectableItems, parameters)
        {
            RosterId = rosterId;
        }
    }
}