// -----------------------------------------------------------------------
// <copyright file="DirectSpecification.cs" company="Servicarte">
// © Servicarte - Projet Expense
// </copyright>
// --------------------------------------------------------------------

using System;
using System.Linq.Expressions;

namespace My.CoachManager.Domain.Core.Specification
{
    /// <summary>
    /// A Direct Specification is a simple implementation
    /// of specification that acquire this from a lambda expression
    /// in constructor.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity that check this specification.</typeparam>
    public sealed class DirectSpecification<TEntity> : Specification<TEntity>
        where TEntity : class
    {
        #region ----- Members -----

        /// <summary>
        /// The matching criteria.
        /// </summary>
        private Expression<Func<TEntity, bool>> _matchingCriteria;

        #endregion ----- Members -----

        #region ----- Constructor -----

        /// <summary>
        /// Initializes a new instance of the <see cref="DirectSpecification{TEntity}"/> class.
        /// </summary>
        /// <param name="matchingCriteria">The matching criteria.</param>
        public DirectSpecification(Expression<Func<TEntity, bool>> matchingCriteria)
        {
            _matchingCriteria = matchingCriteria ?? throw new ArgumentNullException(nameof(matchingCriteria));
        }

        #endregion ----- Constructor -----

        #region ----- Override Method -----

        /// <summary>
        /// Satisfied By Pattern.
        /// </summary>
        /// <returns>The expression.</returns>
        public override Expression<Func<TEntity, bool>> SatisfiedBy()
        {
            return _matchingCriteria;
        }

        #endregion ----- Override Method -----
    }
}
