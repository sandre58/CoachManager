using My.CoachManager.Presentation.Core.Dialog;
using My.CoachManager.Presentation.Core.Manager;
using My.CoachManager.Presentation.Models;
using My.CoachManager.Presentation.Modules.Shared.Interfaces;
using Prism.Commands;
using System;

namespace My.CoachManager.Presentation.Modules.Shared.Managers
{
    public static class PlayerManager
    {
        #region Commands

        /// <summary>
        /// Gets Global Navigate command.
        /// </summary>
        public static DelegateCommand<RosterPlayerModel> EditPlayerCommand => new DelegateCommand<RosterPlayerModel>(x => EditPlayer(x));

        #endregion Commands

        #region Methods

        /// <summary>
        /// Edit a player.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="callback"></param>
        private static void EditPlayer(RosterPlayerModel item, Action<IWorkspaceDialog> callback = null)
        {
            if (item == null) return;

            DialogManager.ShowEditDialog<IRosterPlayerEditView>(item.Id, dialog =>
            {
                if (dialog.Result == DialogResult.Ok)
                {
                    // TODO: Refresh item
                }

                callback?.Invoke(dialog);
            });
        }

        #endregion Methods
    }
}