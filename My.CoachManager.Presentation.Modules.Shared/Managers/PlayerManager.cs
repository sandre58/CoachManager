using My.CoachManager.Presentation.Core.Dialog;
using My.CoachManager.Presentation.Core.Manager;
using My.CoachManager.Presentation.Models;
using My.CoachManager.Presentation.Modules.Shared.Interfaces;
using Prism.Commands;
using System;
using My.CoachManager.Presentation.Core.ViewModels.Interfaces;

namespace My.CoachManager.Presentation.Modules.Shared.Managers
{
    public static class PlayerManager
    {
        #region Constants

        public const string RosterPlayerView = "RosterPlayerView";

#endregion

        #region Commands

        /// <summary>
        /// Gets Global Navigate command.
        /// </summary>
        public static DelegateCommand<RosterPlayerModel> EditPlayerCommand => new DelegateCommand<RosterPlayerModel>(x => EditPlayer(x));

        /// <summary>
        /// Gets Global Navigate command.
        /// </summary>
        public static DelegateCommand<RosterPlayerModel> NavigateToPlayerCommand => new DelegateCommand<RosterPlayerModel>(NavigateToPlayer);

        #endregion Commands

        #region Methods

        /// <summary>
        /// Edit a player.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="callback"></param>
        public static void EditPlayer(RosterPlayerModel item, Action<IWorkspaceDialog> callback = null)
        {
            if (item == null) return;

            DialogManager.ShowEditDialog<IRosterPlayerEditView>(item.Id, dialog =>
            {
                if (dialog.Result == DialogResult.Ok)
                {
                    if(dialog.Content.DataContext is IItemViewModel model)
                    item.SetProperties(model.Item);
                }

                callback?.Invoke(dialog);
            });
        }

        /// <summary>
        /// Edit a player.
        /// </summary>
        /// <param name="item"></param>
        public static void NavigateToPlayer(RosterPlayerModel item)
        {
            NavigationManager.NavigateTo(RosterPlayerView, item.Id);
        }

        #endregion Methods
    }
}