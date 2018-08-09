using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Constants;
using My.CoachManager.CrossCutting.Core.Enums;
using My.CoachManager.CrossCutting.Core.Resources;
using My.CoachManager.CrossCutting.Core.Resources.Entities;

namespace My.CoachManager.CrossCutting.Core.Metadatas
{
    /// <summary>
    /// Provides metadata for a Person Entity.
    /// </summary>
    public abstract class PersonMetadata : EntityMetadata
    {
        [Display(Name = "LastName", ResourceType = typeof(PersonResources))]
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        public string LastName { get; set; }

        [Display(Name = "FirstName", ResourceType = typeof(PersonResources))]
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        public string FirstName { get; set; }

        [Display(Name = "Birthdate", ResourceType = typeof(PersonResources))]
        public DateTime? Birthdate { get; set; }

        [Display(Name = "PlaceOfBirth", ResourceType = typeof(PersonResources))]
        public string PlaceOfBirth { get; set; }

        [Display(Name = "Country", ResourceType = typeof(PersonResources))]
        public int? CountryId { get; set; }

        [Display(Name = "Photo", ResourceType = typeof(PersonResources))]
        public byte[] Photo { get; set; }

        [Display(Name = "Gender", ResourceType = typeof(PersonResources))]
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        public GenderType Gender { get; set; }

        [Display(Name = "Address", ResourceType = typeof(PersonResources))]
        public int? AddressId { get; set; }

        [Display(Name = "LicenseNumber", ResourceType = typeof(PersonResources))]
        [MaxLength(10, ErrorMessageResourceName = "MaxLenghtFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        public string LicenseNumber { get; set; }

        [Display(Name = "Description", ResourceType = typeof(PersonResources))]
        public string Description { get; set; }

        [Display(Name = "Size", ResourceType = typeof(PersonResources))]
        [MaxLength(4, ErrorMessageResourceName = "MaxLenghtFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        public string Size { get; set; }

        [Display(Name = "Contacts", ResourceType = typeof(PersonResources))]
        public ICollection<object> Contacts { get; set; }
    }
}