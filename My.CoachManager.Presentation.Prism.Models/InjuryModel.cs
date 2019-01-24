using My.CoachManager.CrossCutting.Core.Enums;
using My.CoachManager.CrossCutting.Core.Resources;
using My.CoachManager.CrossCutting.Core.Resources.Entities;
using My.CoachManager.Presentation.Prism.Core.Attributes.Validation;
using My.CoachManager.Presentation.Prism.Core.Models;
using System;
using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Helpers;

namespace My.CoachManager.Presentation.Prism.Models
{
    /// <summary>
    /// Provides properties for a injury Entity.
    /// </summary>
    public class InjuryModel : EntityModel
    {
        /// <summary>
        /// Initialise a new instance of <see cref="InjuryModel"/>.
        /// </summary>
        public InjuryModel()
        {
            Rules.Add(nameof(Date), ValidationMessageResources.StartDateLessOrEqualsThanEndDateMessage, o =>
            {
                var item = (InjuryModel)o;
                return item.ExpectedReturn == null || item.Date.Date <= item.ExpectedReturn.Value.Date;
            });

            Rules.Add(nameof(ExpectedReturn), ValidationMessageResources.StartDateLessOrEqualsThanEndDateMessage, o =>
            {
                var item = (InjuryModel)o;
                return item.ExpectedReturn == null || item.Date.Date <= item.ExpectedReturn.Value.Date;
            });
        }

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
        [ValidateProperty(nameof(ExpectedReturn))]
        public virtual DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the expected return date.
        /// </summary>
        [Display(Name = "ExpectedReturn", ResourceType = typeof(InjuryResources))]
        [ValidateProperty(nameof(Date))]
        public virtual DateTime? ExpectedReturn { get; set; }

        /// <summary>
        /// Gets or sets the expected return date.
        /// </summary>
        public virtual string ExpectedReturnLabel
        {
            get
            {
                if (!ExpectedReturn.HasValue) return string.Empty;

                var valueInDays = DateTimeHelper.NumberOfDays(Date, ExpectedReturn.Value);
                if (valueInDays <= 0) return string.Format(InjuryResources.ExpectedReturnInDays, valueInDays);

                var valueInWeeks = DateTimeHelper.NumberOfWeeks(Date, ExpectedReturn.Value);
                if (valueInWeeks <= 7) return string.Format(InjuryResources.ExpectedReturnInWeeks, valueInWeeks);

                var valueInMonths = DateTimeHelper.NumberOfMonth(Date, ExpectedReturn.Value);
                return string.Format(InjuryResources.ExpectedReturnInMonth, valueInMonths);
            }
        }

        /// <summary>
        /// Gets or sets the severity.
        /// </summary>
        [Display(Name = "Severity", ResourceType = typeof(InjuryResources))]
        [Required]
        public virtual InjurySeverity Severity { get; set; }
    }
}