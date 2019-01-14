using System;
using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Resources;
using My.CoachManager.CrossCutting.Core.Resources.Entities;
using My.CoachManager.Presentation.Prism.Core.Models;

namespace My.CoachManager.Presentation.Prism.Models
{
    /// <summary>
    /// Provides properties for a training Entity.
    /// </summary>
    public class TrainingModel : EntityModel
    {

        /// <summary>
        /// Gets or sets the roster id.
        /// </summary>
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        [Display(Name = "Roster", ResourceType = typeof(TrainingResources))]
        public int? RosterId { get; set; }

        /// <summary>
        /// Gets or sets the roster.
        /// </summary>
        public virtual RosterModel Roster { get; set; }

        /// <summary>
        /// Gets or sets the place.
        /// </summary>
        [Display(Name = "Place", ResourceType = typeof(TrainingResources))]
        public virtual string Place { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        [Display(Name = "StartDate", ResourceType = typeof(TrainingResources))]
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        public virtual DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        [Display(Name = "EndDate", ResourceType = typeof(TrainingResources))]
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        public virtual DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets the is cancelled value.
        /// </summary>
        [Display(Name = "IsCancelled", ResourceType = typeof(TrainingResources))]
        public virtual bool IsCancelled { get; set; }
    }
}