namespace My.CoachManager.Presentation.Wpf.Tests
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            var vm = new MainWindowViewModel();

            DataContext = vm;
        }
    }
}
