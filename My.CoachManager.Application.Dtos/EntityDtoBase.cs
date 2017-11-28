using System;
using System.Runtime.Serialization;

namespace My.CoachManager.Application.Dtos
{
    /// <summary>
    /// Dto classe base.
    /// </summary>
    [DataContract]
    public abstract class EntityDtoBase : IEntityDtoBase
    {
        [DataMember]
        public DateTime? CreatedDate { get; set; }

        [DataMember]
        public string CreatedBy { get; set; }

        [DataMember]
        public DateTime? ModifiedDate { get; set; }

        [DataMember]
        public string ModifiedBy { get; set; }
    }
}