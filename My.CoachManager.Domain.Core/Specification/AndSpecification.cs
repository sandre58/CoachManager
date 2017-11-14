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
            if (leftSide == null)
            {
                throw new ArgumentNullException("leftSide");
            }

            if (rightSide == null)
            {
                throw new ArgumentNullException("rightSide");
            }

            _leftSideSpecification = leftSide;
            _rightSideSpecification = rightSide;
        }

        #endregion ----- Constructor -----

        #region ----- Override Methods -----

        /// <summary>
        /// Gets the Left side specification.
        /// </summary>
        public override ISpecification<TEntity> LeftSideSpecification
        {
            get { return _leftSideSpecification; }
        }

        /// <summary>
        /// Gets the Right side specification.
        /// </summary>
        public override ISpecification<TEntity> RightSideSpecification
        {
            get { return _rightSideSpecification; }
        }

        /// <summary>
        /// Satisfied the by.
        /// </summary>
        /// <returns>The expression.</returns>
        public override Expression<Func<TEntity, bool>> SatisfiedBy()
        {
            Expression<Func<TEntity, bool>> left = _leftSideSpecification.SatisfiedBy();
            Expression<Func<TEntity, bool>> right = _rightSideSpecification.SatisfiedBy();

            return left.And(right);
        }

        #endregion ----- Override Methods -----
    }
}