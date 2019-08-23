using FluentValidation;

using My.CoachManager.CrossCutting.Resources;
using My.CoachManager.Domain.Core;

namespace My.CoachManager.Domain.ReferenceModule.Aggregate
{

    /// <summary>
    /// Validates entity.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class ReferenceValidator<TEntity> : AbstractValidator<TEntity>
        where TEntity : class, IReference, new ()
    {
        #region Fields

        private readonly IRepository<TEntity> _repository;

        #endregion

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="ReferenceValidator{TEntity}"/>.
        /// </summary>
        /// <param name="repository"></param>
        public ReferenceValidator(IRepository<TEntity> repository)
        {
            _repository = repository;

            RuleFor(x => x.Code).Must(MustBeUnique)
                .WithMessage(x => string.Format(ValidationMessageResources.AlreadyExistMessage, x.Code));
        }

        #endregion

        #region  Methods

        /// <summary>
        /// Computes if entity is unique.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        private bool MustBeUnique(TEntity entity, string code)
        {
            return !_repository.Any(ReferenceSpecification.IsUnique(entity));
        }

        #endregion


    }
}
