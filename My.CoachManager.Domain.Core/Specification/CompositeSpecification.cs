// -----------------------------------------------------------------------
// <copyright file="CompositeSpecification.cs" company="Servicarte">
// © Servicarte - Projet Expense
// </copyright>
// --------------------------------------------------------------------

namespace My.CoachManager.Domain.Core.Specification
{
    /// <summary>
    /// Base class for composite specifications.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity that check this specification.</typeparam>
    public abstract class CompositeSpecification<TEntity> : Specification<TEntity>
         where TEntity : class
    {
        #region ----- Properties -----

        /// <summary>
        /// Gets the Left side specification for this composite element.
        /// </summary>
        public abstract ISpecification<TEntity> LeftSideSpecification { get; }

        /// <summary>
        /// Gets the Right side specification for this composite element.
        /// </summary>
        public abstract ISpecification<TEntity> RightSideSpecification { get; }

        #endregion
    }
}
