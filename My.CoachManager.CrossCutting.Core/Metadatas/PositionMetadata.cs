using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Constants;
using My.CoachManager.CrossCutting.Core.Resources;
using My.CoachManager.CrossCutting.Core.Resources.Entities;

namespace My.CoachManager.CrossCutting.Core.Metadatas
{
    /// <summary>
    /// Provides metadata for a Position Entity.
    /// </summary>
    public class PositionMetadata : ReferenceMetadata
    {
        [Display(Name = "Row", ResourceType = typeof(PositionResources))]
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        [Range(PositionConstants.MinRow, PositionConstants.MaxRow, ErrorMessageResourceName = "RangeFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        public int Row { get; set; }

        [Display(Name = "Column", ResourceType = typeof(PositionResources))]
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        [Range(PositionConstants.MinColumn, PositionConstants.MaxColumn, ErrorMessageResourceName = "RangeFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        public int Column { get; set; }
    }
}