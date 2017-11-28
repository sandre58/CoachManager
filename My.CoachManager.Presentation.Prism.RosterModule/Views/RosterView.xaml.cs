using My.CoachManager.Presentation.Prism.RosterModule.ViewModels;

namespace My.CoachManager.Presentation.Prism.RosterModule.Views
{
    /// <summary>
    /// Logique d'interaction pour RosterView.xaml
    /// </summary>
    public partial class RosterView
    {
        public RosterView(IRosterViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}