using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using My.CoachManager.CrossCutting.Core.Constants;
using My.CoachManager.CrossCutting.Core.Resources.Entities;

namespace My.CoachManager.CrossCutting.Core.Metadatas
{
    public abstract class EntityMetadataBase
    {
        #region Public Properties

        [Display(Name = "CreatedDate", ResourceType = typeof(EntityResources))]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [DefaultValue(SqlConstants.GetUtcDate)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public object CreatedDate { get; set; }

        [Display(Name = "CreatedBy", ResourceType = typeof(EntityResources))]
        public object CreatedBy { get; set; }

        [Display(Name = "ModifiedDate", ResourceType = typeof(EntityResources))]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [DefaultValue(SqlConstants.GetUtcDate)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public object ModifiedDate { get; set; }

        [Display(Name = "ModifiedBy", ResourceType = typeof(EntityResources))]
        public object ModifiedBy { get; set; }

        #endregion Public Properties
    }
}