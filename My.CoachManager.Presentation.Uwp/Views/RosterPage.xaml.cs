using My.CoachManager.Presentation.Uwp.ViewModels;

namespace My.CoachManager.Presentation.Uwp.Views
{
    public sealed partial class RosterPage
    {
        private RosterViewModel ViewModel => DataContext as RosterViewModel;

        public RosterPage()
        {
            InitializeComponent();
        }
    }
}
