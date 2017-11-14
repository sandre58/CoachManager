using System.Runtime.Serialization;

namespace My.CoachManager.Application.Dtos
{
    /// <summary>
    /// Dto classe base.
    /// </summary>
    [DataContract]
    public abstract class EntityDto : EntityDtoBase, IEntityDto
    {
        /// <summary>
        /// Id.
        /// </summary>
        [DataMember]
        public int Id { get; set; }
    }
}