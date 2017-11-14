using My.CoachManager.Presentation.Prism.Administration.ViewModels;

namespace My.CoachManager.Presentation.Prism.Administration.Views
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