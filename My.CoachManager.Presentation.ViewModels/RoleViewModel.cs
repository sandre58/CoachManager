using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Metadatas;

namespace My.CoachManager.Presentation.ViewModels
{
    [MetadataType(typeof(RoleMetadata))]
    public class RoleViewModel : DataEntityViewModel
    {
        public RoleViewModel()
        {
            Permissions = new ObservableCollection<PermissionViewModel>();
            Users = new ObservableCollection<UserViewModel>();
        }

        public ICollection<PermissionViewModel> Permissions { get; set; }

        public ICollection<UserViewModel> Users { get; set; }
    }
}