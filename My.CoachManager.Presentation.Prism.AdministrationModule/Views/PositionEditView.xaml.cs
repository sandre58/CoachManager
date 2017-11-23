using My.CoachManager.Presentation.Prism.Administration.ViewModels;

namespace My.CoachManager.Presentation.Prism.Administration.Views
{
    /// <summary>
    /// Logique d'interaction pour CategoryEditView.xaml
    /// </summary>
    public partial class PositionEditView
    {
        public PositionEditView(IPositionEditViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}