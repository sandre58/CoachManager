using FluentValidation.Results;

using My.CoachManager.Domain.Entities;
using My.CoachManager.Domain.InjuryModule.Aggregates;

namespace My.CoachManager.Domain.InjuryModule.Services
{
    public class InjuryDomainService : IInjuryDomainService
    {

        #region methods

        /// <summary>
        /// Validates entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ValidationResult Validate(Injury entity)
        {
            var validator = new InjuryValidator();
            return validator.Validate(entity);
        }

        #endregion methods
    }
}
