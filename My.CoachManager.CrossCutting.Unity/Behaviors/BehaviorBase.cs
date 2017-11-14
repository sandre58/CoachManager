using System;
using System.Collections.Generic;

namespace My.CoachManager.CrossCutting.Unity.Behaviors
{
    /// <summary>
    /// Behavior Base for Unity.
    /// </summary>
    public abstract class BehaviorBase
    {
        #region ----- Properties -----

        /// <summary>
        /// Gets a value indicating whether Execute the action.
        /// </summary>
        public bool WillExecute
        {
            get { return true; }
        }

        #endregion ----- Properties -----

        /// <summary>
        /// Get the needed interfaces.
        /// </summary>
        /// <returns>The interfaces list.</returns>
        public IEnumerable<Type> GetRequiredInterfaces()
        {
            // TODO : Must be Study Correctly
            return Type.EmptyTypes;
        }
    }
}