using My.CoachManager.Presentation.Prism.AdministrationModule.ViewModels;

namespace My.CoachManager.Presentation.Prism.AdministrationModule.Views
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