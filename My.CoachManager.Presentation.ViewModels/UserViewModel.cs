using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Metadatas;
using My.CoachManager.Presentation.Core.ViewModels;

namespace My.CoachManager.Presentation.ViewModels
{
    [MetadataType(typeof(UserMetadata))]
    public class UserViewModel : EntityViewModel
    {
        public UserViewModel()
        {
            Roles = new ObservableCollection<RoleViewModel>();
        }

        private string _name;
        public string Name { get { return _name; } set { SetEntityProperty(() => _name = value, value, Name); } }

        private string _login;
        public string Login { get { return _login; } set { SetEntityProperty(() => _login = value, value, Login); } }

        private string _password;
        public string Password { get { return _password; } set { SetEntityProperty(() => _password = value, value, Password); } }

        private string _mail;
        public string Mail { get { return _mail; } set { SetEntityProperty(() => _mail = value, value, Mail); } }

        public ICollection<RoleViewModel> Roles { get; set; }
    }
}