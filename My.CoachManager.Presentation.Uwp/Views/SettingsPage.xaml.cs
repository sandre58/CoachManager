using My.CoachManager.Presentation.Uwp.ViewModels;

namespace My.CoachManager.Presentation.Uwp.Views
{
    public sealed partial class SettingsPage
    {
        private SettingsViewModel ViewModel => DataContext as SettingsViewModel;

        public SettingsPage()
        {
            InitializeComponent();
        }
    }
}
