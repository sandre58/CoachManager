using My.CoachManager.Presentation.Prism.Core;
using My.CoachManager.Presentation.Prism.RosterModule.ViewModels;

namespace My.CoachManager.Presentation.Prism.RosterModule.Views
{
    /// <summary>
    /// Logique d'interaction pour CategoriesListView.xaml
    /// </summary>
    public partial class PlayersListView
    {
        public PlayersListView(IPlayersListViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }

        public PlayersListView()
        {
            DataContext = Locator.GetInstance<IPlayersListViewModel>();
            InitializeComponent();
        }
    }
}