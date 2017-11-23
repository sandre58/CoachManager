using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Constants;
using My.CoachManager.CrossCutting.Core.Enums;
using My.CoachManager.CrossCutting.Core.Metadatas;

namespace My.CoachManager.Presentation.Prism.ViewModels
{
    [MetadataType(typeof(PlayerMetadata))]
    public class PlayerViewModel : PersonViewModel
    {
        /// <summary>
        /// Initialise a new instance of <see cref="PlayerViewModel"/>.
        /// </summary>
        public PlayerViewModel()
        {
            Laterality = PlayerConstants.DefaultLaterality;
            Positions = new ObservableCollection<PlayerPositionViewModel>();
        }

        private int? _categoryId;

        /// <summary>
        /// Gets or sets the category id.
        /// </summary>
        public int? CategoryId
        {
            get { return _categoryId; }
            set { SetProperty(ref _categoryId, value); }
        }

        private CategoryViewModel _category;

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        public CategoryViewModel Category
        {
            get { return _category; }
            set { SetProperty(ref _category, value); }
        }

        private Laterality _laterality;

        /// <summary>
        /// Gets or sets the latérality.
        /// </summary>
        public Laterality Laterality
        {
            get { return _laterality; }
            set { SetProperty(ref _laterality, value); }
        }

        private int? _height;

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        public int? Height
        {
            get { return _height; }
            set { SetProperty(ref _height, value); }
        }

        private int? _weight;

        /// <summary>
        /// Gets or sets the weight.
        /// </summary>
        public int? Weight
        {
            get { return _weight; }
            set { SetProperty(ref _weight, value); }
        }

        private int? _shoesSize;

        /// <summary>
        /// Gets or sets the shoes size.
        /// </summary>
        public int? ShoesSize
        {
            get { return _shoesSize; }
            set { SetProperty(ref _shoesSize, value); }
        }

        private ObservableCollection<PlayerPositionViewModel> _positions;

        /// <summary>
        /// Gets or set the positions.
        /// </summary>
        public ObservableCollection<PlayerPositionViewModel> Positions
        {
            get { return _positions; }
            set
            {
                SetProperty(ref _positions, value);
            }
        }
    }
}