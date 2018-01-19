using My.CoachManager.Presentation.Prism.Modules.Administration.ViewModels;

namespace My.CoachManager.Presentation.Prism.Modules.Administration.Views
{
    /// <summary>
    /// Logique d'interaction pour CategoryEditView.xaml
    /// </summary>
    public partial class SeasonEditView
    {
        public SeasonEditView(ISeasonEditViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}