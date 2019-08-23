using CommonServiceLocator;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using My.CoachManager.Presentation.Wpf.Core.Manager;
using My.CoachManager.Presentation.Wpf.Messages;
using My.CoachManager.Presentation.Wpf.ViewModels.Misc;

namespace My.CoachManager.Presentation.Wpf.Commands
{
    public static class AppCommandsManager
    {

        private static readonly IMessenger Messenger = ServiceLocator.Current.GetInstance<IMessenger>();

        /// <summary>
        /// Gets or sets command.
        /// </summary>
        public static RelayCommand ToggleSettingsCommand { get; } = new RelayCommand(() => Messenger.Send(new ToggleSettingsRequestMessage()));

        /// <summary>
        /// Gets or sets command.
        /// </summary>
        public static RelayCommand ShowAboutCommand { get; } = new RelayCommand(() =>
        {
            DialogManager.ShowDialog<AboutViewModel>();
        });

        // /// <summary>
        // /// Gets or sets command.
        // /// </summary>
        //public static RelayCommand<RosterPlayerModel> EditRosterPlayerCommand { get; } = new RelayCommand<RosterPlayerModel>(x => Messenger.GetEvent<EditRosterPlayerRequestEvent>().Publish(new EditItemRequestEventArgs(x.Id,
        //     dialog =>
        //     {
        //         if (dialog.Result == DialogResult.Ok && dialog.Content is IItemViewModel model)
        //         {
        //             x.SetProperties(model.Item);
        //         }
        //     })));

        // /// <summary>
        // /// Gets or sets command.
        // /// </summary>
        // public static RelayCommand<RosterPlayerModel> NavigateToPlayerCommand { get; } = new RelayCommand<RosterPlayerModel>(x => NavigationManager.NavigateTo("RosterPlayerViewModel", x.Id));
    }
}
