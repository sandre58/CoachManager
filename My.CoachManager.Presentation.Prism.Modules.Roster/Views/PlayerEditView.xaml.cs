using My.CoachManager.Presentation.Prism.Modules.Roster.ViewModels;

namespace My.CoachManager.Presentation.Prism.Modules.Roster.Views
{
    /// <summary>
    /// Logique d'interaction pour CategoryEditView.xaml
    /// </summary>
    public partial class PlayerEditView
    {
        public PlayerEditView(IPlayerEditViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}