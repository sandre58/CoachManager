using System.Collections.Generic;
using System.Runtime.Serialization;

namespace My.CoachManager.Application.Dtos.Competitions
{
    /// <summary>
    /// Data Transfer Object for Club item.
    /// </summary>
    [DataContract]
    public class ClubDto : EntityDto
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [DataMember]
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets the small name.
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the abbreviation.
        /// </summary>
        [DataMember]
        public string Abbreviation { get; set; }

        /// <summary>
        /// Gets or sets the logo.
        /// </summary>
        [DataMember]
        public byte[] Logo { get; set; }

        /// <summary>
        /// Gets or sets the home color.
        /// </summary>
        [DataMember]
        public string HomeColor { get; set; }

        /// <summary>
        /// Gets or sets the away color.
        /// </summary>
        [DataMember]
        public string AwayColor { get; set; }
    }
}