// -----------------------------------------------------------------------
// <copyright file="OrSpecification.cs" company="Servicarte">
// © Servicarte - Projet Expense
// </copyright>
// --------------------------------------------------------------------

using System;
using System.Linq.Expressions;

namespace My.CoachManager.Domain.Core.Specification
{
    /// <summary>
    /// A Logic OR Specification.
    /// </summary>
    /// <typeparam name="T">Type of entity that check this specification.</typeparam>
    public sealed class OrSpecification<T> : CompositeSpecification<T>
         where T : class
    {
        #region ----- Members -----

        /// <summary>
        /// The right side specification.
        /// </summary>
        private readonly ISpecification<T> _rightSideSpecification;

        /// <summary>
        /// The left side specification.
        /// </summary>
        private readonly ISpecification<T> _leftSideSpecification;

        #endregion ----- Members -----

        #region ----- Constructor -----

        /// <summary>
        /// Initializes a new instance of the <see cref="OrSpecification{T}"/> class.
        /// </summary>
        /// <param name="leftSide">The left side.</param>
        /// <param name="rightSide">The right side.</param>
        public OrSpecification(ISpecification<T> leftSide, ISpecification<T> rightSide)
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

        #region ----- Overrides Methods -----

        /// <summary>
        /// Gets the Left side specification.
        /// </summary>
        public override ISpecification<T> LeftSideSpecification
        {
            get { return _leftSideSpecification; }
        }

        /// <summary>
        /// Gets the Right side specification.
        /// </summary>
        public override ISpecification<T> RightSideSpecification
        {
            get { return _rightSideSpecification; }
        }

        /// <summary>
        /// Satisfied the by.
        /// </summary>
        /// <returns>Lambda expression.</returns>
        public override Expression<Func<T, bool>> SatisfiedBy()
        {
            Expression<Func<T, bool>> left = _leftSideSpecification.SatisfiedBy();
            Expression<Func<T, bool>> right = _rightSideSpecification.SatisfiedBy();

            return left.Or(right);
        }

        #endregion ----- Overrides Methods -----
    }
}