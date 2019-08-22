using System.Runtime.Serialization;

namespace My.CoachManager.Application.Dtos
{
    /// <summary>
    /// Data Transfer Object for Address item.
    /// </summary>
    [DataContract]
    public class AddressDto : EntityDto
    {
        /// <summary>
        /// Gets or sets the first row.
        /// </summary>
        [DataMember]
        public string Row1 { get; set; }

        /// <summary>
        /// Gets or sets the second row.
        /// </summary>
        [DataMember]
        public string Row2 { get; set; }

        /// <summary>
        /// Gets or sets the postal code.
        /// </summary>
        [DataMember]
        public string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        [DataMember]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the country id.
        /// </summary>
        [DataMember]
        public int? CountryId { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        [DataMember]
        public CountryDto Country { get; set; }

        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        [DataMember]
        public double Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        [DataMember]
        public double Longitude { get; set; }
    }
}
