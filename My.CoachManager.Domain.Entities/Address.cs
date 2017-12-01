﻿using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Metadatas;

namespace My.CoachManager.Domain.Entities
{
    /// <summary>
    /// Provides properties for a Address Entity.
    /// </summary>
    [MetadataType(typeof(AddressMetadata))]
    public class Address : Entity
    {
        /// <summary>
        /// Gets or sets the first row.
        /// </summary>
        public string Row1 { get; set; }

        /// <summary>
        /// Gets or sets the second row.
        /// </summary>
        public string Row2 { get; set; }

        /// <summary>
        /// Gets or sets the postal code.
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the country id.
        /// </summary>
        public int? CountryId { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        public Country Country { get; set; }

        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// Gets or sets the Business identifier.
        /// </summary>
        public override string BusinessKey
        {
            get { return string.Join(" ", Row1, Row2, PostalCode, City != null ? City.ToUpper() : ""); }
        }
    }
}