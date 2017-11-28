using System.Security.Permissions;
using Prism.Regions;

namespace My.CoachManager.Presentation.Prism.Home.Views
{
    /// <summary>
    /// Logique d'interaction pour HomeNavigationItemView.xaml
    /// </summary>
    [ViewSortHint("01")]
    [PrincipalPermission(SecurityAction.Demand)]
    public partial class HomeNavigationView
    {
        public HomeNavigationView()
        {
            InitializeComponent();
        }
    }
}