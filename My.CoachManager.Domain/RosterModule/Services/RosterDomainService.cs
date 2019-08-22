using FluentValidation.Results;
using My.CoachManager.Domain.Entities;
using My.CoachManager.Domain.RosterModule.Aggregate;

namespace My.CoachManager.Domain.RosterModule.Services
{
    public class RosterDomainService : IRosterDomainService
    {

        #region Methods
        /// <summary>
        /// Validates entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ValidationResult Validate(Roster entity)
        {
            var validator = new RosterValidator();
            return validator.Validate(entity);
        }

        /// <summary>
        /// Check if the category is used by others properties.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool IsUsed(int id)
        {
            return false;
        }

        #endregion Methods
    }
}
