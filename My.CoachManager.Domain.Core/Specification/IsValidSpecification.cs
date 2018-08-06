// -----------------------------------------------------------------------
// <copyright file="AndSpecification.cs" company="Servicarte">
// © Servicarte - Projet Expense
// </copyright>
// --------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace My.CoachManager.Domain.Core.Specification
{
    /// <summary>
    /// A logic AND Specification.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity that check this specification.</typeparam>
    public abstract class IsValidSpecification<TEntity> : Specification<TEntity>
       where TEntity : class, IEntity, new()
    {
        #region ----- Fields ------

        /// <summary>
        /// The right side specification.
        /// </summary>
        public IList<string> Errors { get; protected set; }

        #endregion ----- Fields ------

        #region Constructors

        protected IsValidSpecification()
        {
            Errors = new List<string>();
        }

        #endregion Constructors

        #region ----- Override Methods -----

        /// <inheritdoc />
        /// <summary>
        /// Satisfied the by.
        /// </summary>
        /// <returns>The expression.</returns>
        public override Expression<Func<TEntity, bool>> SatisfiedBy()
        {
            return entity => ValidateEntity(entity);
        }

        /// <summary>
        /// Validates entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected abstract bool ValidateEntity(TEntity entity);

        #endregion ----- Override Methods -----
    }
}