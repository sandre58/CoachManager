using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using My.CoachManager.CrossCutting.Core.Constants;
using My.CoachManager.CrossCutting.Core.Resources.Entities;

namespace My.CoachManager.CrossCutting.Core.Metadatas
{
    /// <summary>
    /// Provides metadata for an entity.
    /// </summary>
    public abstract class EntityMetadata
    {
        [Display(Name = "Id", ResourceType = typeof(EntityResources))]
        [Key]
        public virtual int Id { get; set; }

        [Display(Name = "CreatedDate", ResourceType = typeof(EntityResources))]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [DefaultValue(SqlConstants.GetUtcDate)]
        public DateTime? CreatedDate { get; set; }

        [Display(Name = "CreatedBy", ResourceType = typeof(EntityResources))]
        public string CreatedBy { get; set; }

        [Display(Name = "ModifiedDate", ResourceType = typeof(EntityResources))]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [DefaultValue(SqlConstants.GetUtcDate)]
        public DateTime? ModifiedDate { get; set; }

        [Display(Name = "ModifiedBy", ResourceType = typeof(EntityResources))]
        public string ModifiedBy { get; set; }
    }
}