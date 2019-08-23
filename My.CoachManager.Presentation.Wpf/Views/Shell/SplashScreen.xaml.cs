using My.CoachManager.Presentation.Wpf.ViewModels.Shell;

namespace My.CoachManager.Presentation.Wpf.Views.Shell
{
    /// <summary>
    /// Interaction logic for SplashScreen.xaml
    /// </summary>
    public partial class SplashScreen
    {
        public SplashScreen(SplashScreenViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}