using System;
using System.Runtime.Serialization;

namespace My.CoachManager.Application.Dtos.Administration
{
    /// <summary>
    /// Data Transfer Object for Season item.
    /// </summary>
    [DataContract]
    public class SeasonDto : DataEntityDto
    {
        [DataMember]
        public DateTime? StartDate { get; set; }

        [DataMember]
        public DateTime? EndDate { get; set; }
    }
}