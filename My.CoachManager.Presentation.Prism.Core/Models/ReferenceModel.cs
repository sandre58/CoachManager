using System;
using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Metadatas;

namespace My.CoachManager.Presentation.Prism.Core.Models
{
    /// <summary>
    /// Provides properties for an item containing Code, Label, Description and Order.
    /// </summary>
    [MetadataType(typeof(ReferenceMetadata))]
    public class ReferenceModel : EntityModel, IReferenceModel
    {
        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        public string Label { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        public string Code { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the sort order.
        /// </summary>
        public int Order { get; set; }

        public int CompareTo(object obj)
        {
            if (obj is ReferenceModel other)
                return string.Compare(Code, other.Code, StringComparison.Ordinal);
            return -1;
        }
    }
}