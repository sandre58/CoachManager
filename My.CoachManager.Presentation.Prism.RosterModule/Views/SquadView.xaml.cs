using My.CoachManager.Presentation.Prism.RosterModule.ViewModels;

namespace My.CoachManager.Presentation.Prism.RosterModule.Views
{
    /// <summary>
    /// Logique d'interaction pour SquadView.xaml
    /// </summary>
    public partial class SquadView
    {
        public SquadView(ISquadViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }

        public SquadView()
        {
            InitializeComponent();
        }
    }
}