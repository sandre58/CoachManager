using System;
using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Enums;
using My.CoachManager.CrossCutting.Core.Resources;
using My.CoachManager.CrossCutting.Core.Resources.Entities;
using My.CoachManager.Presentation.Prism.Core.Models;

namespace My.CoachManager.Presentation.Prism.Models
{
    /// <summary>
    /// Provides properties for a injury Entity.
    /// </summary>
    public class InjuryModel : EntityModel
    {

        /// <summary>
        /// Gets or sets the condition.
        /// </summary>
        [Display(Name = "Condition", ResourceType = typeof(InjuryResources))]
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        public virtual string Condition { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        [Display(Name = "Type", ResourceType = typeof(InjuryResources))]
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        public virtual InjuryType Type { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        [Display(Name = "Description", ResourceType = typeof(InjuryResources))]
        public virtual string Description { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        [Display(Name = "Date", ResourceType = typeof(InjuryResources))]
        public virtual DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the expected return date.
        /// </summary>
        [Display(Name = "ExpectedReturn", ResourceType = typeof(InjuryResources))]
        public virtual DateTime? ExpectedReturn { get; set; }

    }
}