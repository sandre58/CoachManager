using My.CoachManager.Presentation.Prism.Modules.Administration.ViewModels;

namespace My.CoachManager.Presentation.Prism.Modules.Administration.Views
{
    /// <summary>
    /// Logique d'interaction pour CategoriesListView.xaml
    /// </summary>
    public partial class PositionsListView
    {
        public PositionsListView(IPositionsListViewModel viewModel)
        {
            DataContext = viewModel;

            InitializeComponent();
        }
    }
}