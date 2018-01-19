using My.CoachManager.Presentation.Prism.Modules.Administration.ViewModels;

namespace My.CoachManager.Presentation.Prism.Modules.Administration.Views
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