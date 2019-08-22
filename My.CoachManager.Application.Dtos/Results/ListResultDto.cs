using System.Collections.Generic;
using System.Runtime.Serialization;

namespace My.CoachManager.Application.Dtos.Results
{
    /// <summary>
    /// Data Transfer Object for Address item.
    /// </summary>
    [DataContract]
    public class ListResultDto<TDtos> where TDtos : IEntityDto
    {
        /// <summary>
        /// Gets or sets the first row.
        /// </summary>
        [DataMember]
        public IList<TDtos> Items { get; set; }

        /// <summary>
        /// Gets or set items count.
        /// </summary>
        [DataMember]
        public int Count { get; set; }

        /// <summary>
        /// Gets or set all items count.
        /// </summary>
        [DataMember]
        public int AllItemsCount { get; set; }

    }
}
