using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using My.CoachManager.CrossCutting.Core.Constants;
using My.CoachManager.CrossCutting.Core.Enums;
using My.CoachManager.CrossCutting.Resources;
using My.CoachManager.CrossCutting.Resources.Entities;

namespace My.CoachManager.Presentation.Models
{
    public class PlayerModel : PersonModel
    {
        /// <summary>
        /// Initialise a new instance of <see cref="PlayerModel"/>.
        /// </summary>
        public PlayerModel()
        {
            Laterality = PlayerConstants.DefaultLaterality;
            Positions = new ObservableCollection<PlayerPositionModel>();
            Injuries = new ObservableCollection<InjuryModel>();
        }

        /// <summary>
        /// Gets or sets the latérality.
        /// </summary>
        [Display(Name = "Laterality", ResourceType = typeof(PlayerResources))]
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        public Laterality Laterality { get; set; }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        [Display(Name = "Height", ResourceType = typeof(PlayerResources))]
        public int? Height { get; set; }

        /// <summary>
        /// Gets or sets the weight.
        /// </summary>
        [Display(Name = "Weight", ResourceType = typeof(PlayerResources))]
        public int? Weight { get; set; }

        /// <summary>
        /// Gets or sets the shoes size.
        /// </summary>
        [Display(Name = "ShoesSize", ResourceType = typeof(PlayerResources))]
        public int? ShoesSize { get; set; }

        /// <summary>
        /// Gets or set the positions.
        /// </summary>
        [Display(Name = "Positions", ResourceType = typeof(PlayerResources))]
        public ObservableCollection<PlayerPositionModel> Positions { get; set; }

        /// <summary>
        /// Gets or set the injuries.
        /// </summary>
        [Display(Name = "Injuries", ResourceType = typeof(PlayerResources))]
        public ObservableCollection<InjuryModel> Injuries { get; set; }

        /// <summary>
        /// Get main positions
        /// </summary>
        public IEnumerable<PositionModel> NaturalPositions => Positions?.Where(x => x.IsNatural).Select(x => x.Position);

        /// <summary>
        /// Get main positions
        /// </summary>
        public PositionModel NaturalPosition => NaturalPositions?.FirstOrDefault();

        /// <summary>
        /// Get natural position
        /// </summary>
        public string NaturalPositionsLiteral => NaturalPositions != null ? string.Join(" / ", NaturalPositions?.OrderBy(x => x.Order).Select(x => x.Label)) : string.Empty;

        /// <summary>
        /// Get Is Injured.
        /// </summary>
        public bool IsInjured => IsInjuredAtDate(DateTime.Today);

        /// <summary>
        /// Get Is Injured.
        /// </summary>
        public InjuryModel Injury
        {
            get { return Injuries != null ? Injuries.FirstOrDefault(x => DateTime.Today >= x.Date && (!x.ExpectedReturn.HasValue || DateTime.Today <= x.ExpectedReturn)) : new InjuryModel(); }
        }

        public bool IsInjuredAtDate(DateTime date)
        {
            return Injuries?.Any(x => date >= x.Date && (!x.ExpectedReturn.HasValue || date <= x.ExpectedReturn)) ?? false;
        }
    }
}
