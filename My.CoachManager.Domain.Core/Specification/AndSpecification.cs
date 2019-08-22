// -----------------------------------------------------------------------
// <copyright file="AndSpecification.cs" company="Servicarte">
// © Servicarte - Projet Expense
// </copyright>
// --------------------------------------------------------------------

using System;
using System.Linq.Expressions;

namespace My.CoachManager.Domain.Core.Specification
{
    /// <summary>
    /// A logic AND Specification.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity that check this specification.</typeparam>
    public sealed class AndSpecification<TEntity> : CompositeSpecification<TEntity>
       where TEntity : class
    {
        #region ----- Fields ------

        /// <summary>
        /// The right side specification.
        /// </summary>
        private readonly ISpecification<TEntity> _rightSideSpecification;

        /// <summary>
        /// The left side specification.
        /// </summary>
        private readonly ISpecification<TEntity> _leftSideSpecification;

        #endregion ----- Fields ------

        #region ----- Constructor -----

        /// <summary>
        /// Initializes a new instance of the <see cref="AndSpecification{TEntity}"/> class.
        /// </summary>
        /// <param name="leftSide">Left side specification.</param>
        /// <param name="rightSide">Right side specification.</param>
        public AndSpecification(ISpecification<TEntity> leftSide, ISpecification<TEntity> rightSide)
        {
            _leftSideSpecification = leftSide ?? throw new ArgumentNullException(nameof(leftSide));
            _rightSideSpecification = rightSide ?? throw new ArgumentNullException(nameof(rightSide));
        }

        #endregion ----- Constructor -----

        #region ----- Override Methods -----

        /// <inheritdoc />
        /// <summary>
        /// Gets the Left side specification.
        /// </summary>
        public override ISpecification<TEntity> LeftSideSpecification => _leftSideSpecification;

        /// <inheritdoc />
        /// <summary>
        /// Gets the Right side specification.
        /// </summary>
        public override ISpecification<TEntity> RightSideSpecification => _rightSideSpecification;

        /// <inheritdoc />
        /// <summary>
        /// Satisfied the by.
        /// </summary>
        /// <returns>The expression.</returns>
        public override Expression<Func<TEntity, bool>> SatisfiedBy()
        {
            var left = _leftSideSpecification.SatisfiedBy();
            var right = _rightSideSpecification.SatisfiedBy();

            return left.And(right);
        }

        #endregion ----- Override Methods -----
    }
}
