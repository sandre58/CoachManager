using My.CoachManager.Presentation.Prism.Modules.Home.ViewModels;

namespace My.CoachManager.Presentation.Prism.Modules.Home.Views
{
    /// <summary>
    /// Logique d'interaction pour HomeView1.xaml
    /// </summary>
    public partial class HomeView
    {
        public HomeView(IHomeViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}