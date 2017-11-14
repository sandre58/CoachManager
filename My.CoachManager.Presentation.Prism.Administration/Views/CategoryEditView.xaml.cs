using My.CoachManager.Presentation.Prism.Administration.ViewModels;

namespace My.CoachManager.Presentation.Prism.Administration.Views
{
    /// <summary>
    /// Logique d'interaction pour CategoryEditView.xaml
    /// </summary>
    public partial class CategoryEditView
    {
        public CategoryEditView(ICategoryEditViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}