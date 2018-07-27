using System.ComponentModel;

namespace My.CoachManager.Presentation.Prism.Core.Models
{
    public interface IValidatable
    {
        /// <summary>
        /// Check if the entity is valid.
        /// </summary>
        /// <returns></returns>
        bool IsValid();
    }
}