using Microsoft.Practices.Unity.InterceptionExtension;

namespace My.CoachManager.CrossCutting.Logging.Handler
{
    /// <summary>
    /// Unity Call Handler for Logging.
    /// </summary>
    public class LoggingCallHandler : ICallHandler
    {
        #region ----- Properties -----

        /// <summary>
        /// Gets or sets the order.
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Gets or sets the action.
        /// </summary>
        /// <value>The action.</value>
        public string Action { get; set; }

        /// <summary>
        /// Gets or sets the message identity.
        /// </summary>
        /// <value>The message identity.</value>
        public string MessageIdentity { get; set; }

        #endregion ----- Properties -----

        #region ----- ICallHandler Methods -----

        /// <summary>
        /// Invoke Method.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="getNext">The getNext.</param>
        /// <returns>A return method.</returns>
        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            // Declare any variables required for values used in this method here. ...

            // Perform any pre-processing tasks required in the custom handler here. This code
            // executes before control passes to the next handler. ...

            // Use the following line of code in any handler to invoke the next handler that the
            // application block should execute. This code gets the current return message that you
            // must pass back to the caller:
            var msg = getNext()(input, getNext);

            // Perform any post-processing tasks required in the custom handler here. This code
            // executes after the invocation of the target object method or property accessor, and
            // before control passes back to the previous handler as the Invoke call stack unwinds.
            // You can modify the return message if required. ...

            // Return the message to the calling code, which may be the previous handler or, if this
            // is the first handler in the chain, the client.
            return msg;
        }

        #endregion ----- ICallHandler Methods -----
    }
}