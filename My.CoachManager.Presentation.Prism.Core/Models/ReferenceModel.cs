using System;
using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Resources;
using My.CoachManager.CrossCutting.Core.Resources.Entities;

namespace My.CoachManager.Presentation.Prism.Core.Models
{
    /// <summary>
    /// Provides properties for an item containing Code, Label, Description and Order.
    /// </summary>
    public class ReferenceModel : EntityModel, IReferenceModel
    {
        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        [Display(Name = "Label", ResourceType = typeof(ReferenceResources))]
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        [MaxLength(100, ErrorMessageResourceName = "MaxLenghtFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        public string Label { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        [Display(Name = "Description", ResourceType = typeof(ReferenceResources))]
        public string Description { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        [Display(Name = "Code", ResourceType = typeof(ReferenceResources))]
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        [MaxLength(15, ErrorMessageResourceName = "MaxLenghtFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        public string Code { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the sort order.
        /// </summary>
        [Display(Name = "Order", ResourceType = typeof(ReferenceResources))]
        public int Order { get; set; }

        /// <summary>
        /// Compares this instance to a specified object and returns an indication of their relative values.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override int CompareTo(object obj)
        {
            if (obj is ReferenceModel other)
            {
                var value = Order.CompareTo(other.Order);

                return value != 0 ? value : string.Compare(Label, other.Label, StringComparison.Ordinal);
            }
            return -1;
        }
    }
}