using My.CoachManager.Presentation.Prism.Modules.Settings.ViewModels;

namespace My.CoachManager.Presentation.Prism.Modules.Settings.Views
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView
    {
        public SettingsView(ISettingsViewModel model)
        {
            DataContext = model;
            InitializeComponent();
        }
    }
}