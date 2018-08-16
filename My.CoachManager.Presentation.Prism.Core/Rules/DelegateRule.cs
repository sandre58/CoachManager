using System;

namespace My.CoachManager.Presentation.Prism.Core.Rules
{
    /// <summary>
    /// Determines whether or not an object satisfies a rule and
    /// provides an error if it does not.
    /// </summary>
    public sealed class DelegateRule : Rule
    {
        private readonly Func<object, bool> _rule;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateRule"/> class.
        /// </summary>
        /// <param name="propertyName">>The name of the property the rules applies to.</param>
        /// <param name="error">The error if the rules fails.</param>
        /// <param name="rule">The rule to execute.</param>
        public DelegateRule(string propertyName, object error, Func<object, bool> rule)
            : base(propertyName, error)
        {
            _rule = rule ?? throw new ArgumentNullException(nameof(rule));
        }

        #endregion Constructors

        #region Rule<T> Members

        /// <inheritdoc />
        /// <summary>
        /// Applies the rule to the specified object.
        /// </summary>
        /// <param name="obj">The object to apply the rule to.</param>
        /// <returns>
        /// <c>true</c> if the object satisfies the rule, otherwise <c>false</c>.
        /// </returns>
        public override bool Apply(object obj)
        {
            return _rule(obj);
        }

        #endregion Rule<T> Members
    }
}