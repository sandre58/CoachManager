using My.CoachManager.Presentation.Prism.Modules.StatusBar.ViewModels;

namespace My.CoachManager.Presentation.Prism.Modules.StatusBar.Views
{
    /// <summary>
    /// Logique d'interaction pour StatusBarView.xaml
    /// </summary>
    public partial class StatusBarView
    {
        public StatusBarView(IStatusBarViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}