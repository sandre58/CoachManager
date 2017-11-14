// -----------------------------------------------------------------------
// <copyright file="NotSpecification.cs" company="Servicarte">
// © Servicarte - Projet Expense
// </copyright>
// --------------------------------------------------------------------

using System;
using System.Linq;
using System.Linq.Expressions;

namespace My.CoachManager.Domain.Core.Specification
{
    /// <summary>
    /// Not Specification convert a original
    /// specification with NOT logic operator.
    /// </summary>
    /// <typeparam name="TEntity">Type of element for this specification.</typeparam>
    public sealed class NotSpecification<TEntity> : Specification<TEntity>
        where TEntity : class
    {
        #region ---- Fields ----

        /// <summary>
        /// The original criteria.
        /// </summary>
        private readonly Expression<Func<TEntity, bool>> _originalCriteria;

        #endregion ---- Fields ----

        #region ----- Constructor -----

        /// <summary>
        /// Initializes a new instance of the <see cref="NotSpecification{TEntity}"/> class.
        /// </summary>
        /// <param name="originalSpecification">The original specification.</param>
        public NotSpecification(ISpecification<TEntity> originalSpecification)
        {
            if (originalSpecification == null)
            {
                throw new ArgumentNullException("originalSpecification");
            }

            _originalCriteria = originalSpecification.SatisfiedBy();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotSpecification{TEntity}"/> class.
        /// </summary>
        /// <param name="originalSpecification">The original specification.</param>
        public NotSpecification(Expression<Func<TEntity, bool>> originalSpecification)
        {
            if (originalSpecification == null)
            {
                throw new ArgumentNullException("originalSpecification");
            }

            _originalCriteria = originalSpecification;
        }

        #endregion ----- Constructor -----

        #region ---- Override Methods -----

        /// <summary>
        /// Satisfied the by.
        /// </summary>
        /// <returns>Lambda expression.</returns>
        public override Expression<Func<TEntity, bool>> SatisfiedBy()
        {
            return Expression.Lambda<Func<TEntity, bool>>(Expression.Not(_originalCriteria.Body), _originalCriteria.Parameters.Single());
        }

        #endregion ---- Override Methods -----
    }
}