using My.CoachManager.CrossCutting.Core.Enums;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace My.CoachManager.Application.Dtos.Parameters
{
    /// <summary>
    /// Data Transfer Object for Address item.
    /// </summary>
    [DataContract]
    public class PlayersListParametersDto : ListParametersDto
    {
        /// <summary>
        /// Gets or set a parameter.
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Gets or set a parameter.
        /// </summary>
        [DataMember]
        public int? YearOfBirth { get; set; }

        /// <summary>
        /// Gets or set a parameter.
        /// </summary>
        [DataMember]
        public string LicenseNumber { get; set; }

        /// <summary>
        /// Gets or set a parameter.
        /// </summary>
        [DataMember]
        public GenderType? Gender { get; set; }

        /// <summary>
        /// Gets or set a parameter.
        /// </summary>
        [DataMember]
        public int? CountryId { get; set; }

        /// <summary>
        /// Gets or set a parameter.
        /// </summary>
        [DataMember]
        public string SortProperty { get; set; }

        /// <summary>
        /// Gets or set a parameter.
        /// </summary>
        [DataMember]
        public ListSortDirection SortDirection { get; set; }
    }
}