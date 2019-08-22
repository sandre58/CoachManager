using System.Diagnostics;

using My.CoachManager.CrossCutting.Logging;

using Unity.Interception.InterceptionBehaviors;
using Unity.Interception.PolicyInjection.Pipeline;

namespace My.CoachManager.Services.Unity.Behaviors
{
    /// <inheritdoc cref="BehaviorBase" />
    /// <summary>
    /// Trace Behavior for Unity.
    /// </summary>
    public class DebugBehavior : BehaviorBase, IInterceptionBehavior
    {
        #region Fields

        private readonly ILogger _logger;

        #endregion
        #region Constructors

        /// <summary>
        /// Initialize a new instance.
        /// </summary>
        /// <param name="logger"></param>
        public DebugBehavior(ILogger logger)
        {
            _logger = logger;
        }

#endregion

        #region ----- IInterceptionBehavior Methods -----

        /// <summary>
        /// Launch Method invocation.
        /// </summary>
        /// <param name="input">Current Interceptor Input.</param>
        /// <param name="getNext">Next Interceptor.</param>
        /// <returns>The output value of invocated methods.</returns>
        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            if (input.MethodBase.DeclaringType == null) return null;

            var startMethod = string.Format(Resources.TraceMessages.StartMethod, input.MethodBase.DeclaringType.Name, input.MethodBase.Name);

            // Log in File
            _logger.Debug(startMethod);

            var watch = new Stopwatch();

            // Start Chronometer
            watch.Start();

            var methodReturn = getNext().Invoke(input, getNext);

            // Stop Chronometer
            watch.Stop();

            // Build End String to log
            var endMethod = string.Format(Resources.TraceMessages.EndMethod, input.MethodBase.DeclaringType.Name, input.MethodBase.Name, watch.ElapsedMilliseconds);

            // Log in File
            _logger.Debug(endMethod);

            // Return Invocated Method Value
            return methodReturn;
        }

        #endregion ----- IInterceptionBehavior Methods -----

    }
}
