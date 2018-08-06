using System.Collections;

namespace My.CoachManager.Presentation.Prism.Core.Models
{
    public interface IValidatable
    {
        /// <summary>
        /// Check if the entity is valid.
        /// </summary>
        /// <returns></returns>
        bool Validate();

        //
        // Résumé :
        //     Gets the validation errors for a specified property or for the entire entity.
        //
        // Paramètres :
        //   propertyName:
        //     The name of the property to retrieve validation errors for; or null or System.String.Empty,
        //     to retrieve entity-level errors.
        //
        // Retourne :
        //     The validation errors for the property or entity.
        IEnumerable GetErrors();
    }
}