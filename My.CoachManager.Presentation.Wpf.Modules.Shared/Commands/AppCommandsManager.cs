using CommonServiceLocator;
using My.CoachManager.Presentation.Models;
using My.CoachManager.Presentation.Wpf.Core.Dialog;
using My.CoachManager.Presentation.Wpf.Core.Manager;
using My.CoachManager.Presentation.Wpf.Core.ViewModels.Interfaces;
using My.CoachManager.Presentation.Wpf.Modules.Shared.Events;
using Prism.Commands;
using Prism.Events;

namespace My.CoachManager.Presentation.Wpf.Modules.Shared.Commands
{
    public static class AppCommandsManager
    {

        private static readonly IEventAggregator EventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();

        /// <summary>
        /// Gets or sets command.
        /// </summary>
        public static DelegateCommand ToggleSettingsCommand { get; } = new DelegateCommand(() => EventAggregator.GetEvent<ToggleSettingsRequestEvent>().Publish());

        /// <summary>
        /// Gets or sets command.
        /// </summary>
        public static DelegateCommand ShowAboutCommand { get; } = new DelegateCommand(() => EventAggregator.GetEvent<ShowAboutViewRequestEvent>().Publish());

        /// <summary>
        /// Gets or sets command.
        /// </summary>
       public static DelegateCommand<RosterPlayerModel> EditRosterPlayerCommand { get; } = new DelegateCommand<RosterPlayerModel>(x => EventAggregator.GetEvent<EditRosterPlayerRequestEvent>().Publish(new EditItemRequestEventArgs(x.Id,
            dialog =>
            {
                if (dialog.Result == DialogResult.Ok && dialog.Content is IItemViewModel model)
                {
                    x.SetProperties(model.Item);
                }
            })));

        /// <summary>
        /// Gets or sets command.
        /// </summary>
        public static DelegateCommand<RosterPlayerModel> NavigateToPlayerCommand { get; } = new DelegateCommand<RosterPlayerModel>(x => NavigationManager.NavigateTo("RosterPlayerViewModel", x.Id));
    }
}
