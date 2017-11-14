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
        public PlayerViewModel()
        {
            Laterality = PlayerConstants.DefaultLaterality;
            Positions = new ObservableCollection<PlayerPositionViewModel>();
            Heights = new ObservableCollection<PlayerHeightViewModel>();
            Weights = new ObservableCollection<PlayerWeightViewModel>();
        }

        private int? _categoryId;

        public int? CategoryId
        {
            get { return _categoryId; }
            set { SetProperty(ref _categoryId, value); }
        }

        private CategoryViewModel _category;

        public CategoryViewModel Category
        {
            get { return _category; }
            set { SetProperty(ref _category, value); }
        }

        private Laterality _laterality;

        public Laterality Laterality
        {
            get { return _laterality; }
            set { SetProperty(ref _laterality, value); }
        }

        private int? _shoesSize;

        public int? ShoesSize
        {
            get { return _shoesSize; }
            set { SetProperty(ref _shoesSize, value); }
        }

        private ObservableCollection<PlayerPositionViewModel> _positions;

        public ObservableCollection<PlayerPositionViewModel> Positions
        {
            get { return _positions; }
            set
            {
                SetProperty(ref _positions, value);
            }
        }

        private ObservableCollection<PlayerHeightViewModel> _heights;

        public ObservableCollection<PlayerHeightViewModel> Heights
        {
            get { return _heights; }
            set
            {
                SetProperty(ref _heights, value);
            }
        }

        private ObservableCollection<PlayerWeightViewModel> _weights;

        public ObservableCollection<PlayerWeightViewModel> Weights
        {
            get { return _weights; }
            set
            {
                SetProperty(ref _weights, value);
            }
        }
    }
}