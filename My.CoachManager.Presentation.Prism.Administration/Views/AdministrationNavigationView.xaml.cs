using System.Security.Permissions;
using My.CoachManager.CrossCutting.Core.Constants;
using Prism.Regions;

namespace My.CoachManager.Presentation.Prism.Administration.Views
{
    /// <summary>
    /// Logique d'interaction pour HomeNavigationItemView.xaml
    /// </summary>
    [ViewSortHint("02")]
    [PrincipalPermission(SecurityAction.Demand)]
    public partial class AdministrationNavigationView
    {
        public AdministrationNavigationView()
        {
            InitializeComponent();
        }
    }
}