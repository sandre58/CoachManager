using My.CoachManager.Presentation.Prism.Core.Models;

namespace My.CoachManager.Presentation.Prism.Models
{
    /// <summary>
    /// Provides properties for a Contact item.
    /// </summary>
    public class CityModel : ModelBase
    {
        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        public virtual string City { get; set; }

        /// <summary>
        /// Gets or sets the postal Code.
        /// </summary>
        public virtual string PostalCode { get; set; }
    }
}