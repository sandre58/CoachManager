using System;

using My.CoachManager.Presentation.Uwp.Tests.ViewModels;

using Windows.UI.Xaml.Controls;

namespace My.CoachManager.Presentation.Uwp.Tests.Views
{
    // TODO WTS: Change the URL for your privacy policy in the Resource File, currently set to https://YourPrivacyUrlGoesHere
    public sealed partial class SettingsPage : Page
    {
        private SettingsViewModel ViewModel => DataContext as SettingsViewModel;

        public SettingsPage()
        {
            InitializeComponent();
        }
    }
}
