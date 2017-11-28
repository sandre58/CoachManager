using My.CoachManager.Presentation.Prism.AdministrationModule.ViewModels;

namespace My.CoachManager.Presentation.Prism.AdministrationModule.Views
{
    /// <summary>
    /// Logique d'interaction pour CategoriesListView.xaml
    /// </summary>
    public partial class SeasonsListView
    {
        public SeasonsListView(ISeasonsListViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}