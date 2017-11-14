using System;
using System.Runtime.Serialization;

namespace My.CoachManager.Application.Dtos.Admin
{
    /// <summary>
    /// Players list Dtos.
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