using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Resources.Entities;
using My.CoachManager.Presentation.Prism.Core.Models;

namespace My.CoachManager.Presentation.Prism.Models
{
    /// <summary>
    /// Provides properties for a Category item.
    /// </summary>
    public class CategoryModel : ReferenceModel
    {
        /// <summary>
        /// Gets or sets the Year.
        /// </summary>
        [Display(Name = "Year", ResourceType = typeof(CategoryResources))]
        public virtual int? Year { get; set; }
    }
}