using System;
using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Metadatas;

namespace My.CoachManager.Presentation.Prism.ViewModels
{
    [MetadataType(typeof(SeasonMetadata))]
    public class SeasonViewModel : DataEntityViewModel
    {
        private DateTime? _startDate;
        public virtual DateTime? StartDate { get { return _startDate; } set { SetProperty(ref _startDate, value); } }

        private DateTime? _endDate;
        public virtual DateTime? EndDate { get { return _endDate; } set { SetProperty(ref _endDate, value); } }
    }
}