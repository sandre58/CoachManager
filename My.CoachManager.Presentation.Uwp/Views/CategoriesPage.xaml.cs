using My.CoachManager.Presentation.Uwp.ViewModels;

namespace My.CoachManager.Presentation.Uwp.Views
{
    public sealed partial class CategoriesPage
    {
        private CategoriesViewModel ViewModel => DataContext as CategoriesViewModel;

        public CategoriesPage()
        {
            InitializeComponent();
        }
    }
}
