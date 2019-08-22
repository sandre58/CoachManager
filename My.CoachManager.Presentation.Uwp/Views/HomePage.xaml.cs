using My.CoachManager.Presentation.Uwp.ViewModels;

namespace My.CoachManager.Presentation.Uwp.Views
{
    public sealed partial class HomePage
    {
        private HomeViewModel ViewModel => DataContext as HomeViewModel;

        public HomePage()
        {
            InitializeComponent();
        }
    }
}
