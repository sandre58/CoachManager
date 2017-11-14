using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using My.CoachManager.CrossCutting.Core.Resources.Entities;

namespace My.CoachManager.CrossCutting.Core.Metadatas
{
    public abstract class EntityMetadata : EntityMetadataBase
    {
        #region Public Properties

        [Display(Name = "Id", ResourceType = typeof(EntityResources))]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }

        #endregion Public Properties
    }
}