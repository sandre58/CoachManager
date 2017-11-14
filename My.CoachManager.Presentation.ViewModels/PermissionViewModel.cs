using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Metadatas;

namespace My.CoachManager.Presentation.ViewModels
{
    [MetadataType(typeof(PermissionMetadata))]
    public class PermissionViewModel : DataEntityViewModel
    {
        public PermissionViewModel()
        {
            Roles = new ObservableCollection<RoleViewModel>();
        }

        public ICollection<RoleViewModel> Roles { get; set; }
    }
}