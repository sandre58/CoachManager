using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Metadatas;
using My.CoachManager.Presentation.Prism.Core.ViewModels;

namespace My.CoachManager.Presentation.Prism.ViewModels
{
    /// <summary>
    /// Provides properties for a Team Entity.
    /// </summary>
    [MetadataType(typeof(TeamMetadata))]
    public class TeamViewModel : EntityViewModel
    {
        private int _clubId;

        /// <summary>
        /// Gets or sets the team's club id.
        /// </summary>
        public int ClubId
        {
            get { return _clubId; }
            set { SetProperty(ref _clubId, value); }
        }

        private ClubViewModel _club;

        /// <summary>
        /// Gets or sets the team's club.
        /// </summary>
        public ClubViewModel Club
        {
            get { return _club; }
            set { SetProperty(ref _club, value); }
        }

        private int _categoryId;

        /// <summary>
        /// Gets or sets the team's category id.
        /// </summary>
        public int CategoryId
        {
            get { return _categoryId; }
            set { SetProperty(ref _categoryId, value); }
        }

        private CategoryViewModel _category;

        /// <summary>
        /// Gets or sets the team's category.
        /// </summary>
        public CategoryViewModel Category
        {
            get { return _category; }
            set { SetProperty(ref _category, value); }
        }

        private string _name;

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
    }
}