using FluentValidation.Results;
using My.CoachManager.Domain.Entities;

namespace My.CoachManager.Domain.SquadModule.Services
{
    public interface ISquadDomainService
    {
        /// <summary>
        /// Validates entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        ValidationResult Validate(Squad entity);
    }
}