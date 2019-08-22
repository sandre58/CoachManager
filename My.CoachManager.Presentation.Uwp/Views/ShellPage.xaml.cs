// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

using System;
using System.Linq;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using My.CoachManager.CrossCutting.Core.Extensions;
using My.CoachManager.Presentation.Uwp.Controls.Parameters;
using My.CoachManager.Presentation.Uwp.ViewModels;
using NavigationViewItem = Microsoft.UI.Xaml.Controls.NavigationViewItem;

namespace My.CoachManager.Presentation.Uwp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ShellPage
    {
        private ShellViewModel ViewModel => DataContext as ShellViewModel;
        public Frame NavigationFrame { get; set; }

        public ShellPage()
        {
            InitializeComponent();

            // Use custom title bar.
            var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            UpdateTitleBarLayout(coreTitleBar);
            Window.Current.SetTitleBar(AppTitleBar);
            coreTitleBar.LayoutMetricsChanged += (s, a) => UpdateTitleBarLayout(s);
        }

        public void SetRootFrame(Frame frame)
        {
            NavigationFrame = frame;
            ContentFrame.Content = NavigationFrame;
            NavigationFrame.Navigated += NavigationFrame_Navigated;
            NavigationFrame.NavigationFailed += (sender, e) => throw e.Exception;
        }

        private void NavigationFrame_Navigated(object sender, NavigationEventArgs e)
        {
            UpdateAppBackButton();
            UpdateSelectedMenuItem(e.SourcePageType);
        }

        private void UpdateTitleBarLayout(CoreApplicationViewTitleBar coreTitleBar)
        {
            // Get the size of the caption controls area and back button
            // (returned in logical pixels), and move your content around as necessary.
            //var isVisible = SystemNavigationManager
            //                    .GetForCurrentView()
            //                    .AppViewBackButtonVisibility == AppViewBackButtonVisibility.Visible;
            //var width = isVisible ? coreTitleBar.SystemOverlayLeftInset : 0;
            //LeftPaddingColumn.Width = new GridLength(width);
            // Update title bar control size as needed to account for system size changes.
            AppTitleBar.Height = coreTitleBar.Height;
        }

        private void UpdateAppBackButton()
        {
            var backButtonVisibility = NavigationFrame.CanGoBack ?
                AppViewBackButtonVisibility.Visible :
                AppViewBackButtonVisibility.Collapsed;
            var snm = SystemNavigationManager.GetForCurrentView();
            snm.AppViewBackButtonVisibility = backButtonVisibility;
        }

        private void UpdateSelectedMenuItem(Type sourcePageType)
        {
            if (sourcePageType == typeof(SettingsPage))
            {
                NavigationViewControl.SelectedItem = NavigationViewControl.SettingsItem as NavigationViewItem;
                return;
            }

            NavigationViewControl.SelectedItem = NavigationViewControl.MenuItems
                .OfType<NavigationViewItem>()
                .FirstOrDefault(menuItem => IsMenuItemForPageType(menuItem, sourcePageType));
        }

        /// <summary>
        /// If source page type is for specified menu item.
        /// </summary>
        /// <param name="menuItem"></param>
        /// <param name="sourcePageType"></param>
        /// <returns></returns>
        private bool IsMenuItemForPageType(NavigationViewItem menuItem, Type sourcePageType)
        {
            var path = sourcePageType.FullName.TextAfter(".Views.").Replace("Page", "");
            var firstLevel = path.Split(".").FirstOrDefault();
            var pageKey = menuItem.GetValue(NavigationParameters.NavigateToProperty) as string;

            return firstLevel != null && pageKey == firstLevel;
        }
    }
}
