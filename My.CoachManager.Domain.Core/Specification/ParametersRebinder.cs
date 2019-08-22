// -----------------------------------------------------------------------
// <copyright file="ParametersRebinder.cs" company="Servicarte">
// © Servicarte - Projet Expense
// </copyright>
// --------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq.Expressions;

namespace My.CoachManager.Domain.Core.Specification
{
    /// <summary>
    /// Helper for binder parameters without use Invoke method in expressions
    /// (this methods is not supported in all LINQ query providers,
    /// for example in Linq2Entities is not supported).
    /// </summary>
    public sealed class ParameterRebinder : ExpressionVisitor
    {
        /// <summary>
        /// The _map.
        /// </summary>
        private readonly Dictionary<ParameterExpression, ParameterExpression> _map;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParameterRebinder"/> class.
        /// </summary>
        /// <param name="map">Map specification.</param>
        public ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
        {
            _map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
        }

        /// <summary>
        /// Replace parameters in expression with a Map information.
        /// </summary>
        /// <param name="map">Map information.</param>
        /// <param name="expression">Expression to replace parameters.</param>
        /// <returns>Expression with parameters replaced.</returns>
        public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression expression)
        {
            return new ParameterRebinder(map).Visit(expression);
        }

        /// <summary>
        /// Visit pattern method.
        /// </summary>
        /// <param name="p">A Parameter expression.</param>
        /// <returns>New visited expression.</returns>
        protected override Expression VisitParameter(ParameterExpression p)
        {
            ParameterExpression replacement;

            if (_map.TryGetValue(p, out replacement))
            {
                p = replacement;
            }

            return base.VisitParameter(p);
        }
    }
}
