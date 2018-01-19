using My.CoachManager.Presentation.Prism.Modules.Administration.ViewModels;

namespace My.CoachManager.Presentation.Prism.Modules.Administration.Views
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