using My.CoachManager.Presentation.Prism.Modules.Settings.ViewModels;

namespace My.CoachManager.Presentation.Prism.Modules.Settings.Views
{
    /// <summary>
    /// Interaction logic for SkinSettingsView.xaml
    /// </summary>
    public partial class SkinSettingsView
    {
        public SkinSettingsView(ISkinSettingsViewModel model)
        {
            DataContext = model;
            InitializeComponent();
        }
    }
}