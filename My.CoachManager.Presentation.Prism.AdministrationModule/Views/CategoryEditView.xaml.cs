using My.CoachManager.Presentation.Prism.AdministrationModule.ViewModels;

namespace My.CoachManager.Presentation.Prism.AdministrationModule.Views
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