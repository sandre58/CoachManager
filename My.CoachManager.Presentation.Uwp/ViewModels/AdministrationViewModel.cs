using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using My.CoachManager.Presentation.Uwp.Controls;
using My.CoachManager.Presentation.Uwp.Core.ViewModels;
using Prism.Commands;
using Prism.Windows.Navigation;

namespace My.CoachManager.Presentation.Uwp.ViewModels
{
    internal class AdministrationViewModel : ScreenViewModel
    {
        #region Fields

        private static INavigationService _navigationService;

        #endregion

        #region Members

        public ICommand ItemInvokedCommand { get; private set; }

        #endregion

        #region Contrstuctors

        public AdministrationViewModel(INavigationService navigationServiceInstance)
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
            ItemInvokedCommand = new DelegateCommand<ItemClickEventArgs>(OnItemInvoked);
        }

        protected override Task LoadDataCoreAsync()
        {
            return Task.CompletedTask;
        }

        #endregion

        #region Navigation

        /// <summary>
        /// When Click on item.
        /// </summary>
        /// <param name="args"></param>
        private void OnItemInvoked(ItemClickEventArgs args)
        {
            if (args.ClickedItem is ExtendedHeaderedContentControl item)
            {
                var param = item.CommandParameter;

                _navigationService.Navigate(param.ToString(), null);
            }
        }
        #endregion

    }
}
