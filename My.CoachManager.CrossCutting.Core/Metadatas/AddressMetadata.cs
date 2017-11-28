using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Resources;
using My.CoachManager.CrossCutting.Core.Resources.Entities;

namespace My.CoachManager.CrossCutting.Core.Metadatas
{
    /// <summary>
    /// Provides Metadata for a Address Entity.
    /// </summary>
    public class AddressMetadata : EntityMetadata
    {
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        [Display(Name = "Row1", ResourceType = typeof(AddressResources))]
        public string Row1 { get; set; }

        [Display(Name = "Row2", ResourceType = typeof(AddressResources))]
        public string Row2 { get; set; }

        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        [Display(Name = "PostalCode", ResourceType = typeof(AddressResources))]
        [MaxLength(5, ErrorMessageResourceName = "MaxLenghtFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        public string PostalCode { get; set; }

        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        [Display(Name = "City", ResourceType = typeof(AddressResources))]
        public string City { get; set; }

        [Display(Name = "Country", ResourceType = typeof(AddressResources))]
        public int? CountryId { get; set; }

        [Display(Name = "Latitude", ResourceType = typeof(AddressResources))]
        public double Latitude { get; set; }

        [Display(Name = "Longitude", ResourceType = typeof(AddressResources))]
        public double Longitude { get; set; }
    }
}