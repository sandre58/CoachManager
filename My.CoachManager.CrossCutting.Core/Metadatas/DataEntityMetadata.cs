using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using My.CoachManager.CrossCutting.Core.Resources;
using My.CoachManager.CrossCutting.Core.Resources.Entities;

namespace My.CoachManager.CrossCutting.Core.Metadatas
{
    public class DataEntityMetadata : EntityMetadata
    {
        [Display(Name = "Label", ResourceType = typeof(DataEntityResources))]
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        [MaxLength(100, ErrorMessageResourceName = "MaxLenghtFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        [Index(IsUnique = true)]
        public string Label { get; set; }

        [Display(Name = "Description", ResourceType = typeof(DataEntityResources))]
        public string Description { get; set; }

        [Display(Name = "Code", ResourceType = typeof(DataEntityResources))]
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        [MaxLength(15, ErrorMessageResourceName = "MaxLenghtFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        [Index(IsUnique = true)]
        public string Code { get; set; }

        [Display(Name = "Order", ResourceType = typeof(DataEntityResources))]
        [DefaultValue(0)]
        public int Order { get; set; }
    }
}