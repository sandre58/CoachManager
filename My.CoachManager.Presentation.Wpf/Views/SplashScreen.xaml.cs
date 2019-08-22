using My.CoachManager.Presentation.Wpf.ViewModels;

namespace My.CoachManager.Presentation.Wpf.Views
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