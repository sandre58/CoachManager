using System;
using System.Windows.Threading;

namespace My.CoachManager.Presentation.Prism.Controls.Helpers
{
    public static class Extensions
    {
        public static T Invoke<T>(this DispatcherObject dispatcherObject, Func<T> func)
        {
            if (dispatcherObject == null)
            {
                throw new ArgumentNullException("dispatcherObject");
            }
            if (func == null)
            {
                throw new ArgumentNullException("func");
            }
            if (dispatcherObject.Dispatcher.CheckAccess())
            {
                return func();
            }
            else
            {
                return dispatcherObject.Dispatcher.Invoke(func);
            }
        }

        public static void Invoke(this DispatcherObject dispatcherObject, Action invokeAction)
        {
            if (dispatcherObject == null)
            {
                throw new ArgumentNullException("dispatcherObject");
            }
            if (invokeAction == null)
            {
                throw new ArgumentNullException("invokeAction");
            }
            if (dispatcherObject.Dispatcher.CheckAccess())
            {
                invokeAction();
            }
            else
            {
                dispatcherObject.Dispatcher.Invoke(invokeAction);
            }
        }

        /// <summary>
        ///   Executes the specified action asynchronously with the DispatcherPriority.Background on the thread that the Dispatcher was created on.
        /// </summary>
        /// <param name="dispatcherObject">The dispatcher object where the action runs.</param>
        /// <param name="invokeAction">An action that takes no parameters.</param>
        /// <param name="priority">The dispatcher priority.</param>
        public static void BeginInvoke(this DispatcherObject dispatcherObject, Action invokeAction, DispatcherPriority priority = DispatcherPriority.Background)
        {
            if (dispatcherObject == null)
            {
                throw new ArgumentNullException("dispatcherObject");
            }
            if (invokeAction == null)
            {
                throw new ArgumentNullException("invokeAction");
            }
            dispatcherObject.Dispatcher.BeginInvoke(priority, invokeAction);
        }
    }
}