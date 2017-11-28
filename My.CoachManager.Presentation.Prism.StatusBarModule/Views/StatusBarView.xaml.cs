using My.CoachManager.Presentation.Prism.StatusBarModule.ViewModels;

namespace My.CoachManager.Presentation.Prism.StatusBarModule.Views
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