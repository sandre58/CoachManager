using System.Runtime.Serialization;

namespace My.CoachManager.Application.Dtos.Parameters
{
    /// <summary>
    /// Data Transfer Object for Address item.
    /// </summary>
    [DataContract]
    public class ListParametersDto
    {
        /// <summary>
        /// Gets or set page to display.
        /// </summary>
        [DataMember]
        public int Page { get; set; }

        /// <summary>
        /// Gets or set items count.
        /// </summary>
        [DataMember]
        public int Count { get; set; }

    }
}
