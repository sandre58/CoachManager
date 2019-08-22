using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.UI.Xaml.Controls;
using My.CoachManager.Presentation.Uwp.Constants;
using My.CoachManager.Presentation.Uwp.Controls.Parameters;
using My.CoachManager.Presentation.Uwp.Core.ViewModels;
using Prism.Commands;
using Prism.Windows.Navigation;

namespace My.CoachManager.Presentation.Uwp.ViewModels
{
    internal class ShellViewModel : ScreenViewModel
    {
        #region Fields

        private static INavigationService _navigationService;

        #endregion

        #region Members

        /// <summary>
        /// Gets or sets selected item.
        /// </summary>
        public object SelectedItem { get; set; }

        /// <summary>
        /// Gets commands for menu item.
        /// </summary>
        public ICommand ItemInvokedCommand { get; private set; }

        #endregion

        #region Contrstuctors

        /// <summary>
        /// Initialise a new instance of <see cref="ShellViewModel"/>.
        /// </summary>
        /// <param name="navigationServiceInstance"></param>
        public ShellViewModel(INavigationService navigationServiceInstance)
        {
            _navigationService = navigationServiceInstance;
        }

        #endregion

        #region Initialization

        /// <summary>
        /// Initialize commands.
        /// </summary>
        protected override void InitializeCommands()
        {
            base.InitializeCommands();
            ItemInvokedCommand = new DelegateCommand<NavigationViewItemInvokedEventArgs>(OnItemInvoked);
        }

        #endregion

        #region Navigation

        /// <summary>
        /// When Click on item.
        /// </summary>
        /// <param name="args"></param>
        private void OnItemInvoked(NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                _navigationService.Navigate(PageTokens.SettingsPage, null);
                return;
            }

            var pageKey = args.InvokedItemContainer.GetValue(NavigationParameters.NavigateToProperty) as string;
            _navigationService.Navigate(pageKey, null);
        }

        #endregion

        protected override Task LoadDataCoreAsync()
        {
            return Task.CompletedTask;
        }
    }
}
