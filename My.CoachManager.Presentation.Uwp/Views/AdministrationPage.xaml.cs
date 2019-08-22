using My.CoachManager.Presentation.Uwp.ViewModels;

namespace My.CoachManager.Presentation.Uwp.Views
{
    public sealed partial class AdministrationPage
    {
        private AdministrationViewModel ViewModel => DataContext as AdministrationViewModel;

        public AdministrationPage()
        {
            InitializeComponent();
        }

    }
}
