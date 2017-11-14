using My.CoachManager.Presentation.Prism.Wpf.ViewModels;

namespace My.CoachManager.Presentation.Prism.Wpf.Views
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class Shell
    {
        public Shell(IShellViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}