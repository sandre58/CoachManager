using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Practices.Unity.InterceptionExtension;
using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.CrossCutting.Logging.Attributes;

namespace My.CoachManager.CrossCutting.Unity.Behaviors
{
    /// <summary>
    /// Trace Behavior for Unity.
    /// </summary>
    public class DebugBehavior : BehaviorBase, IInterceptionBehavior
    {
        #region ----- IInterceptionBehavior Methods -----

        /// <summary>
        /// Launch Method invocation.
        /// </summary>
        /// <param name="input">Current Interceptor Input.</param>
        /// <param name="getNext">Next Interceptor.</param>
        /// <returns>The output value of invocated methods.</returns>
        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            // Get logger instance
            var logger = UnityFactory.Resolve<ILogger>();

            var methodAttribute = input.MethodBase.GetCustomAttributes(typeof(LoggingContextAttribute), true).FirstOrDefault() as LoggingContextAttribute;
            if (input.MethodBase.DeclaringType != null)
            {
                var classAttribute = input.MethodBase.DeclaringType.GetCustomAttributes(typeof(LoggingContextAttribute), true).FirstOrDefault() as LoggingContextAttribute;

                input.InvocationContext.Add("MethodAttribute", methodAttribute);
                input.InvocationContext.Add("ClassAttribute", classAttribute);

                // Build String to Log
                var parameters = new List<string>();

                foreach (var item in input.Arguments)
                {
                    if (item != null)
                    {
                        parameters.Add(item.ToString());
                    }
                    else
                    {
                        parameters.Add("null");
                    }
                }

                string startMethod = string.Format(Resources.TraceMessages.StartMethod, input.MethodBase.DeclaringType.Name, input.MethodBase.Name);

                // Log in File
                logger.Debug(startMethod, new LoggingContext { Action = methodAttribute != null ? methodAttribute.Action : "NoAction" });

                var watch = new Stopwatch();

                // Start Chronometer
                watch.Start();

                var methodReturn = getNext().Invoke(input, getNext);

                // Stop Chronometer
                watch.Stop();

                // Build End String to log
                var endMethod = string.Format(Resources.TraceMessages.EndMethod, input.MethodBase.DeclaringType.Name, input.MethodBase.Name, watch.ElapsedMilliseconds);

                // Log in File
                logger.Debug(endMethod, new LoggingContext { Action = methodAttribute != null ? methodAttribute.Action : "NoAction" });

                // Return Invocated Method Value
                return methodReturn;
            }

            return null;
        }

        #endregion ----- IInterceptionBehavior Methods -----
    }
}