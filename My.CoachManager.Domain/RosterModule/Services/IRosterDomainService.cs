using FluentValidation.Results;

using My.CoachManager.Domain.Entities;

namespace My.CoachManager.Domain.RosterModule.Services
{
    public interface IRosterDomainService
    {
        /// <summary>
        /// Validates entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        ValidationResult Validate(Roster entity);

        /// <summary>
        /// Check if the item is used by others properties.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool IsUsed(int id);
    }
}
