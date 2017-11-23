using My.CoachManager.Presentation.Prism.Administration.ViewModels;

namespace My.CoachManager.Presentation.Prism.Administration.Views
{
    /// <summary>
    /// Logique d'interaction pour CategoriesListView.xaml
    /// </summary>
    public partial class CategoriesListView
    {
        public CategoriesListView(ICategoriesListViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}