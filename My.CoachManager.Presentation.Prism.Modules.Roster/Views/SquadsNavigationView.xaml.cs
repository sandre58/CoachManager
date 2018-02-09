using My.CoachManager.Presentation.Prism.Modules.Roster.ViewModels;
using Prism.Regions;

namespace My.CoachManager.Presentation.Prism.Modules.Roster.Views
{
    /// <summary>
    /// Logique d'interaction pour RosterNavigationView.xaml
    /// </summary>
    [ViewSortHint("02")]
    public partial class SquadsNavigationView
    {
        public SquadsNavigationView(SquadsNavigationViewModel model)
        {
            DataContext = model;
            InitializeComponent();
        }
    }
}