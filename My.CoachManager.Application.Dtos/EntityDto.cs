using System;
using System.Runtime.Serialization;

namespace My.CoachManager.Application.Dtos
{
    /// <summary>
    /// Dto classe base.
    /// </summary>
    [DataContract]
    public abstract class EntityDto : IEntityDto
    {
        /// <summary>
        /// Id.
        /// </summary>
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the CRUD Status of DTO.
        /// </summary>
        public CrudStatus CrudStatus { get; set; }

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