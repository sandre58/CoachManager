using My.CoachManager.Presentation.Prism.Core;
using My.CoachManager.Presentation.Prism.Modules.Roster.ViewModels;

namespace My.CoachManager.Presentation.Prism.Modules.Roster.Views
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