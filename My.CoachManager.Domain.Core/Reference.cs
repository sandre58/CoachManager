using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Metadatas;

namespace My.CoachManager.Domain.Core
{
    /// <summary>
    /// Provides properties for a entity containing Code, Label, Description and Order.
    /// </summary>
    [MetadataType(typeof(ReferenceMetadata))]
    public abstract class Reference : Entity, IReference, IOrderable
    {
        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the sort order.
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Gets or sets the Business identifier.
        /// </summary>
        public override string BusinessKey
        {
            get { return Label; }
        }
    }
}