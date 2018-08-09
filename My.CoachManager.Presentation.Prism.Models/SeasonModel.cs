using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Resources;
using My.CoachManager.CrossCutting.Core.Resources.Entities;
using My.CoachManager.Presentation.Prism.Core.Models;

namespace My.CoachManager.Presentation.Prism.Models
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
        public virtual DateTime? StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        [Display(Name = "EndDate", ResourceType = typeof(SeasonResources))]
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        [DataType(DataType.Date)]
        public virtual DateTime? EndDate { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Gets error for a property.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected override IEnumerable<string> ComputeErrors(string propertyName, object value)
        {
            var errors = new List<string>();

            switch (propertyName)
            {
                case nameof(StartDate):
                case nameof(EndDate):
                    if (StartDate >= EndDate)
                    {
                        errors.Add(ValidationMessageResources.StartDateLessThanEndDateMessage);
                    }

                    break;
            }

            return errors;
        }
    }
}