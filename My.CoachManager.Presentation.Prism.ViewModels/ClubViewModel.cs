using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Metadatas;
using My.CoachManager.Presentation.Prism.Core.Models;
using My.CoachManager.Presentation.Prism.Core.ViewModels;

namespace My.CoachManager.Presentation.Prism.ViewModels
{
    /// <summary>
    /// Provides properties for a Club Entity.
    /// </summary>
    [MetadataType(typeof(ClubMetadata))]
    public class ClubViewModel : EntityModel
    {
        /// <summary>
        /// Initialise a new instance of <see cref="ClubViewModel"/>.
        /// </summary>
        public ClubViewModel()
        {
            Teams = new ObservableCollection<TeamViewModel>();
        }

        private string _fullName;

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string FullName
        {
            get { return _fullName; }
            set { SetProperty(ref _fullName, value); }
        }

        private string _name;

        /// <summary>
        /// Gets or sets the small name.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private string _abbreviation;

        /// <summary>
        /// Gets or sets the abbreviation.
        /// </summary>
        public string Abbreviation
        {
            get { return _abbreviation; }
            set { SetProperty(ref _abbreviation, value); }
        }

        private byte[] _logo;

        /// <summary>
        /// Gets or sets the logo.
        /// </summary>
        public byte[] Logo
        {
            get { return _logo; }
            set { SetProperty(ref _logo, value); }
        }

        private string _homeColor;

        /// <summary>
        /// Gets or sets the home color.
        /// </summary>
        public string HomeColor
        {
            get { return _homeColor; }
            set { SetProperty(ref _homeColor, value); }
        }

        private string _awayColor;

        /// <summary>
        /// Gets or sets the away color.
        /// </summary>
        public string AwayColor
        {
            get { return _awayColor; }
            set { SetProperty(ref _awayColor, value); }
        }

        private ICollection<TeamViewModel> _teams;

        /// <summary>
        /// Gets or set the teams.
        /// </summary>
        public ICollection<TeamViewModel> Teams
        {
            get { return _teams; }
            set { SetProperty(ref _teams, value); }
        }
    }
}