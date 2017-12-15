using System;
using System.Diagnostics.CodeAnalysis;

namespace My.CoachManager.CrossCutting.Core.Helpers
{
    /// <summary>
    /// The parameter helper.
    /// </summary>
    public static class ParameterHelper
    {
        /// <summary>
        /// Requires that the condition is satisfied.
        /// </summary>
        /// <param name="condition">The condition.</param>
        /// <param name="message">The message to display if condition fails.</param>
        /// <typeparam name="TException">The exception to throw.</typeparam>
        /// <exception cref="Exception">The exception thrown if the condition fails.</exception>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "Must be done like this.")]
        public static void Requires<TException>(bool condition, string message = null) where TException : Exception, new()
        {
            if (condition)
            {
                return;
            }

            Console.Write(message ?? string.Empty);
            var exception = (TException)Activator.CreateInstance(typeof(TException), message ?? string.Empty);
            throw exception;
        }
    }
}