using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace My.CoachManager.Presentation.Prism.Core.Rules
{
    /// <inheritdoc />
    /// <summary>
    /// A collection of rules.
    /// </summary>
    public sealed class RuleCollection : Collection<Rule>
    {
        #region Public Methods

        /// <summary>
        /// Adds a new <see cref="Rule"/> to this instance.
        /// </summary>
        /// <param name="propertyName">The name of the property the rules applies to.</param>
        /// <param name="error">The error if the object does not satisfy the rule.</param>
        /// <param name="rule">The rule to execute.</param>
        public void Add(string propertyName, object error, Func<object, bool> rule)
        {
            Add(new DelegateRule(propertyName, error, rule));
        }

        /// <summary>
        /// Applies the <see cref="Rule"/>'s contained in this instance to <paramref name="obj"/>.
        /// </summary>
        /// <param name="obj">The object to apply the rules to.</param>
        /// <param name="propertyName">Name of the property we want to apply rules for. <c>null</c>
        /// to apply all rules.</param>
        /// <returns>A collection of errors.</returns>
        public IEnumerable<object> Apply(object obj, string propertyName)
        {
            return (from rule in this where string.IsNullOrEmpty(propertyName) || rule.PropertyName.Equals(propertyName) where !rule.Apply(obj) select rule.Error).ToList();
        }

        #endregion Public Methods
    }
}