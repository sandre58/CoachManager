using My.CoachManager.Presentation.Models;
using My.CoachManager.Presentation.Wpf.Core.Dialog;
using My.CoachManager.Presentation.Wpf.Core.Helpers;
using My.CoachManager.Presentation.Wpf.Core.Manager;
using My.CoachManager.Presentation.Wpf.Core.ViewModels.Base;
using My.CoachManager.Presentation.Wpf.Modules.Roster.Views;
using My.CoachManager.Presentation.Wpf.Modules.Shared;
using Prism.Commands;

namespace My.CoachManager.Presentation.Wpf.Modules.Roster.ViewModels
{
    public class RosterNavigationViewModel : ScreenViewModel
    {
        #region Members

        /// <summary>
        /// Gets or sets the edit squad command.
        /// </summary>
        public DelegateCommand<SquadModel> EditSquadCommand { get; set; }

        /// <summary>
        /// Gets or sets selectable rosters.
        /// </summary>
        public RosterModel Roster { get; set; }

        #endregion Members

        #region Initialisation

        /// <inheritdoc />
        /// <summary>
        /// Launch on constructor for initialize all command property.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();

            EditSquadCommand = new DelegateCommand<SquadModel>(EditSquad);

            Roster = AppManager.Roster;

        }

    #endregion Initialisation

        #region EditSquad

        /// <summary>
        /// Edit squad.
        /// </summary>
        private void EditSquad(SquadModel item)
        {
            DialogManager.ShowEditDialog<SquadEditViewModel>(item.Id, dialog =>
            {
                if (dialog.Result == DialogResult.Ok)
                {
                    ApplicationHelper.Restart();
                }
            });
        }

        #endregion EditSquad
    }
}