using FluentValidation.Results;

using My.CoachManager.Domain.Entities;
using My.CoachManager.Domain.SquadModule.Aggregate;

namespace My.CoachManager.Domain.SquadModule.Services
{
    public class SquadDomainService : ISquadDomainService
    {

        #region Methods
        /// <summary>
        /// Validates entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ValidationResult Validate(Squad entity)
        {
            var validator = new SquadValidator();
            return validator.Validate(entity);
        }

        #endregion Methods
    }
}
