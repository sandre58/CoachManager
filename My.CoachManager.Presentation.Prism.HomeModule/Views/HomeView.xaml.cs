using My.CoachManager.Presentation.Prism.Home.ViewModels;

namespace My.CoachManager.Presentation.Prism.Home.Views
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