using System;
using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Metadatas;
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
        public virtual DateTime? StartDate { get ; set; }



        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        public virtual DateTime? EndDate { get; set; }
    }
}