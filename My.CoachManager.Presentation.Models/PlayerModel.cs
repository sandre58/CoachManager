﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using My.CoachManager.CrossCutting.Core.Constants;
using My.CoachManager.CrossCutting.Core.Enums;
using My.CoachManager.CrossCutting.Core.Resources;
using My.CoachManager.CrossCutting.Core.Resources.Entities;

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
        /// Gets or sets the category id.
        /// </summary>
        [Display(Name = "Category", ResourceType = typeof(PlayerResources))]
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        public int? CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        [Display(Name = "Category", ResourceType = typeof(PlayerResources))]
        public CategoryModel Category { get; set; }

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
        /// Get natural position
        /// </summary>
        public string NaturalPositions
        {
            get
            {
                if (Positions == null || Positions.Count <= 0) return string.Empty;
                return string.Join(" / ", Positions.Where(x => x.IsNatural).OrderBy(x => x.Position.Order).Select(x => x.Position.Label));
            }
        }

        /// <summary>
        /// Get a string shows main positions.
        /// </summary>
        public string PositionsLiteral
        {
            get
            {
                if (Positions == null || Positions.Count <= 0) return string.Empty;
                return string.Join(", ", Positions.Where(x => x.Rating > 3).OrderBy(x => x.Position.Order).Select(x => x.Position.Code));
            }
        }

        /// <summary>
        /// Get Is injuried.
        /// </summary>
        public bool IsInjured => IsInjuredAtDate(DateTime.Today);

        /// <summary>
        /// Get Is injuried.
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