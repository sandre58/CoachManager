using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using My.CoachManager.CrossCutting.Core.Resources;
using My.CoachManager.CrossCutting.Core.Resources.Entities;

namespace My.CoachManager.CrossCutting.Core.Metadatas
{
    public class PlayerHeightMetadata : ForeignEntityMetadata
    {
        [Key]
        [Column(Order = 1)]
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        public int PlayerId { get; set; }

        [Key]
        [Column(Order = 2)]
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        public DateTime Date { get; set; }

        [Display(Name = "Height", ResourceType = typeof(PlayerResources))]
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        public int Value { get; set; }
    }
}