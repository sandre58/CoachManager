using System.Security.Permissions;
using My.CoachManager.CrossCutting.Core.Constants;
using My.CoachManager.Presentation.Prism.Administration.ViewModels;

namespace My.CoachManager.Presentation.Prism.Administration.Views
{
    /// <summary>
    /// Logique d'interaction pour CategoriesListView.xaml
    /// </summary>
    [PrincipalPermission(SecurityAction.Demand, Role = PermissionConstants.AccessAdmin)]
    public partial class CategoriesListView
    {
        public CategoriesListView(ICategoriesListViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}