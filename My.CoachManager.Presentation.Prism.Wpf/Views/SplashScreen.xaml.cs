using My.CoachManager.Presentation.Prism.Wpf.ViewModels;

namespace My.CoachManager.Presentation.Prism.Wpf.Views
{
    /// <summary>
    /// Interaction logic for SplashScreen.xaml
    /// </summary>
    public partial class SplashScreen
    {
        public SplashScreen(ISplashScreenViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}