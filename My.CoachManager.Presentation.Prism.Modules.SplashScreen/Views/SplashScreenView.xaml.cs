using My.CoachManager.Presentation.Prism.Modules.SplashScreen.ViewModels;

namespace My.CoachManager.Presentation.Prism.Modules.SplashScreen.Views
{
    /// <summary>
    /// Interaction logic for SplashScreen.xaml
    /// </summary>
    public partial class SplashScreenView
    {
        public SplashScreenView(ISplashScreenViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}