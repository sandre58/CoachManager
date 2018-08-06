// -----------------------------------------------------------------------
// <copyright file="AndSpecification.cs" company="Servicarte">
// © Servicarte - Projet Expense
// </copyright>
// --------------------------------------------------------------------

using My.CoachManager.CrossCutting.Core.Resources;
using My.CoachManager.Domain.Core;
using My.CoachManager.Domain.Core.Specification;
using My.CoachManager.Domain.Entities;
using My.CoachManager.Domain.ReferenceModule.Aggregates;

namespace My.CoachManager.Domain.SeasonModule.Aggregate
{
    /// <summary>
    /// A logic AND Specification.
    /// </summary>
    public sealed class SeasonIsValidSpecification : IsValidSpecification<Season>
    {
        private readonly IRepository<Season> _repository;

        #region Constructors

        public SeasonIsValidSpecification(IRepository<Season> repository)
        {
            _repository = repository;
        }

        #endregion Constructors

        /// <summary>
        /// Validates entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected override bool ValidateEntity(Season entity)
        {
            Errors.Clear();

            if (_repository.Any(ReferenceSpecification.IsUnique(entity)))
            {
                Errors.Add(string.Format(ValidationMessageResources.AlreadyExistMessage, entity.Code));
            }

            if (entity.StartDate >= entity.EndDate)
            {
                Errors.Add(ValidationMessageResources.StartDateLessThanEndDateMessage);
            }

            return Errors.Count == 0;
        }
    }
}