using System;
using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Metadatas;

namespace My.CoachManager.Presentation.Prism.ViewModels
{
    /// <summary>
    /// Provides properties for a Season Entity.
    /// </summary>
    [MetadataType(typeof(SeasonMetadata))]
    public class SeasonViewModel : DataEntityViewModel
    {
        private DateTime? _startDate;

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        public virtual DateTime? StartDate { get { return _startDate; } set { SetProperty(ref _startDate, value); } }

        private DateTime? _endDate;

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        public virtual DateTime? EndDate { get { return _endDate; } set { SetProperty(ref _endDate, value); } }
    }
}