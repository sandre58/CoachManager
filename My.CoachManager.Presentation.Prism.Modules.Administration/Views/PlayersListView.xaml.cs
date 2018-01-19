using My.CoachManager.Presentation.Prism.Modules.Administration.ViewModels;

namespace My.CoachManager.Presentation.Prism.Modules.Administration.Views
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
    }
}