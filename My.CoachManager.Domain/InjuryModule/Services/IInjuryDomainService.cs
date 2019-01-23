using FluentValidation.Results;
using My.CoachManager.Domain.Entities;

namespace My.CoachManager.Domain.InjuryModule.Services
{
    public interface IInjuryDomainService
    {

        /// <summary>
        /// Validates entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        ValidationResult Validate(Injury entity);
    }
}