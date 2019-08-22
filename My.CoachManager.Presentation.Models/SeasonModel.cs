using System;
using System.ComponentModel.DataAnnotations;

using My.CoachManager.CrossCutting.Core.Enums;
using My.CoachManager.CrossCutting.Core.Resources;
using My.CoachManager.CrossCutting.Core.Resources.Entities;
using My.CoachManager.Presentation.Core.Attributes.Validation;
using My.CoachManager.Presentation.Core.Models;

namespace My.CoachManager.Presentation.Models
{
    /// <summary>
    /// Provides properties for a Season Entity.
    /// </summary>
    public class SeasonModel : ReferenceModel
    {
        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        [Display(Name = "StartDate", ResourceType = typeof(SeasonResources))]
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        [DataType(DataType.Date)]
        [CompareToProperty(nameof(EndDate), ComparableOperator.LessThan, ErrorMessageResourceName = "StartDateLessThanEndDateMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        [ValidateProperty(nameof(EndDate))]
        public virtual DateTime? StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        [Display(Name = "EndDate", ResourceType = typeof(SeasonResources))]
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        [DataType(DataType.Date)]
        [CompareToProperty(nameof(StartDate), ComparableOperator.GreaterThan, ErrorMessageResourceName = "StartDateLessThanEndDateMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        [ValidateProperty(nameof(StartDate))]
        public virtual DateTime? EndDate { get; set; }
    }
}
