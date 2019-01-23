using My.CoachManager.Presentation.Prism.Core.Models;
using My.CoachManager.Presentation.Prism.Core.ViewModels.Interfaces;

namespace My.CoachManager.Presentation.Prism.Core.ViewModels
{
    public class ItemParameters : ModelBase, IScreenParameters
    {
        /// <summary>
        /// Gets or sets Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets parameters for new item.
        /// </summary>
        public static ItemParameters New => new ItemParameters(0);

        /// <summary>
        /// Initialise a new instance of <see cref="ItemParameters"/>.
        /// </summary>
        /// <param name="id"></param>
        public ItemParameters(int id)
        {
            Id = id;
        }
    }
}
