using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using My.CoachManager.CrossCutting.Core.Resources;

namespace My.CoachManager.CrossCutting.Core.Metadatas
{
    public class SeasonCoachMetadata : ForeignEntityMetadata
    {
        [Key]
        [Column(Order = 1)]
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        public int SeasonId { get; set; }

        [Key]
        [Column(Order = 2)]
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        public int CoachId { get; set; }
    }
}