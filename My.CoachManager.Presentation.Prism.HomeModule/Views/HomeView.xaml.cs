using My.CoachManager.Presentation.Prism.HomeModule.ViewModels;

namespace My.CoachManager.Presentation.Prism.HomeModule.Views
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