using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Metadatas;
using My.CoachManager.CrossCutting.Core.Resources;
using My.CoachManager.Presentation.Prism.Core.Models;

namespace My.CoachManager.Presentation.Prism.Models
{
    /// <summary>
    /// Provides properties for a Season Entity.
    /// </summary>
    [MetadataType(typeof(SeasonMetadata))]
    public class SeasonModel : ReferenceModel
    {
        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        public virtual DateTime? StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        public virtual DateTime? EndDate { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Gets error for a property.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected override IEnumerable<ValidationResult> ComputeErrors(string propertyName, object value)
        {
            var errors = new List<ValidationResult>();

            switch (propertyName)
            {
                case nameof(StartDate):
                case nameof(EndDate):
                    if (StartDate >= EndDate)
                    {
                        errors.Add(new ValidationResult(ValidationMessageResources.StartDateLessThanEndDateMessage));
                    }

                    break;
            }

            return errors;
        }
    }
}