using System.ComponentModel;
using System.Data;

namespace My.CoachManager.Presentation.Prism.Tests
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new PresentationModel();
        }
    }
}