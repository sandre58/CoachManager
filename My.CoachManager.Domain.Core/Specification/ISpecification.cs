﻿// -----------------------------------------------------------------------
// <copyright file="ISpecification.cs" company="Servicarte">
// © Servicarte - Projet Expense
// </copyright>
// --------------------------------------------------------------------

using System;
using System.Linq.Expressions;

namespace My.CoachManager.Domain.Core.Specification
{
    /// <summary>
    /// Base contract for Specification pattern, for more information
    /// about this pattern see http://martinfowler.com/apsupp/spec.pdf
    /// or http://en.wikipedia.org/wiki/Specification_pattern.
    /// This is really a variant implementation where we have added Linq and
    /// lambda expression into this pattern.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity.</typeparam>
    public interface ISpecification<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Check if this specification is satisfied by a specific expression lambda.
        /// </summary>
        /// <returns>Lambda expression.</returns>
        Expression<Func<TEntity, bool>> SatisfiedBy();

        /// <summary>
        ///
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool SatisfiedBy(TEntity entity);
    }
}