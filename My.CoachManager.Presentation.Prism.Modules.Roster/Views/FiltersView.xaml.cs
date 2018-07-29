using My.CoachManager.Presentation.Prism.Modules.Roster.ViewModels;

namespace My.CoachManager.Presentation.Prism.Modules.Roster.Views
{
    /// <summary>
    /// Interaction logic for FiltersView.xaml
    /// </summary>
    public partial class FiltersView
    {
        public FiltersView(IFiltersViewModel model)
        {
            DataContext = model;
            InitializeComponent();
        }
    }
}