using FluentValidation.Results;

using My.CoachManager.Domain.Entities;

namespace My.CoachManager.Domain.PersonModule.Services
{
    public interface IPlayerDomainService
    {

        /// <summary>
        /// Check if the category is used by others properties.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool IsUsed(int id);

        /// <summary>
        /// Validates entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        ValidationResult Validate(Player entity);
    }
}
