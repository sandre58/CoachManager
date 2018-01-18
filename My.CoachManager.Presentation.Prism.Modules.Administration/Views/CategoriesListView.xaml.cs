using My.CoachManager.Presentation.Prism.Modules.Administration.ViewModels;

namespace My.CoachManager.Presentation.Prism.Modules.Administration.Views
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