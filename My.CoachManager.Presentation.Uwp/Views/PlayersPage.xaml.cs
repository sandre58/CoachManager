using My.CoachManager.Presentation.Uwp.ViewModels;

namespace My.CoachManager.Presentation.Uwp.Views
{
    public sealed partial class PlayersPage
    {
        private PlayersViewModel ViewModel => DataContext as PlayersViewModel;

        public PlayersPage()
        {
            InitializeComponent();
        }
    }
}
